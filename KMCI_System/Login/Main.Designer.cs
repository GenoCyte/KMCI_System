using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace KMCI_System.Login
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel panelSidebar;    
        private Guna2Panel panelLogo;
        private Label lblAppTitle; 
        private Guna2Button btnDashboard;
        private Guna2Button btnManagement;
        private Guna2Panel panelManagementSubmenu;
        private Guna2Button btnCompany;
        private Guna2Button btnProduct;
        private Guna2Button btnProject;
        private Guna2Button btnUsers;
        private Guna2Button btnWorkflow;
        private Guna2Button btnLogout;
        private Guna2Panel panelMain;

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
            this.panelSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.panelManagementSubmenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnWorkflow = new Guna.UI2.WinForms.Guna2Button();
            this.btnUsers = new Guna.UI2.WinForms.Guna2Button();
            this.btnProject = new Guna.UI2.WinForms.Guna2Button();
            this.btnProduct = new Guna.UI2.WinForms.Guna2Button();
            this.btnCompany = new Guna.UI2.WinForms.Guna2Button();
            this.btnManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.panelLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelSidebar.SuspendLayout();
            this.panelManagementSubmenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.White;
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.panelManagementSubmenu);
            this.panelSidebar.Controls.Add(this.btnManagement);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.FillColor = System.Drawing.Color.White;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(240, 800);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FillColor = System.Drawing.Color.White;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogout.ForeColor = System.Drawing.Color.Black;
            this.btnLogout.Location = new System.Drawing.Point(0, 752);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(240, 48);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelManagementSubmenu
            // 
            this.panelManagementSubmenu.BackColor = System.Drawing.Color.White;
            this.panelManagementSubmenu.Controls.Add(this.btnWorkflow);
            this.panelManagementSubmenu.Controls.Add(this.btnUsers);
            this.panelManagementSubmenu.Controls.Add(this.btnProject);
            this.panelManagementSubmenu.Controls.Add(this.btnProduct);
            this.panelManagementSubmenu.Controls.Add(this.btnCompany);
            this.panelManagementSubmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelManagementSubmenu.FillColor = System.Drawing.Color.White;
            this.panelManagementSubmenu.Location = new System.Drawing.Point(0, 172);
            this.panelManagementSubmenu.Name = "panelManagementSubmenu";
            this.panelManagementSubmenu.Size = new System.Drawing.Size(240, 0);
            this.panelManagementSubmenu.TabIndex = 3;
            this.panelManagementSubmenu.Visible = false;
            // 
            // btnWorkflow
            // 
            this.btnWorkflow.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWorkflow.FillColor = System.Drawing.Color.White;
            this.btnWorkflow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWorkflow.ForeColor = System.Drawing.Color.Black;
            this.btnWorkflow.Location = new System.Drawing.Point(0, 160);
            this.btnWorkflow.Name = "btnWorkflow";
            this.btnWorkflow.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.btnWorkflow.Size = new System.Drawing.Size(240, 40);
            this.btnWorkflow.TabIndex = 8;
            this.btnWorkflow.Text = "    Workflow Templates";
            this.btnWorkflow.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnUsers
            // 
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsers.FillColor = System.Drawing.Color.White;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUsers.ForeColor = System.Drawing.Color.Black;
            this.btnUsers.Location = new System.Drawing.Point(0, 120);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.btnUsers.Size = new System.Drawing.Size(240, 40);
            this.btnUsers.TabIndex = 7;
            this.btnUsers.Text = "    Users";
            this.btnUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnProject
            // 
            this.btnProject.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProject.FillColor = System.Drawing.Color.White;
            this.btnProject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProject.ForeColor = System.Drawing.Color.Black;
            this.btnProject.Location = new System.Drawing.Point(0, 80);
            this.btnProject.Name = "btnProject";
            this.btnProject.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.btnProject.Size = new System.Drawing.Size(240, 40);
            this.btnProject.TabIndex = 6;
            this.btnProject.Text = "    Project";
            this.btnProject.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnProduct
            // 
            this.btnProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduct.FillColor = System.Drawing.Color.White;
            this.btnProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProduct.ForeColor = System.Drawing.Color.Black;
            this.btnProduct.Location = new System.Drawing.Point(0, 40);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.btnProduct.Size = new System.Drawing.Size(240, 40);
            this.btnProduct.TabIndex = 5;
            this.btnProduct.Text = "    Product";
            this.btnProduct.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnCompany
            // 
            this.btnCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCompany.FillColor = System.Drawing.Color.White;
            this.btnCompany.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCompany.ForeColor = System.Drawing.Color.Black;
            this.btnCompany.Location = new System.Drawing.Point(0, 0);
            this.btnCompany.Name = "btnCompany";
            this.btnCompany.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.btnCompany.Size = new System.Drawing.Size(240, 40);
            this.btnCompany.TabIndex = 4;
            this.btnCompany.Text = "    Company";
            this.btnCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCompany.Click += new System.EventHandler(this.btnCompany_Click);
            // 
            // btnManagement
            // 
            this.btnManagement.BorderRadius = 6;
            this.btnManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManagement.FillColor = System.Drawing.Color.White;
            this.btnManagement.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnManagement.ForeColor = System.Drawing.Color.Black;
            this.btnManagement.Location = new System.Drawing.Point(0, 128);
            this.btnManagement.Name = "btnManagement";
            this.btnManagement.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnManagement.Size = new System.Drawing.Size(240, 44);
            this.btnManagement.TabIndex = 2;
            this.btnManagement.Text = "  Management";
            this.btnManagement.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnManagement.Click += new System.EventHandler(this.btnManagement_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BorderRadius = 6;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FillColor = System.Drawing.Color.Black;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 80);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(240, 48);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "  Dashboard";
            this.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.Transparent;
            this.panelLogo.Controls.Add(this.lblAppTitle);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.FillColor = System.Drawing.Color.Transparent;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(240, 80);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLogo_Paint);
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.Black;
            this.lblAppTitle.Location = new System.Drawing.Point(16, 24);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(151, 41);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "KingLand";
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(240, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(880, 800);
            this.panelMain.TabIndex = 1;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1120, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KingLand Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelManagementSubmenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}