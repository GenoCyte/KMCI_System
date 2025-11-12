using KMCI_System.Login;
using MySql.Data.MySqlClient;
using System.Data;

namespace KMCI_System.SalesModule
{
    public partial class CreateQuotation : UserControl
    {
        //Company Info
        private Label lblClientName;
        private TextBox txtClientName;
        private Label lblTin;
        private TextBox txtTin;
        private Label lblContactPerson;
        private TextBox txtContactPerson;
        private Label lblContactNumber;
        private TextBox txtContactNumber;
        private Label lblContactEmail;
        private TextBox txtContactEmail;

        //Quotation info
        private Label lblRequestedBy;
        private TextBox txtRequestedBy;
        private Label lblPaymentTerms;
        private TextBox txtPaymentTerms;
        private Label lblDeliveryterms;
        private TextBox txtDeliveryterms;
        private Label lblBidPercentage;
        private TextBox txtBidPercentage;
        private Label lblDeliveryAddress;
        private TextBox txtDeliveryAddress;
        private Button btnAddProduct;

        //Quotation items
        private Panel pnlQuotationPanel;
        private Label lblQuotationHeader;
        private DataGridView dgvPricing;
        private Panel headerPanel;
        private ComboBox cmbPricing;

        //Pricing calculation
        private Label lblBidpriceHeader;
        private Label lblVatExcluded;
        private Label lblVatExcludedNumber;
        private Label lblOutputVat;
        private Label lblOutputVatNumber;
        private Label lblTotalBidPrice;
        private Label lblTotalBidPriceNumber;
        private Label lblCostHeader;
        private Label lblVatExcludedNumber2;
        private Label lblVatExcluded2;
        private Label lblInputVat;
        private Label lblInputVatNumber;
        private Label lblTotalCost;
        private Label lblTotalCostNumber;
        private Label lblTaxSummaryHeader;
        private Label lblVatPayable;
        private Label lblVatPayableNumber;
        private Label lblGrossProfit;
        private Label lblGrossProfitNumber;
        private Label lblEwt;
        private TextBox txtEwt;
        private Label lblEwtTotal;
        private Label line2;

        //Final net profit calculation
        private Label lblIncomeTax;
        private Label lblIncomeTaxNumber;
        private Label lblEwt2;
        private Label lblEwtnumber2;
        private Label lblFinalIncometax;
        private Label lblFinalIncometaxNumber;
        private Label lblNetProfit;
        private Label lblNetProfitNumber;
        private Label lblContingency;
        private TextBox txtContingency;
        private Label lblContingencyNumber;
        private Label lblFinalNetProfit;
        private Label lblFinalNetProfitNumber;
        private Label lblProfitMargin;
        private Label lblProfitMarginNumber;
        private Panel pnlPricing;
        private Label lblRemarkHeader;
        private TextBox txtRemarks;

        //Buttons
        private Panel pnlButtons;
        private Button btnClearForms;
        private Button btnSaveAndExportQuotation;
        private Button btnSaveQuotation;

        private int ypos;
        private string projectCode;
        private int companyId;
        private string currentUser; // ✅ Added field for current user ID

        public CreateQuotation()
        {
            InitializeComponent();

            // Enable vertical scrolling for the entire form
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1100, 700);

            // ✅ TODO: Replace with actual user authentication logic
            // For example: currentUserId = UserSession.CurrentUserId;
            currentUser = Session.CurrentUserName;

            SetupForm();
            SetupDataGridView();
            SetupPriceCalculation();
        }

        public CreateQuotation(string projectCode) : this()
        {
            this.projectCode = projectCode;
            LoadProjectDetails();
        }

        private void LoadProjectDetails()
        {
            if (string.IsNullOrEmpty(projectCode))
                return;

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Get company details, proponent info, and address
                    string query = @"
                        SELECT 
                            pl.company_id,
                            pl.company_name,
                            pl.tin,
                            p.proponent_name,
                            p.proponent_number,
                            p.proponent_email,
                            ca.house_num,
                            ca.street,
                            ca.subdivision,
                            ca.barangay,
                            ca.city,
                            ca.province,
                            ca.region
                        FROM project_list pl
                        LEFT JOIN proponents p ON pl.proponent_id = p.id
                        LEFT JOIN company_address ca ON pl.address_id = ca.id
                        WHERE pl.project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load Company Info
                                companyId = reader["company_id"] != DBNull.Value
                                    ? Convert.ToInt32(reader["company_id"])
                                    : 0;

                                txtClientName.Text = reader["company_name"]?.ToString() ?? string.Empty;
                                txtTin.Text = reader["tin"]?.ToString() ?? string.Empty;

                                // Load Proponent Info
                                txtContactPerson.Text = reader["proponent_name"]?.ToString() ?? string.Empty;
                                txtContactNumber.Text = reader["proponent_number"]?.ToString() ?? string.Empty;
                                txtContactEmail.Text = reader["proponent_email"]?.ToString() ?? string.Empty;

                                // Load Delivery Address
                                string addressParts = string.Empty;
                                if (reader["house_num"] != DBNull.Value && !string.IsNullOrEmpty(reader["house_num"].ToString()))
                                    addressParts += reader["house_num"].ToString() + " ";
                                if (reader["street"] != DBNull.Value && !string.IsNullOrEmpty(reader["street"].ToString()))
                                    addressParts += reader["street"].ToString() + ", ";
                                if (reader["subdivision"] != DBNull.Value && !string.IsNullOrEmpty(reader["subdivision"].ToString()))
                                    addressParts += reader["subdivision"].ToString() + ", ";
                                if (reader["barangay"] != DBNull.Value && !string.IsNullOrEmpty(reader["barangay"].ToString()))
                                    addressParts += reader["barangay"].ToString() + ", ";
                                if (reader["city"] != DBNull.Value && !string.IsNullOrEmpty(reader["city"].ToString()))
                                    addressParts += reader["city"].ToString() + ", ";
                                if (reader["province"] != DBNull.Value && !string.IsNullOrEmpty(reader["province"].ToString()))
                                    addressParts += reader["province"].ToString();

                                txtDeliveryAddress.Text = addressParts.TrimEnd(',', ' ');
                            }
                            else
                            {
                                MessageBox.Show(
                                    $"Project '{projectCode}' not found in database.",
                                    "Project Not Found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error loading project details: {ex.Message}",
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void SetupForm()
        {
            ypos = 80;

            lblClientName = new Label
            {
                Text = "Client Name:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblClientName);

            txtClientName = new TextBox
            {
                Location = new Point(150, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtClientName);

            lblTin = new Label
            {
                Text = "TIN:",
                Location = new Point(550, ypos),
                AutoSize = true
            };
            Controls.Add(lblTin);

            txtTin = new TextBox
            {
                Location = new Point(700, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtTin);

            ypos += 40;

            lblContactPerson = new Label
            {
                Text = "Contact Person:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblContactPerson);

            txtContactPerson = new TextBox
            {
                Location = new Point(150, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtContactPerson);

            lblContactNumber = new Label
            {
                Text = "Contact Number:",
                Location = new Point(550, ypos),
                AutoSize = true
            };
            Controls.Add(lblContactNumber);

            txtContactNumber = new TextBox
            {
                Location = new Point(700, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtContactNumber);

            ypos += 40;

            lblContactEmail = new Label
            {
                Text = "Email:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblContactEmail);

            txtContactEmail = new TextBox
            {
                Location = new Point(150, ypos),
                Width = 350,
                ReadOnly = true,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(txtContactEmail);

            ypos += 60;

            lblRequestedBy = new Label
            {
                Text = "Requested By:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblRequestedBy);

            txtRequestedBy = new TextBox
            {
                Location = new Point(150, ypos),
                Text = "Employee",
                Width = 350
            };
            Controls.Add(txtRequestedBy);

            lblPaymentTerms = new Label
            {
                Text = "Payment Terms:",
                Location = new Point(550, ypos),
                AutoSize = true
            };
            Controls.Add(lblPaymentTerms);

            txtPaymentTerms = new TextBox
            {
                Location = new Point(700, ypos),
                Width = 350
            };
            txtPaymentTerms.KeyPress += TxtNumericOnly_KeyPress; // ✅ Add numeric validation
            Controls.Add(txtPaymentTerms);

            ypos += 40;

            lblBidPercentage = new Label
            {
                Text = "Bid Percentage:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblBidPercentage);

            txtBidPercentage = new TextBox
            {
                Location = new Point(150, ypos),
                Text = "15",
                Width = 350
            };
            txtBidPercentage.TextChanged += TxtBidPercentage_TextChanged;
            Controls.Add(txtBidPercentage);

            lblDeliveryterms = new Label
            {
                Text = "Delivery Terms:",
                Location = new Point(550, ypos),
                AutoSize = true
            };
            Controls.Add(lblDeliveryterms);

            txtDeliveryterms = new TextBox
            {
                Location = new Point(700, ypos),
                Width = 350
            };
            txtDeliveryterms.KeyPress += TxtNumericOnly_KeyPress; // ✅ Add numeric validation
            Controls.Add(txtDeliveryterms);

            ypos += 40;

            lblDeliveryAddress = new Label
            {
                Text = "Delivery Address:",
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblDeliveryAddress);
            txtDeliveryAddress = new TextBox
            {
                Location = new Point(150, ypos),
                Width = 900
            };
            Controls.Add(txtDeliveryAddress);

            ypos += 60;

            lblQuotationHeader = new Label
            {
                Text = "Quotation Items",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            Controls.Add(lblQuotationHeader);

            btnAddProduct = new Button
            {
                Text = "Add Product",
                Location = new Point(950, ypos - 5),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                TabIndex = 3,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnAddProduct.Click += BtnAddProduct_Click;
            Controls.Add(btnAddProduct);
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            using (var addProductForm = new AddProductList())
            {
                // Set form properties for better UX
                addProductForm.StartPosition = FormStartPosition.CenterParent;
                addProductForm.Size = new Size(1200, 600);
                addProductForm.Text = "Add Products to Quotation";

                if (addProductForm.ShowDialog() == DialogResult.OK)
                {
                    // Get selected products from the form
                    var selectedProducts = addProductForm.GetSelectedProducts();

                    if (selectedProducts != null && selectedProducts.Count > 0)
                    {
                        // Add each selected product to the DataGridView
                        foreach (var product in selectedProducts)
                        {
                            // Check if product already exists in the grid
                            bool productExists = false;
                            foreach (DataGridViewRow row in dgvPricing.Rows)
                            {
                                if (row.Cells["ItemCode"].Value?.ToString() == product.SKU)
                                {
                                    productExists = true;
                                    break;
                                }
                            }

                            if (!productExists)
                            {
                                // Add new product row with base price
                                AddProduct(
                                    product.SKU,
                                    product.Name,
                                    product.Description,
                                    product.Brand,
                                    product.BasePrice // ✅ Pass base price
                                );
                            }
                            else
                            {
                                MessageBox.Show(
                                    $"Product '{product.Name}' is already in the quotation.",
                                    "Duplicate Product",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                            }
                        }

                        // Move success message outside the loop
                        MessageBox.Show(
                            $"{selectedProducts.Count} product(s) added to quotation.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
        }

        private void SetupDataGridView()
        {
            ypos += 40;

            pnlQuotationPanel = new Panel
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 450, // Initial height, will be adjusted dynamically
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Padding = new Padding(0, 0, 0, 10) // Padding at bottom for aesthetics
            };

            // Create header panel for group titles
            headerPanel = new Panel
            {
                Location = new Point(0, 0), // Position at top of pnlQuotationPanel
                Height = 40,
                Width = 1465, // Full width to match total column width
                BackColor = Color.FromArgb(250, 250, 250)
            };

            // Add group header labels - positioned to match column groups
            Label lblProductDetails = new Label
            {
                Text = "PRODUCT DETAILS",
                Location = new Point(0, 10),
                Width = 500, // ItemCode(100) + Name(120) + Description(180) + Brand(100)
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.LightBlue,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10, 0, 0, 0)
            };

            Label lblInternalPricing = new Label
            {
                Text = "INTERNAL PRICING",
                Location = new Point(500, 10),
                Width = 400, // InternalPrice(120) + Supplier(150) + AmountToPay(130)
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Orange,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10, 0, 0, 0)
            };

            Label lblClientPricing = new Label
            {
                Text = "CLIENT PRICING",
                Location = new Point(900, 10),
                Width = 630, // ABCPrice(120) + Quantity(100) + ProposalPrice(130) + TotalAmount(130) + Action(80)
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Violet,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10, 0, 0, 0)
            };

            headerPanel.Controls.AddRange(new Control[] { lblProductDetails, lblInternalPricing, lblClientPricing });
            pnlQuotationPanel.Controls.Add(headerPanel);

            // Create DataGridView
            dgvPricing = new DataGridView
            {
                Location = new Point(0, 40), // Position below headerPanel
                Width = 1465, // Full width to match total column width
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 50 },
                ScrollBars = ScrollBars.None, // Disable scrollbars on DataGridView
                EnableHeadersVisualStyles = false
            };

            // Style headers
            dgvPricing.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvPricing.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPricing.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvPricing.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPricing.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dgvPricing.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvPricing.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvPricing.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvPricing.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPricing.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            dgvPricing.GridColor = Color.FromArgb(220, 220, 220);
            dgvPricing.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // ===== PRODUCT DETAILS COLUMNS =====
            dgvPricing.Columns.Add("ItemCode", "Item Code");
            dgvPricing.Columns.Add("Name", "Name");
            dgvPricing.Columns.Add("Description", "Description");
            dgvPricing.Columns.Add("Brand", "Brand");

            // ===== INTERNAL PRICING COLUMNS =====
            dgvPricing.Columns.Add("InternalPrice", "Internal Price");

            // Supplier dropdown column
            DataGridViewComboBoxColumn cmbSupplier = new DataGridViewComboBoxColumn
            {
                Name = "Supplier",
                HeaderText = "Supplier",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
            };
            cmbSupplier.Items.AddRange(new[] { "-", "Supplier A", "Supplier B", "Supplier C" });
            dgvPricing.Columns.Add(cmbSupplier);

            dgvPricing.Columns.Add("AmountToPay", "Amount to Pay");

            // ===== CLIENT PRICING COLUMNS =====
            dgvPricing.Columns.Add("ABCPrice", "ABC Price");
            dgvPricing.Columns.Add("Quantity", "Quantity");
            dgvPricing.Columns.Add("ProposalPrice", "Proposal Price");
            dgvPricing.Columns.Add("TotalAmount", "Total Amount");

            // Action column with button
            DataGridViewButtonColumn btnAction = new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Text = "✖",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvPricing.Columns.Add(btnAction);

            // Set column widths
            dgvPricing.Columns["ItemCode"].Width = 100;
            dgvPricing.Columns["Name"].Width = 120;
            dgvPricing.Columns["Description"].Width = 180;
            dgvPricing.Columns["Brand"].Width = 100;

            dgvPricing.Columns["InternalPrice"].Width = 120;
            dgvPricing.Columns["Supplier"].Width = 150;
            dgvPricing.Columns["AmountToPay"].Width = 130;

            dgvPricing.Columns["ABCPrice"].Width = 120;
            dgvPricing.Columns["Quantity"].Width = 100;
            dgvPricing.Columns["ProposalPrice"].Width = 130;
            dgvPricing.Columns["TotalAmount"].Width = 130;
            dgvPricing.Columns["Action"].Width = 80;

            // Set read-only columns
            dgvPricing.Columns["ItemCode"].ReadOnly = true;
            dgvPricing.Columns["Name"].ReadOnly = true;
            dgvPricing.Columns["Description"].ReadOnly = true;
            dgvPricing.Columns["Brand"].ReadOnly = true;
            dgvPricing.Columns["AmountToPay"].ReadOnly = true;
            dgvPricing.Columns["ProposalPrice"].ReadOnly = false;  // Add this line
            dgvPricing.Columns["TotalAmount"].ReadOnly = true;

            // Alignment
            dgvPricing.Columns["InternalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPricing.Columns["AmountToPay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPricing.Columns["ABCPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPricing.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPricing.Columns["ProposalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPricing.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Format currency columns
            dgvPricing.Columns["InternalPrice"].DefaultCellStyle.Format = "0.00";
            dgvPricing.Columns["AmountToPay"].DefaultCellStyle.Format = "₱0.00";
            dgvPricing.Columns["ABCPrice"].DefaultCellStyle.Format = "₱ 0";
            dgvPricing.Columns["ProposalPrice"].DefaultCellStyle.Format = "₱0.00";  // Changed from "₱ 0.00" to "₱0.00"
            dgvPricing.Columns["TotalAmount"].DefaultCellStyle.Format = "₱0.00";

            // Events
            dgvPricing.CellValueChanged += DgvPricing_CellValueChanged;
            dgvPricing.CellContentClick += DgvPricing_CellContentClick;
            dgvPricing.CurrentCellDirtyStateChanged += DgvPricing_CurrentCellDirtyStateChanged;
            dgvPricing.RowsAdded += DgvPricing_RowsChanged;
            dgvPricing.DataError += DgvPricing_DataError;

            pnlQuotationPanel.Controls.Add(dgvPricing);
            Controls.Add(pnlQuotationPanel);

            // Adjust panel height after adding data
            AdjustPanelHeight();
        }

        private void DgvPricing_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commit changes immediately for ComboBox
            if (dgvPricing.IsCurrentCellDirty)
            {
                dgvPricing.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvPricing_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPricing.Rows[e.RowIndex];

            // Handle supplier selection change
            if (e.ColumnIndex == dgvPricing.Columns["Supplier"].Index)
            {
                string selectedSupplier = row.Cells["Supplier"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedSupplier) && selectedSupplier != "-")
                {
                    // Get supplier data from row's Tag
                    List<SupplierInfo> suppliers = row.Tag as List<SupplierInfo>;

                    if (suppliers != null)
                    {
                        // Find the selected supplier
                        var supplier = suppliers.FirstOrDefault(s => s.VendorName == selectedSupplier);

                        if (supplier != null && supplier.BasePrice > 0)
                        {
                            // Update Internal Price with supplier's price
                            row.Cells["InternalPrice"].Value = supplier.BasePrice;
                        }
                    }
                }
            }

            // Calculate when InternalPrice, Quantity, or Supplier changes
            if (e.ColumnIndex == dgvPricing.Columns["InternalPrice"].Index ||
                e.ColumnIndex == dgvPricing.Columns["Quantity"].Index ||
                e.ColumnIndex == dgvPricing.Columns["Supplier"].Index)
            {
                CalculateRowValues(row);
            }
        }

        private void DgvPricing_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent crashes on data errors
            e.ThrowException = false;
            System.Diagnostics.Debug.WriteLine($"DataGridView error at [{e.RowIndex}, {e.ColumnIndex}]: {e.Exception?.Message}");
        }

        private void CalculateRowValues(DataGridViewRow row)
        {
            try
            {
                // Get values from cells
                decimal internalPrice = 0;
                int quantity = 0;
                decimal bidPercentage = 0;

                // Parse InternalPrice
                if (row.Cells["InternalPrice"].Value != null &&
                    decimal.TryParse(row.Cells["InternalPrice"].Value.ToString(), out decimal parsedPrice))
                {
                    internalPrice = parsedPrice;
                }

                // Parse Quantity
                if (row.Cells["Quantity"].Value != null &&
                    int.TryParse(row.Cells["Quantity"].Value.ToString(), out int parsedQty))
                {
                    quantity = parsedQty;
                }

                // Parse BidPercentage from txtBidPercentage
                if (!string.IsNullOrWhiteSpace(txtBidPercentage.Text) &&
                    decimal.TryParse(txtBidPercentage.Text, out decimal parsedBidPercentage))
                {
                    bidPercentage = parsedBidPercentage;
                }

                // Calculate Amount to Pay = InternalPrice * Quantity
                decimal amountToPay = internalPrice * quantity;
                row.Cells["AmountToPay"].Value = amountToPay;

                // Calculate Proposal Price = InternalPrice + (InternalPrice * BidPercentage / 100)
                decimal proposalPrice = internalPrice + (internalPrice * bidPercentage / 100);
                row.Cells["ProposalPrice"].Value = proposalPrice;

                // Calculate Total Amount = ProposalPrice * Quantity
                decimal totalAmount = proposalPrice * quantity;
                row.Cells["TotalAmount"].Value = totalAmount;

                // After updating row values, recalculate the summary
                CalculatePricingSummary();
            }
            catch (Exception ex)
            {
                // Handle any calculation errors silently or log them
                System.Diagnostics.Debug.WriteLine($"Calculation error: {ex.Message}");
            }
        }

        private void CalculatePricingSummary()
        {
            try
            {
                // Sum all TotalAmount values from the DataGridView
                decimal totalAmountSum = 0;
                decimal amountToPaySum = 0;

                foreach (DataGridViewRow row in dgvPricing.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["TotalAmount"].Value != null &&
                            decimal.TryParse(row.Cells["TotalAmount"].Value.ToString(), out decimal totalAmt))
                        {
                            totalAmountSum += totalAmt;
                        }

                        if (row.Cells["AmountToPay"].Value != null &&
                            decimal.TryParse(row.Cells["AmountToPay"].Value.ToString(), out decimal amtToPay))
                        {
                            amountToPaySum += amtToPay;
                        }
                    }
                }

                // 1. BID PRICE CALCULATION (VAT CALCULATIONS)
                decimal vatExcluded = totalAmountSum / 1.12m;
                decimal outputVat = vatExcluded * 0.12m;
                decimal totalBidPrice = vatExcluded + outputVat;

                lblVatExcludedNumber.Text = $"₱ {vatExcluded:N2}";
                lblOutputVatNumber.Text = $"₱ {outputVat:N2}";
                lblTotalBidPriceNumber.Text = $"₱ {totalBidPrice:N2}";

                // 2. COST CALCULATION
                decimal vatExcluded2 = amountToPaySum / 1.12m;
                decimal inputVat = vatExcluded2 * 0.12m;
                decimal totalCost = vatExcluded2 + inputVat;

                lblVatExcludedNumber2.Text = $"₱ {vatExcluded2:N2}";
                lblInputVatNumber.Text = $"₱ {inputVat:N2}";
                lblTotalCostNumber.Text = $"₱ {totalCost:N2}";

                // 3. TAX SUMMARY - VAT PAYABLE
                decimal vatPayable = outputVat - inputVat;
                lblVatPayableNumber.Text = $"₱ {vatPayable:N2}";

                // 4. TAX SUMMARY - GROSS PROFIT
                decimal grossProfit = vatExcluded - vatExcluded2;
                lblGrossProfitNumber.Text = $"₱ {grossProfit:N2}";

                // 5. TAX SUMMARY - EWT (Withholding Tax)
                decimal ewtPercentage = 0;
                if (!string.IsNullOrWhiteSpace(txtEwt.Text) &&
                    decimal.TryParse(txtEwt.Text, out decimal parsedEwt))
                {
                    ewtPercentage = parsedEwt;
                }
                decimal ewtTotal = vatExcluded * (ewtPercentage / 100);
                lblEwtTotal.Text = $"₱ {ewtTotal:N2}";

                // 6. INCOME TAX CALCULATION
                decimal incomeTax = grossProfit * 0.25m;
                lblIncomeTaxNumber.Text = $"₱ {incomeTax:N2}";

                // Display EWT Amount (same as ewtTotal)
                lblEwtnumber2.Text = $"₱ {ewtTotal:N2}";

                // Final Income Tax
                decimal finalIncomeTax = incomeTax - ewtTotal;
                lblFinalIncometaxNumber.Text = $"₱ {finalIncomeTax:N2}";

                // 7. NET PROFIT
                decimal netProfit = grossProfit - finalIncomeTax;
                lblNetProfitNumber.Text = $"₱ {netProfit:N2}";

                // 8. CONTINGENCY
                decimal contingencyPercentage = 0;
                if (!string.IsNullOrWhiteSpace(txtContingency.Text) &&
                    decimal.TryParse(txtContingency.Text, out decimal parsedContingency))
                {
                    contingencyPercentage = parsedContingency;
                }
                decimal contingencyAmount = netProfit * (contingencyPercentage / 100);
                lblContingencyNumber.Text = $"₱ {contingencyAmount:N2}";

                // 9. FINAL NET PROFIT
                decimal finalNetProfit = netProfit - contingencyAmount;
                lblFinalNetProfitNumber.Text = $"₱ {finalNetProfit:N2}";

                // 10. PROFIT MARGIN
                decimal profitMargin = 0;
                if (totalBidPrice != 0)
                {
                    profitMargin = (finalNetProfit / totalBidPrice) * 100;
                }
                lblProfitMarginNumber.Text = $"{profitMargin:N2}%";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Pricing summary calculation error: {ex.Message}");
            }
        }

        private void DgvPricing_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle Action button click (delete row)
            if (e.ColumnIndex == dgvPricing.Columns["Action"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Remove this item?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dgvPricing.Rows.RemoveAt(e.RowIndex);
                    AdjustPanelHeight();
                }
            }
        }

        // Method to add new row
        public void AddProduct(string itemCode, string name, string description, string brand, decimal basePrice)
        {
            int rowIndex = dgvPricing.Rows.Add(
                itemCode,           // ItemCode
                name,               // Name
                description,        // Description
                brand,              // Brand
                basePrice,          // InternalPrice (default base price)
                "-",                // Supplier (default)
                0,                  // AmountToPay (calculated)
                0,                  // ABCPrice
                1,                  // Quantity (default 1)
                0,                  // ProposalPrice (calculated)
                0                   // TotalAmount (calculated)
            );

            // Load suppliers for this product
            List<SupplierInfo> suppliers = LoadSuppliersForProduct(itemCode);

            // Get the supplier cell for the newly added row
            DataGridViewComboBoxCell supplierCell = (DataGridViewComboBoxCell)dgvPricing.Rows[rowIndex].Cells["Supplier"];

            // Clear existing items
            supplierCell.Items.Clear();

            // Add default option
            supplierCell.Items.Add("-");

            // Add all suppliers
            foreach (var supplier in suppliers)
            {
                supplierCell.Items.Add(supplier.VendorName);
            }

            // ✅ NEW: Auto-select lowest price supplier
            if (suppliers.Count > 0)
            {
                // Find supplier with lowest base price
                var lowestPriceSupplier = suppliers.OrderBy(s => s.BasePrice).First();

                // Set the supplier dropdown to the lowest price supplier
                supplierCell.Value = lowestPriceSupplier.VendorName;

                // Update Internal Price with the lowest price
                dgvPricing.Rows[rowIndex].Cells["InternalPrice"].Value = lowestPriceSupplier.BasePrice;
            }
            else
            {
                // No suppliers found, set default value
                supplierCell.Value = "-";
            }

            // Store supplier data in the row's Tag for later use
            dgvPricing.Rows[rowIndex].Tag = suppliers;

            // Calculate row values
            CalculateRowValues(dgvPricing.Rows[rowIndex]);

            AdjustPanelHeight();
        }

        private void AdjustPanelHeight()
        {
            int headerHeight = 40; // Height of headerPanel
            int dgvHeaderHeight = dgvPricing.ColumnHeadersHeight;
            int rowHeight = dgvPricing.RowTemplate.Height;
            int rowCount = dgvPricing.Rows.Count;

            // Calculate required height
            int maxRows = 8; // Maximum rows to display before scrolling
            int visibleRows = Math.Min(rowCount, maxRows);

            // Calculate heights
            int dgvHeight = dgvHeaderHeight + (visibleRows * rowHeight) + 2; // +2 for borders
            int totalPanelHeight = headerHeight + dgvHeight + 10; // +10 for padding

            // Update sizes
            dgvPricing.Height = dgvHeight;
            pnlQuotationPanel.Height = totalPanelHeight;

            // Move pnlPricing below the pnlQuotationPanel with 20px spacing
            if (pnlPricing != null)
            {
                int newPricingYPos = pnlQuotationPanel.Bottom + 20;
                pnlPricing.Location = new Point(pnlPricing.Location.X, newPricingYPos);
            }

            // Move remarks section below pnlPricing with 20px spacing
            if (lblRemarkHeader != null && pnlPricing != null)
            {
                lblRemarkHeader.Location = new Point(20, pnlPricing.Bottom + 20);
            }

            if (txtRemarks != null && lblRemarkHeader != null)
            {
                txtRemarks.Location = new Point(20, lblRemarkHeader.Bottom + 10);
            }

            // Move buttons panel below remarks with 20px spacing
            if (pnlButtons != null && txtRemarks != null)
            {
                pnlButtons.Location = new Point(20, txtRemarks.Bottom + 20);
            }

            // Update AutoScrollMinSize to accommodate all panels
            int totalHeight = pnlButtons != null ? pnlButtons.Bottom + 100 :
                      (pnlPricing != null ? pnlPricing.Bottom + 100 : pnlQuotationPanel.Bottom + 100);
            this.AutoScrollMinSize = new Size(1100, totalHeight);
        }

        // Event handler for row changes
        private void DgvPricing_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustPanelHeight();
        }

        private void SetupPriceCalculation()
        {
            // Position below the quotation panel with 20px spacing
            int pricingYPos = pnlQuotationPanel.Bottom + 20;

            pnlPricing = new Panel
            {
                Location = new Point(20, pricingYPos),
                Width = 1050,
                Height = 400,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(pnlPricing);

            //Bid Price Calculation Labels
            lblBidpriceHeader = new Label
            {
                Text = "BID PRICE CALCULATION",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(10, 10),
                ForeColor = Color.Blue,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblBidpriceHeader);

            lblVatExcluded = new Label
            {
                Text = "VAT Excluded:",
                Location = new Point(20, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatExcluded);

            lblVatExcludedNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(270, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatExcludedNumber);

            lblOutputVat = new Label
            {
                Text = "Output VAT (12%):",
                Location = new Point(20, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblOutputVat);

            lblOutputVatNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(270, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblOutputVatNumber);

            lblTotalBidPrice = new Label
            {
                Text = "Total Bid Price:",
                Location = new Point(20, 110),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblTotalBidPrice);

            lblTotalBidPriceNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(270, 110),
                ForeColor = Color.Blue,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblTotalBidPriceNumber);

            //Cost Calculation Labels
            lblCostHeader = new Label
            {
                Text = "COST CALCULATION",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(380, 10),
                ForeColor = Color.Orange,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblCostHeader);

            lblVatExcluded2 = new Label
            {
                Text = "VAT Excluded:",
                Location = new Point(380, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatExcluded2);

            lblVatExcludedNumber2 = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(630, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatExcludedNumber2);

            lblInputVat = new Label
            {
                Text = "Input VAT (12%):",
                Location = new Point(380, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblInputVat);

            lblInputVatNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(630, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblInputVatNumber);

            lblTotalCost = new Label
            {
                Text = "Total Cost:",
                Location = new Point(380, 110),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblTotalCost);

            lblTotalCostNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(630, 110),
                ForeColor = Color.Orange,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblTotalCostNumber);

            //Tax Summary Labels
            lblTaxSummaryHeader = new Label
            {
                Text = "TAX SUMMARY",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(750, 10),
                ForeColor = Color.Violet,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblTaxSummaryHeader);

            lblVatPayable = new Label
            {
                Text = "VAT Payable:",
                Location = new Point(750, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatPayable);

            lblVatPayableNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 50),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblVatPayableNumber);

            lblGrossProfit = new Label
            {
                Text = "Gross Profit:",
                Location = new Point(750, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblGrossProfit);

            lblGrossProfitNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 80),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblGrossProfitNumber);

            lblEwt = new Label
            {
                Text = "EWT (%):",
                Location = new Point(750, 110),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblEwt);

            txtEwt = new TextBox
            {
                Location = new Point(940, 105),
                Width = 50,
                Text = "1"
            };
            txtEwt.TextChanged += TxtEwt_TextChanged;
            pnlPricing.Controls.Add(txtEwt);

            lblEwtTotal = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 110),
                ForeColor = Color.Violet,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblEwtTotal);

            line2 = new Label
            {
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(0, 140),
                Width = 1050,
                Height = 1
            };
            pnlPricing.Controls.Add(line2);

            //Final Net Profit Calculation Labels
            lblIncomeTax = new Label
            {
                Text = "Income Tax @ 25%:",
                Location = new Point(750, 150),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblIncomeTax);

            lblIncomeTaxNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 150),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblIncomeTaxNumber);

            lblEwt2 = new Label
            {
                Text = "EWT Amount:",
                Location = new Point(750, 180),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblEwt2);

            lblEwtnumber2 = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 180),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblEwtnumber2);

            lblFinalIncometax = new Label
            {
                Text = "Final Income Tax:",
                Location = new Point(750, 210),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblFinalIncometax);

            lblFinalIncometaxNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 210),
                ForeColor = Color.Violet,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblFinalIncometaxNumber);

            lblNetProfit = new Label
            {
                Text = "Net Profit:",
                Location = new Point(750, 240),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblNetProfit);

            lblNetProfitNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 240),
                ForeColor = Color.Violet,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblNetProfitNumber);

            lblContingency = new Label
            {
                Text = "Contingency (5%):",
                Location = new Point(750, 270),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblContingency);

            txtContingency = new TextBox
            {
                Location = new Point(940, 265),
                Width = 50,
                Text = "5"
            };
            txtContingency.TextChanged += TxtContingency_TextChanged;
            pnlPricing.Controls.Add(txtContingency);

            lblContingencyNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(1000, 270),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblContingencyNumber);

            lblFinalNetProfit = new Label
            {
                Text = "Final Net Profit:",
                Location = new Point(750, 300),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblFinalNetProfit);

            lblFinalNetProfitNumber = new Label
            {
                Text = "₱ 0.00",
                Location = new Point(980, 300),
                ForeColor = Color.Green,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true

            };
            lblFinalNetProfitNumber.TextChanged += (s, e) =>
            {
                // Compute how much wider it should be
                int newWidth = TextRenderer.MeasureText(lblFinalNetProfitNumber.Text, lblFinalNetProfitNumber.Font).Width;
                int delta = newWidth - lblFinalNetProfitNumber.Width;

                // Shift to the left by the width increase
                lblFinalNetProfitNumber.Left -= delta;

                // Update the width so AutoSize doesn't override this
                lblFinalNetProfitNumber.Width = newWidth;
            };
            pnlPricing.Controls.Add(lblFinalNetProfitNumber);

            lblProfitMargin = new Label
            {
                Text = "Profit Margin:",
                Location = new Point(750, 330),
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblProfitMargin);

            lblProfitMarginNumber = new Label
            {
                Text = "0.00%",
                Location = new Point(1000, 330),
                ForeColor = Color.Green,
                AutoSize = true
            };
            pnlPricing.Controls.Add(lblProfitMarginNumber);

            // Remarks section - positioned relative to pnlPricing
            lblRemarkHeader = new Label
            {
                Text = "Remarks",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(20, pnlPricing.Bottom + 20),
                AutoSize = true
            };
            Controls.Add(lblRemarkHeader);

            txtRemarks = new TextBox
            {
                Location = new Point(20, lblRemarkHeader.Bottom + 10),
                Width = 1050,
                Height = 80,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                PlaceholderText = "Additional Remarks..."
            };
            Controls.Add(txtRemarks);

            //Buttons panel - positioned relative to txtRemarks
            pnlButtons = new Panel
            {
                Location = new Point(20, txtRemarks.Bottom + 20),
                Width = 1050,
                Height = 50
            };
            Controls.Add(pnlButtons);

            btnClearForms = new Button
            {
                Text = "Clear Form",
                Location = new Point(610, 10),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                TabIndex = 4,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            //btnClearForms.Click += BtnClearForms_Click;
            pnlButtons.Controls.Add(btnClearForms);

            btnSaveAndExportQuotation = new Button
            {
                Text = "Save and Export Quotation",
                Location = new Point(750, 10),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 30),
                TabIndex = 5,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnSaveAndExportQuotation.Click += BtnSaveAndExportQuotation_Click;
            pnlButtons.Controls.Add(btnSaveAndExportQuotation);

            btnSaveQuotation = new Button
            {
                Text = "Save Quotation",
                Location = new Point(940, 10),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                TabIndex = 6,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnSaveQuotation.Click += BtnSaveQuotation_Click;
            pnlButtons.Controls.Add(btnSaveQuotation);

            // Update the total scroll size to include all elements
            this.AutoScrollMinSize = new Size(1100, pnlButtons.Bottom + 100);
        }

        private void TxtBidPercentage_TextChanged(object sender, EventArgs e)
        {
            // Recalculate all rows when bid percentage changes
            foreach (DataGridViewRow row in dgvPricing.Rows)
            {
                if (!row.IsNewRow)
                {
                    CalculateRowValues(row);
                }
            }
        }

        private void TxtEwt_TextChanged(object sender, EventArgs e)
        {
            // Recalculate pricing summary when EWT percentage changes
            CalculatePricingSummary();
        }

        private void TxtContingency_TextChanged(object sender, EventArgs e)
        {
            // Recalculate pricing summary when Contingency percentage changes
            CalculatePricingSummary();
        }

        private void BtnSaveAndExportQuotation_Click(object sender, EventArgs e)
        {
            if (!ValidateQuotationData())
                return;

            // Save and get the quotation_id
            int savedQuotationId = SaveQuotation();  // ✅ Now this works

            if (savedQuotationId > 0)  // Only export if save was successful
            {
                ExportToPdf(savedQuotationId);
            }
        }

        private void BtnSaveQuotation_Click(object sender, EventArgs e)
        {
            if (ValidateQuotationData())
            {
                SaveQuotation();  // ✅ Still works, just ignores return value
            }
        }

        private bool ValidateQuotationData()
        {
            // Check if project code exists
            if (string.IsNullOrEmpty(projectCode))
            {
                MessageBox.Show("Project code is missing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if company ID exists
            if (companyId == 0)
            {
                MessageBox.Show("Company information is missing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if there are items in the grid
            if (dgvPricing.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one product to the quotation.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if delivery terms is filled
            if (string.IsNullOrWhiteSpace(txtDeliveryterms.Text))
            {
                MessageBox.Show("Please enter delivery terms.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ✅ NEW: Validate that ABC Price > Proposal Price for all items
            foreach (DataGridViewRow row in dgvPricing.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal abcPrice = 0;
                    decimal proposalPrice = 0;

                    // Parse ABC Price
                    if (row.Cells["ABCPrice"].Value != null)
                        decimal.TryParse(row.Cells["ABCPrice"].Value.ToString(), out abcPrice);

                    // Parse Proposal Price
                    if (row.Cells["ProposalPrice"].Value != null)
                        decimal.TryParse(row.Cells["ProposalPrice"].Value.ToString(), out proposalPrice);

                    // Validate ABC > Proposal Price
                    if (abcPrice <= proposalPrice)
                    {
                        string itemName = row.Cells["Name"].Value?.ToString() ?? "Unknown Item";
                        MessageBox.Show(
                            $"Validation Error: ABC Price (₱{abcPrice:N2}) must be greater than Proposal Price (₱{proposalPrice:N2}) for item '{itemName}'.",
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return false;
                    }

                    // Also check if ABC Price is entered
                    if (abcPrice == 0)
                    {
                        string itemName = row.Cells["Name"].Value?.ToString() ?? "Unknown Item";
                        MessageBox.Show(
                            $"Please enter ABC Price for item '{itemName}'.",
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return false;
                    }
                }
            }

            return true;
        }

        private int SaveQuotation()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Generate quotation name in format RFQ-XXX
                    string quotationName = GenerateQuotationName(conn, transaction);

                    // Get customer_id, address_id, and proponent_id from project_list
                    int customerId = 0;
                    int addressId = 0;
                    int proponentId = 0;

                    string getProjectInfoQuery = @"
                        SELECT company_id, address_id, proponent_id 
                        FROM project_list 
                        WHERE project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(getProjectInfoQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerId = reader["company_id"] != DBNull.Value ? Convert.ToInt32(reader["company_id"]) : 0;
                                addressId = reader["address_id"] != DBNull.Value ? Convert.ToInt32(reader["address_id"]) : 0;
                                proponentId = reader["proponent_id"] != DBNull.Value ? Convert.ToInt32(reader["proponent_id"]) : 0;
                            }
                        }
                    }

                    // Parse total amount from label
                    decimal totalAmount = 0;
                    string totalAmountText = lblTotalBidPriceNumber.Text.Replace("₱", "").Replace(",", "").Trim();
                    decimal.TryParse(totalAmountText, out totalAmount);

                    // Insert into quotation table
                    string insertQuotationQuery = @"
                        INSERT INTO quotation 
                        (quotation_name, project_code, company_id, address_id, proponent_id, quotation_date, 
                        validity_period, delivery_time, payment, total_cost, bid_price, bid_percentage, status, remarks, created_by)
                        VALUES 
                        (@quotation_name, @project_code, @company_id, @address_id, @proponent_id, @quotation_date, 
                        @validity_period, @delivery_time, @payment, @total_cost, @bid_price, @bid_percentage, @status, @remarks, @created_by);
                        SELECT LAST_INSERT_ID();";

                    int quotationId = 0;
                    using (MySqlCommand cmd = new MySqlCommand(insertQuotationQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@quotation_name", quotationName);
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        cmd.Parameters.AddWithValue("@company_id", customerId);
                        cmd.Parameters.AddWithValue("@address_id", addressId);
                        cmd.Parameters.AddWithValue("@proponent_id", proponentId);
                        cmd.Parameters.AddWithValue("@quotation_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@validity_period", DateTime.Now.AddDays(7));
                        cmd.Parameters.AddWithValue("@payment", txtPaymentTerms.Text);
                        cmd.Parameters.AddWithValue("@delivery_time", txtDeliveryterms.Text);
                        cmd.Parameters.AddWithValue("@total_cost", lblTotalCostNumber.Text.Replace("₱", "").Replace(",", "").Trim());
                        cmd.Parameters.AddWithValue("@bid_price", totalAmount);
                        cmd.Parameters.AddWithValue("@bid_percentage", txtBidPercentage.Text);
                        cmd.Parameters.AddWithValue("@status", "Pending");
                        cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                        cmd.Parameters.AddWithValue("@created_by", currentUser);

                        quotationId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert quotation items
                    string insertItemQuery = @"
                        INSERT INTO quotation_items 
                        (quotation_id, sku_upc, pref_vendor, quantity, unit_price, abc, proposal_price, sub_total)
                        VALUES 
                        (@quotation_id, @sku_upc, @pref_vendor, @quantity, @unit_price, @abc, @proposal_price, @sub_total)";

                    foreach (DataGridViewRow row in dgvPricing.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            using (MySqlCommand cmd = new MySqlCommand(insertItemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@quotation_id", quotationId);
                                cmd.Parameters.AddWithValue("@sku_upc", row.Cells["ItemCode"].Value?.ToString() ?? string.Empty);
                                cmd.Parameters.AddWithValue("@pref_vendor", row.Cells["Supplier"].Value?.ToString() ?? string.Empty);

                                // ✅ FIX: Parse Quantity correctly
                                int quantity = 0;
                                if (row.Cells["Quantity"].Value != null)
                                    int.TryParse(row.Cells["Quantity"].Value.ToString(), out quantity);
                                cmd.Parameters.AddWithValue("@quantity", quantity);

                                // ✅ FIX: Parse unit_price from InternalPrice
                                decimal unitPrice = 0;
                                if (row.Cells["InternalPrice"].Value != null)
                                    decimal.TryParse(row.Cells["InternalPrice"].Value.ToString(), out unitPrice);
                                cmd.Parameters.AddWithValue("@unit_price", unitPrice);

                                // ✅ FIX: Parse abc from ABCPrice
                                decimal abc = 0;
                                if (row.Cells["ABCPrice"].Value != null)
                                    decimal.TryParse(row.Cells["ABCPrice"].Value.ToString(), out abc);
                                cmd.Parameters.AddWithValue("@abc", abc);

                                // ✅ FIX: Parse proposal_price from ProposalPrice (not InternalPrice!)
                                decimal proposalPrice = 0;
                                if (row.Cells["ProposalPrice"].Value != null)
                                    decimal.TryParse(row.Cells["ProposalPrice"].Value.ToString(), out proposalPrice);
                                cmd.Parameters.AddWithValue("@proposal_price", proposalPrice);

                                // ✅ FIX: Parse sub_total from TotalAmount
                                decimal subTotal = 0;
                                if (row.Cells["TotalAmount"].Value != null)
                                    decimal.TryParse(row.Cells["TotalAmount"].Value.ToString(), out subTotal);
                                cmd.Parameters.AddWithValue("@sub_total", subTotal);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();

                    MessageBox.Show(
                        $"Quotation saved successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return quotationId;
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show(
                        $"Error saving quotation: {ex.Message}",
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return 0;
                }
            }
        }

        private string GenerateQuotationName(MySqlConnection conn, MySqlTransaction transaction)
        {
            try
            {
                // Get the highest RFQ number
                string maxQuery = @"
                    SELECT MAX(CAST(SUBSTRING(quotation_name, 5) AS UNSIGNED)) 
                    FROM quotation 
                    WHERE quotation_name LIKE 'RFQ-%'";

                using (MySqlCommand cmd = new MySqlCommand(maxQuery, conn, transaction))
                {
                    object result = cmd.ExecuteScalar();
                    int nextNumber = (result == null || result == DBNull.Value) ? 1 : Convert.ToInt32(result) + 1;

                    // Format: RFQ-XXX (3-digit zero-padded)
                    return $"RFQ-{nextNumber:D3}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating quotation name: {ex.Message}");
                // Fallback to timestamp-based name
                return $"RFQ-{DateTime.Now:yyyyMMddHHmmss}";
            }
        }

        private void ExportToPdf(int quotationId)
        {
            // Setup save dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files|*.pdf";
            saveDialog.Title = "Export Quotation to PDF";
            saveDialog.FileName = $"Quotation_KMCI_{projectCode}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
            saveDialog.DefaultExt = "pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Generate PDF
                    KinglandQuotationPdfGenerator generator = new KinglandQuotationPdfGenerator();

                    // Optional: If you have a logo file
                    string logoPath = Path.Combine(Application.StartupPath, "logo.png");

                    generator.GenerateQuotationPdf(quotationId, saveDialog.FileName, logoPath);

                    // Ask if user wants to send email with the PDF
                    var emailResult = MessageBox.Show(
                        "PDF generated successfully! Would you like to send it via email to the client?",
                        "Send Email",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (emailResult == DialogResult.Yes)
                    {
                        // Send email with PDF attachment
                        bool emailSent = EmailSender.SendQuotationEmail(quotationId, saveDialog.FileName);
                        
                        if (emailSent)
                        {
                            MessageBox.Show("Quotation PDF has been emailed to the client successfully!", 
                                "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    // Ask if user wants to open the PDF
                    var openResult = MessageBox.Show(
                        "Do you want to open the PDF?",
                        "Open PDF",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (openResult == DialogResult.Yes)
                    {
                        var processStartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveDialog.FileName,
                            UseShellExecute = true
                        };
                        System.Diagnostics.Process.Start(processStartInfo);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Add this helper class at the class level
        private class SupplierInfo
        {
            public string VendorName { get; set; }
            public decimal BasePrice { get; set; }
        }

        // ✅ NEW: Numeric validation method for text boxes (allows only digits)
        private void TxtNumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and control keys
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Method to load suppliers from multiple rows in product_list
        private List<SupplierInfo> LoadSuppliersForProduct(string skuUpc)
        {
            List<SupplierInfo> suppliers = new List<SupplierInfo>();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT pref_vendor, base_price 
                        FROM product_list 
                        WHERE sku_upc = @sku_upc 
                        AND pref_vendor IS NOT NULL 
                        AND pref_vendor != ''
                        ORDER BY pref_vendor";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sku_upc", skuUpc);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string vendorName = reader["pref_vendor"]?.ToString();
                                decimal basePrice = reader["base_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["base_price"])
                                    : 0;

                                if (!string.IsNullOrEmpty(vendorName))
                                {
                                    suppliers.Add(new SupplierInfo
                                    {
                                        VendorName = vendorName.Trim(),
                                        BasePrice = basePrice
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading suppliers: {ex.Message}");
                }
            }

            return suppliers;
        }
    }
}
