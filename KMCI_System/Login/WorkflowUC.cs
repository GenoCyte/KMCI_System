using Guna.UI2.WinForms;

namespace KMCI_System.Login
{
    public class WorkflowUC : UserControl
    {
        private Panel headerPanel;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2DataGridView dgv;
        private Panel topPanel;
        private Guna2Button btnAddTemplate;

        public WorkflowUC()
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
                Text = "Workflow Templates",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(24, 16)
            };

            lblSubtitle = new Guna2HtmlLabel()
            {
                Text = "Create and manage workflow templates and stages",
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

            btnAddTemplate = new Guna2Button()
            {
                Text = "Add Template",
                Size = new Size(140, 36),
            };
            btnAddTemplate.FillColor = Color.Black;
            btnAddTemplate.ForeColor = Color.White;
            btnAddTemplate.BorderRadius = 10;
            btnAddTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topPanel.SizeChanged += (s, e) =>
            {
                btnAddTemplate.Location = new Point(Math.Max(24, topPanel.ClientSize.Width - btnAddTemplate.Width - 24), (topPanel.Height - btnAddTemplate.Height) / 2);
            };

            topPanel.Controls.Add(btnAddTemplate);

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

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TemplateName", HeaderText = "Template Name" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Description", HeaderText = "Description" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Stages", HeaderText = "Stages" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Project", HeaderText = "Project" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Action", HeaderText = "Action" });

            this.Controls.Add(dgv);
            this.Controls.Add(topPanel);
            this.Controls.Add(headerPanel);
        }
    }
}
