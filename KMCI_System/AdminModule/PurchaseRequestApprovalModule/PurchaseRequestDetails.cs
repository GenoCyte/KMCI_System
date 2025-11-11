using KMCI_System.Login; // ✅ Add to access Session class
using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.PurchaseRequestApprovalModule
{
    public partial class PurchaseRequestDetails : UserControl
    {
        private UserControl currentUserControl;
        private int ypos = 20;
        private int prId;
        private string prName;
        private string projectCode;
        private string vendorName;

        // UI Components
        private Label lblHeader;
        private Label lblRequestedBy;
        private TextBox txtRequestedBy;
        private Label lblProjectCode;
        private TextBox txtProjectCode;

        // Vendor Information Section
        private Label lblVendorHeader;
        private Label lblVendorName;
        private TextBox txtVendorName;
        private Label lblVendorAddress;
        private TextBox txtVendorAddress;
        private Label lblVendorPhone;
        private TextBox txtVendorPhone;
        private Label lblVendorEmail;
        private TextBox txtVendorEmail;
        private Label lblVendorPerson;
        private TextBox txtVendorPerson;

        // Product List Section
        private Label lblItemsHeader;
        private DataGridView dgvItems;
        private Label lblGrandTotal;
        private TextBox txtGrandTotal;

        // Action Buttons
        private Button btnApprove;
        private Button btnReject;
        private Button btnBack;

        public PurchaseRequestDetails(int prId)
        {
            this.prId = prId;
            this.AutoScroll = true;
            InitializeComponent();
            SetupUI();
            LoadPurchaseRequestDetails();
            LoadVendorInformation();
            LoadPurchaseRequestItems();
        }

        private void SetupUI()
        {
            // Back Button
            btnBack = new Button
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

            // Header
            lblHeader = new Label
            {
                Text = "Purchase Request Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            ypos += 60;

            // Requested By
            lblRequestedBy = new Label
            {
                Text = "Requested By:",
                Location = new Point(20, ypos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lblRequestedBy);

            txtRequestedBy = new TextBox
            {
                Location = new Point(180, ypos - 3),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtRequestedBy);

            // Project Code
            lblProjectCode = new Label
            {
                Text = "Project Code:",
                Location = new Point(550, ypos),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lblProjectCode);

            txtProjectCode = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtProjectCode);

            ypos += 60;

            // Vendor Information Section Header
            lblVendorHeader = new Label
            {
                Text = "Vendor Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            this.Controls.Add(lblVendorHeader);

            ypos += 40;

            // Vendor Name
            lblVendorName = new Label
            {
                Text = "Vendor Name:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorName);

            txtVendorName = new TextBox
            {
                Location = new Point(180, ypos - 3),
                Width = 300,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorName);

            // Vendor Email
            lblVendorEmail = new Label
            {
                Text = "Email:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(550, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorEmail);

            txtVendorEmail = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Width = 250,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorEmail);

            ypos += 40;

            // Contact Person
            lblVendorPerson = new Label
            {
                Text = "Contact Person:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorPerson);

            txtVendorPerson = new TextBox
            {
                Location = new Point(180, ypos - 3),
                Width = 300,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorPerson);

            // Vendor Phone
            lblVendorPhone = new Label
            {
                Text = "Phone:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(550, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorPhone);

            txtVendorPhone = new TextBox
            {
                Location = new Point(680, ypos - 3),
                Width = 250,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorPhone);

            ypos += 40;

            // Vendor Address
            lblVendorAddress = new Label
            {
                Text = "Address:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblVendorAddress);

            txtVendorAddress = new TextBox
            {
                Location = new Point(180, ypos - 3),
                Width = 750,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(txtVendorAddress);

            ypos += 60;

            // Items Section Header
            lblItemsHeader = new Label
            {
                Text = "Product List",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            this.Controls.Add(lblItemsHeader);

            ypos += 40;

            // Product List DataGridView
            dgvItems = new DataGridView
            {
                Location = new Point(20, ypos),
                Width = 1050,
                Height = 250,
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
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
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
            dgvItems.Columns.Add("ProductName", "Product Name");
            dgvItems.Columns.Add("Brand", "Brand");
            dgvItems.Columns.Add("Quantity", "Quantity");
            dgvItems.Columns.Add("BasePrice", "Base Price");
            dgvItems.Columns.Add("SubTotal", "Sub Total");

            // Set column widths and alignment
            dgvItems.Columns["ProductName"].FillWeight = 35;
            dgvItems.Columns["Brand"].FillWeight = 20;
            dgvItems.Columns["Quantity"].FillWeight = 15;
            dgvItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns["BasePrice"].FillWeight = 15;
            dgvItems.Columns["BasePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns["SubTotal"].FillWeight = 15;
            dgvItems.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvItems.RowsAdded += (s, e) => AdjustItemsGridHeight();
            dgvItems.RowsRemoved += (s, e) => AdjustItemsGridHeight();

            this.Controls.Add(dgvItems);

            ypos += dgvItems.Height + 20;

            // Grand Total
            lblGrandTotal = new Label
            {
                Text = "Grand Total:",
                Location = new Point(800, ypos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            this.Controls.Add(lblGrandTotal);

            txtGrandTotal = new TextBox
            {
                Location = new Point(900, ypos),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(txtGrandTotal);

            ypos += 60;

            // Action Buttons
            btnApprove = new Button
            {
                Text = "Approve",
                Location = new Point(800, ypos),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnApprove.Click += btnApprove_Click;
            this.Controls.Add(btnApprove);

            btnReject = new Button
            {
                Text = "Reject",
                Location = new Point(930, ypos),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnReject.Click += btnReject_Click;
            this.Controls.Add(btnReject);

            AdjustItemsGridHeight();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PurchaseRequestManagement());
        }

        private void LoadUserControl(UserControl userControl)
        {
            var adminForm = this.FindForm() as AdminForm;
            if (adminForm == null) return;

            adminForm.panel1.Controls.Clear();

            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill;
            adminForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void LoadPurchaseRequestDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            pr.pr_name,
                            pr.project_code,
                            pr.vendor_name,
                            pr.grand_total,
                            pr.status
                        FROM purchase_request pr
                        WHERE pr.id = @pr_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pr_id", prId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                prName = reader["pr_name"]?.ToString() ?? "";
                                projectCode = reader["project_code"]?.ToString() ?? "";
                                vendorName = reader["vendor_name"]?.ToString() ?? "";

                                // ✅ Use logged-in user's name from Session instead of hardcoded value
                                txtRequestedBy.Text = !string.IsNullOrEmpty(Session.CurrentUserName)
     ? Session.CurrentUserName
  : "Unknown User";

                                txtProjectCode.Text = projectCode;

                                decimal grandTotal = reader["grand_total"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["grand_total"]) : 0;
                                txtGrandTotal.Text = $"₱ {grandTotal:N2}";

                                // Update header with PR name
                                lblHeader.Text = $"Purchase Request Details - {prName}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase request details: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVendorInformation()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
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
                                txtVendorName.Text = reader["vendor_name"]?.ToString() ?? "";
                                txtVendorPerson.Text = reader["vendor_person"]?.ToString() ?? "";
                                txtVendorPhone.Text = reader["vendor_phone"]?.ToString() ?? "";
                                txtVendorEmail.Text = reader["vendor_email"]?.ToString() ?? "";
                                txtVendorAddress.Text = reader["vendor_address"]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vendor information: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseRequestItems()
        {
            try
            {
                dgvItems.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            pri.prod_name,
                            pri.brand,
                            pri.quantity,
                            pri.unit_price,
                            pri.sub_total
                        FROM purchase_request_items pri
                        WHERE pri.pr_id = @pr_id
                        ORDER BY pri.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pr_id", prId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["prod_name"]?.ToString() ?? "";
                                string brand = reader["brand"]?.ToString() ?? "";
                                int quantity = reader["quantity"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal unitPrice = reader["unit_price"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["unit_price"]) : 0;
                                decimal subTotal = reader["sub_total"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["sub_total"]) : 0;

                                dgvItems.Rows.Add(
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase request items: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            int maxVisibleRows = 6;
            int rowsToCount = Math.Min(dgvItems.Rows.Count, maxVisibleRows);

            for (int i = 0; i < rowsToCount; i++)
            {
                totalHeight += dgvItems.Rows[i].Height;
            }

            totalHeight += 2;
            dgvItems.Height = totalHeight;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to approve Purchase Request {prName}?",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UpdatePurchaseRequestStatus("Approved");
                UpdateProductInventory();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to reject Purchase Request {prName}?",
                "Confirm Rejection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                UpdatePurchaseRequestStatus("Rejected");
            }
        }

        private void UpdatePurchaseRequestStatus(string status)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        UPDATE purchase_request 
                        SET status = @status 
                        WHERE id = @pr_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@pr_id", prId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show(
                                $"Purchase Request {prName} has been {status.ToLower()} successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoadUserControl(new PurchaseRequestManagement());
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to update purchase request status.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error updating purchase request status: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateProductInventory()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Get all items from the purchase request
                    string selectQuery = @"
                        SELECT prod_name, quantity
                        FROM purchase_request_items
                        WHERE pr_id = @pr_id";

                    using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@pr_id", prId);

                        using (MySqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            List<(string prodName, int quantity)> items = new List<(string, int)>();

                            while (reader.Read())
                            {
                                string prodName = reader["prod_name"]?.ToString() ?? "";
                                int quantity = reader["quantity"] != DBNull.Value
                                    ? Convert.ToInt32(reader["quantity"]) : 0;

                                items.Add((prodName, quantity));
                            }

                            reader.Close();

                            // Update inventory for each product
                            foreach (var item in items)
                            {
                                string updateQuery = @"
                                    UPDATE product_list 
                                    SET incoming = incoming + @quantity 
                                    WHERE prod_name = @prod_name";

                                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@quantity", item.quantity);
                                    updateCmd.Parameters.AddWithValue("@prod_name", item.prodName);

                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error updating product inventory: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}