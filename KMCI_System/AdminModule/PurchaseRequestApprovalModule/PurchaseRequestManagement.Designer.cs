namespace KMCI_System.AdminModule.PurchaseRequestApprovalModule
{
    partial class PurchaseRequestManagement
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
            line1 = new Label();
            header2 = new Label();
            subHeader = new Label();
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
            header.Text = "Purchase Request";
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
            header2.Text = "Purchase Request Management";
            // 
            // subHeader
            // 
            subHeader.AutoSize = true;
            subHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            subHeader.ForeColor = SystemColors.GrayText;
            subHeader.Location = new Point(15, 120);
            subHeader.Name = "label1";
            subHeader.Size = new Size(118, 15);
            subHeader.TabIndex = 1;
            subHeader.Text = "Manage Purchase Request";
            // 
            // PrOrderManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(header);
            Controls.Add(line1);
            Controls.Add(header2);
            Controls.Add(subHeader);
            Name = "PRManagement";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Label header;
        private Label line1;
        private Label header2;
        private Label subHeader;
    }
}
