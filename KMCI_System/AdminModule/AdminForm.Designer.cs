namespace KMCI_System.AdminModule
{
    partial class AdminForm
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
            lblTitle = new Label();
            lblModuleName = new Label();
            lblGreeting = new Label();
            btnDashboardManagement = new Button();
            btnProjectManagement = new Button();
            btnBudgetAllocationManagement = new Button();
            btnPurchaseRequestManagement = new Button();
            btnUserManagement = new Button();
            panel1 = new Panel();
            btnLogOut = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(8, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(184, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Kingland";
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreeting.Location = new Point(12, 70);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(154, 21);
            lblGreeting.TabIndex = 1;
            lblGreeting.Text = "Good Day, Employee";
            // 
            // lblModuleName
            // 
            lblModuleName.AutoSize = true;
            lblModuleName.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblModuleName.Location = new Point(12, 100);
            lblModuleName.Name = "lblModuleName";
            lblModuleName.Size = new Size(197, 32);
            lblModuleName.AutoSize = false;
            lblModuleName.TabIndex = 2;
            lblModuleName.Text = "Admin Module";
            // 
            // btnDashboardManagement
            // 
            btnDashboardManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnDashboardManagement.FlatStyle = FlatStyle.Flat;
            btnDashboardManagement.FlatAppearance.BorderSize = 1;
            btnDashboardManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDashboardManagement.ForeColor = Color.White;
            btnDashboardManagement.MouseEnter += (s, e) =>
            {
                btnDashboardManagement.BackColor = Color.White;
                btnDashboardManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnDashboardManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnDashboardManagement.MouseLeave += (s, e) =>
            {
                btnDashboardManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnDashboardManagement.FlatAppearance.BorderColor = Color.White;
                btnDashboardManagement.ForeColor = Color.White;
            };
            btnDashboardManagement.Location = new Point(12, 150);
            btnDashboardManagement.Name = "btnDashboardManagement";
            btnDashboardManagement.Size = new Size(170, 40);
            btnDashboardManagement.TabIndex = 3;
            btnDashboardManagement.Text = "Dashboard";
            btnDashboardManagement.UseVisualStyleBackColor = false;
            btnDashboardManagement.Cursor = Cursors.Hand;
            btnDashboardManagement.Click += btnDashboardManagement_Click;
            // 
            // btnProjectManagement
            // 
            btnProjectManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnProjectManagement.FlatStyle = FlatStyle.Flat;
            btnProjectManagement.FlatAppearance.BorderSize = 2;
            btnProjectManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProjectManagement.ForeColor = Color.White;
            btnProjectManagement.MouseEnter += (s, e) =>
            {
                btnProjectManagement.BackColor = Color.White;
                btnProjectManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnProjectManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnProjectManagement.MouseLeave += (s, e) =>
            {
                btnProjectManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnProjectManagement.FlatAppearance.BorderColor = Color.White;
                btnProjectManagement.ForeColor = Color.White;
            };
            btnProjectManagement.Location = new Point(12, 200);
            btnProjectManagement.Name = "btnProjectManagement";
            btnProjectManagement.Size = new Size(170, 40);
            btnProjectManagement.TabIndex = 4;
            btnProjectManagement.Text = "Project Management";
            btnProjectManagement.UseVisualStyleBackColor = false;
            btnProjectManagement.Cursor = Cursors.Hand;
            btnProjectManagement.Click += btnProjectManagement_Click;
            // 
            // btnBudgetAllocationManagement
            // 
            btnBudgetAllocationManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnBudgetAllocationManagement.FlatStyle = FlatStyle.Flat;
            btnBudgetAllocationManagement.FlatAppearance.BorderSize = 2;
            btnBudgetAllocationManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBudgetAllocationManagement.ForeColor = Color.White;
            btnBudgetAllocationManagement.MouseEnter += (s, e) =>
            {
                btnBudgetAllocationManagement.BackColor = Color.White;
                btnBudgetAllocationManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnBudgetAllocationManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnBudgetAllocationManagement.MouseLeave += (s, e) =>
            {
                btnBudgetAllocationManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnBudgetAllocationManagement.FlatAppearance.BorderColor = Color.White;
                btnBudgetAllocationManagement.ForeColor = Color.White;
            };
            btnBudgetAllocationManagement.Location = new Point(12, 250);
            btnBudgetAllocationManagement.Name = "btnBudgetAllocationManagement";
            btnBudgetAllocationManagement.Size = new Size(170, 40);
            btnBudgetAllocationManagement.TabIndex = 5;
            btnBudgetAllocationManagement.Text = "Budget Allocation";
            btnBudgetAllocationManagement.UseVisualStyleBackColor = false;
            btnBudgetAllocationManagement.Cursor = Cursors.Hand;
            btnBudgetAllocationManagement.Click += btnBudgetAllocationManagement_Click;
            // 
            // btnPurchaseRequestManagement
            // 
            btnPurchaseRequestManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnPurchaseRequestManagement.FlatStyle = FlatStyle.Flat;
            btnPurchaseRequestManagement.FlatAppearance.BorderSize = 2;
            btnPurchaseRequestManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPurchaseRequestManagement.ForeColor = Color.White;
            btnPurchaseRequestManagement.MouseEnter += (s, e) =>
            {
                btnPurchaseRequestManagement.BackColor = Color.White;
                btnPurchaseRequestManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnPurchaseRequestManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnPurchaseRequestManagement.MouseLeave += (s, e) =>
            {
                btnPurchaseRequestManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnPurchaseRequestManagement.FlatAppearance.BorderColor = Color.White;
                btnPurchaseRequestManagement.ForeColor = Color.White;
            };
            btnPurchaseRequestManagement.Location = new Point(12, 300);
            btnPurchaseRequestManagement.Name = "btnPurchaseRequestManagement";
            btnPurchaseRequestManagement.Size = new Size(170, 40);
            btnPurchaseRequestManagement.TabIndex = 6;
            btnPurchaseRequestManagement.Text = "Purchase Request";
            btnPurchaseRequestManagement.UseVisualStyleBackColor = false;
            btnPurchaseRequestManagement.Cursor = Cursors.Hand;
            btnPurchaseRequestManagement.Click += btnPurchaseRequestManagement_Click;
            // 
            // btnUserManagement
            // 
            btnUserManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnUserManagement.FlatStyle = FlatStyle.Flat;
            btnUserManagement.FlatAppearance.BorderSize = 2;
            btnUserManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUserManagement.ForeColor = Color.White;
            btnUserManagement.MouseEnter += (s, e) =>
            {
                btnUserManagement.BackColor = Color.White;
                btnUserManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnUserManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnUserManagement.MouseLeave += (s, e) =>
            {
                btnUserManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnUserManagement.FlatAppearance.BorderColor = Color.White;
                btnUserManagement.ForeColor = Color.White;
            };
            btnUserManagement.Location = new Point(12, 350);
            btnUserManagement.Name = "btnUserManagement";
            btnUserManagement.Size = new Size(170, 40);
            btnUserManagement.TabIndex = 7;
            btnUserManagement.Text = "User Management";
            btnUserManagement.UseVisualStyleBackColor = false;
            btnUserManagement.Cursor = Cursors.Hand;
            btnUserManagement.Click += btnUserManagement_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.None;
            panel1.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(160, 160, 160), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1);
                }
            };
            panel1.Location = new Point(200, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(588, 426);
            panel1.TabIndex = 8;
            // 
            // btnLogOut
            // 
            btnLogOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogOut.BackColor = Color.White;
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.FlatAppearance.BorderSize = 1;
            btnLogOut.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btnLogOut.MouseEnter += (s, e) =>
            {
                btnLogOut.BackColor = Color.FromArgb(0, 120, 215);
                btnLogOut.FlatAppearance.BorderColor = Color.White;
                btnLogOut.ForeColor = Color.White;
            };

            btnLogOut.MouseLeave += (s, e) =>
            {
                btnLogOut.BackColor = Color.White;
                btnLogOut.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnLogOut.ForeColor = Color.FromArgb(0, 120, 215);
            };
            btnLogOut.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogOut.ForeColor = Color.FromArgb(0, 120, 215);
            btnLogOut.Location = new Point(12, 398);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(170, 40);
            btnLogOut.TabIndex = 9;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Cursor = Cursors.Hand;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogOut);
            Controls.Add(panel1);
            Controls.Add(btnUserManagement);
            Controls.Add(btnPurchaseRequestManagement);
            Controls.Add(btnBudgetAllocationManagement);
            Controls.Add(btnProjectManagement);
            Controls.Add(btnDashboardManagement);
            Controls.Add(lblModuleName);
            Controls.Add(lblGreeting);
            Controls.Add(lblTitle);
            Name = "AdminForm";
            Text = "KMCI - Admin Module";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblModuleName;
        private Label lblGreeting;
        private Button btnDashboardManagement;
        private Button btnProjectManagement;
        private Button btnBudgetAllocationManagement;
        private Button btnPurchaseRequestManagement;
        private Button btnUserManagement;
        public Panel panel1;
        private Button btnLogOut;
    }
}