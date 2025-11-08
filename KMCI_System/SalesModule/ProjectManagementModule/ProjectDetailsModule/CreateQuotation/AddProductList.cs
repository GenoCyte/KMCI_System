using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation
{
    public partial class AddProductList : Form
    {
        private DataGridView dgvProducts;
        private ComboBox cboFilterBrand;
        private ComboBox cboFilterCategory;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnCancel;
        private List<SelectedProduct> selectedProducts = new List<SelectedProduct>();

        public class SelectedProduct
        {
            public string SKU { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Brand { get; set; }
            public decimal BasePrice { get; set; } // ✅ Add this property
        }

        public AddProductList()
        {
            InitializeComponent();
            SetupControls();
            SetupDataGridView();
            LoadBrands();
            LoadCategories();
            LoadProject();
        }

        private void SetupControls()
        {
            // Setup search textbox
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 300,
                Font = new Font("Segoe UI", 10)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            this.Controls.Add(txtSearch);

            Label lblSearch = new Label
            {
                Text = "Search:",
                Location = new Point(20, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(lblSearch);

            // Setup brand filter
            cboFilterBrand = new ComboBox
            {
                Location = new Point(340, 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboFilterBrand.SelectedIndexChanged += CboFilterBrand_SelectedIndexChanged;
            this.Controls.Add(cboFilterBrand);

            Label lblBrand = new Label
            {
                Text = "Brand:",
                Location = new Point(340, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(lblBrand);

            // Setup category filter
            cboFilterCategory = new ComboBox
            {
                Location = new Point(560, 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboFilterCategory.SelectedIndexChanged += CboFilterCategory_SelectedIndexChanged;
            this.Controls.Add(cboFilterCategory);

            Label lblCategory = new Label
            {
                Text = "Category:",
                Location = new Point(560, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(lblCategory);

            // Add Product button
            btnAdd = new Button
            {
                Text = "Add Selected Products",
                Location = new Point(this.ClientSize.Width - 320, this.ClientSize.Height - 50),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            // Cancel button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(this.ClientSize.Width - 160, this.ClientSize.Height - 50),
                Width = 140,
                Height = 35,
                BackColor = Color.FromArgb(220, 220, 220),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            selectedProducts.Clear();

            foreach (DataGridViewRow row in dgvProducts.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    selectedProducts.Add(new SelectedProduct
                    {
                        SKU = row.Cells["SKU"].Value?.ToString(),
                        Name = row.Cells["Name"].Value?.ToString(),
                        Description = row.Cells["Description"].Value?.ToString(),
                        Brand = row.Cells["Brand"].Value?.ToString(),
                        BasePrice = Convert.ToDecimal(row.Cells["Base Price"].Value) // ✅ Capture Base Price
                    });
                }
            }

            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Please select at least one product.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<SelectedProduct> GetSelectedProducts()
        {
            return selectedProducts;
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
                MultiSelect = true,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 45 },
                ScrollBars = ScrollBars.Both,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false
            };

            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.Location = new Point(20, 60);
            dgvProducts.Width = this.ClientSize.Width - 40;
            dgvProducts.Height = this.ClientSize.Height - 130;

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
            dgvProducts.Columns.Add("Base Price", "Base Price");
            dgvProducts.Columns.Add("Outgoing", "Outgoing");
            dgvProducts.Columns.Add("Incoming", "Incoming");
            dgvProducts.Columns.Add("Current", "Current");
            dgvProducts.Columns.Add("Status", "Status");

            // Set column widths
            dgvProducts.Columns["SKU"].Width = 100;
            dgvProducts.Columns["Name"].Width = 200;
            dgvProducts.Columns["Description"].Width = 250;
            dgvProducts.Columns["Brand"].Width = 120;
            dgvProducts.Columns["SubCategory"].Width = 120;
            dgvProducts.Columns["UOM"].Width = 80;
            dgvProducts.Columns["Base Price"].Width = 80;
            dgvProducts.Columns["Outgoing"].Width = 90;
            dgvProducts.Columns["Incoming"].Width = 90;
            dgvProducts.Columns["Current"].Width = 90;
            dgvProducts.Columns["Status"].Width = 100;

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

            this.Controls.Add(dgvProducts);
        }

        private void LoadBrands()
        {
            cboFilterBrand.Items.Clear();
            cboFilterBrand.Items.Add("All Brands");

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT brand FROM product_list WHERE brand IS NOT NULL AND brand != '' ORDER BY brand";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboFilterBrand.Items.Add(reader["brand"].ToString());
                    }
                }
            }

            cboFilterBrand.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            cboFilterCategory.Items.Clear();
            cboFilterCategory.Items.Add("All Categories");

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT sub_category FROM product_list WHERE sub_category IS NOT NULL AND sub_category != '' ORDER BY sub_category";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboFilterCategory.Items.Add(reader["sub_category"].ToString());
                    }
                }
            }

            cboFilterCategory.SelectedIndex = 0;
        }

        private void LoadProject()
        {
            dgvProducts.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT sku_upc, prod_name, description, brand, sub_category, uom, base_price,
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
                            reader["base_price"].ToString(),
                            reader["outgoing"].ToString(),
                            reader["incoming"].ToString(),
                            reader["current"].ToString(),
                            reader["status"].ToString()
                        );
                    }
                }
            }

            dgvProducts.ClearSelection();
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

                // Check search filter
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

                // Check category filter
                if (selectedCategory == "All Categories" || string.IsNullOrEmpty(selectedCategory))
                {
                    matchesCategory = true;
                }
                else
                {
                    matchesCategory = row.Cells["SubCategory"].Value?.ToString() == selectedCategory;
                }

                row.Visible = matchesSearch && matchesBrand && matchesCategory;
            }
        }
    }
}
