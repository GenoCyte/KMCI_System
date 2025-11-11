using MySql.Data.MySqlClient;

namespace KMCI_System.InventoryModule.InventoryManagementModule
{
    public partial class InventoryDetails : UserControl
    {
        private int ypos;
        private UserControl currentUserControl;
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

        private DataGridView dgvQuotationItems;

        // Items Table Components
        private Label lblItemsHeader;
        private Label lblGrandTotal;
        private TextBox txtGrandTotal;
        private decimal grandTotal = 0;

        public InventoryDetails(string projectCode)
        {
            this.projectCode = projectCode;
            InitializeComponent();
            SetupUI();
            LoadQuotationDetails();
            LoadQuotationItems(); // Add this line to load the items
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new InventoryManagement());
        }

        private void LoadUserControl(UserControl userControl)
        {
            var inventoryForm = this.FindForm() as InventoryForm;
            // Clear existing controls in panel
            inventoryForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            inventoryForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupUI()
        {
            int yPos = 20;
            int leftMargin = 30;

            // Header
            lblHeader = new Label
            {
                Text = "Item Delivery Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            Controls.Add(lblHeader);

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
                Text = "Quotation Items by Vendor",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            Controls.Add(lblItemsHeader);

            ypos = yPos + 40; // Store position for dynamic content

            // Quotation Items DataGridView
            dgvQuotationItems = new DataGridView
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
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvQuotationItems.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvQuotationItems.DefaultCellStyle.BackColor = Color.White;
            dgvQuotationItems.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvQuotationItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvQuotationItems.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvQuotationItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvQuotationItems.GridColor = Color.FromArgb(220, 220, 220);
            dgvQuotationItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvQuotationItems.Columns.Add("VendorName", "Vendor Name");
            dgvQuotationItems.Columns.Add("TotalItems", "Total Items");
            dgvQuotationItems.Columns.Add("SubTotal", "Sub Total");

            // Set column widths and alignment
            dgvQuotationItems.Columns["VendorName"].FillWeight = 50;
            dgvQuotationItems.Columns["TotalItems"].FillWeight = 20;
            dgvQuotationItems.Columns["TotalItems"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuotationItems.Columns["SubTotal"].FillWeight = 30;
            dgvQuotationItems.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvQuotationItems.CellClick += dgvQuotationItems_CellClick;
            dgvQuotationItems.RowsAdded += (s, e) => AdjustAddressGridHeight();
            dgvQuotationItems.RowsRemoved += (s, e) => AdjustAddressGridHeight();

            Controls.Add(dgvQuotationItems);

            ypos += dgvQuotationItems.Height + 10;

            lblGrandTotal = new Label
            {
                Text = "Grand Total:",
                Location = new Point(700, ypos),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblGrandTotal);

            txtGrandTotal = new TextBox
            {
                Location = new Point(850, ypos),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ReadOnly = true,
                Text = "₱ " + grandTotal,
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = HorizontalAlignment.Right
            };
            Controls.Add(txtGrandTotal);
        }

        private void dgvQuotationItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String vendorName = dgvQuotationItems.Rows[e.RowIndex].Cells["VendorName"].Value.ToString();
                // ✅ FIXED: Explicitly use PurchasingModule's ProjectOverview
                LoadUserControl(new UpdateInventory());
            }
        }

        private void AdjustAddressGridHeight()
        {
            if (dgvQuotationItems.Rows.Count == 0)
            {
                dgvQuotationItems.Height = dgvQuotationItems.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvQuotationItems.ColumnHeadersHeight;
            int maxVisibleRows = 4;
            int rowsToCount = Math.Min(dgvQuotationItems.Rows.Count, maxVisibleRows);

            for (int i = 0; i < rowsToCount; i++)
            {
                totalHeight += dgvQuotationItems.Rows[i].Height;
            }

            totalHeight += 2;
            dgvQuotationItems.Height = totalHeight;

            // Adjust proponents section position
            int newYPosition = dgvQuotationItems.Location.Y + dgvQuotationItems.Height + 20;
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
                            WHERE q.project_code = @project_code AND q.status = 'Approved'";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

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
                dgvQuotationItems.Rows.Clear();
                grandTotal = 0; // Reset grand total before calculating

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Query to get vendor-grouped data from quotation_items for this project
                    string query = @"
                        SELECT 
                            po.vendor_name,
                            SUM(po.quantity) as total_quantity,
                            SUM(po.grand_total) as vendor_subtotal
                        FROM purchase_order po
                        INNER JOIN purchase_order_items poi ON po.id = poi.po_id
                        WHERE po.project_code = @project_code
                        GROUP BY po.vendor_name
                        ORDER BY po.vendor_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string vendorName = reader["vendor_name"]?.ToString() ?? "N/A";
                                int totalItems = reader["total_quantity"] != DBNull.Value
                                    ? Convert.ToInt32(reader["total_quantity"]) : 0;
                                decimal subTotal = reader["vendor_subtotal"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["vendor_subtotal"]) : 0;

                                dgvQuotationItems.Rows.Add(
                                    vendorName,
                                    totalItems,
                                    $"₱ {subTotal:N2}"
                                );
                                grandTotal += subTotal;
                            }
                        }
                    }
                }

                // Update the grand total textbox AFTER calculating
                txtGrandTotal.Text = $"₱ {grandTotal:N2}";

                dgvQuotationItems.ClearSelection();
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

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}
