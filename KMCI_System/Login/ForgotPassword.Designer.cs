namespace KMCI_System.Login
{
    partial class ForgotPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbPhone = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSendOTP = new Guna.UI2.WinForms.Guna2Button();
            this.btnVerifyOTP = new Guna.UI2.WinForms.Guna2Button();
            this.tbOTP = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // tbPhone
            // 
            this.tbPhone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPhone.DefaultText = "";
            this.tbPhone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbPhone.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbPhone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbPhone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbPhone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbPhone.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbPhone.Location = new System.Drawing.Point(60, 28);
            this.tbPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.PlaceholderText = "Phone Number";
            this.tbPhone.SelectedText = "";
            this.tbPhone.Size = new System.Drawing.Size(213, 32);
            this.tbPhone.TabIndex = 0;
            this.tbPhone.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // btnSendOTP
            // 
            this.btnSendOTP.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSendOTP.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSendOTP.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSendOTP.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSendOTP.FillColor = System.Drawing.Color.Black;
            this.btnSendOTP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendOTP.ForeColor = System.Drawing.Color.White;
            this.btnSendOTP.Location = new System.Drawing.Point(108, 76);
            this.btnSendOTP.Name = "btnSendOTP";
            this.btnSendOTP.Size = new System.Drawing.Size(108, 32);
            this.btnSendOTP.TabIndex = 1;
            this.btnSendOTP.Text = "Send";
            this.btnSendOTP.Click += new System.EventHandler(this.btnSendOTP_Click_1);
            // 
            // btnVerifyOTP
            // 
            this.btnVerifyOTP.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVerifyOTP.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVerifyOTP.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVerifyOTP.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVerifyOTP.FillColor = System.Drawing.Color.Black;
            this.btnVerifyOTP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnVerifyOTP.ForeColor = System.Drawing.Color.White;
            this.btnVerifyOTP.Location = new System.Drawing.Point(108, 179);
            this.btnVerifyOTP.Name = "btnVerifyOTP";
            this.btnVerifyOTP.Size = new System.Drawing.Size(108, 32);
            this.btnVerifyOTP.TabIndex = 3;
            this.btnVerifyOTP.Text = "Verify";
            this.btnVerifyOTP.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // tbOTP
            // 
            this.tbOTP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbOTP.DefaultText = "";
            this.tbOTP.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbOTP.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbOTP.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbOTP.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbOTP.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbOTP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbOTP.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbOTP.Location = new System.Drawing.Point(60, 131);
            this.tbOTP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbOTP.Name = "tbOTP";
            this.tbOTP.PlaceholderText = "Enter OTP";
            this.tbOTP.SelectedText = "";
            this.tbOTP.Size = new System.Drawing.Size(213, 32);
            this.tbOTP.TabIndex = 2;
            // 
            // ForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 310);
            this.Controls.Add(this.btnVerifyOTP);
            this.Controls.Add(this.tbOTP);
            this.Controls.Add(this.btnSendOTP);
            this.Controls.Add(this.tbPhone);
            this.Name = "ForgotPassword";
            this.Text = "ForgotPassword";
            this.Load += new System.EventHandler(this.ForgotPassword_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tbPhone;
        private Guna.UI2.WinForms.Guna2Button btnVerifyOTP;
        private Guna.UI2.WinForms.Guna2TextBox tbOTP;
        private Guna.UI2.WinForms.Guna2Button btnSendOTP;
    }
}