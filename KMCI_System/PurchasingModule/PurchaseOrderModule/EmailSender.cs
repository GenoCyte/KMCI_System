using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MySql.Data.MySqlClient;

namespace KMCI_System.PurchasingModule
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

        // Configuration
        private static EmailProvider CurrentProvider = EmailProvider.Gmail;

        // Gmail Settings
        private const string GMAIL_SMTP = "smtp.gmail.com";
        private const int GMAIL_PORT = 587;

        // Outlook/Hotmail Settings
        private const string OUTLOOK_SMTP = "smtp-mail.outlook.com";
        private const int OUTLOOK_PORT = 587;

        // Your email credentials - UPDATE THESE
        private const string SENDER_EMAIL = "kinglandmarketingcompany.sales@gmail.com";
        private const string SENDER_PASSWORD = "qywl dezp afvp dwga";
        private const string SENDER_NAME = "Kingland Marketing Company Inc.";

        /// <summary>
        /// Sends a purchase order email with PDF attachment to the vendor
        /// </summary>
        public static bool SendPurchaseOrderEmail(int purchaseOrderId, string pdfFilePath)
        {
            try
            {
                // Get vendor email and purchase order details
                var emailData = GetPurchaseOrderEmailData(purchaseOrderId);

                if (string.IsNullOrEmpty(emailData.VendorEmail))
                {
                    MessageBox.Show("Vendor email not found. Please update the vendor's email address.",
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
        public static bool SendPurchaseOrderEmail(int purchaseOrderId, string pdfFilePath, string recipientEmail)
        {
            try
            {
                var emailData = GetPurchaseOrderEmailData(purchaseOrderId);
                emailData.VendorEmail = recipientEmail;

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

        private static bool SendEmail(PurchaseOrderEmailData emailData, string pdfFilePath)
        {
            try
            {
                // Create email message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(SENDER_NAME, SENDER_EMAIL));
                message.To.Add(new MailboxAddress(emailData.VendorContactPerson, emailData.VendorEmail));
                message.Subject = $"Purchase Order {emailData.PONumber} - {emailData.VendorName}";

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

                MessageBox.Show($"Email sent successfully to {emailData.VendorEmail}!",
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

        private static PurchaseOrderEmailData GetPurchaseOrderEmailData(int purchaseOrderId)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            PurchaseOrderEmailData data = new PurchaseOrderEmailData();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        po.id,
                        po.po_name,
                        po.po_date,
                        po.grand_total,
                        po.vendor_name,
                        v.vendor_person,
                        v.vendor_email,
                        v.vendor_phone,
                        v.vendor_address
                    FROM purchase_order po
                    LEFT JOIN vendor_list v ON po.vendor_id = v.id
                    WHERE po.id = @purchaseOrderId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@purchaseOrderId", purchaseOrderId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.PONumber = reader["po_name"]?.ToString() ?? "";
                            data.PODate = reader["po_date"] != DBNull.Value ? 
                                Convert.ToDateTime(reader["po_date"]) : DateTime.Now;
                            data.TotalAmount = Convert.ToDecimal(reader["grand_total"]);
                            data.VendorName = reader["vendor_name"]?.ToString() ?? "";
                            data.VendorContactPerson = reader["vendor_person"]?.ToString() ?? "";
                            data.VendorEmail = reader["vendor_email"]?.ToString() ?? "";
                            data.VendorPhone = reader["vendor_phone"]?.ToString() ?? "";
                            data.VendorAddress = reader["vendor_address"]?.ToString() ?? "";
                        }
                    }
                }
            }

            return data;
        }

        private static string GenerateEmailBody(PurchaseOrderEmailData data)
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
        .po-number {{
            font-size: 24px;
            font-weight: bold;
            color: #009EE3;
            text-align: center;
            margin: 10px 0;
        }}
        .po-date {{
            text-align: center;
            color: #666;
            font-size: 14px;
            margin-bottom: 15px;
        }}
        .vendor-name {{
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
        .instructions-box {{
            background-color: #e8f4f8;
            border: 2px solid #009EE3;
            border-radius: 5px;
            padding: 20px;
            margin: 20px 0;
        }}
        .instructions-box h3 {{
            color: #009EE3;
            margin-top: 0;
            margin-bottom: 15px;
        }}
        .instructions-box ul {{
            margin: 0;
            padding-left: 20px;
        }}
        .instructions-box li {{
            margin: 8px 0;
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
        <p>Purchase Order</p>
    </div>
    
    <div class='content'>
        <div class='greeting'>
            Dear <strong>{data.VendorContactPerson}</strong>,
        </div>
        
        <p>
            We are pleased to issue this Purchase Order for the products/services as agreed upon. 
            Please review the attached document carefully and confirm your acceptance.
        </p>
        
        <div class='highlight-box'>
            <div class='po-number'>{data.PONumber}</div>
            <div class='po-date'>Date: {data.PODate:MMMM dd, yyyy}</div>
            <div class='vendor-name'>{data.VendorName}</div>
            <div class='total-amount'>
                <span class='total-label'>Total Purchase Order Amount</span>
                <span class='total-value'>₱{data.TotalAmount:N2}</span>
            </div>
        </div>
        
        <div class='attachment-notice'>
            <div class='attachment-icon'>📎</div>
            <strong>Complete Purchase Order Details Attached</strong><br/>
            <p style='margin: 10px 0 0 0; color: #666;'>
                Please refer to the attached PDF document for the complete breakdown of items, 
                quantities, pricing, payment terms, delivery schedule, and all other relevant details.
            </p>
        </div>
        
        <div class='instructions-box'>
            <h3>📋 Next Steps:</h3>
            <ul>
                <li>Review the attached Purchase Order document carefully</li>
                <li>Confirm acceptance and estimated delivery date</li>
                <li>Prepare the items as specified in the order</li>
                <li>Provide tracking information once shipped</li>
                <li>Send invoice and necessary documents for payment processing</li>
            </ul>
        </div>
        
        <p>
            Please confirm receipt of this Purchase Order and provide an estimated delivery timeline. 
            If you have any questions or concerns regarding this order, please contact us immediately.
        </p>
        
        <div class='contact-info'>
            <strong>📞 Contact Information</strong>
            <div class='contact-item'>📧 <strong>Email:</strong> inquiry@kingland.ph</div>
            <div class='contact-item'>📧 <strong>Purchasing:</strong> r.abanilla@kingland.ph</div>
            <div class='contact-item'>📱 <strong>Phone:</strong> 0917-198-8306</div>
            <div class='contact-item'>🌐 <strong>Website:</strong> https://shop.kingland.ph</div>
            <div class='contact-item'>
                📍 <strong>Delivery Address:</strong> #930 Mamafid Road, Cabuyao, Laguna
            </div>
            <div class='contact-item'>
                📍 <strong>Office Address:</strong> Phase 4B Block 7 Lot 28 Golden City, Dila,<br/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;City of Santa Rosa, Laguna, Philippines 4026
            </div>
        </div>
        
        <div class='footer'>
            <p>
                Thank you for your partnership and prompt service. We look forward to receiving 
                the items as specified in this Purchase Order.
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

    public class PurchaseOrderEmailData
    {
        public string PONumber { get; set; } = "";
        public DateTime PODate { get; set; }
        public decimal TotalAmount { get; set; }
        public string VendorName { get; set; } = "";
        public string VendorContactPerson { get; set; } = "";
        public string VendorEmail { get; set; } = "";
        public string VendorPhone { get; set; } = "";
        public string VendorAddress { get; set; } = "";
    }
}