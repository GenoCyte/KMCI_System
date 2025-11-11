using Guna.UI2.WinForms;

namespace KMCI_System.Login
{
    public class ProductUC : UserControl
    {
        private Panel headerPanel;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2DataGridView dgv;
        private Panel topPanel;
        private Guna2Button btnAdd;

        public ProductUC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White
            };

            lblTitle = new Guna2HtmlLabel()
            {
                Text = "Product Management",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(24, 16)
            };

            lblSubtitle = new Guna2HtmlLabel()
            {
                Text = "Manage products and inventory",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(24, 48)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);

            topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.White
            };

            btnAdd = new Guna2Button()
            {
                Text = "Add Product",
                Size = new Size(120, 36),
            };
            btnAdd.FillColor = Color.Black;
            btnAdd.ForeColor = Color.White;
            btnAdd.BorderRadius = 10;
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topPanel.SizeChanged += (s, e) =>
            {
                btnAdd.Location = new Point(Math.Max(24, topPanel.ClientSize.Width - btnAdd.Width - 24), (topPanel.Height - btnAdd.Height) / 2);
            };
            topPanel.Controls.Add(btnAdd);

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "SKU", HeaderText = "SKU" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductName", HeaderText = " Name" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Description", HeaderText = "Description" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Brand", HeaderText = "Bwrand" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Category", HeaderText = " Sub Category" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Uom", HeaderText = "UOM" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Outcoming", HeaderText = "Outcoming" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Incoming", HeaderText = "Incoming" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Outcoming", HeaderText = "Outcoming" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Current", HeaderText = "Current" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Action", HeaderText = "Action" });

            this.Controls.Add(dgv);
            this.Controls.Add(topPanel);
            this.Controls.Add(headerPanel);
        }
    }
}