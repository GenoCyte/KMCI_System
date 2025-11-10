using KMCI_System.PurchasingModule.PurchaseOrderModule;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.AdminModule.BudgetAllocationApprovalModule
{
    public partial class BudgetAllocationDetails : UserControl
    {
        private int selectedBudgetId;
        private string selectedProjectCode;

        // Detail view components
        private Panel detailsPanel;
        private Label lblDetailsHeader;
        private Label lblProjectCodeLabel;
        private TextBox txtProjectCode;
        private Label lblRequestedByLabel;
        private TextBox txtRequestedBy;
        
        // Client Information
        private Label lblClientInfoHeader;
        private Label lblCompanyNameLabel;
        private TextBox txtCompanyName;
        private Label lblTinLabel;
        private TextBox txtTin;
        private Label lblContactPersonLabel;
        private TextBox txtContactPerson;
        private Label lblContactEmailLabel;
        private TextBox txtContactEmail;
        private Label lblContactPhoneLabel;
        private TextBox txtContactPhone;
        private Label lblAddressLabel;
        private TextBox txtAddress;

        // Budget Allocation Details
        private Label lblBudgetDetailsHeader;
        private Label lblQuotationIdLabel;
        private TextBox txtQuotationId;
        private Label lblBidPriceLabel;
        private TextBox txtBidPrice;
        private Label lblTotalCostLabel;
        private TextBox txtTotalCost;
        private Label lblStatusLabel;
        private TextBox txtStatus;

        // Budget Categories
        private Label lblCategoriesHeader;
        private DataGridView dgvCategories;
        private Label lblTotalBudgetLabel;
        private TextBox txtTotalBudget;

        // Action buttons
        private Button btnApprove;
        private Button btnReject;
        private Button btnBack;

        public BudgetAllocationDetails(int budgetId, string projectCode)
        {
            InitializeComponent();
            
            // Store the parameters
            selectedBudgetId = budgetId;
            selectedProjectCode = projectCode;
            
            // Setup UI
            SetupDetailsPanel();
            
            // Load the budget details immediately
            LoadBudgetDetails(budgetId, projectCode);
            
            // Show the details panel
            detailsPanel.Visible = true;
        }

        private void SetupDetailsPanel()
        {
            detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.White,
                AutoScroll = true
            };

            int yPos = 20;
            int leftMargin = 30;
            int labelWidth = 150;
            int textBoxWidth = 300;

            // Header
            lblDetailsHeader = new Label
            {
                Text = "Budget Allocation Request Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblDetailsHeader);

            // Back Button
            btnBack = new Button
            {
                Text = "← Back",
                Location = new Point(900, yPos),
                Size = new Size(100, 35),
                BackColor = Color.Black,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;
            detailsPanel.Controls.Add(btnBack);

            yPos += 60;

            // Project Code
            lblProjectCodeLabel = new Label
            {
                Text = "Project Code:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            detailsPanel.Controls.Add(lblProjectCodeLabel);

            txtProjectCode = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtProjectCode);

            // Requested By (adjacent)
            lblRequestedByLabel = new Label
            {
                Text = "Requested By:",
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 50, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            detailsPanel.Controls.Add(lblRequestedByLabel);

            txtRequestedBy = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 210, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtRequestedBy);

            yPos += 50;

            // Client Information Section
            lblClientInfoHeader = new Label
            {
                Text = "CLIENT INFORMATION",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            detailsPanel.Controls.Add(lblClientInfoHeader);

            yPos += 40;

            // Company Name
            lblCompanyNameLabel = new Label
            {
                Text = "Company Name:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblCompanyNameLabel);

            txtCompanyName = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtCompanyName);

            // TIN (adjacent)
            lblTinLabel = new Label
            {
                Text = "TIN:",
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 50, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblTinLabel);

            txtTin = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 210, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtTin);

            yPos += 40;

            // Contact Person
            lblContactPersonLabel = new Label
            {
                Text = "Contact Person:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblContactPersonLabel);

            txtContactPerson = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtContactPerson);

            // Contact Phone (adjacent)
            lblContactPhoneLabel = new Label
            {
                Text = "Contact Number:",
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 50, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblContactPhoneLabel);

            txtContactPhone = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 210, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtContactPhone);

            yPos += 40;

            // Contact Email
            lblContactEmailLabel = new Label
            {
                Text = "Email:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblContactEmailLabel);

            txtContactEmail = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtContactEmail);

            yPos += 40;

            // Address
            lblAddressLabel = new Label
            {
                Text = "Address:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblAddressLabel);

            txtAddress = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(770, 50),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Multiline = true
            };
            detailsPanel.Controls.Add(txtAddress);

            yPos += 70;

            // Budget Details Section
            lblBudgetDetailsHeader = new Label
            {
                Text = "BUDGET ALLOCATION DETAILS",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            detailsPanel.Controls.Add(lblBudgetDetailsHeader);

            yPos += 40;

            // Quotation ID
            lblQuotationIdLabel = new Label
            {
                Text = "Quotation ID:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblQuotationIdLabel);

            txtQuotationId = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtQuotationId);

            // Status (adjacent)
            lblStatusLabel = new Label
            {
                Text = "Status:",
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 50, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblStatusLabel);

            txtStatus = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 210, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            detailsPanel.Controls.Add(txtStatus);

            yPos += 40;

            // Bid Price
            lblBidPriceLabel = new Label
            {
                Text = "Bid Price:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblBidPriceLabel);

            txtBidPrice = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + 10, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtBidPrice);

            // Total Cost (adjacent)
            lblTotalCostLabel = new Label
            {
                Text = "Total Cost:",
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 50, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(lblTotalCostLabel);

            txtTotalCost = new TextBox
            {
                Location = new Point(leftMargin + labelWidth + textBoxWidth + 210, yPos),
                Size = new Size(textBoxWidth, 25),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            detailsPanel.Controls.Add(txtTotalCost);

            yPos += 50;

            // Categories Section
            lblCategoriesHeader = new Label
            {
                Text = "Budget Categories",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblCategoriesHeader);

            yPos += 40;

            // Categories DataGridView
            dgvCategories = new DataGridView
            {
                Location = new Point(leftMargin, yPos),
                Width = 970,
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
                RowTemplate = { Height = 40 },
                EnableHeadersVisualStyles = false
            };

            // Style headers
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvCategories.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCategories.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Style cells
            dgvCategories.DefaultCellStyle.BackColor = Color.White;
            dgvCategories.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCategories.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCategories.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Add columns
            dgvCategories.Columns.Add("CategoryName", "Category Name");
            dgvCategories.Columns.Add("Budget", "Budget Amount");

            detailsPanel.Controls.Add(dgvCategories);

            yPos += 320;

            // Total Budget
            lblTotalBudgetLabel = new Label
            {
                Text = "Total Budget:",
                Location = new Point(leftMargin + 620, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            detailsPanel.Controls.Add(lblTotalBudgetLabel);

            txtTotalBudget = new TextBox
            {
                Location = new Point(leftMargin + 780, yPos),
                Size = new Size(220, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right
            };
            detailsPanel.Controls.Add(txtTotalBudget);

            yPos += 50;

            // Action Buttons
            btnApprove = new Button
            {
                Text = "✓ Approve",
                Location = new Point(leftMargin + 750, yPos),
                Size = new Size(120, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += BtnApprove_Click;
            detailsPanel.Controls.Add(btnApprove);

            btnReject = new Button
            {
                Text = "✗ Reject",
                Location = new Point(leftMargin + 880, yPos),
                Size = new Size(120, 40),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += BtnReject_Click;
            detailsPanel.Controls.Add(btnReject);

            yPos += 70;

            // Set auto scroll size
            detailsPanel.AutoScrollMinSize = new Size(1050, yPos);

            this.Controls.Add(detailsPanel);
        }

        private void LoadBudgetDetails(int budgetId, string projectCode)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                
                // Load budget allocation and client details
                string query = @"
                    SELECT 
                        ba.project_code, ba.quotation_id, ba.bid_price, ba.total_cost, ba.status,
                        pl.company_name, pl.tin,
                        p.proponent_name, p.proponent_email, p.proponent_number,
                        ca.house_num, ca.street, ca.subdivision, ca.barangay, ca.city, ca.province, ca.region,
                        q.requested_by
                    FROM budget_allocation ba
                    LEFT JOIN project_list pl ON ba.project_code = pl.project_code
                    LEFT JOIN proponents p ON pl.proponent_id = p.id
                    LEFT JOIN company_address ca ON pl.address_id = ca.id
                    LEFT JOIN quotation q ON ba.quotation_id = q.quotation_id
                    WHERE ba.id = @budgetId AND ba.project_code = @projectCode";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@budgetId", budgetId);
                    cmd.Parameters.AddWithValue("@projectCode", projectCode);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Project Info
                            txtProjectCode.Text = reader["project_code"]?.ToString() ?? "";
                            txtRequestedBy.Text = reader["requested_by"]?.ToString() ?? "N/A";

                            // Client Info
                            txtCompanyName.Text = reader["company_name"]?.ToString() ?? "";
                            txtTin.Text = reader["tin"]?.ToString() ?? "";
                            txtContactPerson.Text = reader["proponent_name"]?.ToString() ?? "";
                            txtContactEmail.Text = reader["proponent_email"]?.ToString() ?? "";
                            txtContactPhone.Text = reader["proponent_number"]?.ToString() ?? "";

                            // Build address
                            var addressParts = new List<string>();
                            string? houseNum = reader["house_num"]?.ToString();
                            if (!string.IsNullOrEmpty(houseNum))
                                addressParts.Add(houseNum);
                            
                            string? street = reader["street"]?.ToString();
                            if (!string.IsNullOrEmpty(street))
                                addressParts.Add(street);
                            
                            string? subdivision = reader["subdivision"]?.ToString();
                            if (!string.IsNullOrEmpty(subdivision))
                                addressParts.Add(subdivision);
                            
                            string? barangay = reader["barangay"]?.ToString();
                            if (!string.IsNullOrEmpty(barangay))
                                addressParts.Add(barangay);
                            
                            string? city = reader["city"]?.ToString();
                            if (!string.IsNullOrEmpty(city))
                                addressParts.Add(city);
                            
                            string? province = reader["province"]?.ToString();
                            if (!string.IsNullOrEmpty(province))
                                addressParts.Add(province);
                            

                            txtAddress.Text = string.Join(", ", addressParts);

                            // Budget Details
                            txtQuotationId.Text = reader["quotation_id"]?.ToString() ?? "N/A";
                            
                            decimal bidPrice = reader["bid_price"] != DBNull.Value ? Convert.ToDecimal(reader["bid_price"]) : 0;
                            decimal totalCost = reader["total_cost"] != DBNull.Value ? Convert.ToDecimal(reader["total_cost"]) : 0;
                            
                            txtBidPrice.Text = bidPrice == 0 ? "On Approval" : $"₱ {bidPrice:N2}";
                            txtTotalCost.Text = totalCost == 0 ? "On Approval" : $"₱ {totalCost:N2}";

                            string status = reader["status"]?.ToString() ?? "Unknown";
                            txtStatus.Text = status;
                            ApplyStatusColor(status);
                        }
                    }
                }

                // Load budget categories
                LoadCategories(budgetId, projectCode);
            }
        }

        private void LoadCategories(int budgetId, string projectCode)
        {
            dgvCategories.Rows.Clear();
            decimal totalBudget = 0;

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT category_name, category_budget
                    FROM budget_category
                    WHERE project_code = @projectCode AND allocation_id = @allocationId
                    ORDER BY category_name";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@projectCode", projectCode);
                    cmd.Parameters.AddWithValue("@allocationId", budgetId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader["category_name"]?.ToString() ?? "";
                            decimal budget = reader["category_budget"] != DBNull.Value 
                                ? Convert.ToDecimal(reader["category_budget"]) : 0;

                            dgvCategories.Rows.Add(categoryName, $"₱ {budget:N2}");
                            totalBudget += budget;
                        }
                    }
                }
            }

            txtTotalBudget.Text = $"₱ {totalBudget:N2}";
        }

        private void ApplyStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    txtStatus.ForeColor = Color.Green;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    break;
                case "rejected":
                    txtStatus.ForeColor = Color.Red;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    break;
                case "pending":
                default:
                    txtStatus.ForeColor = Color.Orange;
                    btnApprove.Enabled = true;
                    btnReject.Enabled = true;
                    break;
            }
        }

        private void BtnApprove_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to approve this budget allocation?",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                UpdateBudgetStatus("Approved");
            }
        }

        private void BtnReject_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reject this budget allocation?",
                "Confirm Rejection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                UpdateBudgetStatus("Rejected");
            }
        }

        private void UpdateBudgetStatus(string status)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "UPDATE budget_allocation SET status = @status WHERE id = @budgetId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@budgetId", selectedBudgetId);
                    cmd.ExecuteNonQuery();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "UPDATE project_list SET budget_allocation = @budgetAllocation WHERE project_code = @projectCode";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Remove the ₱ symbol and commas from the text before parsing
                    string budgetText = txtTotalBudget.Text.Replace("₱", "").Replace(",", "").Trim();
                    decimal budgetValue = decimal.Parse(budgetText);
                    
                    cmd.Parameters.AddWithValue("@budgetAllocation", budgetValue);
                    cmd.Parameters.AddWithValue("@projectCode", selectedProjectCode);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Budget allocation {status.ToLower()} successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            BtnBack_Click(this, EventArgs.Empty);
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            // Navigate back to BudgetApprovalManagement
            var adminForm = this.FindForm() as AdminForm;
            if (adminForm != null)
            {
                adminForm.panel1.Controls.Clear();
                
                BudgetApprovalManagement budgetApprovalManagement = new BudgetApprovalManagement();
                budgetApprovalManagement.Dock = DockStyle.Fill;
                adminForm.panel1.Controls.Add(budgetApprovalManagement);
                budgetApprovalManagement.BringToFront();
            }
        }
    }
}
