using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory
{
    public partial class BudgetDetails : UserControl
    {
        private int budgetId;
        private string projectCode;

        // Budget Info Components
        private Label lblHeader;
        private Label lblProjectCode;
        private TextBox txtProjectCode;
        private Label lblQuotationId;
        private TextBox txtQuotationId;
        private Label lblBidPrice;
        private TextBox txtBidPrice;
        private Label lblTotalCost;
        private TextBox txtTotalCost;
        private Label lblStatus;
        private TextBox txtStatus;

        // Category Table Components
        private Label lblCategoryHeader;
        private DataGridView dgvCategories;
        private Label lblTotalBudget;
        private TextBox txtTotalBudget;

        private Button btnBack;

        public BudgetDetails(int budgetId, string projectCode)
        {
            InitializeComponent();
            this.budgetId = budgetId;
            this.projectCode = projectCode;

            this.AutoScroll = true;
            SetupUI();
            LoadBudgetDetails();
            LoadCategoryDetails();
        }

        private void SetupUI()
        {
            int yPos = 20;
            int leftMargin = 30;

            // Header
            lblHeader = new Label
            {
                Text = "Budget Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            Controls.Add(lblHeader);

            // Back Button
            btnBack = new Button
            {
                Text = "← Back",
                Location = new Point(1000, yPos),
                Size = new Size(75, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;
            Controls.Add(btnBack);

            yPos += 60;

            // Project Code
            lblProjectCode = new Label
            {
                Text = "Project Code:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProjectCode);

            txtProjectCode = new TextBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtProjectCode);

            // Quotation ID (adjacent)
            lblQuotationId = new Label
            {
                Text = "Quotation ID:",
                Location = new Point(leftMargin + 400, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblQuotationId);

            txtQuotationId = new TextBox
            {
                Location = new Point(leftMargin + 520, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtQuotationId);

            yPos += 40;

            // Bid Price
            lblBidPrice = new Label
            {
                Text = "Bid Price:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblBidPrice);

            txtBidPrice = new TextBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtBidPrice);

            // Total Cost (adjacent)
            lblTotalCost = new Label
            {
                Text = "Total Cost:",
                Location = new Point(leftMargin + 400, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblTotalCost);

            txtTotalCost = new TextBox
            {
                Location = new Point(leftMargin + 520, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtTotalCost);

            yPos += 40;

            // Status
            lblStatus = new Label
            {
                Text = "Status:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblStatus);

            txtStatus = new TextBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtStatus);

            yPos += 60;

            // Category Header
            lblCategoryHeader = new Label
            {
                Text = "Budget Categories",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            Controls.Add(lblCategoryHeader);

            yPos += 40;

            // Categories DataGridView
            dgvCategories = new DataGridView
            {
                Location = new Point(leftMargin, yPos),
                Width = 1020,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 40 },
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = true,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Vertical
            };

            // Style headers
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvCategories.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategories.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvCategories.DefaultCellStyle.BackColor = Color.White;
            dgvCategories.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCategories.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCategories.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvCategories.GridColor = Color.FromArgb(220, 220, 220);
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Add columns
            dgvCategories.Columns.Add("CategoryName", "Category Name");
            dgvCategories.Columns.Add("Budget", "Budget");
            dgvCategories.Columns.Add("Expenses", "Expenses");
            dgvCategories.Columns.Add("Remaining", "Remaining");

            Controls.Add(dgvCategories);

            yPos += 320;

            // Total Budget Label
            lblTotalBudget = new Label
            {
                Text = "Total Budget:",
                Location = new Point(leftMargin + 620, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(lblTotalBudget);

            // Total Budget TextBox
            txtTotalBudget = new TextBox
            {
                Location = new Point(leftMargin + 750, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Text = "₱ 0.00",
                TextAlign = HorizontalAlignment.Right
            };
            Controls.Add(txtTotalBudget);

            yPos += txtTotalBudget.Height + 20;

            Label lblSpace = new Label
            {
                Location = new Point(leftMargin + 750, yPos),
                Height = 20
            };
            Controls.Add(lblSpace);
        }

        private void LoadBudgetDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT project_code, quotation_id, bid_price, total_cost, status 
                                    FROM budget_allocation 
                                    WHERE id = @budget_id AND project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@budget_id", budgetId);
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtProjectCode.Text = reader["project_code"]?.ToString() ?? "";
                                txtQuotationId.Text = reader["quotation_id"]?.ToString() ?? "N/A";

                                decimal bidPrice = reader["bid_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_price"]) : 0;
                                decimal totalCost = reader["total_cost"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["total_cost"]) : 0;

                                txtBidPrice.Text = $"₱ {bidPrice:N2}";
                                txtTotalCost.Text = $"₱ {totalCost:N2}";

                                string status = reader["status"]?.ToString() ?? "Unknown";
                                txtStatus.Text = status;

                                // Apply status color
                                ApplyStatusColor(status);
                            }
                            else
                            {
                                MessageBox.Show("Budget not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading budget details: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryDetails()
        {
            try
            {
                dgvCategories.Rows.Clear();
                decimal totalBudget = 0;

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT category_name, category_budget, category_expenses, category_remaining
                                    FROM budget_category 
                                    WHERE project_code = @project_code
                                    ORDER BY category_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["category_name"]?.ToString() ?? "";
                                decimal budget = reader["category_budget"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["category_budget"]) : 0;
                                decimal expenses = reader["category_expenses"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["category_expenses"]) : 0;
                                decimal remaining = reader["category_remaining"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["category_remaining"]) : 0;

                                dgvCategories.Rows.Add(
                                    categoryName,
                                    $"₱ {budget:N2}",
                                    $"₱ {expenses:N2}",
                                    $"₱ {remaining:N2}"
                                );

                                totalBudget += budget;

                                // Apply color to remaining column based on value
                                int rowIndex = dgvCategories.Rows.Count - 1;
                                if (remaining < 0)
                                {
                                    dgvCategories.Rows[rowIndex].Cells["Remaining"].Style.ForeColor = Color.Red;
                                    dgvCategories.Rows[rowIndex].Cells["Remaining"].Style.Font = new Font(dgvCategories.Font, FontStyle.Bold);
                                }
                                else if (remaining < budget * 0.2m) // Less than 20% remaining
                                {
                                    dgvCategories.Rows[rowIndex].Cells["Remaining"].Style.ForeColor = Color.Orange;
                                    dgvCategories.Rows[rowIndex].Cells["Remaining"].Style.Font = new Font(dgvCategories.Font, FontStyle.Bold);
                                }
                            }
                        }
                    }
                }

                txtTotalBudget.Text = $"₱ {totalBudget:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category details: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusColor(string status)
        {
            Color statusColor;

            switch (status.ToLower())
            {
                case "approved":
                    statusColor = Color.Green;
                    break;
                case "for approval":
                case "pending":
                    statusColor = Color.Orange;
                    break;
                case "rejected":
                case "cancelled":
                    statusColor = Color.Red;
                    break;
                default:
                    statusColor = Color.Gray;
                    break;
            }

            txtStatus.ForeColor = statusColor;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to ProjectDirectory
            var projectOverview = this.Parent?.Parent as ProjectOverview;

            if (projectOverview != null)
            {
                Panel parentPanel = projectOverview.Controls.Find("panel1", false).FirstOrDefault() as Panel;

                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();

                    ProjectDirectory projectDirectory = new ProjectDirectory(projectCode);
                    projectDirectory.Dock = DockStyle.Fill;
                    parentPanel.Controls.Add(projectDirectory);
                    projectDirectory.BringToFront();
                }
            }
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}