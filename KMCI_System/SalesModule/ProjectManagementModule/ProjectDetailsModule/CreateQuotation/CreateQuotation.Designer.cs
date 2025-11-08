using static System.Windows.Forms.LinkLabel;

namespace KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation
{
    partial class CreateQuotation
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
            header.Text = "Create Quotation";
            //
            // line1
            //
            line1.BackColor = Color.Gray;
            line1.Location = new Point(20, 200);
            line1.Name = "line1";
            line1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            line1.Size = new Size(110, 1);
            line1.Margin = new Padding(0, 0, 10, 0);
            line1.TabIndex = 2;
            // 
            // CreateQuotation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(header);
            Controls.Add(line1);
            Name = "CreateQuotation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label header;
        private Label line1;
    }
}
