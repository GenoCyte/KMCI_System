namespace KMCI_System.PurchasingModule.PurchaseRequestModule
{
    partial class CreatePurchaseRequest
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
            line1 = new Label();
            btnBack = new Button();
            SuspendLayout();
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
            // btnBack
            //
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.BackColor = Color.FromArgb(0, 120, 215);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(1050, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // ProjectManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(line1);
            Controls.Add(btnBack);
            Name = "CreatePurchaseRequest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label line1;
        private Button btnBack;
    }
}
