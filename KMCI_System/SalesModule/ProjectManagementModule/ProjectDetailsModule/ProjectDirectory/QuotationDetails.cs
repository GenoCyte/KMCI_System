using MySql.Data.MySqlClient;

namespace KMCI_System.SalesModule
{
    public partial class QuotationDetails : UserControl
    {
        private int quotationId;
        private string quotationName;
        private string projectCode;

        // Company Info Components
        private Label lblHeader;
        private Label lblCompanyName;
        private TextBox txtCompanyName;
        private Label lblCompanyTin;
        private TextBox txtCompanyTin;

        // Proponent Info Components
        private Label lblProponentName;
        private TextBox txtProponentName;
        private Label lblProponentNumber;
        private TextBox txtProponentNumber;
        private Label lblProponentEmail;
        private TextBox txtProponentEmail;

        // Address Component
        private Label lblAddress;
        private TextBox txtAddress;

        // Quotation Info Components
        private Label lblBidPercentage;
        private TextBox txtBidPercentage;
        private Label lblBidPrice;
        private TextBox txtBidPrice;
        private Label lblTotalCost;
        private TextBox txtTotalCost;
        private Label lblDeliveryTime;
        private TextBox txtDeliveryTime;
        private Label lblPayment;
        private TextBox txtPayment;
        private Label lblStatus;
        private TextBox txtStatus;

        // Items Table Components
        private Label lblItemsHeader;
        private DataGridView dgvItems;
        private Label lblTotalAmount;
        private TextBox txtTotalAmount;

        private Button btnMarkAsApproved;
        private Button btnBack;

        public QuotationDetails(int quotationId, string quotationName, string projectCode)
        {
            InitializeComponent();
            this.quotationId = quotationId;
            this.quotationName = quotationName;
            this.projectCode = projectCode;

            this.AutoScroll = true;
            SetupUI();
            LoadQuotationDetails();
            LoadQuotationItems();
        }

        private void SetupUI()
        {
            int yPos = 20;
            int leftMargin = 30;

            // Header
            lblHeader = new Label
            {
                Text = "Quotation Details",
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

            // Company Section
            Label lblCompanySection = new Label
            {
                Text = "Company Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            Controls.Add(lblCompanySection);

            yPos += 35;

            // Company Name
            lblCompanyName = new Label
            {
                Text = "Company Name:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblCompanyName);

            txtCompanyName = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtCompanyName);

            // Company TIN (adjacent)
            lblCompanyTin = new Label
            {
                Text = "TIN:",
                Location = new Point(leftMargin + 530, yPos),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblCompanyTin);

            txtCompanyTin = new TextBox
            {
                Location = new Point(leftMargin + 730, yPos),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtCompanyTin);

            yPos += 40;

            // Address
            lblAddress = new Label
            {
                Text = "Address:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblAddress);

            txtAddress = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(800, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtAddress);

            yPos += 50;

            // Proponent Section
            Label lblProponentSection = new Label
            {
                Text = "Proponent Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            Controls.Add(lblProponentSection);

            yPos += 35;

            // Proponent Name
            lblProponentName = new Label
            {
                Text = "Proponent Name:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProponentName);

            txtProponentName = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtProponentName);

            // Proponent Email (adjacent)
            lblProponentEmail = new Label
            {
                Text = "Email:",
                Location = new Point(leftMargin + 530, yPos),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProponentEmail);

            txtProponentEmail = new TextBox
            {
                Location = new Point(leftMargin + 730, yPos),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtProponentEmail);

            yPos += 40;

            // Proponent Number
            lblProponentNumber = new Label
            {
                Text = "Contact Number:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProponentNumber);

            txtProponentNumber = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtProponentNumber);

            yPos += 50;

            // Quotation Info Section
            Label lblQuotationSection = new Label
            {
                Text = "Quotation Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            Controls.Add(lblQuotationSection);

            yPos += 35;

            // First Row - Bid Percentage and Bid Price
            lblBidPercentage = new Label
            {
                Text = "Bid Percentage:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblBidPercentage);

            txtBidPercentage = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtBidPercentage);

            lblBidPrice = new Label
            {
                Text = "Bid Price:",
                Location = new Point(leftMargin + 530, yPos),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblBidPrice);

            txtBidPrice = new TextBox
            {
                Location = new Point(leftMargin + 730, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtBidPrice);

            yPos += 40;

            // Second Row - Delivery Time and Payment
            lblDeliveryTime = new Label
            {
                Text = "Delivery Time:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblDeliveryTime);

            txtDeliveryTime = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtDeliveryTime);

            lblTotalCost = new Label
            {
                Text = "Total Cost:",
                Location = new Point(leftMargin + 530, yPos),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblTotalCost);

            txtTotalCost = new TextBox
            {
                Location = new Point(leftMargin + 730, yPos),
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
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblStatus);

            txtStatus = new TextBox
            {
                Location = new Point(leftMargin + 180, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtStatus);

            lblPayment = new Label
            {
                Text = "Payment Terms:",
                Location = new Point(leftMargin + 530, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblPayment);

            txtPayment = new TextBox
            {
                Location = new Point(leftMargin + 730, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtPayment);

            yPos += 60;

            // Items Header
            lblItemsHeader = new Label
            {
                Text = "Quotation Items",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            Controls.Add(lblItemsHeader);

            yPos += 40;

            // Items DataGridView
            dgvItems = new DataGridView
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
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvItems.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvItems.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvItems.DefaultCellStyle.BackColor = Color.White;
            dgvItems.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvItems.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvItems.GridColor = Color.FromArgb(220, 220, 220);
            dgvItems.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Add columns
            dgvItems.Columns.Add("SkuUpc", "SKU/UPC");
            dgvItems.Columns.Add("Name", "Product Name");
            dgvItems.Columns.Add("Brand", "Brand");
            dgvItems.Columns.Add("Qty", "Quantity");
            dgvItems.Columns.Add("UnitPrice", "Unit Price");
            dgvItems.Columns.Add("SubTotal", "Sub Total");

            // Set column widths
            dgvItems.Columns["SkuUpc"].Width = 120;
            dgvItems.Columns["Name"].Width = 250;
            dgvItems.Columns["Brand"].Width = 150;
            dgvItems.Columns["Qty"].Width = 100;
            dgvItems.Columns["UnitPrice"].Width = 150;
            dgvItems.Columns["SubTotal"].Width = 150;

            // Center align numeric columns
            dgvItems.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Controls.Add(dgvItems);

            yPos += 320;

            // Total Amount Label
            lblTotalAmount = new Label
            {
                Text = "Total Amount:",
                Location = new Point(leftMargin + 700, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(lblTotalAmount);

            // Total Amount TextBox
            txtTotalAmount = new TextBox
            {
                Location = new Point(leftMargin + 820, yPos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Text = "₱ 0.00",
                TextAlign = HorizontalAlignment.Right
            };
            Controls.Add(txtTotalAmount);

            yPos += 40;

            btnMarkAsApproved = new Button
            {
                Text = "Mark as Approved",
                Location = new Point(leftMargin + 870, yPos),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMarkAsApproved.Click += btnMarkAsApproved_Click;
            Controls.Add(btnMarkAsApproved);

            yPos += btnMarkAsApproved.Height + 10;

            Label lblSpace = new Label
            {
                Location = new Point(leftMargin + 750, yPos),
                Height = 20
            };
            Controls.Add(lblSpace);
        }

        private void LoadQuotationDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            q.bid_percentage,
                            q.bid_price,
                            q.total_cost,
                            q.delivery_time,
                            q.payment,
                            q.status,
                            c.company_name,
                            c.tin,
                            ca.house_num,
                            ca.street,
                            ca.subdivision,
                            ca.barangay,
                            ca.city,
                            ca.province,
                            ca.region,
                            p.proponent_name,
                            p.proponent_number,
                            p.proponent_email
                        FROM quotation q
                        LEFT JOIN company_list c ON q.company_id = c.id
                        LEFT JOIN company_address ca ON q.address_id = ca.id
                        LEFT JOIN proponents p ON q.proponent_id = p.id
                        WHERE q.quotation_id = @quotation_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quotation_id", quotationId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Company Information
                                txtCompanyName.Text = reader["company_name"]?.ToString() ?? "";
                                txtCompanyTin.Text = reader["tin"]?.ToString() ?? "";

                                // Concatenate address
                                List<string> addressParts = new List<string>();
                                if (reader["house_num"] != DBNull.Value && !string.IsNullOrEmpty(reader["house_num"].ToString()))
                                    addressParts.Add(reader["house_num"].ToString());
                                if (reader["street"] != DBNull.Value && !string.IsNullOrEmpty(reader["street"].ToString()))
                                    addressParts.Add(reader["street"].ToString());
                                if (reader["subdivision"] != DBNull.Value && !string.IsNullOrEmpty(reader["subdivision"].ToString()))
                                    addressParts.Add(reader["subdivision"].ToString());
                                if (reader["barangay"] != DBNull.Value && !string.IsNullOrEmpty(reader["barangay"].ToString()))
                                    addressParts.Add(reader["barangay"].ToString());
                                if (reader["city"] != DBNull.Value && !string.IsNullOrEmpty(reader["city"].ToString()))
                                    addressParts.Add(reader["city"].ToString());
                                if (reader["province"] != DBNull.Value && !string.IsNullOrEmpty(reader["province"].ToString()))
                                    addressParts.Add(reader["province"].ToString());

                                txtAddress.Text = string.Join(", ", addressParts);

                                // Proponent Information
                                txtProponentName.Text = reader["proponent_name"]?.ToString() ?? "";
                                txtProponentNumber.Text = reader["proponent_number"]?.ToString() ?? "";
                                txtProponentEmail.Text = reader["proponent_email"]?.ToString() ?? "";

                                // Quotation Information
                                decimal bidPercentage = reader["bid_percentage"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_percentage"]) : 0;
                                txtBidPercentage.Text = $"{bidPercentage}%";

                                decimal bidPrice = reader["bid_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["bid_price"]) : 0;
                                txtBidPrice.Text = $"₱ {bidPrice:N2}";

                                decimal totalCost = reader["total_cost"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["total_cost"]) : 0;
                                txtTotalCost.Text = $"₱ {totalCost:N2}";

                                txtDeliveryTime.Text = reader["delivery_time"]?.ToString() ?? "";
                                txtPayment.Text = reader["payment"]?.ToString() ?? "";

                                string status = reader["status"]?.ToString() ?? "Unknown";
                                txtStatus.Text = status;

                                // Apply status color
                                ApplyStatusColor(status);
                            }
                            else
                            {
                                MessageBox.Show("Quotation not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation details: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuotationItems()
        {
            try
            {
                dgvItems.Rows.Clear();
                decimal totalAmount = 0;

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            qi.sku_upc,
                            p.prod_name,
                            p.brand,
                            qi.quantity,
                            qi.unit_price,
                            qi.sub_total
                        FROM quotation_items qi
                        LEFT JOIN product_list p ON qi.sku_upc = p.sku_upc
                        WHERE qi.quotation_id = @quotation_id
                        GROUP BY qi.sku_upc, qi.quantity, qi.unit_price, qi.sub_total, p.prod_name, p.brand
                        ORDER BY qi.item_id AND p.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@quotation_id", quotationId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string skuUpc = reader["sku_upc"]?.ToString() ?? "";
                                string name = reader["prod_name"]?.ToString() ?? "";
                                string brand = reader["brand"]?.ToString() ?? "";
                                int qty = reader["quantity"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal unitPrice = reader["unit_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["unit_price"]) : 0;
                                decimal subTotal = reader["sub_total"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["sub_total"]) : 0;

                                dgvItems.Rows.Add(
                                    skuUpc,
                                    name,
                                    brand,
                                    qty,
                                    $"₱ {unitPrice:N2}",
                                    $"₱ {subTotal:N2}"
                                );

                                totalAmount += subTotal;
                            }
                        }
                    }
                }

                txtTotalAmount.Text = $"₱ {totalAmount:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotation items: {ex.Message}", "Database Error",
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
                case "pending":
                case "for approval":
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

        private void btnMarkAsApproved_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE quotation
                        SET status = 'Approved'
                        WHERE quotation_id = @quotation_id";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@quotation_id", quotationId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Quotation marked as Approved.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatus.Text = "Approved";
                            ApplyStatusColor("Approved");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update quotation status.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating quotation status: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}