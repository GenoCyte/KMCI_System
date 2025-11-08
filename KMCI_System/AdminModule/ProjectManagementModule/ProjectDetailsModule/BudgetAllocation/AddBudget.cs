using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation
{
    public partial class AddBudget : Form
    {
        private Label lblProjectCode = null!;
        private TextBox txtProjectCode = null!;
        private Label lblQuotation = null!;
        private ComboBox cboQuotation = null!;
        private Label lblBidPrice = null!;
        private TextBox txtBidPrice = null!;
        private Label lblTotalCost = null!;
        private TextBox txtTotalCost = null!;
        private Label lblCategoryHeader = null!;
        private DataGridView dgvCategories = null!;
        private Button btnAddCategory = null!;
        private Label lblTotalBudget = null!;
        private TextBox txtTotalBudget = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        private string projectCode;

        public AddBudget(string projectCode)
        {
            this.projectCode = projectCode;
            InitializeComponent();
            SetupForm();
            LoadProjectData();
            LoadQuotations();
        }

        private void SetupForm()
        {
            int yPos = 30;
            int leftColumnX = 30;

            // Project Code Label
            lblProjectCode = new Label
            {
                Text = "Project Code:",
                Location = new Point(leftColumnX, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProjectCode);

            // Project Code TextBox (readonly)
            txtProjectCode = new TextBox
            {
                Location = new Point(leftColumnX, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Text = projectCode
            };
            Controls.Add(txtProjectCode);

            // Quotation Label (adjacent to project code)
            lblQuotation = new Label
            {
                Text = "Quotation:",
                Location = new Point(leftColumnX + 230, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblQuotation);

            // Quotation ComboBox
            cboQuotation = new ComboBox
            {
                Location = new Point(leftColumnX + 230, yPos + 25),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboQuotation.SelectedIndexChanged += CboQuotation_SelectedIndexChanged;
            Controls.Add(cboQuotation);

            yPos += 80;

            // Bid Price Label
            lblBidPrice = new Label
            {
                Text = "Bid Price:",
                Location = new Point(leftColumnX, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblBidPrice);

            // Bid Price TextBox
            txtBidPrice = new TextBox
            {
                Location = new Point(leftColumnX, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtBidPrice);

            // Total Cost Label
            lblTotalCost = new Label
            {
                Text = "Total Cost:",
                Location = new Point(leftColumnX + 230, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblTotalCost);

            // Total Cost TextBox
            txtTotalCost = new TextBox
            {
                Location = new Point(leftColumnX + 230, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtTotalCost);

            yPos += 80;

            // Category Header
            lblCategoryHeader = new Label
            {
                Text = "Budget Categories",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftColumnX, yPos),
                AutoSize = true
            };
            Controls.Add(lblCategoryHeader);

            // Add Category Button
            btnAddCategory = new Button
            {
                Text = "Add Category",
                Location = new Point(760, yPos - 5),
                Size = new Size(110, 30),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAddCategory.FlatAppearance.BorderSize = 0;
            btnAddCategory.Click += BtnAddCategory_Click;
            Controls.Add(btnAddCategory);

            yPos += 40;

            // Categories DataGridView
            dgvCategories = new DataGridView
            {
                Location = new Point(leftColumnX, yPos),
                Width = 840,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = true,
                ReadOnly = false,
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
                EnableHeadersVisualStyles = false
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

            // Grid lines - Enable borders for all cells
            dgvCategories.GridColor = Color.FromArgb(220, 220, 220);
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvCategories.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Add columns
            dgvCategories.Columns.Add("Name", "Name");
            dgvCategories.Columns.Add("Budget", "Budget");

            // Add delete button column
            DataGridViewButtonColumn btnDeleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Actions",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvCategories.Columns.Add(btnDeleteColumn);

            // Register event handlers
            dgvCategories.CellContentClick += DgvCategories_CellContentClick;
            dgvCategories.CellValueChanged += DgvCategories_CellValueChanged;
            dgvCategories.RowsAdded += DgvCategories_RowsChanged;
            dgvCategories.RowsRemoved += DgvCategories_RowsChanged;

            Controls.Add(dgvCategories);

            yPos += 320;

            // Total Budget Label
            lblTotalBudget = new Label
            {
                Text = "Total Budget:",
                Location = new Point(leftColumnX + 440, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(lblTotalBudget);

            // Total Budget TextBox
            txtTotalBudget = new TextBox
            {
                Location = new Point(leftColumnX + 570, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Text = "₱ 0.00",
                TextAlign = HorizontalAlignment.Right
            };
            Controls.Add(txtTotalBudget);

            yPos += 50;

            // Save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(680, yPos),
                Size = new Size(90, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(780, yPos),
                Size = new Size(90, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);
        }

        private void BtnAddCategory_Click(object? sender, EventArgs e)
        {
            // Add a new row with default values
            int rowIndex = dgvCategories.Rows.Add("", 0.00);

            // Set focus to the Name cell for editing
            dgvCategories.CurrentCell = dgvCategories.Rows[rowIndex].Cells["Name"];
            dgvCategories.BeginEdit(true);
        }

        private void DgvCategories_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Handle Delete button click
            if (e.ColumnIndex == dgvCategories.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this category?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dgvCategories.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void DgvCategories_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            // Recalculate total when Budget column value changes
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCategories.Columns["Budget"].Index)
            {
                CalculateTotalBudget();
            }
        }

        private void DgvCategories_RowsChanged(object? sender, EventArgs e)
        {
            // Recalculate total when rows are added or removed
            CalculateTotalBudget();
        }

        private void CalculateTotalBudget()
        {
            try
            {
                decimal total = 0;

                foreach (DataGridViewRow row in dgvCategories.Rows)
                {
                    if (row.Cells["Budget"].Value != null)
                    {
                        if (decimal.TryParse(row.Cells["Budget"].Value.ToString(), out decimal budget))
                        {
                            total += budget;
                        }
                    }
                }

                txtTotalBudget.Text = $"₱ {total:N2}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating total budget: {ex.Message}");
            }
        }

        private void LoadProjectData()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT budget_allocation 
                                   FROM project_list 
                                   WHERE project_code = @projectCode";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@projectCode", projectCode);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // You can display project budget if needed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading project data: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuotations()
        {
            try
            {
                cboQuotation.Items.Clear();

                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT quotation_id, quotation_name 
                                   FROM quotation 
                                   WHERE project_code = @projectCode AND status = 'Approved'
                                   ORDER BY quotation_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@projectCode", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cboQuotation.Items.Add(new QuotationItem
                                {
                                    Id = Convert.ToInt32(reader["quotation_id"]),
                                    Code = reader["quotation_name"].ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }

                if (cboQuotation.Items.Count > 0)
                {
                    cboQuotation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotations: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboQuotation_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboQuotation.SelectedItem == null) return;

            QuotationItem selectedQuotation = (QuotationItem)cboQuotation.SelectedItem;
            LoadQuotationDetails(selectedQuotation.Id);
        }

        private void LoadQuotationDetails(int quotationId)
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT bid_price, total_cost 
                                   FROM quotation 
                                   WHERE quotation_id = @quotationId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quotationId", quotationId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal bidPrice = reader["bid_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_price"]) : 0;
                                decimal totalCost = reader["total_cost"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["total_cost"]) : 0;

                                txtBidPrice.Text = $"₱ {bidPrice:N2}";
                                txtTotalCost.Text = $"₱ {totalCost:N2}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation details: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            // Validate quotation selection
            if (cboQuotation.SelectedItem == null)
            {
                MessageBox.Show("Please select a quotation.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboQuotation.Focus();
                return;
            }

            // Validate categories
            if (dgvCategories.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one budget category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate category data
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (row.Cells["Name"].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells["Name"].Value.ToString()))
                {
                    MessageBox.Show("Please enter a name for all categories.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (row.Cells["Budget"].Value == null ||
                    !decimal.TryParse(row.Cells["Budget"].Value.ToString(), out decimal budget) ||
                    budget < 0)
                {
                    MessageBox.Show("Please enter valid budget amounts for all categories.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Save to database
            try
            {
                SaveCategory();
                SaveBudget();
                MessageBox.Show("Budget added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving budget: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveBudget() 
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        QuotationItem? selectedQuotation = cboQuotation.SelectedItem as QuotationItem;
                        
                        if (selectedQuotation == null)
                        {
                            throw new InvalidOperationException("No quotation selected.");
                        }
                        
                        // Parse the currency values to decimal
                        decimal bidPrice = decimal.Parse(txtBidPrice.Text.Replace("₱", "").Replace(",", "").Trim());
                        decimal totalCost = decimal.Parse(txtTotalCost.Text.Replace("₱", "").Replace(",", "").Trim());
                        decimal totalBudget = decimal.Parse(txtTotalBudget.Text.Replace("₱", "").Replace(",", "").Trim());
                        
                        string query = @"
                            INSERT INTO budget_allocation
                            (project_code, quotation_id, bid_price, total_cost, status) 
                            VALUES (@projectCode, @quotation_id, @bid_price, @total_cost, @status)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@projectCode", projectCode);
                            cmd.Parameters.AddWithValue("@quotation_id", selectedQuotation.Id);
                            cmd.Parameters.AddWithValue("@bid_price", bidPrice);
                            cmd.Parameters.AddWithValue("@total_cost", totalCost);
                            cmd.Parameters.AddWithValue("@status", "For Approval");
                            cmd.ExecuteNonQuery();
                        }

                        // Update project_list table with the total budget
                        string updateQuery = @"
                            UPDATE project_list
                            SET budget_allocation = @total_budget
                            WHERE project_code = @projectCode";

                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@projectCode", projectCode);
                            cmd.Parameters.AddWithValue("@total_budget", totalBudget);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void SaveCategory()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {   
                        // Save each category
                        foreach (DataGridViewRow row in dgvCategories.Rows)
                        {
                            string categoryName = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                            decimal budget = Convert.ToDecimal(row.Cells["Budget"].Value);

                            string query = @"INSERT INTO budget_category 
                                           (project_code, category_name, category_budget, 
                                            category_expenses, category_remaining) 
                                           VALUES (@projectCode, @categoryName, @budget, 0, @budget)";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@projectCode", projectCode);
                                cmd.Parameters.AddWithValue("@categoryName", categoryName);
                                cmd.Parameters.AddWithValue("@budget", budget);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Helper class for ComboBox items
        private class QuotationItem
        {
            public int Id { get; set; }
            public string Code { get; set; } = string.Empty;

            public override string ToString()
            {
                return Code;
            }
        }
    }
}