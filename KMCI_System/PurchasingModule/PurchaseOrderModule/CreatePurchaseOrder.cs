using KMCI_System.PurchasingModule.PurchaseRequestModule;
using KMCI_System.Login;
using MySql.Data.MySqlClient;

namespace KMCI_System.PurchasingModule.PurchaseOrderModule
{
    public partial class CreatePurchaseOrder : UserControl
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
        private string currentUser = Session.CurrentUserName;
        private string poNumber;

        public CreatePurchaseOrder(string vendorName, string prName, string projectCode)
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
            LoadUserControl(new PurchaseOrderList(projectCode));
        }

        private void LoadUserControl(UserControl userControl)
        {
            var salesForm = this.FindForm() as PurchasingForm;
            // Clear existing controls in panel
            salesForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            salesForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupUI()
        {
            // Add Back Button at the top
            Button btnBack = new Button
            {
                Text = "Back",
                Location = new Point(1050, ypos),
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
                Text = "Create Purchase Order",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            ypos += 60;

            lblVendorHeader = new Label
            {
                Text = "Supplier Information",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, ypos),
                AutoSize = true,
            };
            this.Controls.Add(lblVendorHeader);

            ypos += 40;

            lblVendorName = new Label
            {
                Text = "Supplier Name:",
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
                Text = "Supplier Email:",
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
                Text = "Contact Number:",
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
                Text = "Supplier Address:",
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

            btnCreatePr = new Button
            {
                Text = "Create Purchase Order",
                Location = new Point(900, ypos),
                Size = new Size(200, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCreatePr.Click += btnCreatePr_Click;
            this.Controls.Add(btnCreatePr);

            AdjustAddressGridHeight();
        }

        private bool IsPurchaseRequestAlreadyCreated(MySqlConnection conn, int vendorId, int? quotationId)
        {
            string checkQuery = @"
                SELECT COUNT(*) 
                FROM purchase_order 
                WHERE vendor_id = @vendor_id";

            // If quotation_id is available, check for that specific combination
            if (quotationId.HasValue)
            {
                checkQuery += " AND quotation_id = @quotation_id";
            }
            else
            {
                // If no quotation_id, check if there's any PR for this vendor without a quotation
                checkQuery += " AND quotation_id IS NULL";
            }

            using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@vendor_id", vendorId);

                if (quotationId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@quotation_id", quotationId.Value);
                }

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void btnCreatePr_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.Rows.Count == 0)
                {
                    MessageBox.Show("No items available to create a purchase order.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm before creating
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to create this Purchase Order?",
                    "Confirm Purchase Order",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                int purchaseOrderId = 0;
                poNumber = string.Empty;

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Get vendor_id
                        int vendorId = 0;
                        string getVendorIdQuery = "SELECT id FROM vendor_list WHERE vendor_name = @vendor_name LIMIT 1";
                        using (MySqlCommand cmd = new MySqlCommand(getVendorIdQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                            object vendorIdResult = cmd.ExecuteScalar();

                            if (vendorIdResult == null)
                            {
                                MessageBox.Show("Vendor not found in the database.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            vendorId = Convert.ToInt32(vendorIdResult);
                        }

                        // Get quotation_id
                        int? quotationId = null;
                        string getQuotationIdQuery = @"
                            SELECT q.quotation_id 
                            FROM quotation q
                            INNER JOIN quotation_items qi ON q.quotation_id = qi.quotation_id
                            WHERE qi.pref_vendor = @vendor_name 
                            AND q.status = 'Approved'";

                        if (!string.IsNullOrEmpty(projectCode))
                        {
                            getQuotationIdQuery += " AND q.project_code = @project_code";
                        }

                        getQuotationIdQuery += " LIMIT 1";

                        using (MySqlCommand cmd = new MySqlCommand(getQuotationIdQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@vendor_name", vendorName);

                            if (!string.IsNullOrEmpty(projectCode))
                            {
                                cmd.Parameters.AddWithValue("@project_code", projectCode);
                            }

                            object quotationIdResult = cmd.ExecuteScalar();
                            if (quotationIdResult != null && quotationIdResult != DBNull.Value)
                            {
                                quotationId = Convert.ToInt32(quotationIdResult);
                            }
                        }

                        // Get pr_id
                        int prId = 0;
                        string getPrIdQuery = "SELECT id FROM purchase_request WHERE vendor_name = @vendor_name AND project_code = @project_code LIMIT 1";
                        using (MySqlCommand cmd = new MySqlCommand(getPrIdQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                            cmd.Parameters.AddWithValue("@project_code", projectCode);
                            object prIdResult = cmd.ExecuteScalar();

                            if (prIdResult == null)
                            {
                                MessageBox.Show("Purchase Request not found in the database.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            prId = Convert.ToInt32(prIdResult);
                        }

                        // Validate if purchase order already exists
                        if (IsPurchaseRequestAlreadyCreated(conn, vendorId, quotationId))
                        {
                            string message = quotationId.HasValue
                                ? $"A Purchase Order has already been created for vendor '{vendorName}' with this quotation."
                                : $"A Purchase Order has already been created for vendor '{vendorName}'.";

                            MessageBox.Show(message, "Duplicate Purchase Order",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        // Generate PO Number
                        poNumber = GeneratePOName(conn, transaction);

                        // Calculate total quantity and grand total from DataGridView
                        int totalQuantity = 0;
                        decimal grandTotal = 0;

                        foreach (DataGridViewRow row in dgvItems.Rows)
                        {
                            if (row.Cells["Qty"].Value != null)
                            {
                                totalQuantity += Convert.ToInt32(row.Cells["Qty"].Value);
                            }

                            if (row.Cells["SubTotal"].Value != null)
                            {
                                string subTotalStr = row.Cells["SubTotal"].Value.ToString()
                                    .Replace("₱", "").Replace(",", "").Trim();
                                grandTotal += decimal.Parse(subTotalStr);
                            }
                        }

                        // Insert into purchase_request table with pr_name
                        string insertQuery = @"
                            INSERT INTO purchase_order (project_code, po_name, vendor_id, quotation_id, po_date, vendor_name, quantity, grand_total, status, created_by)
                            VALUES (@project_code, @po_name, @vendor_id, @quotation_id, @po_date, @vendor_name, @quantity, @grand_total, @status, @created_by)";

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@project_code", projectCode);
                            cmd.Parameters.AddWithValue("@po_name", poNumber);
                            cmd.Parameters.AddWithValue("@vendor_id", vendorId);
                            cmd.Parameters.AddWithValue("@quotation_id", quotationId.HasValue ? (object)quotationId.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@po_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                            cmd.Parameters.AddWithValue("@quantity", totalQuantity);
                            cmd.Parameters.AddWithValue("@grand_total", grandTotal);
                            cmd.Parameters.AddWithValue("@status", "Pending");
                            cmd.Parameters.AddWithValue("@created_by", currentUser);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                purchaseOrderId = (int)cmd.LastInsertedId;

                                // Insert all items into purchase_order_items table
                                string insertItemsQuery = @"
                                    INSERT INTO purchase_order_items (po_id, sku_upc, prod_name, brand, quantity, unit_price, sub_total, status)
                                    VALUES (@po_id, @sku_upc, @prod_name, @brand, @quantity, @unit_price, @sub_total, @status)";

                                foreach (DataGridViewRow row in dgvItems.Rows)
                                {
                                    using (MySqlCommand itemCmd = new MySqlCommand(insertItemsQuery, conn, transaction))
                                    {
                                        string skuUpc = row.Cells["SkuUpc"].Value?.ToString() ?? "";
                                        string productName = row.Cells["Name"].Value?.ToString() ?? "";
                                        string brand = row.Cells["Brand"].Value?.ToString() ?? "";
                                        int quantity = row.Cells["Qty"].Value != null ? Convert.ToInt32(row.Cells["Qty"].Value) : 0;

                                        string unitPriceStr = row.Cells["UnitPrice"].Value?.ToString()
                                            .Replace("₱", "").Replace(",", "").Trim() ?? "0";
                                        decimal unitPrice = decimal.Parse(unitPriceStr);

                                        string subTotalStr = row.Cells["SubTotal"].Value?.ToString()
                                            .Replace("₱", "").Replace(",", "").Trim() ?? "0";
                                        decimal subTotal = decimal.Parse(subTotalStr);
                                        string status = "Pending";

                                        itemCmd.Parameters.AddWithValue("@po_id", purchaseOrderId);
                                        itemCmd.Parameters.AddWithValue("@sku_upc", skuUpc);
                                        itemCmd.Parameters.AddWithValue("@prod_name", productName);
                                        itemCmd.Parameters.AddWithValue("@brand", brand);
                                        itemCmd.Parameters.AddWithValue("@quantity", quantity);
                                        itemCmd.Parameters.AddWithValue("@unit_price", unitPrice);
                                        itemCmd.Parameters.AddWithValue("@sub_total", subTotal);
                                        itemCmd.Parameters.AddWithValue("@status", status);

                                        itemCmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();

                                // Export to PDF
                                ExportToPdf(purchaseOrderId);

                                MessageBox.Show($"Purchase Request {poNumber} Created Successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Navigate back to previous screen
                                if (!string.IsNullOrEmpty(projectCode))
                                {
                                    LoadUserControl(new PurchaseRequestDetails(projectCode));
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Failed to create Purchase Request.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating Purchase Order: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GeneratePOName(MySqlConnection conn, MySqlTransaction transaction)
        {
            try
            {
                // Get the highest PO number
                string maxQuery = @"
                    SELECT MAX(CAST(SUBSTRING(po_name, 4) AS UNSIGNED)) 
                    FROM purchase_order
                    WHERE po_name LIKE 'PO-%'";

                using (MySqlCommand cmd = new MySqlCommand(maxQuery, conn, transaction))
                {
                    object result = cmd.ExecuteScalar();
                    int nextNumber = (result == null || result == DBNull.Value) ? 1 : Convert.ToInt32(result) + 1;

                    // Format: PO-XXX (3-digit zero-padded)
                    return $"PO-{nextNumber:D3}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating PO name: {ex.Message}");
                // Fallback to timestamp-based name
                return $"PO-{DateTime.Now:yyyyMMddHHmmss}";
            }
        }

        private void ExportToPdf(int PoId)
        {
            // Setup save dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files|*.pdf";
            saveDialog.Title = "Export Purchase Order to PDF";
            saveDialog.FileName = $"PO_KMCI_{poNumber}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
            saveDialog.DefaultExt = "pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Generate PDF
                    PurchaseOrderPdfGenerator2 generator = new PurchaseOrderPdfGenerator2();

                    // Optional: If you have a logo file
                    string logoPath = Path.Combine(Application.StartupPath, "logo.png");

                    generator.GeneratePurchaseOrderPdf(PoId, saveDialog.FileName, logoPath);

                    // Ask if user wants to send email with the PDF
                    var emailResult = MessageBox.Show(
                        "PDF generated successfully! Would you like to send it via email to the vendor?",
                        "Send Email",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (emailResult == DialogResult.Yes)
                    {
                        // Send email with PDF attachment
                        bool emailSent = EmailSender.SendPurchaseOrderEmail(PoId, saveDialog.FileName);
                        
                        if (emailSent)
                        {
                            MessageBox.Show("Purchase Order PDF has been emailed to the vendor successfully!", 
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

                    // Modified query to get items only for the selected vendor
                    // Join with product_list where the vendor matches
                    string query = @"
                SELECT 
                    pri.sku_upc,
                    pri.quantity,
                    pri.unit_price,
                    pri.sub_total,
                    p.prod_name,
                    p.brand
                FROM purchase_request_items pri
                INNER JOIN purchase_request pr ON pri.pr_id = pr.id
                INNER JOIN product_list p ON pri.sku_upc = p.sku_upc 
                    AND p.pref_vendor = @vendor_name
                WHERE pr.vendor_name = @vendor_name";

                    if (!string.IsNullOrEmpty(projectCode))
                    {
                        query += " AND pr.project_code = @project_code";
                    }

                    query += " GROUP BY pri.sku_upc, pri.quantity, pri.unit_price, pri.sub_total, p.prod_name, p.brand";
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