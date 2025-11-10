
namespace KMCI_System
{
    partial class SalesForm : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnProjectManagement = new Label();
            Line1 = new Label();
            btnCompanyManagement = new Label();
            btnSupplierManagement = new Label();
            btnProductManagement = new Label();
            panel1 = new Panel();
            btnLogOut = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(184, 45);
            label1.TabIndex = 0;
            label1.Text = "Kingland";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 105);
            label2.Name = "label2";
            label2.Size = new Size(158, 32);
            label2.TabIndex = 1;
            label2.Text = "Sales Module";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(154, 21);
            label3.TabIndex = 2;
            label3.Text = "Good Day, Employee";
            // 
            // btnProjectManagement
            // 
            btnProjectManagement.AutoSize = true;
            btnProjectManagement.Cursor = Cursors.Hand;
            btnProjectManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProjectManagement.Location = new Point(25, 150);
            btnProjectManagement.Name = "btnProjectManagement";
            btnProjectManagement.Size = new Size(129, 17);
            btnProjectManagement.TabIndex = 3;
            btnProjectManagement.Text = "Project Management";
            btnProjectManagement.Click += btnProjectManagement_Click;
            // 
            // Line1
            // 
            Line1.BackColor = SystemColors.ActiveCaptionText;
            Line1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Line1.Location = new Point(19, 150);
            Line1.Name = "Line1";
            Line1.Size = new Size(1, 110);
            Line1.TabIndex = 4;
            // 
            // btnCompanyManagement
            // 
            btnCompanyManagement.AutoSize = true;
            btnCompanyManagement.Cursor = Cursors.Hand;
            btnCompanyManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCompanyManagement.Location = new Point(25, 180);
            btnCompanyManagement.Name = "btnCompanyManagement";
            btnCompanyManagement.Size = new Size(144, 17);
            btnCompanyManagement.TabIndex = 5;
            btnCompanyManagement.Text = "Company Management";
            btnCompanyManagement.Click += btnCompanyManagement_Click;
            // 
            // btnSupplierManagement
            // 
            btnSupplierManagement.AutoSize = true;
            btnSupplierManagement.Cursor = Cursors.Hand;
            btnSupplierManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSupplierManagement.Location = new Point(25, 210);
            btnSupplierManagement.Name = "btnSupplierManagement";
            btnSupplierManagement.Size = new Size(137, 17);
            btnSupplierManagement.TabIndex = 6;
            btnSupplierManagement.Text = "Supplier Management";
            btnSupplierManagement.Click += btnSupplierManagement_Click;
            // 
            // btnProductManagement
            // 
            btnProductManagement.AutoSize = true;
            btnProductManagement.Cursor = Cursors.Hand;
            btnProductManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnProductManagement.Location = new Point(25, 240);
            btnProductManagement.Name = "btnProductManagement";
            btnProductManagement.Size = new Size(134, 17);
            btnProductManagement.TabIndex = 7;
            btnProductManagement.Text = "Product Management";
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
            panel1.Location = new Point(200, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(580, 261);
            panel1.TabIndex = 0;
            //
            // btnLogOut
            //
            btnLogOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogOut.Location = new Point(40, 200);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(120, 40);
            btnLogOut.TabIndex = 8;
            btnLogOut.Text = "Log Out";
            btnLogOut.BackColor = SystemColors.Control;
            btnLogOut.FlatAppearance.BorderSize = 2;
            btnLogOut.FlatAppearance.BorderColor = Color.Gray;
            btnLogOut.Click += btnLogOut_Click;


            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(800, 300);
            Controls.Add(panel1);
            Controls.Add(btnProductManagement);
            Controls.Add(btnSupplierManagement);
            Controls.Add(btnCompanyManagement);
            Controls.Add(Line1);
            Controls.Add(btnProjectManagement);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogOut);
            Name = "MainForm";
            Text = "KMCI";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();    
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label btnProjectManagement;
        private Label Line1;
        private Label btnCompanyManagement;
        private Label btnSupplierManagement;
        private Label btnProductManagement;
        public Panel panel1;
        private Button btnLogOut;
    }
}
