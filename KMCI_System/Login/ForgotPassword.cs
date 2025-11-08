using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.Login
{
    public partial class ForgotPassword : Form
    {
        private string generatedOTP;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (tbOTP.Text == generatedOTP)
                MessageBox.Show("OTP Verified! You may now reset your password.");
            else
                MessageBox.Show("Invalid OTP. Please try again.");
        }

        private string SendSMS(string number, string message)
        {
            // --- iTexMo API Details ---
            string apiCode = "PR-SAMPL123456_ABCDE";
            string apiPassword = "";

            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("https://www.itexmo.com/php_api/api.php", new NameValueCollection()
                    {
                        { "1", number },
                        { "2", message },
                        { "3", apiCode },
                        { "passwd", apiPassword }
                    });

                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return "-1";
            }
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {

        }
  

     

     

        private void btnSendOTP_Click_1(object sender, EventArgs e)
        {
             string phone = tbPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter your phone number.");
                return;
            }

            // Generate 6-digit OTP
            Random rand = new Random();
            generatedOTP = rand.Next(100000, 999999).ToString();

            // Send SMS
            string result = SendSMS(phone, $"Your OTP code is: {generatedOTP}");

            if (result == "0")
                MessageBox.Show("✅ OTP sent successfully!");
            else
                MessageBox.Show("❌ Failed to send OTP. Error code: " + result);
        }
    }
}
