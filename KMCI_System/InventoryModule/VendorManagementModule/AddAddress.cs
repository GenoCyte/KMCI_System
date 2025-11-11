using MySql.Data.MySqlClient;

namespace KMCI_System.LogisticsModule
{
    public partial class AddAddress : Form
    {
        private String companyName;

        // Address
        private GroupBox grpAddress;
        private TextBox txtHouseNumber;
        private TextBox txtStreet;
        private TextBox txtSubdivision;
        private TextBox txtBarangay;
        private TextBox txtCity;
        private TextBox txtProvince;
        private TextBox txtRegion;
        private Label lblHouseNumber;
        private Label lblStreet;
        private Label lblSubdivision;
        private Label lblBarangay;
        private Label lblCity;
        private Label lblProvince;
        private Label lblRegion;

        // Buttons
        private Button btnSave;
        private Button btnCancel;

        public AddAddress(String companyName)
        {
            this.companyName = companyName;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            int yPosition = 20;
            int leftMargin = 30;
            int rightMargin = 30;
            int controlWidth = 640;

            // Address GroupBox
            grpAddress = new GroupBox
            {
                Text = "Address *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(controlWidth, 350),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(grpAddress);

            int addressYPos = 30;
            int addressLeftMargin = 20;

            // House Number
            lblHouseNumber = new Label
            {
                Text = "House Number:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblHouseNumber);

            txtHouseNumber = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtHouseNumber);

            // Street
            lblStreet = new Label
            {
                Text = "Street:",
                Location = new Point(330, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblStreet);

            txtStreet = new TextBox
            {
                Location = new Point(330, addressYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtStreet);

            addressYPos += 63;

            // Subdivision
            lblSubdivision = new Label
            {
                Text = "Subdivision:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblSubdivision);

            txtSubdivision = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtSubdivision);

            addressYPos += 63;

            // Barangay
            lblBarangay = new Label
            {
                Text = "Barangay:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblBarangay);

            txtBarangay = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtBarangay);

            addressYPos += 63;

            // City
            lblCity = new Label
            {
                Text = "City:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblCity);

            txtCity = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtCity);

            // Province
            lblProvince = new Label
            {
                Text = "Province:",
                Location = new Point(330, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblProvince);

            txtProvince = new TextBox
            {
                Location = new Point(330, addressYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtProvince);

            addressYPos += 63;

            // Region
            lblRegion = new Label
            {
                Text = "Region:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblRegion);

            txtRegion = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtRegion);

            yPosition += 370;

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

            if (string.IsNullOrWhiteSpace(txtBarangay.Text) || string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtProvince.Text) || string.IsNullOrWhiteSpace(txtRegion.Text))
            {
                MessageBox.Show("Please complete all required address fields (Barangay, City, Province, Region).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save to database
            try
            {
                SaveAddress();
                MessageBox.Show("Address added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAddress()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Select company
                        string companyQuery = @"
                            SELECT id
                            FROM company_list
                            WHERE company_name = @companyName";

                        int companyId;
                        using (MySqlCommand cmd = new MySqlCommand(companyQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@companyName", companyName);

                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                companyId = Convert.ToInt32(result);
                            }
                            else
                            {
                                throw new Exception("Company not found in company_list.");
                            }
                        }

                        // Insert address
                        string addressQuery = @"
                            INSERT INTO company_address 
                            (company_id, house_num, street, subdivision, barangay, city, province, region)
                            VALUES 
                            (@companyId, @house_num, @street, @subdivision, @barangay, @city, @province, @region)";

                        using (MySqlCommand cmd = new MySqlCommand(addressQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@companyId", companyId);
                            cmd.Parameters.AddWithValue("@house_num", txtHouseNumber.Text.Trim());
                            cmd.Parameters.AddWithValue("@street", txtStreet.Text.Trim());
                            cmd.Parameters.AddWithValue("@subdivision", txtSubdivision.Text.Trim());
                            cmd.Parameters.AddWithValue("@barangay", txtBarangay.Text.Trim());
                            cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                            cmd.Parameters.AddWithValue("@province", txtProvince.Text.Trim());
                            cmd.Parameters.AddWithValue("@region", txtRegion.Text.Trim());
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
