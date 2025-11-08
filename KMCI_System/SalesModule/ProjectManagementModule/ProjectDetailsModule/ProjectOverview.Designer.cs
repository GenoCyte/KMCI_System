namespace KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule
{
    partial class ProjectOverview
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
            panel1 = new Panel();
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
            line1.Size = new Size(1160, 1);
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
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(20, 150);
            panel1.Name = "panel1";
            panel1.Size = new Size(1120, 570);
            panel1.TabIndex = 0;
            panel1.AutoScroll = true;
            // 
            // ProjectDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(header);
            Controls.Add(btnBack);
            Controls.Add(line1);
            Controls.Add(panel1);
            Name = "ProjectDetails";
            Size = new Size(1160, 730);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label header;
        private Button btnBack;
        private Label line1;
        public Panel panel1;
    }
}
