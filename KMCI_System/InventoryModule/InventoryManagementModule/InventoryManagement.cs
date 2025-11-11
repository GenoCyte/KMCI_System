using MySql.Data.MySqlClient;
using System.Text;

namespace KMCI_System.InventoryModule.InventoryManagementModule
{
    public partial class InventoryManagement : UserControl
    {
        private string projectCode;
        private UserControl currentUserControl;

        // UI Components
        private Label lblHeader;
        private Label lblProjectCode;
        private ComboBox cboProjectCode;
        private DataGridView dgvPOItems;
        private Label lblItemsHeader;
        private Button btnMarkAsDelivered;
        private Button btnMarkForDelivery;
        private Button btnMarkDeliveredToClient;
        private Label lblStatusSummary;
        private TextBox txtStatusSummary;

        public InventoryManagement()
        {
            InitializeComponent();
            SetupUI();
            LoadProjects();
        }

        private void SetupUI()
        {
            int yPos = 20;
            int leftMargin = 30;

            // Header
            lblHeader = new Label
            {
                Text = "Update Inventory - Purchase Order Status",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true
            };
            Controls.Add(lblHeader);

            yPos += 60;

            // Project Code Selection
            lblProjectCode = new Label
            {
                Text = "Select Project:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProjectCode);

            cboProjectCode = new ComboBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboProjectCode.SelectedIndexChanged += CboProjectCode_SelectedIndexChanged;
            Controls.Add(cboProjectCode);

            yPos += 60;

            // Items Header
            lblItemsHeader = new Label
            {
                Text = "Purchase Order Items",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(leftMargin, yPos),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            Controls.Add(lblItemsHeader);

            yPos += 40;

            // Items DataGridView
            dgvPOItems = new DataGridView
            {
                Location = new Point(leftMargin, yPos),
                Width = 1050,
                Height = 350,
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
            dgvPOItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvPOItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPOItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvPOItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPOItems.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvPOItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvPOItems.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvPOItems.DefaultCellStyle.BackColor = Color.White;
            dgvPOItems.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvPOItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvPOItems.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPOItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvPOItems.GridColor = Color.FromArgb(220, 220, 220);
            dgvPOItems.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Add columns
            dgvPOItems.Columns.Add("ItemId", "Item ID");
            dgvPOItems.Columns["ItemId"].Visible = false;

            dgvPOItems.Columns.Add("POName", "PO Name");
            dgvPOItems.Columns.Add("VendorName", "Vendor");
            dgvPOItems.Columns.Add("SkuUpc", "SKU/UPC");
            dgvPOItems.Columns.Add("ProductName", "Product Name");
            dgvPOItems.Columns.Add("Brand", "Brand");
            dgvPOItems.Columns.Add("Quantity", "Ordered Qty");
            dgvPOItems.Columns.Add("ReceivedQty", "Received Qty");
            dgvPOItems.Columns.Add("Status", "Status");

            // Add checkbox column for selection
            DataGridViewCheckBoxColumn chkSelect = new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "Select",
                Width = 60,
                FalseValue = false,
                TrueValue = true
            };
            dgvPOItems.Columns.Insert(0, chkSelect);

            // Set column widths
            dgvPOItems.Columns["Select"].Width = 60;
            dgvPOItems.Columns["POName"].Width = 120;
            dgvPOItems.Columns["VendorName"].Width = 120;
            dgvPOItems.Columns["SkuUpc"].Width = 100;
            dgvPOItems.Columns["ProductName"].Width = 200;
            dgvPOItems.Columns["Brand"].Width = 120;
            dgvPOItems.Columns["Quantity"].Width = 80;
            dgvPOItems.Columns["ReceivedQty"].Width = 100;
            dgvPOItems.Columns["Status"].Width = 120;

            // Make ReceivedQty editable
            dgvPOItems.Columns["ReceivedQty"].ReadOnly = false;

            // Center align numeric columns
            dgvPOItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPOItems.Columns["ReceivedQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPOItems.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPOItems.CellValueChanged += DgvPOItems_CellValueChanged;
            dgvPOItems.CurrentCellDirtyStateChanged += DgvPOItems_CurrentCellDirtyStateChanged;

            Controls.Add(dgvPOItems);

            yPos += 370;

            // Status Summary
            lblStatusSummary = new Label
            {
                Text = "Status Summary:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblStatusSummary);

            txtStatusSummary = new TextBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(900, 60),
                Font = new Font("Segoe UI", 9F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Multiline = true
            };
            Controls.Add(txtStatusSummary);

            yPos += 80;

            // Action Buttons
            btnMarkAsDelivered = new Button
            {
                Text = "Mark Selected as Delivered to Logistics",
                Location = new Point(leftMargin, yPos),
                Size = new Size(280, 40),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnMarkAsDelivered.FlatAppearance.BorderSize = 0;
            btnMarkAsDelivered.Click += BtnMarkAsDelivered_Click;
            Controls.Add(btnMarkAsDelivered);

            btnMarkForDelivery = new Button
            {
                Text = "Mark Orders Ready for Delivery",
                Location = new Point(leftMargin + 300, yPos),
                Size = new Size(250, 40),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnMarkForDelivery.FlatAppearance.BorderSize = 0;
            btnMarkForDelivery.Click += BtnMarkForDelivery_Click;
            Controls.Add(btnMarkForDelivery);

            btnMarkDeliveredToClient = new Button
            {
                Text = "Mark Delivered to Client",
                Location = new Point(leftMargin + 570, yPos),
                Size = new Size(220, 40),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnMarkDeliveredToClient.FlatAppearance.BorderSize = 0;
            btnMarkDeliveredToClient.Click += BtnMarkDeliveredToClient_Click;
            Controls.Add(btnMarkDeliveredToClient);
        }

        private void LoadProjects()
        {
            cboProjectCode.Items.Clear();
            cboProjectCode.Items.Add("-- Select Project --");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT project_code, company_name 
                        FROM project_list 
                        ORDER BY project_code DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectCode = reader["project_code"].ToString();
                            string companyName = reader["company_name"].ToString();
                            cboProjectCode.Items.Add(new ProjectItem
                            {
                                ProjectCode = projectCode,
                                DisplayText = $"{projectCode} - {companyName}"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboProjectCode.DisplayMember = "DisplayText";
            cboProjectCode.SelectedIndex = 0;
        }

        private void CboProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProjectCode.SelectedIndex > 0 && cboProjectCode.SelectedItem is ProjectItem selectedProject)
            {
                projectCode = selectedProject.ProjectCode;
                LoadAllPurchaseOrderItems();
            }
            else
            {
                dgvPOItems.Rows.Clear();
                txtStatusSummary.Clear();
                btnMarkAsDelivered.Enabled = false;
                btnMarkForDelivery.Enabled = false;
                btnMarkDeliveredToClient.Enabled = false;
            }
        }

        private void LoadAllPurchaseOrderItems()
        {
            dgvPOItems.Rows.Clear();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            poi.id,
                            po.po_name,
                            po.vendor_name,
                            poi.sku_upc,
                            poi.prod_name,
                            poi.brand,
                            poi.quantity,
                            COALESCE(poi.received_qty, 0) as received_qty,
                            COALESCE(poi.status, 'Pending') as delivery_status
                        FROM purchase_order_items poi
                        INNER JOIN purchase_order po ON poi.po_id = po.id
                        WHERE po.project_code = @project_code
                        ORDER BY po.po_name, poi.id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int itemId = Convert.ToInt32(reader["id"]);
                                string poName = reader["po_name"].ToString();
                                string vendorName = reader["vendor_name"].ToString();
                                string skuUpc = reader["sku_upc"].ToString();
                                string productName = reader["prod_name"].ToString();
                                string brand = reader["brand"].ToString();
                                int quantity = Convert.ToInt32(reader["quantity"]);
                                int receivedQty = Convert.ToInt32(reader["received_qty"]);
                                string status = reader["delivery_status"].ToString();

                                dgvPOItems.Rows.Add(
                                    false, // Select checkbox
                                    itemId,
                                    poName,
                                    vendorName,
                                    skuUpc,
                                    productName,
                                    brand,
                                    quantity,
                                    receivedQty,
                                    status
                                );

                                // Apply color coding to status
                                int rowIndex = dgvPOItems.Rows.Count - 1;
                                ApplyStatusColor(rowIndex, status, quantity, receivedQty);
                            }
                        }
                    }
                }

                dgvPOItems.ClearSelection();
                UpdateStatusSummary();
                CheckDeliveryEligibility();
                CheckClientDeliveryEligibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase order items: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusColor(int rowIndex, string status, int orderedQty, int receivedQty)
        {
            DataGridViewRow row = dgvPOItems.Rows[rowIndex];
            DataGridViewCell statusCell = row.Cells["Status"];

            switch (status.ToLower())
            {
                case "delivered":
                    statusCell.Style.BackColor = Color.LightGreen;
                    statusCell.Style.ForeColor = Color.DarkGreen;
                    statusCell.Style.Font = new Font(dgvPOItems.Font, FontStyle.Bold);
                    break;
                case "partial":
                    statusCell.Style.BackColor = Color.LightYellow;
                    statusCell.Style.ForeColor = Color.DarkOrange;
                    statusCell.Style.Font = new Font(dgvPOItems.Font, FontStyle.Bold);
                    break;
                case "pending":
                    statusCell.Style.BackColor = Color.LightGray;
                    statusCell.Style.ForeColor = Color.Black;
                    break;
            }

            // Highlight rows where received qty doesn't match ordered qty
            if (receivedQty > 0 && receivedQty < orderedQty)
            {
                row.Cells["ReceivedQty"].Style.BackColor = Color.LightYellow;
            }
            else if (receivedQty >= orderedQty)
            {
                row.Cells["ReceivedQty"].Style.BackColor = Color.LightGreen;
            }
        }

        private void DgvPOItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPOItems.IsCurrentCellDirty)
            {
                dgvPOItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvPOItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Update button states when checkboxes change
                if (e.ColumnIndex == dgvPOItems.Columns["Select"].Index)
                {
                    UpdateButtonStates();
                }
                // Save and update inventory when received qty changes
                else if (e.ColumnIndex == dgvPOItems.Columns["ReceivedQty"].Index)
                {
                    SaveReceivedQuantityChanges(e.RowIndex);
                    UpdateStatusSummary();
                    CheckDeliveryEligibility();
                    UpdateItemStatus(e.RowIndex);
                }
            }
        }

        private void UpdateItemStatus(int rowIndex)
        {
            DataGridViewRow row = dgvPOItems.Rows[rowIndex];

            if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int orderedQty) &&
                int.TryParse(row.Cells["ReceivedQty"].Value?.ToString(), out int receivedQty))
            {
                string newStatus;
                if (receivedQty == 0)
                {
                    newStatus = "Pending";
                }
                else if (receivedQty >= orderedQty)
                {
                    newStatus = "Delivered";
                }
                else
                {
                    newStatus = "Partial";
                }

                row.Cells["Status"].Value = newStatus;
                ApplyStatusColor(rowIndex, newStatus, orderedQty, receivedQty);
                UpdateStatusSummary();
                CheckDeliveryEligibility();
            }
        }

        private void UpdateButtonStates()
        {
            bool anySelected = false;
            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                if (row.Cells["Select"].Value != null && (bool)row.Cells["Select"].Value)
                {
                    anySelected = true;
                    break;
                }
            }
            btnMarkAsDelivered.Enabled = anySelected;
        }

        private void CheckDeliveryEligibility()
        {
            // Check if all items are completely delivered
            bool allDelivered = true;

            if (dgvPOItems.Rows.Count == 0)
            {
                btnMarkForDelivery.Enabled = false;
                return;
            }

            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString()?.ToLower() ?? "pending";
                if (status != "delivered")
                {
                    allDelivered = false;
                    break;
                }
            }

            btnMarkForDelivery.Enabled = allDelivered;
        }

        private void CheckClientDeliveryEligibility()
        {
            // Check if all purchase orders for this project are "Ready for Delivery"
            if (string.IsNullOrEmpty(projectCode))
            {
                btnMarkDeliveredToClient.Enabled = false;
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(*) as total_orders,
                               SUM(CASE WHEN status = 'Ready for Delivery' THEN 1 ELSE 0 END) as ready_orders
                        FROM purchase_order
                        WHERE project_code = @project_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalOrders = Convert.ToInt32(reader["total_orders"]);
                                int readyOrders = Convert.ToInt32(reader["ready_orders"]);

                                // Enable button only if all orders are ready and there's at least one order
                                btnMarkDeliveredToClient.Enabled = totalOrders > 0 && totalOrders == readyOrders;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnMarkDeliveredToClient.Enabled = false;
            }
        }

        private void UpdateStatusSummary()
        {
            int totalItems = dgvPOItems.Rows.Count;
            int deliveredItems = 0;
            int partialItems = 0;
            int pendingItems = 0;

            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString()?.ToLower() ?? "pending";
                switch (status)
                {
                    case "delivered":
                        deliveredItems++;
                        break;
                    case "partial":
                        partialItems++;
                        break;
                    case "pending":
                        pendingItems++;
                        break;
                }
            }

            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Total Items: {totalItems}");
            summary.AppendLine($"Delivered: {deliveredItems} | Partial: {partialItems} | Pending: {pendingItems}");

            if (deliveredItems == totalItems && totalItems > 0)
            {
                summary.AppendLine("✓ All items received - Ready for delivery!");
            }
            else if (partialItems > 0 || deliveredItems > 0)
            {
                summary.AppendLine("⚠ Some items received - Waiting for complete order");
            }

            txtStatusSummary.Text = summary.ToString();
        }

        private async void BtnMarkAsDelivered_Click(object sender, EventArgs e)
        {
            List<int> selectedItemIds = new List<int>();
            List<int> selectedRowIndices = new List<int>();

            // Collect selected items
            for (int i = 0; i < dgvPOItems.Rows.Count; i++)
            {
                DataGridViewRow row = dgvPOItems.Rows[i];
                if (row.Cells["Select"].Value != null && (bool)row.Cells["Select"].Value)
                {
                    if (int.TryParse(row.Cells["ItemId"].Value?.ToString(), out int itemId))
                    {
                        selectedItemIds.Add(itemId);
                        selectedRowIndices.Add(i);
                    }
                }
            }

            if (selectedItemIds.Count == 0)
            {
                MessageBox.Show("Please select items to mark as delivered.", "No Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
            $"Mark {selectedItemIds.Count} selected item(s) as delivered to logistics?\n\n" +
            "This will update the received quantity to match the ordered quantity.",
            "Confirm Delivery",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Disable button and show cursor feedback
            btnMarkAsDelivered.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                await Task.Run(() =>
                {
                    using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                    {
                        conn.Open();
                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Batch update purchase order items
                                StringBuilder updateItemsQuery = new StringBuilder();
                                List<MySqlParameter> itemParameters = new List<MySqlParameter>();

                                for (int i = 0; i < selectedRowIndices.Count; i++)
                                {
                                    int rowIndex = selectedRowIndices[i];
                                    DataGridViewRow row = dgvPOItems.Rows[rowIndex];
                                    int itemId = Convert.ToInt32(row.Cells["ItemId"].Value);
                                    int orderedQty = Convert.ToInt32(row.Cells["Quantity"].Value);

                                    updateItemsQuery.Append($@"
                                        UPDATE purchase_order_items 
                                        SET received_qty = @received_qty{i},
                                        status = 'Delivered',
                                        delivery_date = NOW()
                                        WHERE id = @item_id{i};");

                                    itemParameters.Add(new MySqlParameter($"@received_qty{i}", orderedQty));
                                    itemParameters.Add(new MySqlParameter($"@item_id{i}", itemId));
                                }

                                using (MySqlCommand cmd = new MySqlCommand(updateItemsQuery.ToString(), conn, transaction))
                                {
                                    cmd.Parameters.AddRange(itemParameters.ToArray());
                                    cmd.ExecuteNonQuery();
                                }

                                // Batch update product inventory using CASE statement
                                StringBuilder updateInventoryQuery = new StringBuilder(@"
                                UPDATE product_list 
                                SET current = current + CASE sku_upc ");

                                StringBuilder incomingUpdate = new StringBuilder(@"
                                incoming = incoming - CASE sku_upc ");

                                List<MySqlParameter> inventoryParameters = new List<MySqlParameter>();
                                StringBuilder skuList = new StringBuilder();

                                for (int i = 0; i < selectedRowIndices.Count; i++)
                                {
                                    int rowIndex = selectedRowIndices[i];
                                    DataGridViewRow row = dgvPOItems.Rows[rowIndex];
                                    string skuUpc = row.Cells["SkuUpc"].Value.ToString();
                                    int orderedQty = Convert.ToInt32(row.Cells["Quantity"].Value);

                                    updateInventoryQuery.Append($"WHEN @sku{i} THEN @qty{i} ");
                                    incomingUpdate.Append($"WHEN @sku{i} THEN @qty{i} ");

                                    inventoryParameters.Add(new MySqlParameter($"@sku{i}", skuUpc));
                                    inventoryParameters.Add(new MySqlParameter($"@qty{i}", orderedQty));

                                    if (i > 0) skuList.Append(", ");
                                    skuList.Append($"@sku{i}");
                                }

                                updateInventoryQuery.Append($"ELSE 0 END, {incomingUpdate}ELSE 0 END WHERE sku_upc IN ({skuList})");

                                using (MySqlCommand cmd = new MySqlCommand(updateInventoryQuery.ToString(), conn, transaction))
                                {
                                    cmd.Parameters.AddRange(inventoryParameters.ToArray());
                                    cmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw new Exception($"Transaction failed: {ex.Message}");
                            }
                        }
                    }
                });

                // Update UI on UI thread
                foreach (int rowIndex in selectedRowIndices)
                {
                    DataGridViewRow row = dgvPOItems.Rows[rowIndex];
                    int orderedQty = Convert.ToInt32(row.Cells["Quantity"].Value);

                    row.Cells["ReceivedQty"].Value = orderedQty;
                    row.Cells["Status"].Value = "Delivered";
                    row.Cells["Select"].Value = false;
                    ApplyStatusColor(rowIndex, "Delivered", orderedQty, orderedQty);
                }
                MessageBox.Show($"{selectedItemIds.Count} item(s) marked as delivered to logistics successfully!",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateStatusSummary();
                CheckDeliveryEligibility();
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating delivery status: {ex.Message}", "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable button and restore cursor
                btnMarkAsDelivered.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnMarkForDelivery_Click(object sender, EventArgs e)
        {
            // Get distinct purchase orders from the current grid
            HashSet<string> purchaseOrders = new HashSet<string>();
            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                string poName = row.Cells["POName"].Value?.ToString();
                if (!string.IsNullOrEmpty(poName))
                {
                    purchaseOrders.Add(poName);
                }
            }

            DialogResult result = MessageBox.Show(
                $"All items are delivered. Mark all purchase orders for this project as 'Ready for Delivery'?\n\n" +
                $"This will update {purchaseOrders.Count} purchase order(s):\n" +
                $"{string.Join(", ", purchaseOrders)}\n\n" +
                "This will update the purchase order status to 'Ready for Delivery' and move inventory to outgoing.",
                "Confirm Ready for Delivery",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Update all purchase orders for this project
                            string updatePOQuery = @"
                                UPDATE purchase_order 
                                SET status = 'Ready for Delivery',
                                    completed_date = NOW()
                                WHERE project_code = @project_code";

                            int poRowsAffected;
                            using (MySqlCommand cmd = new MySqlCommand(updatePOQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@project_code", projectCode);
                                poRowsAffected = cmd.ExecuteNonQuery();
                            }

                            // Update inventory for all items - move from current to outgoing
                            foreach (DataGridViewRow row in dgvPOItems.Rows)
                            {
                                string skuUpc = row.Cells["SkuUpc"].Value?.ToString();
                                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                                string inventoryQuery = @"
                                    UPDATE product_list 
                                    SET outgoing = outgoing + @quantity,
                                        current = current - @quantity
                                    WHERE sku_upc = @sku_upc";

                                using (MySqlCommand cmd = new MySqlCommand(inventoryQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@quantity", quantity);
                                    cmd.Parameters.AddWithValue("@sku_upc", skuUpc);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();

                            MessageBox.Show($"{poRowsAffected} purchase order(s) marked as ready for delivery successfully!\n" +
                                          $"Inventory has been moved to outgoing.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnMarkForDelivery.Enabled = false;
                            CheckClientDeliveryEligibility();
                            // Optionally reload the data to reflect any changes
                            LoadAllPurchaseOrderItems();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating purchase order status: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarkDeliveredToClient_Click(object sender, EventArgs e)
        {
            // Get distinct purchase orders from the current grid
            HashSet<string> purchaseOrders = new HashSet<string>();
            int totalItems = 0;

            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                string poName = row.Cells["POName"].Value?.ToString();
                if (!string.IsNullOrEmpty(poName))
                {
                    purchaseOrders.Add(poName);
                    totalItems++;
                }
            }

            if (totalItems == 0)
            {
                MessageBox.Show("No items found to process.", "No Items",
         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
            $"Mark all purchase orders for this project as 'Delivered to Client'?\n\n" +
            $"This will update {purchaseOrders.Count} purchase order(s):\n" +
          $"{string.Join(", ", purchaseOrders)}\n\n" +
            $"Total items to process: {totalItems}\n\n" +
               "This will update the purchase order status to 'Delivered to Client' and finalize inventory.",
             "Confirm Delivery to Client",
                        MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Update all purchase orders for this project
                            string updatePOQuery = @"
                UPDATE purchase_order 
        SET status = 'Delivered to Client',
     delivered_date = NOW()
      WHERE project_code = @project_code 
            AND status = 'Ready for Delivery'";

                            int poRowsAffected;
                            using (MySqlCommand cmd = new MySqlCommand(updatePOQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@project_code", projectCode);
                                poRowsAffected = cmd.ExecuteNonQuery();
                            }

                            // Track inventory updates
                            int inventoryRowsAffected = 0;
                            List<string> failedItems = new List<string>();

                            // Update inventory for all items - subtract from outgoing
                            foreach (DataGridViewRow row in dgvPOItems.Rows)
                            {
                                string skuUpc = row.Cells["SkuUpc"].Value?.ToString();
                                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                                if (string.IsNullOrEmpty(skuUpc))
                                {
                                    failedItems.Add($"Row {row.Index + 1}: Missing SKU/UPC");
                                    continue;
                                }

                                string inventoryQuery = @"
      UPDATE product_list 
             SET outgoing = outgoing - @quantity
  WHERE sku_upc = @sku_upc";

                                using (MySqlCommand cmd = new MySqlCommand(inventoryQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@quantity", quantity);
                                    cmd.Parameters.AddWithValue("@sku_upc", skuUpc);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        inventoryRowsAffected++;
                                    }
                                    else
                                    {
                                        failedItems.Add($"SKU: {skuUpc} - Not found in inventory");
                                    }
                                }
                            }

                            // Check if all items were processed
                            if (failedItems.Count > 0)
                            {
                                string failureMessage = $"Some items could not be processed:\n\n" +
                                 string.Join("\n", failedItems.Take(10));

                                if (failedItems.Count > 10)
                                {
                                    failureMessage += $"\n... and {failedItems.Count - 10} more";
                                }

                                DialogResult continueResult = MessageBox.Show(
                                  failureMessage + "\n\nDo you want to continue with the transaction?",
                                       "Warning",
                                       MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

                                if (continueResult != DialogResult.Yes)
                                {
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            transaction.Commit();

                            string successMessage = $"Purchase orders marked as delivered to client successfully!\n\n" +
                                 $"Purchase Orders Updated: {poRowsAffected}\n" +
                               $"Inventory Items Updated: {inventoryRowsAffected} of {totalItems}\n";

                            if (failedItems.Count > 0)
                            {
                                successMessage += $"Items with issues: {failedItems.Count}";
                            }

                            MessageBox.Show(successMessage, "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnMarkDeliveredToClient.Enabled = false;
                            LoadAllPurchaseOrderItems();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking as delivered to client: {ex.Message}", "Database Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }

        // Helper classes
        private class ProjectItem
        {
            public string ProjectCode { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        private void SaveReceivedQuantityChanges(int rowIndex)
        {
            DataGridViewRow row = dgvPOItems.Rows[rowIndex];

            if (!int.TryParse(row.Cells["ItemId"].Value?.ToString(), out int itemId))
                return;

            if (!int.TryParse(row.Cells["ReceivedQty"].Value?.ToString(), out int newReceivedQty))
                return;

            if (!int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int orderedQty))
                return;

            string skuUpc = row.Cells["SkuUpc"].Value?.ToString();
            if (string.IsNullOrEmpty(skuUpc))
                return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Get the previous received quantity
                            int previousReceivedQty = 0;
                            string getQuery = "SELECT COALESCE(received_qty, 0) as received_qty FROM purchase_order_items WHERE id = @item_id";

                            using (MySqlCommand cmd = new MySqlCommand(getQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@item_id", itemId);
                                object result = cmd.ExecuteScalar();
                                if (result != null)
                                {
                                    previousReceivedQty = Convert.ToInt32(result);
                                }
                            }

                            // Calculate the difference
                            int qtyDifference = newReceivedQty - previousReceivedQty;

                            if (qtyDifference != 0)
                            {
                                // Update purchase order item
                                string status = newReceivedQty == 0 ? "Pending" :
                                              (newReceivedQty >= orderedQty ? "Delivered" : "Partial");

                                string updateItemQuery = @"
                                    UPDATE purchase_order_items 
                                    SET received_qty = @received_qty,
                                        status = @status" +
                                        (status == "Delivered" ? ", delivery_date = NOW()" : "") + @"
                                    WHERE id = @item_id";

                                using (MySqlCommand cmd = new MySqlCommand(updateItemQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@received_qty", newReceivedQty);
                                    cmd.Parameters.AddWithValue("@status", status);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.ExecuteNonQuery();
                                }

                                // Update product inventory - add difference to current and subtract from incoming
                                string inventoryQuery = @"
                                    UPDATE product_list 
                                    SET current = current + @quantity,
                                        incoming = incoming - @quantity
                                    WHERE sku_upc = @sku_upc";

                                using (MySqlCommand cmd = new MySqlCommand(inventoryQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@quantity", qtyDifference);
                                    cmd.Parameters.AddWithValue("@sku_upc", skuUpc);
                                    cmd.ExecuteNonQuery();
                                }

                                row.Cells["Status"].Value = status;
                                ApplyStatusColor(rowIndex, status, orderedQty, newReceivedQty);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving received quantity: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadAllPurchaseOrderItems(); // Reload to revert changes
            }
        }
    }
}