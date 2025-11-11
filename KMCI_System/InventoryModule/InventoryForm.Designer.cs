namespace KMCI_System.InventoryModule
{
    partial class InventoryForm
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
            btnProjectManagement = new Button();
            btnInventoryManagement = new Button();
            btnProductManagement = new Button();
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
            lblModuleName.Text = "Inventory Module";
            // 
            // btnProjectManagement
            // 
            btnProjectManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnProjectManagement.FlatStyle = FlatStyle.Flat;
            btnProjectManagement.FlatAppearance.BorderSize = 1;
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
            btnProjectManagement.Location = new Point(12, 150);
            btnProjectManagement.Name = "btnProjectManagement";
            btnProjectManagement.Size = new Size(170, 40);
            btnProjectManagement.TabIndex = 3;
            btnProjectManagement.Text = "Project Management";
            btnProjectManagement.UseVisualStyleBackColor = false;
            btnProjectManagement.Cursor = Cursors.Hand;
            btnProjectManagement.Click += btnProjectManagement_Click;
            // 
            // btnInventoryManagement
            // 
            btnInventoryManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnInventoryManagement.FlatStyle = FlatStyle.Flat;
            btnInventoryManagement.FlatAppearance.BorderSize = 2;
            btnInventoryManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnInventoryManagement.ForeColor = Color.White;
            btnInventoryManagement.MouseEnter += (s, e) =>
            {
                btnInventoryManagement.BackColor = Color.White;
                btnInventoryManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnInventoryManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnInventoryManagement.MouseLeave += (s, e) =>
            {
                btnInventoryManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnInventoryManagement.FlatAppearance.BorderColor = Color.White;
                btnInventoryManagement.ForeColor = Color.White;
            };
            btnInventoryManagement.Location = new Point(12, 200);
            btnInventoryManagement.Name = "btnInventoryManagement";
            btnInventoryManagement.Size = new Size(170, 40);
            btnInventoryManagement.TabIndex = 4;
            btnInventoryManagement.Text = "Inventory Management";
            btnInventoryManagement.UseVisualStyleBackColor = false;
            btnInventoryManagement.Cursor = Cursors.Hand;
            btnInventoryManagement.Click += btnInventoryManagement_Click;
            // 
            // btnProductManagement
            // 
            btnProductManagement.BackColor = Color.FromArgb(0, 120, 215);
            btnProductManagement.FlatStyle = FlatStyle.Flat;
            btnProductManagement.FlatAppearance.BorderSize = 2;
            btnProductManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProductManagement.ForeColor = Color.White;
            btnProductManagement.MouseEnter += (s, e) =>
            {
                btnProductManagement.BackColor = Color.White;
                btnProductManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnProductManagement.ForeColor = Color.FromArgb(0, 120, 215);
            };

            btnProductManagement.MouseLeave += (s, e) =>
            {
                btnProductManagement.BackColor = Color.FromArgb(0, 120, 215);
                btnProductManagement.FlatAppearance.BorderColor = Color.White;
                btnProductManagement.ForeColor = Color.White;
            };
            btnProductManagement.Location = new Point(12, 250);
            btnProductManagement.Name = "btnProductManagement";
            btnProductManagement.Size = new Size(170, 40);
            btnProductManagement.TabIndex = 5;
            btnProductManagement.Text = "Product Management";
            btnProductManagement.UseVisualStyleBackColor = false;
            btnProductManagement.Cursor = Cursors.Hand;
            btnProductManagement.Click += btnProductManagement_Click;
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
            panel1.TabIndex = 6;
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
            btnLogOut.TabIndex = 7;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Cursor = Cursors.Hand;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // InventoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogOut);
            Controls.Add(panel1);
            Controls.Add(btnProductManagement);
            Controls.Add(btnInventoryManagement);
            Controls.Add(btnProjectManagement);
            Controls.Add(lblModuleName);
            Controls.Add(lblGreeting);
            Controls.Add(lblTitle);
            Name = "InventoryForm";
            Text = "KMCI - Inventory Module";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblModuleName;
        private Label lblGreeting;
        private Button btnProjectManagement;
        private Button btnInventoryManagement;
        private Button btnProductManagement;
        public Panel panel1;
        private Button btnLogOut;
    }
}