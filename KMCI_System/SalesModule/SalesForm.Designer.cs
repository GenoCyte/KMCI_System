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
            lblTitle = new Label();
            lblModuleName = new Label();
         lblGreeting = new Label();
            btnProjectManagement = new Button();
  btnCompanyManagement = new Button();
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
      lblModuleName.Text = "Sales Module";
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
   // btnCompanyManagement
         // 
        btnCompanyManagement.BackColor = Color.FromArgb(0, 120, 215);
      btnCompanyManagement.FlatStyle = FlatStyle.Flat;
    btnCompanyManagement.FlatAppearance.BorderSize = 2;
            btnCompanyManagement.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
  btnCompanyManagement.ForeColor = Color.White;
       btnCompanyManagement.MouseEnter += (s, e) =>
        {
  btnCompanyManagement.BackColor = Color.White;
      btnCompanyManagement.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
          btnCompanyManagement.ForeColor = Color.FromArgb(0, 120, 215);
    };

      btnCompanyManagement.MouseLeave += (s, e) =>
       {
            btnCompanyManagement.BackColor = Color.FromArgb(0, 120, 215);
          btnCompanyManagement.FlatAppearance.BorderColor = Color.White;
  btnCompanyManagement.ForeColor = Color.White;
   };
    btnCompanyManagement.Location = new Point(12, 200);
  btnCompanyManagement.Name = "btnCompanyManagement";
   btnCompanyManagement.Size = new Size(170, 40);
            btnCompanyManagement.TabIndex = 4;
            btnCompanyManagement.Text = "Company Management";
            btnCompanyManagement.UseVisualStyleBackColor = false;
            btnCompanyManagement.Cursor = Cursors.Hand;
      btnCompanyManagement.Click += btnCompanyManagement_Click;
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
         panel1.TabIndex = 7;
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
         btnLogOut.TabIndex = 6;
          btnLogOut.Text = "Log Out";
        btnLogOut.UseVisualStyleBackColor = false;
     btnLogOut.Cursor = Cursors.Hand;
            btnLogOut.Click += btnLogOut_Click;
         // 
  // SalesForm
    // 
  AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogOut);
            Controls.Add(panel1);
            Controls.Add(btnCompanyManagement);
            Controls.Add(btnProjectManagement);
            Controls.Add(lblModuleName);
   Controls.Add(lblGreeting);
     Controls.Add(lblTitle);
            Name = "SalesForm";
    Text = "KMCI - Sales Module";
  WindowState = FormWindowState.Maximized;
    ResumeLayout(false);
 PerformLayout();
        }

      #endregion

        private Label lblTitle;
      private Label lblModuleName;
        private Label lblGreeting;
        private Button btnProjectManagement;
        private Button btnCompanyManagement;
      public Panel panel1;
        private Button btnLogOut;
    }
}
