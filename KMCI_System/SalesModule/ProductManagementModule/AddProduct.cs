using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.SalesModule.ProductManagementModule
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
        private TextBox txtSubCategory;
        private TextBox txtUOM;
        private Label lblSKU;
        private Label lblName;
        private Label lblDescription;
        private Label lblBrand;
        private Label lblSubCategory;
        private Label lblUOM;

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
        }

        private void SetupForm()
        {
            int yPosition = 20;
            int leftMargin = 30;
            int rightMargin = 30;
            int controlWidth = 640;

            // Product GroupBox
            grpProduct = new GroupBox
            {
                Text = "Product *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(controlWidth, 300),
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
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtBrand);

            productYPos += 63;

            // SubCategory
            lblSubCategory = new Label
            {
                Text = "Sub Category:",
                Location = new Point(productLeftMargin, productYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(lblSubCategory);

            txtSubCategory = new TextBox
            {
                Location = new Point(productLeftMargin, productYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProduct.Controls.Add(txtSubCategory);

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

            // UPDATE yPosition before creating Status GroupBox
            yPosition += grpProduct.Height + 20; // Add spacing after Product group

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
                Checked = true // Default to Active
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

            yPosition += grpStatus.Height + 20; // Update for buttons below

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

            yPosition += 60; // Add some bottom spacing

            // NOW set the form size to fit all content
            this.ClientSize = new Size(700, yPosition);

            // Update AutoScrollMinSize (in case content is taller than screen)
            int maxHeight = Screen.PrimaryScreen.WorkingArea.Height - 100;
            if (yPosition > maxHeight)
            {
                this.ClientSize = new Size(700, maxHeight);
                this.AutoScrollMinSize = new Size(700, yPosition);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs

            if (string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txtSubCategory.Text) ||
                string.IsNullOrWhiteSpace(txtUOM.Text))
            {
                MessageBox.Show("Please complete all required address fields (Barangay, City, Province, Region).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
                        // Insert Product
                        string addressQuery = @"
                            INSERT INTO product_list 
                            (sku_upc, prod_name, description, brand, sub_category, uom, outgoing, incoming, current, status)
                            VALUES 
                            (@sku_upc, @prod_name, @description, @brand, @sub_category, @uom, @outgoing, @incoming, @current, @active_discon)";

                        using (MySqlCommand cmd = new MySqlCommand(addressQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sku_upc", txtSKU.Text.Trim());
                            cmd.Parameters.AddWithValue("@prod_name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                            cmd.Parameters.AddWithValue("@brand", txtBrand.Text.Trim());
                            cmd.Parameters.AddWithValue("@sub_category", txtSubCategory.Text.Trim());
                            cmd.Parameters.AddWithValue("@uom", txtUOM.Text.Trim());
                            cmd.Parameters.AddWithValue("@outgoing", 0);
                            cmd.Parameters.AddWithValue("@incoming", 0);
                            cmd.Parameters.AddWithValue("@current", 0);
                            cmd.Parameters.AddWithValue("@active_discon", rbActive.Checked ? "Active" : "Inactive");
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
