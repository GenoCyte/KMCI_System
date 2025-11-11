namespace KMCI_System.LogisticsModule
{
    partial class VendorDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            header = new Label();
            btnBack = new Button();
            line1 = new Label();
            header2 = new Label();
            SuspendLayout();
            // 
            // header
            // 
            header.AutoSize = true;
            header.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header.Location = new Point(15, 15);
            header.Name = "label1";
            header.Size = new Size(118, 15);
            header.TabIndex = 1;
            header.Text = "Projects";
            //
            // line1
            //
            line1.BackColor = SystemColors.ActiveCaptionText;
            line1.Location = new Point(0, 60);
            line1.Name = "line1";
            line1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            line1.Size = new Size(150, 1);
            line1.TabIndex = 2;
            // 
            // header2
            // 
            header2.AutoSize = true;
            header2.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header2.Location = new Point(10, 80);
            header2.Name = "label1";
            header2.Size = new Size(118, 15);
            header2.TabIndex = 1;
            header2.Text = "Company Details";
            //
            // btnBack
            //
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.BackColor = Color.FromArgb(0, 120, 215);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(540, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            btnBack.Anchor = AnchorStyles.Top;
            // 
            // ProjectManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(header);
            Controls.Add(btnBack);
            Controls.Add(line1);
            Controls.Add(header2);
            Name = "CompanyDetails";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label header;
        private Button btnBack;
        private Label line1;
        private Label header2;
    }
}
