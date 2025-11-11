using MySql.Data.MySqlClient;

namespace KMCI_System.InventoryModule
{
    public partial class AddProduct : Form
    {
        private String companyName;

        // Product Details
        private GroupBox grpProduct;
        private TextBox txtSKU;
        private TextBox txtName;
        private TextBox txtDescription;
        private TextBox txtBrand;
        private ComboBox cmbMainCategory;
        private ComboBox cmbAdditionalCategory;
        private ComboBox cmbSubCategory; // ✅ Changed from TextBox to ComboBox
        private ComboBox cmbPrefVendor;
        private TextBox txtUOM;
        private TextBox txtBasePrice;
        private Label lblSKU;
        private Label lblName;
        private Label lblDescription;
        private Label lblBrand;
        private Label lblMainCategory;
        private Label lblAdditionalCategory;
        private Label lblSubCategory;
        private Label lblPrefVendor;
        private Label lblUOM;
        private Label lblBasePrice;

        // Status - Changed to RadioButton
        private GroupBox grpStatus;
        private RadioButton rbActive;
        private RadioButton rbInActive;

        // Buttons
        private Button btnSave;
        private Button btnCancel;

        public AddProduct()
        {
            //this.companyName = companyName;
            InitializeComponent();
            SetupForm();
            LoadMainCategories();
            LoadAdditionalCategories();
            LoadSubCategories(); // ✅ Added
            LoadVendors();
        }

        private void SetupForm()
        {
            int yPosition = 20;
            int leftMargin = 30;
            int rightMargin = 30;
            int controlWidth = 640;

            // Product GroupBox - Increased height to accommodate new fields
            grpProduct = new GroupBox
            {
                Text = "Product *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(controlWidth, 490),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(grpProduct);

            int productYPos = 30;
            int productLeftMargin = 20;

            // SKU
            lblSKU = new Label
            {
                Text = "SKU:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblSKU);

            txtSKU = new TextBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtSKU);

            // Name
            lblName = new Label
            {
                Text = "Name:",
                Location = new Point(330, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblName);

            txtName = new TextBox
            {
                Location = new Point(330, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtName);

            productYPos += 63;

            // Description
            lblDescription = new Label
            {
                Text = "Description:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblDescription);

            txtDescription = new TextBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtDescription);

            productYPos += 63;

            // Brand
            lblBrand = new Label
            {
                Text = "Brand:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblBrand);

            txtBrand = new TextBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtBrand);

            // Base Price
            lblBasePrice = new Label
            {
                Text = "Base Price:",
                Location = new Point(330, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblBasePrice);

            txtBasePrice = new TextBox
            {
                Location = new Point(330, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F),
                PlaceholderText = "0.00"
            };
            grpProduct.Controls.Add(txtBasePrice);

            productYPos += 63;

            // Main Category - EDITABLE ComboBox
            lblMainCategory = new Label
            {
                Text = "Main Category:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblMainCategory);

            cmbMainCategory = new ComboBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F),
                DropDownStyle = ComboBoxStyle.DropDown
            };
            grpProduct.Controls.Add(cmbMainCategory);

            // Additional Category - EDITABLE ComboBox
            lblAdditionalCategory = new Label
            {
                Text = "Additional Category:",
                Location = new Point(330, productYPos),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblAdditionalCategory);

            cmbAdditionalCategory = new ComboBox
            {
                Location = new Point(330, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F),
                DropDownStyle = ComboBoxStyle.DropDown
            };
            grpProduct.Controls.Add(cmbAdditionalCategory);

            productYPos += 63;

            // ✅ SubCategory - Changed to EDITABLE ComboBox
            lblSubCategory = new Label
            {
                Text = "Sub Category:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblSubCategory);

            cmbSubCategory = new ComboBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F),
                DropDownStyle = ComboBoxStyle.DropDown // ✅ Editable ComboBox
            };
            grpProduct.Controls.Add(cmbSubCategory);

            // UOM
            lblUOM = new Label
            {
                Text = "UOM:",
                Location = new Point(330, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblUOM);

            txtUOM = new TextBox
            {
                Location = new Point(330, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtUOM);

            productYPos += 63;

            // Preferred Vendor - EDITABLE ComboBox
            lblPrefVendor = new Label
            {
                Text = "Preferred Vendor:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblPrefVendor);

            cmbPrefVendor = new ComboBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F),
                DropDownStyle = ComboBoxStyle.DropDown
            };
            grpProduct.Controls.Add(cmbPrefVendor);

            productYPos += 63;

            // UPDATE yPosition before creating Status GroupBox
            yPosition += grpProduct.Height + 20;

            // Status GroupBox
            grpStatus = new GroupBox
            {
                Text = "Status *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(controlWidth, 80),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(grpStatus);

            rbActive = new RadioButton
            {
                Text = "Active",
                Location = new Point(20, 30),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F),
                Checked = true
            };
            grpStatus.Controls.Add(rbActive);

            rbInActive = new RadioButton
            {
                Text = "Inactive",
                Location = new Point(160, 30),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F)
            };
            grpStatus.Controls.Add(rbInActive);

            yPosition += grpStatus.Height + 20;

            // Save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(480, yPosition),
                Size = new Size(90, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(580, yPosition),
                Size = new Size(90, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);

            yPosition += 60;

            this.ClientSize = new Size(700, yPosition);

            int maxHeight = Screen.PrimaryScreen.WorkingArea.Height - 100;
            if (yPosition > maxHeight)
            {
                this.ClientSize = new Size(700, maxHeight);
                this.AutoScrollMinSize = new Size(700, yPosition);
            }
        }

        private void LoadMainCategories()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT main_category FROM product_list WHERE main_category IS NOT NULL AND main_category != '' ORDER BY main_category";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbMainCategory.Items.Clear();

                        while (reader.Read())
                        {
                            string category = reader["main_category"].ToString();
                            if (!string.IsNullOrWhiteSpace(category))
                            {
                                cmbMainCategory.Items.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading main categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdditionalCategories()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT additional_category FROM product_list WHERE additional_category IS NOT NULL AND additional_category != '' ORDER BY additional_category";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbAdditionalCategory.Items.Clear();

                        while (reader.Read())
                        {
                            string category = reader["additional_category"].ToString();
                            if (!string.IsNullOrWhiteSpace(category))
                            {
                                cmbAdditionalCategory.Items.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading additional categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NEW METHOD - Load Sub Categories
        private void LoadSubCategories()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT sub_category FROM product_list WHERE sub_category IS NOT NULL AND sub_category != '' ORDER BY sub_category";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbSubCategory.Items.Clear();

                        while (reader.Read())
                        {
                            string category = reader["sub_category"].ToString();
                            if (!string.IsNullOrWhiteSpace(category))
                            {
                                cmbSubCategory.Items.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sub categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVendors()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT vendor_name FROM vendor_list ORDER BY vendor_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbPrefVendor.Items.Clear();

                        while (reader.Read())
                        {
                            string vendorName = reader["vendor_name"].ToString();
                            if (!string.IsNullOrWhiteSpace(vendorName))
                            {
                                cmbPrefVendor.Items.Add(vendorName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vendors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtSKU.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(cmbSubCategory.Text) ||
                string.IsNullOrWhiteSpace(txtUOM.Text))
            {
                MessageBox.Show("Please complete all required fields (SKU, Name, Brand, Sub Category, UOM).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate base price
            if (!string.IsNullOrWhiteSpace(txtBasePrice.Text))
            {
                if (!decimal.TryParse(txtBasePrice.Text, out decimal basePrice) || basePrice < 0)
                {
                    MessageBox.Show("Please enter a valid base price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Save to database
            try
            {
                SaveProduct();
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveProduct()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Get text from ComboBox (works for both selected items and typed text)
                        string mainCategory = !string.IsNullOrWhiteSpace(cmbMainCategory.Text) ? cmbMainCategory.Text.Trim() : null;
                        string additionalCategory = !string.IsNullOrWhiteSpace(cmbAdditionalCategory.Text) ? cmbAdditionalCategory.Text.Trim() : null;
                        string subCategory = !string.IsNullOrWhiteSpace(cmbSubCategory.Text) ? cmbSubCategory.Text.Trim() : null; // ✅ Changed from txtSubCategory
                        string prefVendor = !string.IsNullOrWhiteSpace(cmbPrefVendor.Text) ? cmbPrefVendor.Text.Trim() : null;

                        // Parse base price
                        decimal? basePrice = null;
                        if (!string.IsNullOrWhiteSpace(txtBasePrice.Text) && decimal.TryParse(txtBasePrice.Text, out decimal parsedPrice))
                        {
                            basePrice = parsedPrice;
                        }

                        // Insert Product
                        string productQuery = @"
                            INSERT INTO product_list 
                            (sku_upc, prod_name, description, brand, main_category, additional_category, sub_category, 
                             uom, pref_vendor, base_price, outgoing, incoming, current, status)
                            VALUES 
                            (@sku_upc, @prod_name, @description, @brand, @main_category, @additional_category, @sub_category, 
                             @uom, @pref_vendor, @base_price, @outgoing, @incoming, @current, @status)";

                        using (MySqlCommand cmd = new MySqlCommand(productQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sku_upc", txtSKU.Text.Trim());
                            cmd.Parameters.AddWithValue("@prod_name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@brand", txtBrand.Text.Trim());
                            cmd.Parameters.AddWithValue("@main_category", string.IsNullOrWhiteSpace(mainCategory) ? DBNull.Value : (object)mainCategory);
                            cmd.Parameters.AddWithValue("@additional_category", string.IsNullOrWhiteSpace(additionalCategory) ? DBNull.Value : (object)additionalCategory);
                            cmd.Parameters.AddWithValue("@sub_category", string.IsNullOrWhiteSpace(subCategory) ? DBNull.Value : (object)subCategory);
                            cmd.Parameters.AddWithValue("@uom", txtUOM.Text.Trim());
                            cmd.Parameters.AddWithValue("@pref_vendor", string.IsNullOrWhiteSpace(prefVendor) ? DBNull.Value : (object)prefVendor);
                            cmd.Parameters.AddWithValue("@base_price", basePrice.HasValue ? (object)basePrice.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@outgoing", 0);
                            cmd.Parameters.AddWithValue("@incoming", 0);
                            cmd.Parameters.AddWithValue("@current", 0);
                            cmd.Parameters.AddWithValue("@status", rbActive.Checked ? "Active" : "Inactive");
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
