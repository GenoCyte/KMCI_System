using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace KMCI_System.LogisticsModule
{
    public partial class AddVendor : Form
    {
        // Vendor Information
        private TextBox txtVendorName;
        private TextBox txtVendorAddress;
        private TextBox txtVendorCity;
        private TextBox txtVendorState;
        private TextBox txtVendorZip;
        private TextBox txtVendorPhone;
        private TextBox txtVendorEmail;
        private TextBox txtVendorPerson;

        private Label lblVendorName;
        private Label lblVendorAddress;
        private Label lblVendorCity;
        private Label lblVendorState;
        private Label lblVendorZip;
        private Label lblVendorPhone;
        private Label lblVendorEmail;
        private Label lblVendorPerson;

        // Buttons
        private Button btnSave;
        private Button btnCancel;

        public AddVendor()
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
            ClientSize = new Size(700, Math.Min(650, screenHeight - 100));
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Vendor";
            BackColor = Color.White;
            AutoScroll = true;
            Padding = new Padding(0, 0, 0, 30);

            ResumeLayout(false);
        }

        private void SetupForm()
        {
            int yPosition = 20;
            int leftMargin = 30;
            int rightMargin = 30;
            int controlWidth = 640;
            int halfWidth = 305;

            // Vendor Name
            lblVendorName = new Label
            {
                Text = "Vendor Name: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorName);

            txtVendorName = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtVendorName);

            yPosition += 70;

            // Contact Person
            lblVendorPerson = new Label
            {
                Text = "Contact Person: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorPerson);

            txtVendorPerson = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtVendorPerson);

            yPosition += 70;

            // Vendor Address
            lblVendorAddress = new Label
            {
                Text = "Address: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorAddress);

            txtVendorAddress = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(controlWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtVendorAddress);

            yPosition += 70;

            // City and State (side by side)
            lblVendorCity = new Label
            {
                Text = "City: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorCity);

            txtVendorCity = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(halfWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtVendorCity);

            lblVendorState = new Label
            {
                Text = "State/Province: *",
                Location = new Point(leftMargin + halfWidth + 30, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorState);

            txtVendorState = new TextBox
            {
                Location = new Point(leftMargin + halfWidth + 30, yPosition + 25),
                Size = new Size(halfWidth, 25),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(txtVendorState);

            yPosition += 70;

            // Zip Code
            lblVendorZip = new Label
            {
                Text = "Zip Code: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorZip);

            txtVendorZip = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(halfWidth, 25),
                Font = new Font("Segoe UI", 10F),
                MaxLength = 10
            };
            txtVendorZip.KeyPress += TxtVendorZip_KeyPress;
            Controls.Add(txtVendorZip);

            yPosition += 70;

            // Phone and Email (side by side)
            lblVendorPhone = new Label
            {
                Text = "Phone: *",
                Location = new Point(leftMargin, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorPhone);

            txtVendorPhone = new TextBox
            {
                Location = new Point(leftMargin, yPosition + 25),
                Size = new Size(halfWidth, 25),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "e.g., 0917-198-8306"
            };
            Controls.Add(txtVendorPhone);

            lblVendorEmail = new Label
            {
                Text = "Email: *",
                Location = new Point(leftMargin + halfWidth + 30, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            Controls.Add(lblVendorEmail);

            txtVendorEmail = new TextBox
            {
                Location = new Point(leftMargin + halfWidth + 30, yPosition + 25),
                Size = new Size(halfWidth, 25),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "e.g., vendor@example.com"
            };
            Controls.Add(txtVendorEmail);

            yPosition += 80;

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

            // Set AutoScrollMinSize to ensure all content is accessible
            this.AutoScrollMinSize = new Size(700, yPosition + 100);
        }

        // Zip Code Input: Only allow digits and some special characters
        private void TxtVendorZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, delete, and hyphen
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        // Validate email format
        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Simple email validation regex
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        // Validate phone format
        private bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove spaces, hyphens, and parentheses for validation
            string cleanPhone = Regex.Replace(phone, @"[\s\-\(\)]", "");

            // Check if it contains only digits and has reasonable length (7-15 digits)
            return Regex.IsMatch(cleanPhone, @"^\d{7,15}$");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate vendor name
            if (string.IsNullOrWhiteSpace(txtVendorName.Text))
            {
                MessageBox.Show("Please enter a vendor name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorName.Focus();
                return;
            }

            // Validate contact person
            if (string.IsNullOrWhiteSpace(txtVendorPerson.Text))
            {
                MessageBox.Show("Please enter a contact person.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorPerson.Focus();
                return;
            }

            // Validate address
            if (string.IsNullOrWhiteSpace(txtVendorAddress.Text))
            {
                MessageBox.Show("Please enter an address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorAddress.Focus();
                return;
            }

            // Validate city
            if (string.IsNullOrWhiteSpace(txtVendorCity.Text))
            {
                MessageBox.Show("Please enter a city.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorCity.Focus();
                return;
            }

            // Validate state/province
            if (string.IsNullOrWhiteSpace(txtVendorState.Text))
            {
                MessageBox.Show("Please enter a state/province.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorState.Focus();
                return;
            }

            // Validate zip code
            if (string.IsNullOrWhiteSpace(txtVendorZip.Text))
            {
                MessageBox.Show("Please enter a zip code.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorZip.Focus();
                return;
            }

            // Validate phone
            if (!ValidatePhone(txtVendorPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number (7-15 digits).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorPhone.Focus();
                return;
            }

            // Validate email
            if (!ValidateEmail(txtVendorEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorEmail.Focus();
                return;
            }

            // Check for duplicate vendor
            if (VendorExists(txtVendorName.Text.Trim()))
            {
                MessageBox.Show("A vendor with this name already exists.", "Duplicate Vendor",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVendorName.Focus();
                return;
            }

            // Save to database
            try
            {
                SaveVendor();
                MessageBox.Show("Vendor added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vendor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VendorExists(string vendorName)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM vendor_list WHERE LOWER(vendor_name) = LOWER(@vendorName)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vendorName", vendorName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void SaveVendor()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    INSERT INTO vendor_list 
                    (vendor_name, vendor_address, vendor_city, vendor_state, vendor_zip, 
                     vendor_phone, vendor_email, vendor_person)
                    VALUES 
                    (@vendorName, @vendorAddress, @vendorCity, @vendorState, @vendorZip, 
                     @vendorPhone, @vendorEmail, @vendorPerson)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vendorName", txtVendorName.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorAddress", txtVendorAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorCity", txtVendorCity.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorState", txtVendorState.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorZip", txtVendorZip.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorPhone", txtVendorPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorEmail", txtVendorEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@vendorPerson", txtVendorPerson.Text.Trim());

                    cmd.ExecuteNonQuery();
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