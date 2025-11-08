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
    public partial class BudgetAllocation : UserControl
    {
        private int ypos;
        private String projectCode;
        private Button btnCreateBudget;

        //Budget Allocation Details
        private Label lblProjectBudget;
        private TextBox txtProjectBudget;
        private Label lblBudgetedExpenses;
        private TextBox txtBudgetedExpenses;
        private Label lblActualExpenses;
        private TextBox txtActualExpenses;
        private Label lblRemainingBudget;
        private TextBox txtRemainingBudget;

        // Category Table Components
        private Label lblCategoryHeader;
        private Button btnAddCategory;
        private DataGridView dgvCategories;

        // Transaction Table Components
        private Label lblTransactionHeader;
        private Button btnAddTransaction;
        private DataGridView dgvTransactions;

        // Store initial Y position for transactions section
        private int transactionSectionStartY;

        public BudgetAllocation(String projectCode)
        {
            if (string.IsNullOrWhiteSpace(projectCode))
                throw new ArgumentException("Project code cannot be null or empty", nameof(projectCode));
                
            this.projectCode = projectCode;
            
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1100, 1000); // Increased for transactions section

            InitializeComponent();
            SetupForm();
            SetupCategory();
            SetupTransaction();
            LoadProjectBudget(); // ✅ Add this line
            LoadCategoriesFromDatabase();
            LoadTransactionFromDatabase(); // ✅ Add this
            LoadProjectBudget(); // Load the project budget from the database
        }

        private void SetupForm()
        {
            btnCreateBudget = new Button
            {
                Text = "Create Budget",
                Location = new Point(1000, 15),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnCreateBudget.Click += BtnCreateBudget_Click;
            Controls.Add(btnCreateBudget);

            ypos = 80;

            lblProjectBudget = new Label
            {
                Text = "Project Budget:",
                Location = new Point(20, ypos),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true
            };
            Controls.Add(lblProjectBudget);

            txtProjectBudget = new TextBox
            {
                Text = "$0.00",
                Location = new Point(150, ypos),
                Width = 350
            };
            Controls.Add(txtProjectBudget);

            lblBudgetedExpenses = new Label
            {
                Text = "Budgeted Expenses:",
                Location = new Point(550, ypos),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true
            };
            Controls.Add(lblBudgetedExpenses);

            txtBudgetedExpenses = new TextBox
            {
                Text = "$0.00",
                Location = new Point(700, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtBudgetedExpenses);

            ypos += 40;

            lblActualExpenses = new Label
            {
                Text = "Actual Expenses:",
                Location = new Point(20, ypos),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true
            };
            Controls.Add(lblActualExpenses);

            txtActualExpenses = new TextBox
            {
                Text = "$0.00",
                Location = new Point(150, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtActualExpenses);

            lblRemainingBudget = new Label
            {
                Text = "Remaining Budget:",
                Location = new Point(550, ypos),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                AutoSize = true
            };
            Controls.Add(lblRemainingBudget);

            txtRemainingBudget = new TextBox
            {
                Text = "$0.00",
                Location = new Point(700, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtRemainingBudget);

            ypos += 60;
        }

        private void BtnCreateBudget_Click(object sender, EventArgs e) 
        {
            using (AddBudget addBudgetForm = new AddBudget(projectCode))
            {
                if (addBudgetForm.ShowDialog() == DialogResult.OK)
                {
                    // Reload categories after successful save
                    LoadCategoriesFromDatabase();
                    LoadTransactionFromDatabase();
                    CalculateTotals();
                }
            }
        }

        private void SetupCategory()
        {
            // Category Header
            lblCategoryHeader = new Label
            {
                Text = "Budget Categories",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblCategoryHeader);

            ypos += 40;

            dgvCategories = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
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
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvCategories.Columns.Add("CategoryName", "Name");
            dgvCategories.Columns.Add("Budget", "Budget");
            dgvCategories.Columns.Add("Expenses", "Expenses");
            dgvCategories.Columns.Add("Remaining", "Remaining");

            // Set read-only for specific columns
            dgvCategories.Columns["Expenses"].ReadOnly = true;
            dgvCategories.Columns["Remaining"].ReadOnly = true;

            // Register event handlers
            dgvCategories.RowsAdded += (s, e) => {
                AdjustCategoryGridHeight();
                RefreshTransactionCategoryComboBox(); // Refresh combo when category added
            };
            dgvCategories.RowsRemoved += (s, e) => {
                AdjustCategoryGridHeight();
                RefreshTransactionCategoryComboBox(); // Refresh combo when category removed
            };
            dgvCategories.CellContentClick += DgvCategories_CellContentClick;
            dgvCategories.CellValueChanged += DgvCategories_CellValueChanged;

            Controls.Add(dgvCategories);

            AdjustCategoryGridHeight();
            
            // Store the starting Y position for transactions (will be updated dynamically)
            ypos += dgvCategories.Height + 40;
            transactionSectionStartY = ypos;
        }

        private void SetupTransaction()
        {
            // Transaction Header
            lblTransactionHeader = new Label
            {
                Text = "Transactions",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, transactionSectionStartY),
                AutoSize = true
            };
            Controls.Add(lblTransactionHeader);

            btnAddTransaction = new Button
            {
                Text = "Add Transaction",
                Location = new Point(920, transactionSectionStartY - 5),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 30)
                // ✅ Remove Anchor property entirely, or set to:
                // Anchor = AnchorStyles.Top
            };
            btnAddTransaction.Click += BtnAddTransaction_Click;
            Controls.Add(btnAddTransaction);

            dgvTransactions = new DataGridView
            {
                Location = new Point(20, transactionSectionStartY + 40),
                Width = 1050,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
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
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Vertical
            };

            // Style headers
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvTransactions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvTransactions.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvTransactions.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvTransactions.DefaultCellStyle.BackColor = Color.White;
            dgvTransactions.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvTransactions.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvTransactions.GridColor = Color.FromArgb(220, 220, 220);
            dgvTransactions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvTransactions.Columns.Add("Description", "Description");
            
            // Create ComboBox column for Category
            DataGridViewComboBoxColumn categoryColumn = new DataGridViewComboBoxColumn
            {
                Name = "Category",
                HeaderText = "Category",
                DataPropertyName = "Category",
                FlatStyle = FlatStyle.Flat,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing // Shows as text until editing
            };
            dgvTransactions.Columns.Add(categoryColumn);
            
            dgvTransactions.Columns.Add("Amount", "Amount");
            
            // Add Date column with date picker
            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Date",
                ValueType = typeof(DateTime),
                DefaultCellStyle = { Format = "yyyy-MM-dd" }
            };
            dgvTransactions.Columns.Add(dateColumn);

            // Add ID column to dgvTransactions (hidden)
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "TransactionID", 
                Visible = false 
            });

            // Add delete button column
            DataGridViewButtonColumn btnDeleteTransaction = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Actions",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvTransactions.Columns.Add(btnDeleteTransaction);

            // Register event handlers
            dgvTransactions.RowsAdded += (s, e) => AdjustTransactionGridHeight();
            dgvTransactions.RowsRemoved += (s, e) => AdjustTransactionGridHeight();
            dgvTransactions.CellContentClick += DgvTransactions_CellContentClick;
            dgvTransactions.CellValueChanged += DgvTransactions_CellValueChanged;
            dgvTransactions.EditingControlShowing += DgvTransactions_EditingControlShowing;

            // Add validation in CellValidating event
            dgvTransactions.CellValidating += (s, e) => 
            {
                if (e.ColumnIndex == dgvTransactions.Columns["Date"].Index)
                {
                    if (!DateTime.TryParse(e.FormattedValue?.ToString(), out _))
                    {
                        MessageBox.Show("Please enter a valid date (yyyy-MM-dd)");
                        e.Cancel = true;
                    }
                }

                // Add validation
                dgvTransactions.CellValidating += (s, e) => 
                {
                    if (e.ColumnIndex == dgvTransactions.Columns["Amount"].Index)
                    {
                        if (!decimal.TryParse(e.FormattedValue?.ToString(), out decimal amount) || amount < 0)
                        {
                            MessageBox.Show("Please enter a valid positive amount");
                            e.Cancel = true;
                        }
                    }
                };
            };

            Controls.Add(dgvTransactions);

            AdjustTransactionGridHeight();
        }

        // Refresh the category dropdown list in transactions grid
        private void RefreshTransactionCategoryComboBox()
        {
            if (dgvTransactions == null || dgvTransactions.Columns["Category"] == null)
                return;

            DataGridViewComboBoxColumn categoryColumn = (DataGridViewComboBoxColumn)dgvTransactions.Columns["Category"];
            
            // Clear existing items
            categoryColumn.Items.Clear();
            
            // Add all categories from dgvCategories
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (!row.IsNewRow && row.Cells["CategoryName"].Value != null)
                {
                    string categoryName = row.Cells["CategoryName"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(categoryName) && !categoryColumn.Items.Contains(categoryName))
                    {
                        categoryColumn.Items.Add(categoryName);
                    }
                }
            }
        }

        private void DgvTransactions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Handle ComboBox selection change for Category column
            if (dgvTransactions.CurrentCell.ColumnIndex == dgvTransactions.Columns["Category"].Index)
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    // Remove previous event handler to avoid duplicates
                    combo.SelectedIndexChanged -= CategoryComboBox_SelectedIndexChanged;
                    // Add event handler
                    combo.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;
                }
            }
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle category selection if needed
            ComboBox combo = sender as ComboBox;
            if (combo != null && dgvTransactions.CurrentCell != null)
            {
                dgvTransactions.CurrentCell.Value = combo.SelectedItem?.ToString();
            }
        }

        private void AdjustCategoryGridHeight()
        {
            if (dgvCategories.Rows.Count == 0)
            {
                dgvCategories.Height = dgvCategories.ColumnHeadersHeight + 2;
            }
            else
            {
                int totalHeight = dgvCategories.ColumnHeadersHeight;
                int maxVisibleRows = 4;
                int rowsToCount = Math.Min(dgvCategories.Rows.Count, maxVisibleRows);

                for (int i = 0; i < rowsToCount; i++)
                {
                    totalHeight += dgvCategories.Rows[i].Height;
                }

                totalHeight += 2;
                dgvCategories.Height = totalHeight;
            }

            // ✅ Update transaction section position based on category grid height
            UpdateTransactionSectionPosition();
        }

        private void UpdateTransactionSectionPosition()
        {
            if (dgvTransactions == null || lblTransactionHeader == null || btnAddTransaction == null)
                return;

            // Calculate new Y position based on category grid's bottom + spacing
            int newYPos = dgvCategories.Location.Y + dgvCategories.Height + 40;

            // Update transaction header position
            lblTransactionHeader.Location = new Point(20, newYPos);
            
            // Update add transaction button position
            btnAddTransaction.Location = new Point(920, newYPos - 5);
            
            // Update transaction grid position
            dgvTransactions.Location = new Point(20, newYPos + 40);
            
            // Update the stored transaction section start Y
            transactionSectionStartY = newYPos;

            // Update the AutoScrollMinSize to accommodate the new layout
            UpdateAutoScrollSize();
        }

        private void UpdateAutoScrollSize()
        {
            // Calculate total required height
            int totalHeight = dgvTransactions.Location.Y + dgvTransactions.Height + 50; // 50px bottom padding
            
            // Update AutoScrollMinSize
            this.AutoScrollMinSize = new Size(1100, Math.Max(1000, totalHeight));
        }

        private void AdjustTransactionGridHeight()
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                dgvTransactions.Height = dgvTransactions.ColumnHeadersHeight + 2;
            }
            else
            {
                int totalHeight = dgvTransactions.ColumnHeadersHeight;
                int maxVisibleRows = 4;
                int rowsToCount = Math.Min(dgvTransactions.Rows.Count, maxVisibleRows);

                for (int i = 0; i < rowsToCount; i++)
                {
                    totalHeight += dgvTransactions.Rows[i].Height;
                }

                totalHeight += 2;
                dgvTransactions.Height = totalHeight;
            }

            // Update scroll size when transaction grid height changes
            UpdateAutoScrollSize();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            // Temporarily disable the event
            dgvCategories.CellValueChanged -= DgvCategories_CellValueChanged;
            
            int rowIndex = dgvCategories.Rows.Add("New Category", 0.00, 0.00, 0.00);
            dgvCategories.Rows[rowIndex].Cells["CategoryName"].Tag = "New Category";
            
            SaveCategoryToDatabase(rowIndex);
            AdjustCategoryGridHeight();
            
            // Re-enable the event
            dgvCategories.CellValueChanged += DgvCategories_CellValueChanged;
            
            dgvCategories.CurrentCell = dgvCategories.Rows[rowIndex].Cells["CategoryName"];
            dgvCategories.BeginEdit(true);
        }

        private void BtnAddTransaction_Click(object sender, EventArgs e)
        {
            // Check if there are categories available
            if (dgvCategories.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one category before adding transactions.", 
                    "No Categories", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Temporarily disable the event
            dgvTransactions.CellValueChanged -= DgvTransactions_CellValueChanged;

            // Add new transaction row with proper defaults
            int rowIndex = dgvTransactions.Rows.Add(
                "New Transaction",                       // Description
                dgvCategories.Rows.Count > 0 
                    ? dgvCategories.Rows[0].Cells["CategoryName"].Value?.ToString() 
                    : "",                                // Category - use first category as default
                0.00m,                                   // Amount
                DateTime.Now,                            // Date (DateTime object, not string)
                null                                     // TransactionID (will be set after save)
            );

            // Store original description for tracking
            dgvTransactions.Rows[rowIndex].Cells["Description"].Tag = "New Transaction";
            
            // ✅ Save to database and retrieve the new ID
            int newTransactionId = SaveTransactionToDatabase(rowIndex);
            
            // Store the transaction ID in the hidden column
            dgvTransactions.Rows[rowIndex].Cells["TransactionID"].Value = newTransactionId;
            
            AdjustTransactionGridHeight();

            // Re-enable the event
            dgvTransactions.CellValueChanged += DgvTransactions_CellValueChanged;

            // Start editing the description cell
            dgvTransactions.CurrentCell = dgvTransactions.Rows[rowIndex].Cells["Description"];
            dgvTransactions.BeginEdit(true);
        }

        private void DgvCategories_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustCategoryGridHeight();
        }

        private void DgvTransactions_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustTransactionGridHeight();
        }

        private void DgvCategories_RowsChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AdjustCategoryGridHeight();
        }

        private void DgvTransactions_RowsChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AdjustTransactionGridHeight();
        }

        private void DgvCategories_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

            // Recalculate remaining when budget changes
            if (e.ColumnIndex == dgvCategories.Columns["Budget"].Index)
            {
                CalculateRemaining(row);
            }

            // If category name changed, refresh the transaction combobox
            if (e.ColumnIndex == dgvCategories.Columns["CategoryName"].Index)
            {
                RefreshTransactionCategoryComboBox();
            }

            // Recalculate totals
            CalculateTotals();

            // Update database
            UpdateCategoryInDatabase(row);
        }

        private void DgvTransactions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvTransactions.Rows[e.RowIndex];

            // If amount or category changed, update the category expenses
            if (e.ColumnIndex == dgvTransactions.Columns["Amount"].Index || 
                e.ColumnIndex == dgvTransactions.Columns["Category"].Index)
            {
                UpdateCategoryExpenses();
            }

            // Recalculate totals
            CalculateTotals();

            // TODO: Update transaction in database
            UpdateTransactionInDatabase(row);
        }

        private void UpdateCategoryExpenses()
        {
            // Reset all category expenses to 0
            foreach (DataGridViewRow categoryRow in dgvCategories.Rows)
            {
                if (!categoryRow.IsNewRow)
                {
                    categoryRow.Cells["Expenses"].Value = 0.00m;
                }
            }

            // Calculate expenses for each category from transactions
            foreach (DataGridViewRow transactionRow in dgvTransactions.Rows)
            {
                if (!transactionRow.IsNewRow)
                {
                    string category = transactionRow.Cells["Category"].Value?.ToString();
                    decimal amount = 0;
                    
                    if (transactionRow.Cells["Amount"].Value != null)
                        decimal.TryParse(transactionRow.Cells["Amount"].Value.ToString(), out amount);

                    if (!string.IsNullOrWhiteSpace(category) && amount > 0)
                    {
                        // Find matching category and add to expenses
                        foreach (DataGridViewRow categoryRow in dgvCategories.Rows)
                        {
                            if (!categoryRow.IsNewRow && 
                                categoryRow.Cells["CategoryName"].Value?.ToString() == category)
                            {
                                decimal currentExpenses = 0;
                                if (categoryRow.Cells["Expenses"].Value != null)
                                    decimal.TryParse(categoryRow.Cells["Expenses"].Value.ToString(), out currentExpenses);
                                
                                categoryRow.Cells["Expenses"].Value = currentExpenses + amount;
                                CalculateRemaining(categoryRow);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void CalculateRemaining(DataGridViewRow row)
        {
            try
            {
                decimal budget = 0;
                decimal expenses = 0;

                if (row.Cells["Budget"].Value != null)
                    decimal.TryParse(row.Cells["Budget"].Value.ToString(), out budget);

                if (row.Cells["Expenses"].Value != null)
                    decimal.TryParse(row.Cells["Expenses"].Value.ToString(), out expenses);

                decimal remaining = budget - expenses;
                row.Cells["Remaining"].Value = remaining;

                // Color code the remaining cell
                if (remaining < 0)
                {
                    row.Cells["Remaining"].Style.ForeColor = Color.Red;
                    row.Cells["Remaining"].Style.Font = new Font(dgvCategories.Font, FontStyle.Bold);
                }
                else if (remaining < budget * 0.2m) // Less than 20% remaining
                {
                    row.Cells["Remaining"].Style.ForeColor = Color.Orange;
                    row.Cells["Remaining"].Style.Font = new Font(dgvCategories.Font, FontStyle.Bold);
                }
                else
                {
                    row.Cells["Remaining"].Style.ForeColor = Color.Green;
                    row.Cells["Remaining"].Style.Font = new Font(dgvCategories.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating remaining: {ex.Message}");
            }
        }

        private void CalculateTotals()
        {
            try
            {
                decimal totalBudget = 0;
                decimal totalExpenses = 0;

                foreach (DataGridViewRow row in dgvCategories.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["Budget"].Value != null)
                        {
                            decimal budget;
                            if (decimal.TryParse(row.Cells["Budget"].Value.ToString(), out budget))
                                totalBudget += budget;
                        }

                        if (row.Cells["Expenses"].Value != null)
                        {
                            decimal expenses;
                            if (decimal.TryParse(row.Cells["Expenses"].Value.ToString(), out expenses))
                                totalExpenses += expenses;
                        }
                    }
                }

                decimal totalRemaining = totalBudget - totalExpenses;

                // Update summary fields
                txtBudgetedExpenses.Text = $"₱ {totalBudget:N2}";
                txtActualExpenses.Text = $"₱ {totalExpenses:N2}";
                txtRemainingBudget.Text = $"₱ {totalRemaining:N2}";

                // Color code remaining budget
                if (totalRemaining < 0)
                {
                    txtRemainingBudget.ForeColor = Color.Red;
                }
                else if (totalRemaining < totalBudget * 0.2m)
                {
                    txtRemainingBudget.ForeColor = Color.Orange;
                }
                else
                {
                    txtRemainingBudget.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating totals: {ex.Message}");
            }
        }

        private void DgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                    string categoryName = dgvCategories.Rows[e.RowIndex].Cells["CategoryName"].Value?.ToString();

                    // Delete from database
                    DeleteCategoryFromDatabase(categoryName);

                    // Delete from grid
                    dgvCategories.Rows.RemoveAt(e.RowIndex);
                    CalculateTotals();
                    AdjustCategoryGridHeight();
                    RefreshTransactionCategoryComboBox();
                }
            }
        }

        private void DgvTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle Delete button click
            if (e.ColumnIndex == dgvTransactions.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this transaction?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    int transactionId = Convert.ToInt32(dgvTransactions.Rows[e.RowIndex].Cells["TransactionID"].Value);
                    
                    // ✅ Delete from database
                    DeleteTransactionFromDatabase(transactionId);

                    // Delete from grid
                    dgvTransactions.Rows.RemoveAt(e.RowIndex);
                    
                    // Recalculate category expenses after deleting transaction
                    UpdateCategoryExpenses();
                    CalculateTotals();
                    AdjustTransactionGridHeight();
                }
            }
        }

        // Public method to add expenses to a category (can be called from other parts of the application)
        public void AddExpenseToCategory(string categoryName, decimal expenseAmount)
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (row.Cells["CategoryName"].Value?.ToString() == categoryName)
                {
                    decimal currentExpenses = 0;
                    if (row.Cells["Expenses"].Value != null)
                        decimal.TryParse(row.Cells["Expenses"].Value.ToString(), out currentExpenses);

                    row.Cells["Expenses"].Value = currentExpenses + expenseAmount;
                    CalculateRemaining(row);
                    CalculateTotals();
                    break;
                }
            }
        }

        // Database Methods
        private string GetConnectionString()
        {
            // Replace with your actual connection string
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }

        private void SaveCategoryToDatabase(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dgvCategories.Rows[rowIndex];

                string categoryName = row.Cells["CategoryName"].Value?.ToString() ?? "New Category";
                decimal budget = 0;
                decimal expenses = 0;
                decimal remaining = 0;

                if (row.Cells["Budget"].Value != null)
                    decimal.TryParse(row.Cells["Budget"].Value.ToString(), out budget);

                if (row.Cells["Expenses"].Value != null)
                    decimal.TryParse(row.Cells["Expenses"].Value.ToString(), out expenses);

                if (row.Cells["Remaining"].Value != null)
                    decimal.TryParse(row.Cells["Remaining"].Value.ToString(), out remaining);

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"INSERT INTO budget_category 
                                    (project_code, category_name, category_budget, category_expenses, category_remaining) 
                                    VALUES (@project_code, @categoryName, @budget, @expenses, @remaining)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@categoryName", categoryName);
                        cmd.Parameters.AddWithValue("@budget", budget);
                        cmd.Parameters.AddWithValue("@expenses", expenses);
                        cmd.Parameters.AddWithValue("@remaining", remaining);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving category: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCategoryInDatabase(DataGridViewRow row)
        {
            try
            {
                // Get the ORIGINAL category name (before edit)
                string originalCategoryName = row.Cells["CategoryName"].Tag?.ToString() 
                                              ?? row.Cells["CategoryName"].Value?.ToString();
                
                // Get the NEW values
                string newCategoryName = row.Cells["CategoryName"].Value?.ToString();
                decimal budget = 0;
                decimal expenses = 0;
                decimal remaining = 0;

                if (row.Cells["Budget"].Value != null)
                    decimal.TryParse(row.Cells["Budget"].Value.ToString(), out budget);

                if (row.Cells["Expenses"].Value != null)
                    decimal.TryParse(row.Cells["Expenses"].Value.ToString(), out expenses);

                if (row.Cells["Remaining"].Value != null)
                    decimal.TryParse(row.Cells["Remaining"].Value.ToString(), out remaining);

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"UPDATE budget_category 
                                    SET category_name = @newName,
                                        category_budget = @budget, 
                                        category_expenses = @expenses, 
                                        category_remaining = @remaining 
                                    WHERE project_code = @project_code 
                                    AND category_name = @originalName";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@newName", newCategoryName);
                        cmd.Parameters.AddWithValue("@originalName", originalCategoryName);
                        cmd.Parameters.AddWithValue("@budget", budget);
                        cmd.Parameters.AddWithValue("@expenses", expenses);
                        cmd.Parameters.AddWithValue("@remaining", remaining);

                        cmd.ExecuteNonQuery();
                    }
                }
                
                // Store the new name as the "original" for next time
                row.Cells["CategoryName"].Tag = newCategoryName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating category: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCategoryFromDatabase(string categoryName)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Delete category
                            using (MySqlCommand cmd = new MySqlCommand(@"
                                DELETE FROM budget_category 
                                WHERE project_code = @project_code 
                                AND category_name = @categoryName", conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@project_code", projectCode);
                                cmd.Parameters.AddWithValue("@categoryName", categoryName);
                                cmd.ExecuteNonQuery();
                            }
                            
                            // Delete associated transactions
                            using (MySqlCommand cmd = new MySqlCommand(@"
                                DELETE FROM budget_transaction 
                                WHERE project_code = @project_code 
                                AND transaction_category = @categoryName", conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@project_code", projectCode);
                                cmd.Parameters.AddWithValue("@categoryName", categoryName);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting category: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoriesFromDatabase()
        {
            try
            {
                dgvCategories.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT bc.category_name, bc.category_budget, bc.category_expenses, bc.category_remaining 
                                    FROM budget_category bc
                                    INNER JOIN budget_allocation ba ON bc.project_code = ba.project_code
                                    WHERE bc.project_code = @project_code 
                                    AND ba.status = 'Approved'";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex = dgvCategories.Rows.Add(
                                    reader["category_name"],
                                    reader["category_budget"],
                                    reader["category_expenses"],
                                    reader["category_remaining"]
                                );
                                
                                // Store original name for tracking updates
                                dgvCategories.Rows[rowIndex].Cells["CategoryName"].Tag = reader["category_name"];
                            }
                        }
                    }
                }

                // Populate the transaction category combobox after loading categories
                RefreshTransactionCategoryComboBox();
                
                CalculateTotals();
                AdjustCategoryGridHeight();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int SaveTransactionToDatabase(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dgvTransactions.Rows[rowIndex];

                // Extract values with proper null handling
                string description = row.Cells["Description"].Value?.ToString() ?? "New Transaction";
                string category = row.Cells["Category"].Value?.ToString() ?? "";

                decimal amount = 0;
                if (row.Cells["Amount"].Value != null)
                    decimal.TryParse(row.Cells["Amount"].Value.ToString(), out amount);

                // Parse date properly
                DateTime date = DateTime.Now;
                if (row.Cells["Date"].Value != null)
                {
                    if (row.Cells["Date"].Value is DateTime dtValue)
                    {
                        date = dtValue;
                    }
                    else if (DateTime.TryParse(row.Cells["Date"].Value.ToString(), out DateTime parsedDate))
                    {
                        date = parsedDate;
                    }
                }

                // Debug output to verify values
                System.Diagnostics.Debug.WriteLine($"Saving transaction: Description={description}, Category={category}, Amount={amount}, Date={date:yyyy-MM-dd}");

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"INSERT INTO budget_transaction 
                                    (project_code, transaction_description, transaction_category, transaction_amount, transaction_date) 
                                    VALUES (@project_code, @transactionDescription, @transactionCategory, @transactionAmount, @transactionDate);
                                    SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@transactionDescription", description);
                        cmd.Parameters.AddWithValue("@transactionCategory", category);
                        cmd.Parameters.AddWithValue("@transactionAmount", amount);
                        
                        // ✅ FIX: Pass DateTime object directly, not string
                        cmd.Parameters.Add("@transactionDate", MySqlDbType.Date).Value = date;

                        // Execute and get the new ID
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void UpdateTransactionInDatabase(DataGridViewRow row)
        {
            try
            {
                // Get the transaction ID (more reliable than description)
                if (row.Cells["TransactionID"].Value == null)
                {
                    System.Diagnostics.Debug.WriteLine("Cannot update transaction: No TransactionID");
                    return;
                }
                
                int transactionId = Convert.ToInt32(row.Cells["TransactionID"].Value);

                // Get the NEW values
                string description = row.Cells["Description"].Value?.ToString() ?? "New Transaction";
                string category = row.Cells["Category"].Value?.ToString() ?? "";
                
                decimal amount = 0;
                if (row.Cells["Amount"].Value != null)
                    decimal.TryParse(row.Cells["Amount"].Value.ToString(), out amount);

                // Parse date properly
                DateTime date = DateTime.Now;
                if (row.Cells["Date"].Value != null)
                {
                    if (row.Cells["Date"].Value is DateTime dtValue)
                    {
                        date = dtValue;
                    }
                    else if (DateTime.TryParse(row.Cells["Date"].Value.ToString(), out DateTime parsedDate))
                    {
                        date = parsedDate;
                    }
                }

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"UPDATE budget_transaction
                                    SET transaction_description = @description,
                                        transaction_category = @category, 
                                        transaction_amount = @amount, 
                                        transaction_date = @date
                                    WHERE id = @transactionId 
                                    AND project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@transactionId", transactionId);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        
                        // ✅ FIX: Pass DateTime object directly
                        cmd.Parameters.Add("@date", MySqlDbType.Date).Value = date;

                        cmd.ExecuteNonQuery();
                    }
                }

                // Store the new description as the "original" for next time
                row.Cells["Description"].Tag = description;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating transaction: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteTransactionFromDatabase(int transactionId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"DELETE FROM budget_transaction 
                                    WHERE project_code = @project_code 
                                    AND id = @transactionId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@transactionId", transactionId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting category: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactionFromDatabase()
        {
            try
            {
                dgvTransactions.Rows.Clear(); // ✅ Corrected to dgvTransactions

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT id, transaction_description, transaction_category, transaction_amount, transaction_date 
                                    FROM budget_transaction 
                                    WHERE project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex = dgvTransactions.Rows.Add(
                                    reader["transaction_description"],
                                    reader["transaction_category"],
                                    reader["transaction_amount"],
                                    reader["transaction_date"],
                                    reader["id"] // Add ID to the row
                                );

                                // Store original name for tracking updates
                                dgvTransactions.Rows[rowIndex].Cells["Description"].Tag = reader["transaction_description"];
                            }
                        }
                    }
                }

                // Populate the transaction category combobox after loading categories
                RefreshTransactionCategoryComboBox();

                CalculateTotals();
                AdjustCategoryGridHeight();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactionH: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this new method to load the project budget
        private void LoadProjectBudget()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT budget_allocation 
                                    FROM project_list 
                                    WHERE project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            decimal budgetAllocation = Convert.ToDecimal(result);
                            txtProjectBudget.Text = $"₱ {budgetAllocation:N2}";
                        }
                        else
                        {
                            txtProjectBudget.Text = "₱ 0.00";
                        }
                    }
                }

                // Make it read-only since it comes from the database
                txtProjectBudget.ReadOnly = true;
                txtProjectBudget.BackColor = Color.WhiteSmoke;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading project budget: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProjectBudget.Text = "₱ 0.00";
            }
        }
    }
}