using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory
{
    public partial class PurchaseOrderDetails : UserControl
    {
        private UserControl currentUserControl;
        private int ypos = 20;
        private int poId;
        private string projectCode;
        private Label lblHeader;
        private Label lblPoInfoHeader;
        private Label lblPoName;
        private Label lblPoDate;
        private Label lblVendorHeader;
        private Label lblVendorName;
        private Label lblVendorAddress;
        private Label lblVendorPhone;
        private Label lblVendorEmail;
        private Label lblVendorPerson;
        private Label lblStatus;
        private TextBox txtPoName;
        private TextBox txtPoDate;
        private TextBox txtVendorName;
        private TextBox txtVendorAddress;
        private TextBox txtVendorPhone;
        private TextBox txtVendorEmail;
        private TextBox txtVendorPerson;
        private TextBox txtStatus;
        private Label lblItemsHeader;
        private DataGridView dgvItems;
        private Label lblTotalQuantity;
        private Label lblGrandTotal;
        private TextBox txtTotalQuantity;
        private TextBox txtGrandTotal;

        public PurchaseOrderDetails(int poId, string projectCode)
        {
            this.poId = poId;
            this.projectCode = projectCode;
            SetupUI();
            LoadPurchaseOrderInformation();
            LoadPurchaseOrderItems();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ProjectDirectory(projectCode));
        }

        private void LoadUserControl(UserControl userControl)
        {
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

        private void SetupUI()
        {
            // Add Back Button at the top
            Button btnBack = new Button
            {
                Text = "Back",
                Location = new Point(1000, ypos),
                Size = new Size(75, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.Click += btnBack_Click;
            this.Controls.Add(btnBack);

            lblHeader = new Label
            {
                Text = "Purchase Order Details",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            ypos += 60;

            // Purchase Order Information Section
            lblPoInfoHeader = new Label
            {
                Text = "Purchase Order Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
            };
            this.Controls.Add(lblPoInfoHeader);

            ypos += 40;

            lblPoName = new Label
            {
                Text = "PO Name:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblPoName);

            txtPoName = new TextBox
            {
                Location = new Point(150, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtPoName);

            lblPoDate = new Label
            {
                Text = "PO Date:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(550, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblPoDate);

            txtPoDate = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtPoDate);

            ypos += 40;

            lblStatus = new Label
            {
                Text = "Status:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblStatus);

            txtStatus = new TextBox
            {
                Location = new Point(150, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtStatus);

            ypos += 60;

            // Vendor Information Section
            lblVendorHeader = new Label
            {
                Text = "Vendor Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
            };
            this.Controls.Add(lblVendorHeader);

            ypos += 40;

            lblVendorName = new Label
            {
                Text = "Vendor Name:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorName);

            txtVendorName = new TextBox
            {
                Location = new Point(150, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorName);

            lblVendorEmail = new Label
            {
                Text = "Vendor Email:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(550, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorEmail);

            txtVendorEmail = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorEmail);

            ypos += 40;

            lblVendorPerson = new Label
            {
                Text = "Contact Person:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorPerson);

            txtVendorPerson = new TextBox
            {
                Location = new Point(150, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorPerson);

            lblVendorPhone = new Label
            {
                Text = "Vendor Phone:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(550, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorPhone);

            txtVendorPhone = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Width = 200,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorPhone);

            ypos += 40;

            lblVendorAddress = new Label
            {
                Text = "Vendor Address:",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorAddress);

            txtVendorAddress = new TextBox
            {
                Location = new Point(150, ypos - 3),
                Width = 730,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorAddress);

            ypos += 60;

            // Items Section
            lblItemsHeader = new Label
            {
                Text = "Purchase Order Items",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
            };
            this.Controls.Add(lblItemsHeader);

            ypos += 40;

            // Items DataGridView
            dgvItems = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1020,
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

            dgvItems.RowsAdded += (s, e) => AdjustItemsGridHeight();
            dgvItems.RowsRemoved += (s, e) => AdjustItemsGridHeight();

            Controls.Add(dgvItems);

            ypos += dgvItems.Height + 20;

            // Total Section
            lblTotalQuantity = new Label
            {
                Text = "Total Quantity:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(640, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblTotalQuantity);

            txtTotalQuantity = new TextBox
            {
                Location = new Point(770, ypos - 3),
                Width = 120,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(txtTotalQuantity);

            ypos += 40;

            lblGrandTotal = new Label
            {
                Text = "Grand Total:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(640, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblGrandTotal);

            txtGrandTotal = new TextBox
            {
                Location = new Point(770, ypos - 3),
                Width = 270,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(txtGrandTotal);

            ypos += 60;

            AdjustItemsGridHeight();
        }

        private void LoadPurchaseOrderInformation()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Query to get purchase order and vendor information
                    string query = @"
   SELECT 
             po.po_name,
           po.vendor_name,
                 po.quantity,
      po.grand_total,
         po.po_date,
         po.status,
            v.vendor_person,
          v.vendor_phone,
              v.vendor_email,
        v.vendor_address
       FROM purchase_order po
   LEFT JOIN vendor_list v ON po.vendor_name = v.vendor_name
       WHERE po.id = @po_id
          LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@po_id", poId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // PO Information
                                txtPoName.Text = reader["po_name"]?.ToString() ?? "";
                                txtPoDate.Text = reader["po_date"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["po_date"]).ToString("yyyy-MM-dd") : "";
                                txtStatus.Text = reader["status"]?.ToString() ?? "Pending";

                                // Vendor Information
                                txtVendorName.Text = reader["vendor_name"]?.ToString() ?? "";
                                txtVendorPerson.Text = reader["vendor_person"]?.ToString() ?? "";
                                txtVendorPhone.Text = reader["vendor_phone"]?.ToString() ?? "";
                                txtVendorEmail.Text = reader["vendor_email"]?.ToString() ?? "";
                                txtVendorAddress.Text = reader["vendor_address"]?.ToString() ?? "";

                                // Totals
                                int totalQuantity = reader["quantity"] != DBNull.Value
                            ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal grandTotal = reader["grand_total"] != DBNull.Value
                    ? Convert.ToDecimal(reader["grand_total"]) : 0;

                                txtTotalQuantity.Text = totalQuantity.ToString();
                                txtGrandTotal.Text = $"₱ {grandTotal:N2}";

                                // Apply status color
                                ApplyStatusColor(txtStatus.Text);
                            }
                            else
                            {
                                MessageBox.Show("Purchase Order not found.", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase order information: {ex.Message}", "Database Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseOrderItems()
        {
            try
            {
                dgvItems.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Query to get items for this purchase order
                    string query = @"
SELECT 
      poi.sku_upc,
           p.prod_name,
        p.brand,
            poi.quantity,
           poi.unit_price,
      poi.sub_total
    FROM purchase_order_items poi
       LEFT JOIN product_list p ON poi.sku_upc = p.sku_upc
          WHERE poi.po_id = @po_id
ORDER BY p.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@po_id", poId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string skuUpc = reader["sku_upc"]?.ToString() ?? "";
                                string productName = reader["prod_name"]?.ToString() ?? "";
                                string brand = reader["brand"]?.ToString() ?? "";
                                int quantity = reader["quantity"] != DBNull.Value
                                        ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal unitPrice = reader["unit_price"] != DBNull.Value
                                      ? Convert.ToDecimal(reader["unit_price"]) : 0;
                                decimal subTotal = reader["sub_total"] != DBNull.Value
                                 ? Convert.ToDecimal(reader["sub_total"]) : 0;

                                dgvItems.Rows.Add(
                              skuUpc,
                                       productName,
                                  brand,
                                quantity,
                                  $"₱ {unitPrice:N2}",
                              $"₱ {subTotal:N2}"
                                          );
                            }
                        }
                    }
                }

                dgvItems.ClearSelection();

                if (dgvItems.Rows.Count == 0)
                {
                    MessageBox.Show("No items found for this purchase order.", "Information",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase order items: {ex.Message}", "Database Error",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdjustItemsGridHeight()
        {
            if (dgvItems.Rows.Count == 0)
            {
                dgvItems.Height = dgvItems.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvItems.ColumnHeadersHeight;
            int maxVisibleRows = 4;
            int rowsToCount = Math.Min(dgvItems.Rows.Count, maxVisibleRows);

            for (int i = 0; i < rowsToCount; i++)
            {
                totalHeight += dgvItems.Rows[i].Height;
            }

            totalHeight += 2;
            dgvItems.Height = totalHeight;
        }

        private void ApplyStatusColor(string status)
        {
            Color statusColor;

            switch (status.ToLower())
            {
                case "approved":
                case "completed":
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
            txtStatus.Font = new Font(txtStatus.Font, FontStyle.Bold);
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}
