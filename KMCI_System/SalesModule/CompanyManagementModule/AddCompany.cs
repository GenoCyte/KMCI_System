using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace KMCI_System.SalesModule
{
    public partial class AddCompany : Form
    {
        // Company Information
        private TextBox txtCompanyName;
        private TextBox txtTIN;
        private Label lblCompanyName;
        private Label lblTIN;

        // Address
        private GroupBox grpAddress;
        private TextBox txtHouseNumber;
        private TextBox txtStreet;
        private TextBox txtSubdivision;
        private TextBox txtBarangay;
        private TextBox txtCity;
        private TextBox txtProvince;
        private TextBox txtZip;
        private Label lblHouseNumber;
        private Label lblStreet;
        private Label lblSubdivision;
        private Label lblBarangay;
        private Label lblCity;
        private Label lblProvince;
        private Label lblZip;

        // Proponents
        private GroupBox grpProponents;
        private TextBox txtProponentName;
        private TextBox txtProponentEmail;
        private TextBox txtProponentNumber;
        private Label lblProponentName;
        private Label lblProponentEmail;
        private Label lblProponentNumber;

        // Buttons
        private Button btnSave;
        private Button btnCancel;

        public AddCompany()
        {
            InitializeComponent();
            SetupForm();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // Get the screen height
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // Form properties
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, screenHeight);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Company";
            BackColor = Color.White;
            AutoScroll = true;
            Padding = new Padding(0, 0, 0, 30); // Add padding at the bottom

            ResumeLayout(false);
        }

        private void SetupForm()
        {
            int yPosition = 20;
            int leftMargin = 30;
            int rightMargin = 30;
            int controlWidth = 640;

            // Company Name
            lblCompanyName = new Label
            {
                Text = "Company Name: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblCompanyName);

            txtCompanyName = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtCompanyName);

            yPosition += 70;

            // TIN
            lblTIN = new Label
            {
                Text = "TIN: * (12 digits)",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblTIN);

            txtTIN = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F),
                MaxLength = 12 // Only 12 digits
            };
            txtTIN.KeyPress += TxtTIN_KeyPress;
            Controls.Add(txtTIN);

            yPosition += 70;

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
            lblZip = new Label
            {
                Text = "Zip:",
                Location = new Point(addressLeftMargin, addressYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(lblZip);

            txtZip = new TextBox
            {
                Location = new Point(addressLeftMargin, addressYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpAddress.Controls.Add(txtZip);

            yPosition += 370;

            // Proponents GroupBox
            grpProponents = new GroupBox
            {
                Text = "Proponent Information *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(controlWidth, 230),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(grpProponents);

            int proponentYPos = 30;
            int proponentLeftMargin = 20;

            // Proponent Name
            lblProponentName = new Label
            {
                Text = "Name:",
                Location = new Point(proponentLeftMargin, proponentYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(lblProponentName);

            txtProponentName = new TextBox
            {
                Location = new Point(proponentLeftMargin, proponentYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(txtProponentName);

            proponentYPos += 63;

            // Proponent Email
            lblProponentEmail = new Label
            {
                Text = "Email:",
                Location = new Point(proponentLeftMargin, proponentYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(lblProponentEmail);

            txtProponentEmail = new TextBox
            {
                Location = new Point(proponentLeftMargin, proponentYPos + 23),
                Size = new Size(590, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(txtProponentEmail);

            proponentYPos += 63;

            // Proponent Number
            lblProponentNumber = new Label
            {
                Text = "Contact Number:",
                Location = new Point(proponentLeftMargin, proponentYPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(lblProponentNumber);

            txtProponentNumber = new TextBox
            {
                Location = new Point(proponentLeftMargin, proponentYPos + 23),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F)
            };
            grpProponents.Controls.Add(txtProponentNumber);

            yPosition += 260;

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

            // Set AutoScrollMinSize to ensure all content is accessible with bottom margin
            this.AutoScrollMinSize = new Size(700, yPosition + 100);
        }

        // TIN Input: Only allow digits (no formatting)
        private void TxtTIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and delete
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        // Validate TIN (12 digits only)
        private bool ValidateTIN(string tin)
        {
            // Must be exactly 12 digits
            if (tin.Length != 12)
            {
                return false;
            }

            // Must contain only digits
            if (!Regex.IsMatch(tin, @"^\d{12}$"))
            {
                return false;
            }

            return true;
        }

        // Format TIN for storage: 123456789012 -> 123-456-789-012
        private string FormatTIN(string tin)
        {
            if (tin.Length == 12)
            {
                return $"{tin.Substring(0, 3)}-{tin.Substring(3, 3)}-{tin.Substring(6, 3)}-{tin.Substring(9, 3)}";
            }
            return tin;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Please enter a company name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompanyName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTIN.Text))
            {
                MessageBox.Show("Please enter a TIN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTIN.Focus();
                return;
            }

            // Validate TIN (must be exactly 12 digits)
            if (!ValidateTIN(txtTIN.Text.Trim()))
            {
                MessageBox.Show("Please enter exactly 12 digits for TIN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTIN.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBarangay.Text) || string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtProvince.Text) || string.IsNullOrWhiteSpace(txtZip.Text))
            {
                MessageBox.Show("Please complete all required address fields (Barangay, City, Province, Region).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save to database
            try
            {
                SaveCompany();
                MessageBox.Show("Company added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving company: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveCompany()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Format TIN before saving (123456789012 -> 123-456-789-012)
                        string formattedTIN = FormatTIN(txtTIN.Text.Trim());

                        // Insert company
                        string companyQuery = @"
                            INSERT INTO company_list 
                            (company_name, tin)
                            VALUES 
                            (@companyName, @tin);
                            SELECT LAST_INSERT_ID();";

                        int companyId;
                        using (MySqlCommand cmd = new MySqlCommand(companyQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@companyName", txtCompanyName.Text.Trim());
                            cmd.Parameters.AddWithValue("@tin", formattedTIN);

                            companyId = Convert.ToInt32(cmd.ExecuteScalar());
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
                            cmd.Parameters.AddWithValue("@region", txtZip.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        // Insert proponent
                        string proponentQuery = @"
                            INSERT INTO proponents 
                            (company_id, proponent_name, proponent_email, proponent_number)
                            VALUES 
                            (@companyId, @name, @email, @number)";

                        using (MySqlCommand cmd = new MySqlCommand(proponentQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@companyId", companyId);
                            cmd.Parameters.AddWithValue("@name", txtProponentName.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", txtProponentEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@number", txtProponentNumber.Text.Trim());
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