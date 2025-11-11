using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory
{
    public partial class PurchaseRequestDetails2 : UserControl
    {
        private UserControl currentUserControl;
        private int ypos = 20;
        private string vendorName;
        private string projectCode;
        private Label lblHeader;
        private Label lblVendorHeader;
        private Label lblVendorName;
        private Label lblVendorAddress;
        private Label lblVendorPhone;
        private Label lblVendorEmail;
        private Label lblVendorPerson;
        private TextBox txtVendorName;
        private TextBox txtVendorAddress;
        private TextBox txtVendorPhone;
        private TextBox txtVendorEmail;
        private TextBox txtVendorPerson;
        private Label lblItemsHeader;
        private DataGridView dgvItems;
        private Button btnCreatePr;

        public PurchaseRequestDetails2(string vendorName, string projectCode)
        {
            this.vendorName = vendorName;
            this.projectCode = projectCode;
            InitializeComponent();
            SetupUI();
            LoadVendorInformation();
            LoadVendorItems();
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
                Text = "Create Purchase Request",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            ypos += 60;

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
                Text = vendorName,
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

            lblItemsHeader = new Label
            {
                Text = "Items to Purchase",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
            };
            this.Controls.Add(lblItemsHeader);

            ypos += 40;

            // Quotation Items DataGridView
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

            dgvItems.RowsAdded += (s, e) => AdjustAddressGridHeight();
            dgvItems.RowsRemoved += (s, e) => AdjustAddressGridHeight();

            Controls.Add(dgvItems);

            ypos += dgvItems.Height + 20;

            AdjustAddressGridHeight();
        }

        private void LoadVendorInformation()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Query to get vendor information based on vendor name
                    string query = @"
                        SELECT DISTINCT
                            v.vendor_name,
                            v.vendor_person,
                            v.vendor_phone,
                            v.vendor_email,
                            v.vendor_address
                        FROM vendor_list v
                        WHERE v.vendor_name = @vendor_name
                        LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vendor_name", vendorName);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtVendorName.Text = reader["vendor_name"]?.ToString() ?? vendorName;
                                txtVendorPerson.Text = reader["vendor_person"]?.ToString() ?? "";
                                txtVendorPhone.Text = reader["vendor_phone"]?.ToString() ?? "";
                                txtVendorEmail.Text = reader["vendor_email"]?.ToString() ?? "";
                                txtVendorAddress.Text = reader["vendor_address"]?.ToString() ?? "";
                            }
                            else
                            {
                                // If vendor not found in vendors table, keep the vendor name
                                txtVendorName.Text = vendorName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vendor information: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVendorItems()
        {
            try
            {
                dgvItems.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Query to get items for this vendor from quotation_items
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
                        LEFT JOIN quotation q ON qi.quotation_id = q.quotation_id  
                        WHERE qi.pref_vendor = @vendor_name 
                        AND q.status = 'Approved'";

                    // Add project_code filter if available
                    if (!string.IsNullOrEmpty(projectCode))
                    {
                        query += " AND q.project_code = @project_code";
                    }

                    query += " ORDER BY p.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vendor_name", vendorName);

                        if (!string.IsNullOrEmpty(projectCode))
                        {
                            cmd.Parameters.AddWithValue("@project_code", projectCode);
                        }

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
                    MessageBox.Show("No items found for this vendor.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vendor items: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdjustAddressGridHeight()
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

            // Adjust proponents section position
            int newYPosition = dgvItems.Location.Y + dgvItems.Height + 20;
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}
