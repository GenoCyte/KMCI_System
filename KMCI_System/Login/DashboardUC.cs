using System;
using System.Drawing;
using System.Windows.Forms;

namespace KMCI_System.Login
{
    public class DashboardUC : UserControl
    {
        private Panel headerPanel;
        private Label title;
        private Label subtitle;

        public DashboardUC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.title);
            this.headerPanel.Controls.Add(this.subtitle);
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(200, 100);
            this.headerPanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(100, 23);
            this.title.TabIndex = 0;
            // 
            // subtitle
            // 
            this.subtitle.Location = new System.Drawing.Point(0, 0);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(100, 23);
            this.subtitle.TabIndex = 1;
            // 
            // DashboardUC
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.headerPanel);
            this.Name = "DashboardUC";
            this.Size = new System.Drawing.Size(1553, 693);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
