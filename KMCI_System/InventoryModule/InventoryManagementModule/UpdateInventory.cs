using MySql.Data.MySqlClient;
using System.Text;

namespace KMCI_System.InventoryModule.InventoryManagementModule
{
    public partial class UpdateInventory : UserControl
    {
        private string projectCode;
        private UserControl currentUserControl;

        // UI Components
        private Label lblHeader;
        private Label lblProjectCode;
        private ComboBox cboProjectCode;
        private Label lblPurchaseOrder;
        private ComboBox cboPurchaseOrder;
        private Button btnLoadItems;
        private DataGridView dgvPOItems;
        private Label lblItemsHeader;
        private Button btnMarkAsDelivered;
        private Button btnMarkForDelivery;
        private Label lblStatusSummary;
        private TextBox txtStatusSummary;

        public UpdateInventory()
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

            yPos += 40;

            // Purchase Order Selection
            lblPurchaseOrder = new Label
            {
                Text = "Purchase Order:",
                Location = new Point(leftMargin, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblPurchaseOrder);

            cboPurchaseOrder = new ComboBox
            {
                Location = new Point(leftMargin + 150, yPos),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false
            };
            Controls.Add(cboPurchaseOrder);

            // Load Items Button
            btnLoadItems = new Button
            {
                Text = "Load Items",
                Location = new Point(leftMargin + 420, yPos),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnLoadItems.FlatAppearance.BorderSize = 0;
            btnLoadItems.Click += BtnLoadItems_Click;
            Controls.Add(btnLoadItems);

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
                Height = 300,
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
            dgvPOItems.Columns["SkuUpc"].Width = 120;
            dgvPOItems.Columns["ProductName"].Width = 250;
            dgvPOItems.Columns["Brand"].Width = 150;
            dgvPOItems.Columns["Quantity"].Width = 100;
            dgvPOItems.Columns["ReceivedQty"].Width = 120;
            dgvPOItems.Columns["Status"].Width = 150;

            // Make ReceivedQty editable
            dgvPOItems.Columns["ReceivedQty"].ReadOnly = false;

            // Center align numeric columns
            dgvPOItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPOItems.Columns["ReceivedQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPOItems.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPOItems.CellValueChanged += DgvPOItems_CellValueChanged;
            dgvPOItems.CurrentCellDirtyStateChanged += DgvPOItems_CurrentCellDirtyStateChanged;

            Controls.Add(dgvPOItems);

            yPos += 320;

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
                Text = "Mark Order Ready for Delivery",
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
                LoadPurchaseOrders();
                cboPurchaseOrder.Enabled = true;
            }
            else
            {
                cboPurchaseOrder.Items.Clear();
                cboPurchaseOrder.Enabled = false;
                btnLoadItems.Enabled = false;
                dgvPOItems.Rows.Clear();
                txtStatusSummary.Clear();
            }
        }

        private void LoadPurchaseOrders()
        {
            cboPurchaseOrder.Items.Clear();
            cboPurchaseOrder.Items.Add("-- Select Purchase Order --");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT id, po_name, vendor_name, status
                        FROM purchase_order 
                        WHERE project_code = @project_code
                        ORDER BY id DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@project_code", projectCode);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int poId = Convert.ToInt32(reader["id"]);
                                string poName = reader["po_name"].ToString();
                                string vendorName = reader["vendor_name"].ToString();
                                string status = reader["status"].ToString();

                                cboPurchaseOrder.Items.Add(new PurchaseOrderItem
                                {
                                    POId = poId,
                                    DisplayText = $"{poName} - {vendorName} ({status})"
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase orders: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboPurchaseOrder.DisplayMember = "DisplayText";
            cboPurchaseOrder.SelectedIndex = 0;
            cboPurchaseOrder.SelectedIndexChanged += CboPurchaseOrder_SelectedIndexChanged;
        }

        private void CboPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoadItems.Enabled = cboPurchaseOrder.SelectedIndex > 0;
            if (cboPurchaseOrder.SelectedIndex <= 0)
            {
                dgvPOItems.Rows.Clear();
                txtStatusSummary.Clear();
                btnMarkAsDelivered.Enabled = false;
                btnMarkForDelivery.Enabled = false;
            }
        }

        private void BtnLoadItems_Click(object sender, EventArgs e)
        {
            if (cboPurchaseOrder.SelectedItem is PurchaseOrderItem selectedPO)
            {
                LoadPurchaseOrderItems(selectedPO.POId);
            }
        }

        private void LoadPurchaseOrderItems(int poId)
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
                            poi.sku_upc,
                            poi.prod_name,
                            poi.brand,
                            poi.quantity,
                            COALESCE(poi.received_qty, 0) as received_qty,
                            COALESCE(poi.status, 'Pending') as delivery_status
                        FROM purchase_order_items poi
                        WHERE poi.po_id = @po_id
                        ORDER BY poi.id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@po_id", poId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int itemId = Convert.ToInt32(reader["id"]);
                                string skuUpc = reader["sku_upc"].ToString();
                                string productName = reader["prod_name"].ToString();
                                string brand = reader["brand"].ToString();
                                int quantity = Convert.ToInt32(reader["quantity"]);
                                int receivedQty = Convert.ToInt32(reader["received_qty"]);
                                string status = reader["delivery_status"].ToString();

                                dgvPOItems.Rows.Add(
                                    false, // Select checkbox
                                    itemId,
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
                // Update status when received qty changes
                else if (e.ColumnIndex == dgvPOItems.Columns["ReceivedQty"].Index)
                {
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

        private void CheckDeliveryEligibility()
        {
            // Check if all items are completely delivered
            bool allDelivered = true;
            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int orderedQty) &&
                    int.TryParse(row.Cells["ReceivedQty"].Value?.ToString(), out int receivedQty))
                {
                    if (receivedQty < orderedQty)
                    {
                        allDelivered = false;
                        break;
                    }
                }
                else
                {
                    allDelivered = false;
                    break;
                }
            }

            btnMarkForDelivery.Enabled = allDelivered && dgvPOItems.Rows.Count > 0;
        }

        private void BtnMarkAsDelivered_Click(object sender, EventArgs e)
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

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (int rowIndex in selectedRowIndices)
                            {
                                DataGridViewRow row = dgvPOItems.Rows[rowIndex];
                                int itemId = Convert.ToInt32(row.Cells["ItemId"].Value);
                                int orderedQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                                string skuUpc = row.Cells["SkuUpc"].Value.ToString();

                                // Update purchase order item
                                string updateQuery = @"
                                    UPDATE purchase_order_items 
                                    SET received_qty = @received_qty,
                                        delivery_status = 'Delivered',
                                        delivery_date = NOW()
                                    WHERE id = @item_id";

                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@received_qty", orderedQty);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.ExecuteNonQuery();
                                }

                                // Update product inventory (incoming stock)
                                string inventoryQuery = @"
                                    UPDATE product_list 
                                    SET incoming = incoming + @quantity,
                                        current = current + @quantity
                                    WHERE sku_upc = @sku_upc";

                                using (MySqlCommand cmd = new MySqlCommand(inventoryQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@quantity", orderedQty);
                                    cmd.Parameters.AddWithValue("@sku_upc", skuUpc);
                                    cmd.ExecuteNonQuery();
                                }

                                // Update the grid
                                row.Cells["ReceivedQty"].Value = orderedQty;
                                row.Cells["Status"].Value = "Delivered";
                                row.Cells["Select"].Value = false;
                                ApplyStatusColor(rowIndex, "Delivered", orderedQty, orderedQty);
                            }

                            transaction.Commit();

                            MessageBox.Show($"{selectedItemIds.Count} item(s) marked as delivered to logistics successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            UpdateStatusSummary();
                            CheckDeliveryEligibility();
                            UpdateButtonStates();
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
                MessageBox.Show($"Error updating delivery status: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarkForDelivery_Click(object sender, EventArgs e)
        {
            if (cboPurchaseOrder.SelectedItem is not PurchaseOrderItem selectedPO)
                return;

            // Verify all items are complete
            foreach (DataGridViewRow row in dgvPOItems.Rows)
            {
                if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int orderedQty) &&
                    int.TryParse(row.Cells["ReceivedQty"].Value?.ToString(), out int receivedQty))
                {
                    if (receivedQty < orderedQty)
                    {
                        MessageBox.Show("Cannot mark order for delivery. Not all items are completely received.",
                            "Incomplete Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            DialogResult result = MessageBox.Show(
                "All items are complete. Mark this purchase order as ready for delivery?\n\n" +
                "This will update the purchase order status to 'Ready for Delivery'.",
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
                    string updateQuery = @"
                        UPDATE purchase_order 
                        SET status = 'Ready for Delivery',
                            completed_date = NOW()
                        WHERE id = @po_id";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@po_id", selectedPO.POId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Purchase order marked as ready for delivery successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnMarkForDelivery.Enabled = false;
                            LoadPurchaseOrders(); // Refresh the PO list to show updated status
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

        private class PurchaseOrderItem
        {
            public int POId { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}