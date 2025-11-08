using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory;
using MySql.Data.MySqlClient;

namespace KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory
{
    public partial class ProjectDirectory : UserControl
    {
        private UserControl currentUserControl;
        private int ypos = 60;
        private string projectCode;

        // Budget Components
        private Label lblBudgetHeader;
        private DataGridView dgvBudget;
        // Quotation Components
        private Label lblQuotationHeader;
        private DataGridView dgvQuotation;
        // Purchasing Components
        private Label lblPurchasingHeader;
        private DataGridView dgvPurchaseRequest;

        public ProjectDirectory(string projectCode)
        {
            this.AutoScroll = true;
            this.projectCode = projectCode;
            InitializeComponent();
            SetupBudget();
            SetupQuotation();
            SetupPurchaseRequest();

            // ✅ Load data from database instead of test data
            LoadBudgetData();
            LoadQuotationData();
            LoadPurchaseRequestData();
        }

        private void LoadUserControlPanel(UserControl userControl)
        {
            var ProjectOverview = this.Parent?.Parent as ProjectOverview;
            ProjectOverview.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            ProjectOverview.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupBudget()
        {
            // Budget Header
            lblBudgetHeader = new Label
            {
                Text = "Budgets",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblBudgetHeader);

            ypos += 40;

            dgvBudget = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 200,
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
            dgvBudget.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvBudget.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvBudget.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvBudget.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBudget.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvBudget.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvBudget.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvBudget.DefaultCellStyle.BackColor = Color.White;
            dgvBudget.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvBudget.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvBudget.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBudget.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvBudget.GridColor = Color.FromArgb(220, 220, 220);
            dgvBudget.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvBudget.Columns.Add("Id", "Budget ID");
            dgvBudget.Columns.Add("QuotationId", "Quotation ID");
            dgvBudget.Columns.Add("BidPrice", "Bid Price");
            dgvBudget.Columns.Add("TotalCost", "Total Cost");
            dgvBudget.Columns.Add("Status", "Status");

            // Register event handlers
            dgvBudget.CellContentClick += dgvBudget_CellContentClick;
            //dgvBudget.CellValueChanged += dgvBudget_CellValueChanged;

            Controls.Add(dgvBudget);

            // Store the starting Y position for transactions (will be updated dynamically)
            ypos += dgvBudget.Height + 40;
        }

        private void dgvBudget_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                // Get the Quotation ID from the clicked row
                int budgetId = Convert.ToInt32(dgvBudget.Rows[e.RowIndex].Cells["Id"].Value);
                LoadUserControlPanel(new BudgetDetails(budgetId, projectCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupQuotation()
        {
            // Quotation Header
            lblQuotationHeader = new Label
            {
                Text = "Quotations",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblQuotationHeader);

            ypos += 40;

            dgvQuotation = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 200,
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
            dgvQuotation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvQuotation.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvQuotation.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvQuotation.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvQuotation.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvQuotation.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvQuotation.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvQuotation.DefaultCellStyle.BackColor = Color.White;
            dgvQuotation.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvQuotation.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvQuotation.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvQuotation.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvQuotation.GridColor = Color.FromArgb(220, 220, 220);
            dgvQuotation.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvQuotation.Columns.Add("Id", "Id");
            dgvQuotation.Columns["Id"].Visible = false;
            dgvQuotation.Columns.Add("Name", "Quotation Name");
            dgvQuotation.Columns.Add("BidPrice", "Bid Price");
            dgvQuotation.Columns.Add("TotalCost", "Total Cost");
            dgvQuotation.Columns.Add("DateCreated", "Date Created");
            dgvQuotation.Columns.Add("Status", "Status");

            // Register event handlers
            dgvQuotation.CellContentClick += dgvQuotation_CellContentClick;
            //dgvQuotation.CellValueChanged += dgvQuotation_CellValueChanged;

            Controls.Add(dgvQuotation);

            // Store the starting Y position for transactions (will be updated dynamically)
            ypos += dgvQuotation.Height + 40;
        }

        private void dgvQuotation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validate row index (ignore header clicks)
            if (e.RowIndex < 0)
                return;

            try
            {
                // ✅ FIX: Get the Value property, then convert to string
                string quotationName = dgvQuotation.Rows[e.RowIndex].Cells["Name"].Value?.ToString() ?? string.Empty;
                int quotationId = Convert.ToInt32(dgvQuotation.Rows[e.RowIndex].Cells["Id"].Value);

                // ✅ Add validation to check if quotation name is empty
                if (string.IsNullOrWhiteSpace(quotationName))
                {
                    MessageBox.Show("Invalid quotation name.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadUserControlPanel(new QuotationDetails(quotationId, quotationName, projectCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupPurchaseRequest()
        {
            // Purchasing Header
            lblPurchasingHeader = new Label
            {
                Text = "Purchasing",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblPurchasingHeader);

            ypos += 40;

            dgvPurchaseRequest = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 200,
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
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvPurchaseRequest.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvPurchaseRequest.DefaultCellStyle.BackColor = Color.White;
            dgvPurchaseRequest.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvPurchaseRequest.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvPurchaseRequest.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPurchaseRequest.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvPurchaseRequest.GridColor = Color.FromArgb(220, 220, 220);
            dgvPurchaseRequest.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvPurchaseRequest.Columns.Add("PrId", "PR ID");
            dgvPurchaseRequest.Columns.Add("VendorName", "Vendor Name");
            dgvPurchaseRequest.Columns.Add("QuotationId", "Quotation ID");
            dgvPurchaseRequest.Columns.Add("Quantity", "Total Quantity");
            dgvPurchaseRequest.Columns.Add("GrandTotal", "Grand Total");

            // Set column widths
            dgvPurchaseRequest.Columns["PrId"].Width = 100;
            dgvPurchaseRequest.Columns["VendorName"].Width = 250;
            dgvPurchaseRequest.Columns["QuotationId"].Width = 150;
            dgvPurchaseRequest.Columns["Quantity"].Width = 150;
            dgvPurchaseRequest.Columns["GrandTotal"].Width = 200;

            // Right align numeric columns
            dgvPurchaseRequest.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPurchaseRequest.Columns["GrandTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Register event handlers
            dgvPurchaseRequest.CellClick += dgvPurchaseRequest_CellContentClick;
            //dgvPurchaseRequest.CellValueChanged += dgvPurchaseRequest_CellValueChanged;

            Controls.Add(dgvPurchaseRequest);

            // Store the starting Y position for transactions (will be updated dynamically)
            ypos += dgvPurchaseRequest.Height + 40;
        }

        private void dgvPurchaseRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validate row index (ignore header clicks)
            if (e.RowIndex < 0)
                return;

            try
            {
                // ✅ FIX: Access the correct column name "VendorName" instead of "Name"
                string vendorName = dgvPurchaseRequest.Rows[e.RowIndex].Cells["VendorName"].Value?.ToString() ?? string.Empty;
                int prId = Convert.ToInt32(dgvPurchaseRequest.Rows[e.RowIndex].Cells["PrId"].Value);

                // ✅ Add validation to check if vendor name is empty
                if (string.IsNullOrWhiteSpace(vendorName))
                {
                    MessageBox.Show("Invalid vendor name.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadUserControlPanel(new PurchaseRequestDetails2(vendorName, projectCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase request details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Database Methods
        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }

        private void LoadBudgetData()
        {
            try
            {
                dgvBudget.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT id, quotation_id, bid_price, total_cost, status 
                                    FROM budget_allocation 
                                    WHERE project_code = @project_code
                                    ORDER BY id DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                int quotationId = reader["quotation_id"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quotation_id"]) : 0;
                                decimal bidPrice = reader["bid_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_price"]) : 0;
                                decimal totalCost = reader["total_cost"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["total_cost"]) : 0;
                                string status = reader["status"]?.ToString() ?? "Unknown";

                                dgvBudget.Rows.Add(
                                    id,
                                    quotationId,
                                    $"₱ {bidPrice:N2}",
                                    $"₱ {totalCost:N2}",
                                    status
                                );

                                // Apply color coding for status
                                int rowIndex = dgvBudget.Rows.Count - 1;
                                ApplyBudgetStatusColor(dgvBudget.Rows[rowIndex], status);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading budget data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuotationData()
        {
            try
            {
                dgvQuotation.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT quotation_id, quotation_name, bid_price, total_cost, quotation_date, status
                            FROM quotation 
                            WHERE project_code = @project_code
                            ORDER BY quotation_id DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int quotationId = reader["quotation_id"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quotation_id"]) : 0;
                                string quotationName = reader["quotation_name"]?.ToString() ?? "Unknown";
                                decimal bidPrice = reader["bid_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_price"]) : 0;
                                decimal totalCost = reader["total_cost"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["total_cost"]) : 0;
                                DateTime dateCreated = reader["quotation_date"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["quotation_date"]) : DateTime.Now;
                                string status = reader["status"]?.ToString() ?? "Unknown";

                                dgvQuotation.Rows.Add(
                                    quotationId,           // ✅ Add the ID first (hidden column)
                                    quotationName,
                                    $"₱ {bidPrice:N2}",
                                    $"₱ {totalCost:N2}",
                                    dateCreated.ToString("yyyy-MM-dd"),
                                    status
                                );
                                int rowIndex = dgvQuotation.Rows.Count - 1;
                                ApplyBudgetStatusColor(dgvQuotation.Rows[rowIndex], status);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseRequestData()
        {
            try
            {
                dgvPurchaseRequest.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            pr.id,
                            v.vendor_name,
                            pr.quotation_id,
                            pr.quantity,
                            pr.grand_total
                        FROM purchase_request pr
                        INNER JOIN vendor_list v ON pr.vendor_id = v.id
                        LEFT JOIN quotation q ON pr.quotation_id = q.quotation_id
                        WHERE q.project_code = @project_code OR pr.quotation_id IS NULL
                        ORDER BY pr.id DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int prId = reader["id"] != DBNull.Value
                                    ? Convert.ToInt32(reader["id"]) : 0;
                                string vendorName = reader["vendor_name"]?.ToString() ?? "Unknown";
                                string quotationId = reader["quotation_id"] != DBNull.Value
                                    ? reader["quotation_id"].ToString() : "N/A";
                                int quantity = reader["quantity"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal grandTotal = reader["grand_total"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["grand_total"]) : 0;

                                dgvPurchaseRequest.Rows.Add(
                                    prId,
                                    vendorName,
                                    quotationId,
                                    quantity,
                                    $"₱ {grandTotal:N2}"
                                );
                            }
                        }
                    }
                }

                dgvPurchaseRequest.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase request data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyBudgetStatusColor(DataGridViewRow row, string status)
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

            row.Cells["Status"].Style.ForeColor = statusColor;
            row.Cells["Status"].Style.Font = new Font(dgvBudget.Font, FontStyle.Bold);
        }

        // ✅ Public method to refresh data (can be called after creating new budget/quotation)
        public void RefreshData()
        {
            LoadBudgetData();
            LoadQuotationData();
            LoadPurchaseRequestData();
        }
    }
}