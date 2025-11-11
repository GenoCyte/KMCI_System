using MySql.Data.MySqlClient;

namespace KMCI_System.SalesModule.ProductManagementModule
{
    public partial class ProductManagement : UserControl
    {
        private DataGridView dgvProducts;

        public ProductManagement()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadBrands();
            LoadCategories();
            LoadProject();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var addProductForm = new SalesModule.ProductManagementModule.AddProduct())
            {
                if (addProductForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBrands();
                    LoadCategories();
                    LoadProject();
                }
            }
        }

        private void SetupDataGridView()
        {
            dgvProducts = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 45 },
                ScrollBars = ScrollBars.Both, // Enable both scrollbars
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false
            };

            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.Location = new Point(20, 210);
            dgvProducts.Width = this.ClientSize.Width - 40;
            dgvProducts.Height = this.ClientSize.Height - 210;

            // Style headers
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvProducts.DefaultCellStyle.BackColor = Color.White;
            dgvProducts.DefaultCellStyle.Padding = new Padding(8, 3, 8, 3);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            // Grid lines
            dgvProducts.GridColor = Color.FromArgb(220, 220, 220);
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns with optimized widths
            dgvProducts.Columns.Add("SKU", "SKU");
            dgvProducts.Columns.Add("Name", "Name");
            dgvProducts.Columns.Add("Description", "Description");
            dgvProducts.Columns.Add("Brand", "Brand");
            dgvProducts.Columns.Add("SubCategory", "Sub Category");
            dgvProducts.Columns.Add("UOM", "UOM");
            dgvProducts.Columns.Add("Outgoing", "Outgoing");
            dgvProducts.Columns.Add("Incoming", "Incoming");
            dgvProducts.Columns.Add("Current", "Current");
            dgvProducts.Columns.Add("Status", "Status");

            // Add action button column
            DataGridViewButtonColumn btnAction = new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvProducts.Columns.Add(btnAction);
            dgvProducts.Columns["Action"].DefaultCellStyle.ForeColor = Color.Red;
            dgvProducts.Columns["Action"].DefaultCellStyle.SelectionForeColor = Color.Red;

            // Set column widths (Total: ~1350px - will scroll horizontally)
            dgvProducts.Columns["SKU"].Width = 100;
            dgvProducts.Columns["Name"].Width = 200;
            dgvProducts.Columns["Description"].Width = 250;
            dgvProducts.Columns["Brand"].Width = 120;
            dgvProducts.Columns["SubCategory"].Width = 120;
            dgvProducts.Columns["UOM"].Width = 80;
            dgvProducts.Columns["Outgoing"].Width = 90;
            dgvProducts.Columns["Incoming"].Width = 90;
            dgvProducts.Columns["Current"].Width = 90;
            dgvProducts.Columns["Status"].Width = 100;
            dgvProducts.Columns["Action"].Width = 100;

            // Center align numeric columns
            dgvProducts.Columns["Outgoing"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns["Incoming"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns["Current"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.Columns["UOM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Make columns non-resizable
            foreach (DataGridViewColumn column in dgvProducts.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            dgvProducts.CellContentClick += dgvProducts_CellContentClick;

            this.Controls.Add(dgvProducts);
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProducts.Columns["Action"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    String productSku = dgvProducts.Rows[e.RowIndex].Cells["SKU"].Value.ToString();
                    DeleteProduct(productSku);
                    dgvProducts.Rows.RemoveAt(e.RowIndex);

                    // Hide details panel when row is deleted
                    //detailsPanel.Visible = false;

                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DeleteProduct(String productSku)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Delete items
                string deleteItems = "DELETE FROM product_list WHERE sku_upc = @sku_upc";
                using (MySqlCommand cmd = new MySqlCommand(deleteItems, conn))
                {
                    cmd.Parameters.AddWithValue("@sku_upc", productSku);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadBrands()
        {
            cboFilterBrand.Items.Clear();
            cboFilterBrand.Items.Add("All Brands");

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT DISTINCT brand FROM product_list WHERE brand IS NOT NULL AND brand != '' ORDER BY brand";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboFilterBrand.Items.Add(reader["brand"].ToString());
                    }
                }
            }

            cboFilterBrand.SelectedIndex = 0; // Select "All Brands" by default
        }

        private void LoadCategories()
        {
            cboFilterCategory.Items.Clear();
            cboFilterCategory.Items.Add("All Categories");

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT DISTINCT sub_category FROM product_list WHERE sub_category IS NOT NULL AND sub_category != '' ORDER BY sub_category";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboFilterCategory.Items.Add(reader["sub_category"].ToString());
                    }
                }
            }

            cboFilterCategory.SelectedIndex = 0; // Select "All Categories" by default
        }

        private void LoadProject()
        {
            dgvProducts.Rows.Clear();  // Clear existing rows first

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT sku_upc, prod_name, description, brand, sub_category, uom,
                       outgoing, incoming, current, status
                FROM product_list
                ORDER BY id DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvProducts.Rows.Add(
                            reader["sku_upc"].ToString(),
                            reader["prod_name"].ToString(),
                            reader["description"].ToString(),
                            reader["brand"].ToString(),
                            reader["sub_category"].ToString(),
                            reader["uom"].ToString(),
                            reader["outgoing"].ToString(),
                            reader["incoming"].ToString(),
                            reader["current"].ToString(),
                            reader["status"].ToString()
                        );
                    }
                }
            }

            dgvProducts.ClearSelection();  // Prevents first row from being selected
            ApplyFilters();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CboFilterBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string searchText = txtSearch.Text.ToLower().Trim();
            string selectedBrand = cboFilterBrand.SelectedItem?.ToString();
            string selectedCategory = cboFilterCategory.SelectedItem?.ToString();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.IsNewRow) continue;

                bool matchesSearch = false;
                bool matchesBrand = false;
                bool matchesCategory = false;

                // Check search filter (search across multiple columns)
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    matchesSearch = true;
                }
                else
                {
                    matchesSearch =
                        row.Cells["SKU"].Value?.ToString().ToLower().Contains(searchText) == true ||
                        row.Cells["Name"].Value?.ToString().ToLower().Contains(searchText) == true ||
                        row.Cells["Description"].Value?.ToString().ToLower().Contains(searchText) == true ||
                        row.Cells["Brand"].Value?.ToString().ToLower().Contains(searchText) == true ||
                        row.Cells["SubCategory"].Value?.ToString().ToLower().Contains(searchText) == true;
                }

                // Check brand filter
                if (selectedBrand == "All Brands" || string.IsNullOrEmpty(selectedBrand))
                {
                    matchesBrand = true;
                }
                else
                {
                    matchesBrand = row.Cells["Brand"].Value?.ToString() == selectedBrand;
                }

                if (selectedCategory == "All Categories" || String.IsNullOrEmpty(selectedCategory))
                {
                    matchesCategory = true;
                }
                else
                {
                    matchesCategory = row.Cells["SubCategory"].Value?.ToString() == selectedCategory;
                }

                // Show row only if it matches both filters
                row.Visible = matchesSearch && matchesBrand && matchesCategory;
            }
        }
    }
}
