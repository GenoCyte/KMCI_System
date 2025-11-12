using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace KMCI_System.SalesModule
{
    public class EmailSender
    {
        // Email Provider Configuration
        public enum EmailProvider
        {
            Gmail,
            Outlook,
            CustomSMTP
        }

        // Configuration - Update these
        private static EmailProvider CurrentProvider = EmailProvider.Gmail;

        // Gmail Settings
        private const string GMAIL_SMTP = "smtp.gmail.com";
        private const int GMAIL_PORT = 587;

        // Outlook/Hotmail Settings (easier setup, no 2FA required initially)
        private const string OUTLOOK_SMTP = "smtp-mail.outlook.com";
        private const int OUTLOOK_PORT = 587;

        // Your email credentials - UPDATE THESE
        private const string SENDER_EMAIL = "kinglandmarketingcompany.sales@gmail.com";
        private const string SENDER_PASSWORD = "qywl dezp afvp dwga";
        private const string SENDER_NAME = "Kingland Marketing Company Inc.";

        /// <summary>
        /// Sends a quotation email with PDF attachment to the client
        /// </summary>
        public static bool SendQuotationEmail(int quotationId, string pdfFilePath)
        {
            try
            {
                // Get client email and quotation details
                var emailData = GetQuotationEmailData(quotationId);

                if (string.IsNullOrEmpty(emailData.ClientEmail))
                {
                    MessageBox.Show("Client email not found. Please update the client's email address.",
                        "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Check if PDF exists
                if (!File.Exists(pdfFilePath))
                {
                    MessageBox.Show("PDF file not found. Please generate the PDF first.",
                        "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return SendEmail(emailData, pdfFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}",
                    "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Sends email to custom recipient
        /// </summary>
        public static bool SendQuotationEmail(int quotationId, string pdfFilePath, string recipientEmail)
        {
            try
            {
                var emailData = GetQuotationEmailData(quotationId);
                emailData.ClientEmail = recipientEmail;

                if (!File.Exists(pdfFilePath))
                {
                    MessageBox.Show("PDF file not found.", "File Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return SendEmail(emailData, pdfFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}",
                    "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static bool SendEmail(QuotationEmailData emailData, string pdfFilePath)
        {
            try
            {
                // Create email message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(SENDER_NAME, SENDER_EMAIL));
                message.To.Add(new MailboxAddress(emailData.ProponentName, emailData.ClientEmail));
                message.Subject = $"Price Quotation {emailData.QuotationNumber} - {emailData.CompanyName}";

                // Create email body
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = GenerateEmailBody(emailData)
                };

                // Attach PDF
                bodyBuilder.Attachments.Add(pdfFilePath);
                message.Body = bodyBuilder.ToMessageBody();

                // Get SMTP settings based on provider
                string smtpServer;
                int smtpPort;

                switch (CurrentProvider)
                {
                    case EmailProvider.Gmail:
                        smtpServer = GMAIL_SMTP;
                        smtpPort = GMAIL_PORT;
                        break;
                    case EmailProvider.Outlook:
                        smtpServer = OUTLOOK_SMTP;
                        smtpPort = OUTLOOK_PORT;
                        break;
                    default:
                        smtpServer = OUTLOOK_SMTP;
                        smtpPort = OUTLOOK_PORT;
                        break;
                }

                // Send email using MailKit
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // Connect to SMTP server
                    client.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);

                    // Authenticate
                    client.Authenticate(SENDER_EMAIL, SENDER_PASSWORD);

                    // Send email
                    client.Send(message);

                    // Disconnect
                    client.Disconnect(true);
                }

                MessageBox.Show($"Email sent successfully to {emailData.ClientEmail}!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}\n\n" +
                              "Please check:\n" +
                              "1. Email and password are correct\n" +
                              "2. Internet connection is active\n" +
                              "3. Email provider allows SMTP access",
                    "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static QuotationEmailData GetQuotationEmailData(int quotationId)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            QuotationEmailData data = new QuotationEmailData();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        q.quotation_id,
                        q.quotation_name,
                        q.quotation_date,
                        q.total_cost,
                        c.company_name,
                        p.proponent_name,
                        p.proponent_email
                    FROM quotation q
                    JOIN company_list c ON q.company_id = c.id
                    LEFT JOIN proponents p ON q.proponent_id = p.id
                    WHERE q.quotation_id = @quotationId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@quotationId", quotationId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.QuotationNumber = "KMCI-" + reader["quotation_id"].ToString().PadLeft(4, '0');
                            data.QuotationName = reader["quotation_name"]?.ToString() ?? "";
                            data.QuotationDate = Convert.ToDateTime(reader["quotation_date"]);
                            data.TotalAmount = Convert.ToDecimal(reader["total_cost"]);
                            data.CompanyName = reader["company_name"]?.ToString() ?? "";
                            data.ProponentName = reader["proponent_name"]?.ToString() ?? "";
                            data.ClientEmail = reader["proponent_email"]?.ToString() ?? "";
                        }
                    }
                }
            }

            return data;
        }

        private static string GenerateEmailBody(QuotationEmailData data)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 0;
        }}
        .header {{
            background-color: #009EE3;
            color: white;
            padding: 30px 20px;
            text-align: center;
            border-radius: 5px 5px 0 0;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .header p {{
            margin: 10px 0 0 0;
            font-size: 16px;
            opacity: 0.95;
        }}
        .content {{
            background-color: #f9f9f9;
            padding: 30px;
            border: 1px solid #ddd;
            border-top: none;
            border-radius: 0 0 5px 5px;
        }}
        .greeting {{
            font-size: 16px;
            margin-bottom: 20px;
            color: #333;
        }}
        .highlight-box {{
            background-color: white;
            padding: 25px;
            border-radius: 5px;
            margin: 25px 0;
            border-left: 4px solid #009EE3;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .quotation-number {{
            font-size: 24px;
            font-weight: bold;
            color: #009EE3;
            text-align: center;
            margin: 10px 0;
        }}
        .quotation-date {{
            text-align: center;
            color: #666;
            font-size: 14px;
            margin-bottom: 15px;
        }}
        .company-name {{
            text-align: center;
            color: #333;
            font-weight: bold;
            margin: 10px 0;
            font-size: 16px;
        }}
        .total-amount {{
            text-align: center;
            font-size: 18px;
            color: #333;
            margin-top: 15px;
            padding-top: 15px;
            border-top: 2px solid #f0f0f0;
        }}
        .total-label {{
            color: #666;
            font-size: 14px;
        }}
        .total-value {{
            font-size: 28px;
            font-weight: bold;
            color: #009EE3;
            display: block;
            margin-top: 5px;
        }}
        .attachment-notice {{
            background-color: #fff9e6;
            border: 2px solid #ffd700;
            border-radius: 5px;
            padding: 20px;
            margin: 20px 0;
            text-align: center;
        }}
        .attachment-notice strong {{
            color: #cc8800;
            font-size: 16px;
        }}
        .attachment-icon {{
            font-size: 40px;
            margin-bottom: 10px;
        }}
        .contact-info {{
            background-color: #f0f8ff;
            padding: 20px;
            border-radius: 5px;
            margin-top: 25px;
            font-size: 14px;
        }}
        .contact-info strong {{
            color: #009EE3;
            display: block;
            margin-bottom: 10px;
            font-size: 16px;
        }}
        .contact-item {{
            margin: 8px 0;
        }}
        .footer {{
            margin-top: 30px;
            padding-top: 20px;
            border-top: 2px solid #009EE3;
            font-size: 14px;
            color: #666;
        }}
        .signature {{
            margin-top: 20px;
        }}
        .disclaimer {{
            font-size: 12px;
            color: #999;
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #ddd;
            font-style: italic;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>KINGLAND MARKETING COMPANY INC.</h1>
        <p>Price Quotation</p>
    </div>
    
    <div class='content'>
        <div class='greeting'>
            Dear <strong>{data.ProponentName}</strong>,
        </div>
        
        <p>
            Thank you for your interest in our products and services. We are pleased to provide you 
            with the following price quotation as requested.
        </p>
        
        <div class='highlight-box'>
            <div class='quotation-number'>{data.QuotationNumber}</div>
            <div class='quotation-date'>Date: {data.QuotationDate:MMMM dd, yyyy}</div>
            <div class='company-name'>{data.CompanyName}</div>
            <div class='total-amount'>
                <span class='total-label'>Total Quotation Amount</span>
                <span class='total-value'>₱{data.TotalAmount:N2}</span>
            </div>
        </div>
        
        <div class='attachment-notice'>
            <div class='attachment-icon'>📎</div>
            <strong>Complete Quotation Details Attached</strong><br/>
            <p style='margin: 10px 0 0 0; color: #666;'>
                Please refer to the attached PDF document for the complete breakdown of items, 
                pricing, payment terms, and all other relevant details.
            </p>
        </div>
        
        <p>
            We have prepared a comprehensive quotation document that includes all the information 
            you need to review our proposal. Should you have any questions or require any 
            clarifications, please feel free to contact us.
        </p>
        
        <div class='contact-info'>
            <strong>📞 Contact Information</strong>
            <div class='contact-item'>📧 <strong>Email:</strong> inquiry@kingland.ph</div>
            <div class='contact-item'>📧 <strong>Sales:</strong> r.abanilla@kingland.ph</div>
            <div class='contact-item'>📱 <strong>Phone:</strong> 0917-198-8306</div>
            <div class='contact-item'>🌐 <strong>Website:</strong> https://shop.kingland.ph</div>
            <div class='contact-item'>
                📍 <strong>Address:</strong> Phase 4B Block 7 Lot 28 Golden City, Dila,<br/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;City of Santa Rosa, Laguna, Philippines 4026
            </div>
        </div>
        
        <div class='footer'>
            <p>
                We value your business and look forward to the opportunity to serve you. 
                Thank you for considering Kingland Marketing Company Inc.
            </p>
            
            <div class='signature'>
                <strong>Best regards,</strong><br/>
                <strong>Richard A. Abanilla</strong><br/>
                President<br/>
                Kingland Marketing Company Inc.
            </div>
        </div>
        
        <div class='disclaimer'>
            This is an automated email. Please do not reply directly to this email address. 
            For all inquiries, please use the contact information provided above.
        </div>
    </div>
</body>
</html>";
        }
    }

    public class QuotationEmailData
    {
        public string QuotationNumber { get; set; } = "";
        public string QuotationName { get; set; } = "";
        public DateTime QuotationDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CompanyName { get; set; } = "";
        public string ProponentName { get; set; } = "";
        public string ClientEmail { get; set; } = "";
    }
}