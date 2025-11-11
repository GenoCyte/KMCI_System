using MySql.Data.MySqlClient;
using KMCI_System.InventoryModule;

namespace KMCI_System.LogisticsModule
{
    public partial class VendorDetails : UserControl
    {
        private UserControl currentUserControl;
        private String vendorName;
        private int vendorId;

        // Vendor detail controls
        private Label lblVendorName;
        private TextBox txtVendorName;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblState;
        private TextBox txtState;
        private Label lblZip;
        private TextBox txtZip;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblContactPerson;
        private TextBox txtContactPerson;
        private Button btnEdit;
        private Button btnSave;
        private Button btnCancel;

        public VendorDetails(String vendorName)
        {
            InitializeComponent();
            SetupForm();
            header.Text = vendorName;
            this.vendorName = vendorName;
            LoadVendorDetails();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new VendorManagement());
        }

        private void LoadUserControl(UserControl userControl)
        {
            var inventoryForm = this.FindForm() as InventoryForm;
            // Clear existing controls in panel
            inventoryForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            inventoryForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupForm()
        {
            int yPosition = 150;
            int xPosition = 30;
            int labelWidth = 150;
            int textBoxWidth = 300;
            int verticalSpacing = 60;

            // Vendor Name
            lblVendorName = new Label
            {
                Text = "Vendor Name:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblVendorName);

            txtVendorName = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtVendorName);

            // Address
            yPosition += verticalSpacing;
            lblAddress = new Label
            {
                Text = "Address:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblAddress);

            txtAddress = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtAddress);

            // City
            yPosition += verticalSpacing;
            lblCity = new Label
            {
                Text = "City:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblCity);

            txtCity = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtCity);

            // State
            yPosition += verticalSpacing;
            lblState = new Label
            {
                Text = "State:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblState);

            txtState = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtState);

            // Second column
            xPosition = 380;
            yPosition = 150;

            // Zip
            lblZip = new Label
            {
                Text = "Zip Code:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblZip);

            txtZip = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtZip);

            // Phone
            yPosition += verticalSpacing;
            lblPhone = new Label
            {
                Text = "Phone:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblPhone);

            txtPhone = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtPhone);

            // Email
            yPosition += verticalSpacing;
            lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtEmail);

            // Contact Person
            yPosition += verticalSpacing;
            lblContactPerson = new Label
            {
                Text = "Contact Person:",
                Location = new Point(xPosition, yPosition),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblContactPerson);

            txtContactPerson = new TextBox
            {
                Location = new Point(xPosition, yPosition + 25),
                Size = new Size(textBoxWidth, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtContactPerson);

            // Buttons
            yPosition += verticalSpacing + 20;
            xPosition = 30;

            btnEdit = new Button
            {
                Text = "Edit Details",
                Location = new Point(xPosition, yPosition),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;
            Controls.Add(btnEdit);

            btnSave = new Button
            {
                Text = "Save Changes",
                Location = new Point(xPosition, yPosition),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 150, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(xPosition + 130, yPosition),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);
        }

        private void LoadVendorDetails()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Get vendor details
                string query = @"SELECT id, vendor_name, vendor_address, vendor_city, vendor_state, 
                                 vendor_zip, vendor_phone, vendor_email, vendor_person 
                                 FROM vendor_list WHERE vendor_name = @vendorName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vendorName", vendorName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vendorId = Convert.ToInt32(reader["id"]);
                            txtVendorName.Text = reader["vendor_name"]?.ToString() ?? "";
                            txtAddress.Text = reader["vendor_address"]?.ToString() ?? "";
                            txtCity.Text = reader["vendor_city"]?.ToString() ?? "";
                            txtState.Text = reader["vendor_state"]?.ToString() ?? "";
                            txtZip.Text = reader["vendor_zip"]?.ToString() ?? "";
                            txtPhone.Text = reader["vendor_phone"]?.ToString() ?? "";
                            txtEmail.Text = reader["vendor_email"]?.ToString() ?? "";
                            txtContactPerson.Text = reader["vendor_person"]?.ToString() ?? "";
                        }
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            // Enable editing
            txtVendorName.ReadOnly = false;
            txtVendorName.BackColor = Color.White;
            txtAddress.ReadOnly = false;
            txtAddress.BackColor = Color.White;
            txtCity.ReadOnly = false;
            txtCity.BackColor = Color.White;
            txtState.ReadOnly = false;
            txtState.BackColor = Color.White;
            txtZip.ReadOnly = false;
            txtZip.BackColor = Color.White;
            txtPhone.ReadOnly = false;
            txtPhone.BackColor = Color.White;
            txtEmail.ReadOnly = false;
            txtEmail.BackColor = Color.White;
            txtContactPerson.ReadOnly = false;
            txtContactPerson.BackColor = Color.White;

            // Toggle buttons
            btnEdit.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtVendorName.Text))
            {
                MessageBox.Show("Vendor name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"UPDATE vendor_list SET 
                                 vendor_name = @vendorName,
                                 vendor_address = @vendorAddress,
                                 vendor_city = @vendorCity,
                                 vendor_state = @vendorState,
                                 vendor_zip = @vendorZip,
                                 vendor_phone = @vendorPhone,
                                 vendor_email = @vendorEmail,
                                 vendor_person = @vendorPerson
                                 WHERE id = @vendorId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vendorName", txtVendorName.Text);
                    cmd.Parameters.AddWithValue("@vendorAddress", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@vendorCity", txtCity.Text);
                    cmd.Parameters.AddWithValue("@vendorState", txtState.Text);
                    cmd.Parameters.AddWithValue("@vendorZip", txtZip.Text);
                    cmd.Parameters.AddWithValue("@vendorPhone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@vendorEmail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@vendorPerson", txtContactPerson.Text);
                    cmd.Parameters.AddWithValue("@vendorId", vendorId);

                    cmd.ExecuteNonQuery();
                }
            }

            // Update the vendor name if changed
            vendorName = txtVendorName.Text;
            header.Text = vendorName;

            // Disable editing
            SetReadOnly(true);

            // Toggle buttons
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;

            MessageBox.Show("Vendor details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Reload original data
            LoadVendorDetails();

            // Disable editing
            SetReadOnly(true);

            // Toggle buttons
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        private void SetReadOnly(bool readOnly)
        {
            txtVendorName.ReadOnly = readOnly;
            txtAddress.ReadOnly = readOnly;
            txtCity.ReadOnly = readOnly;
            txtState.ReadOnly = readOnly;
            txtZip.ReadOnly = readOnly;
            txtPhone.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            txtContactPerson.ReadOnly = readOnly;

            Color backColor = readOnly ? Color.White : Color.White;
            txtVendorName.BackColor = backColor;
            txtAddress.BackColor = backColor;
            txtCity.BackColor = backColor;
            txtState.BackColor = backColor;
            txtZip.BackColor = backColor;
            txtPhone.BackColor = backColor;
            txtEmail.BackColor = backColor;
            txtContactPerson.BackColor = backColor;
        }
    }
}
