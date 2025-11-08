using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace KMCI_System.Login
{
    public class CompanyUC : UserControl
    {
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel topPanel;
        private Guna2Button btnAdd;
        private Guna2Button btnEdit;
        private Guna2Button btnDelete;
        private DataGridView dgv;

        public CompanyUC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Header panel (title + subtitle)
            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White
            };

            lblTitle = new Label()
            {
                Text = "Company Management",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(24, 16)
            };

            lblSubtitle = new Label()
            {
                Text = "Manage companies, roles and related data",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(24, 48)
            };
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);

            // Top toolbar with action buttons
            topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.White
            };

            btnEdit = new Guna2Button()
            {
                Text = "Edit",
                Size = new Size(90, 36),
                Location = new Point(24, 14),
            };
            btnEdit.FillColor = Color.White;
            btnEdit.ForeColor = Color.Black;
            btnEdit.BorderRadius = 8;
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            btnDelete = new Guna2Button()
            {
                Text = "Delete",
                Size = new Size(90, 36),
                Location = new Point(124, 14),
            };
            btnDelete.FillColor = Color.White;
            btnDelete.ForeColor = Color.Black;
            btnDelete.BorderRadius = 8;
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            btnAdd = new Guna2Button()
            {
                Text = "Add",
                Size = new Size(90, 36),
            };
            btnAdd.FillColor = Color.Black;
            btnAdd.ForeColor = Color.White;
            btnAdd.BorderRadius = 10;
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // reposition add on resize
            topPanel.SizeChanged += (s, e) =>
            {
                btnAdd.Location = new Point(Math.Max(24, topPanel.ClientSize.Width - btnAdd.Width - 24), (topPanel.Height - btnAdd.Height) / 2);
            };

            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(btnAdd);

            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CompanyName", HeaderText = "Company Name" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TIN", HeaderText = "TIN" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Roles", HeaderText = "Roles" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Projects", HeaderText = "Projects" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Addresses", HeaderText = "Addresses" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Proponents", HeaderText = "Proponents" });

            // Add controls: DataGrid, toolbar, header (header added last to appear at top)
            this.Controls.Add(dgv);
            this.Controls.Add(topPanel);
            this.Controls.Add(headerPanel);
        }
    }
}
