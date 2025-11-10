using System;
using System.Windows.Forms;

namespace KMCI_System.Login
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelShadow;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtext;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.LinkLabel lnkForgotPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;

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
            panelShadow = new Panel();
            panelMain = new Panel();
            lblWelcome = new Label();
            lblSubtext = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            chkRemember = new CheckBox();
            lnkForgotPassword = new LinkLabel();
            btnLogin = new Button();
            btnCancel = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelShadow
            // 
            panelShadow.BackColor = Color.FromArgb(40, 0, 0, 0);
            panelShadow.BorderStyle = BorderStyle.FixedSingle;
            panelShadow.Location = new Point(178, 118);
            panelShadow.Name = "panelShadow";
            panelShadow.Size = new Size(560, 380);
            panelShadow.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(lblWelcome);
            panelMain.Controls.Add(lblSubtext);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(txtPassword);
            panelMain.Controls.Add(lnkForgotPassword);
            panelMain.Controls.Add(btnLogin);
            panelMain.Controls.Add(btnCancel);
            panelMain.Location = new Point(170, 110);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(560, 380);
            panelMain.TabIndex = 1;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.Black;
            lblWelcome.Location = new Point(40, 30);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(147, 41);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Welcome";
            // 
            // lblSubtext
            // 
            lblSubtext.AutoSize = true;
            lblSubtext.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtext.ForeColor = Color.Gray;
            lblSubtext.Location = new Point(40, 80);
            lblSubtext.Name = "lblSubtext";
            lblSubtext.Size = new Size(151, 19);
            lblSubtext.TabIndex = 3;
            lblSubtext.Text = "Sign in to your account";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmail.ForeColor = Color.Black;
            txtEmail.Location = new Point(40, 120);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(480, 27);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(40, 170);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(480, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
           
     
            // 
            // lnkForgotPassword
            // 
            lnkForgotPassword.Location = new Point(0, 0);
            lnkForgotPassword.Name = "lnkForgotPassword";
            lnkForgotPassword.Size = new Size(100, 23);
            lnkForgotPassword.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Black;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 250);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(480, 42);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(530, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(20, 20);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "×";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // Login
            // 
            AcceptButton = btnLogin;
            BackColor = SystemColors.HighlightText;
            CancelButton = btnCancel;
            ClientSize = new Size(900, 600);
            Controls.Add(panelShadow);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign in";
            Load += Login_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);

        }



        #endregion

        protected override void OnShown(System.EventArgs e)
        {
            base.OnShown(e);

            // apply rounded regions after layout is finalized
            ApplyRoundedRegion(panelShadow, 20);
            ApplyRoundedRegion(panelMain, 20);
            ApplyRoundedRegion(btnLogin, 10);

            panelMain.BringToFront();
        }
    }
}