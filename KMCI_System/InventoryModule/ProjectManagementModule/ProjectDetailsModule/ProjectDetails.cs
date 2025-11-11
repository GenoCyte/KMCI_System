using MySql.Data.MySqlClient;

namespace KMCI_System.LogisticsModule
{
    public partial class ProjectDetails : UserControl
    {
        private Label lblClientName;
        private Label lblTin;
        private Label lblDescription;
        private Label lblApproveBudget;
        private Label lblStatus;
        private TextBox txtClientName;
        private TextBox txtTin;
        private TextBox txtDescription;
        private TextBox txtApproveBudget;
        private TextBox txtStatus;
        private DataGridView dgvAddresses;
        private Label lblHeaderAddress;
        private Button btnSelectAddress;
        private Label lblHeaderProponents;
        private Button btnSelectProponent;
        private DataGridView dgvProponents;
        private String projectCode;
        private int companyId;

        public ProjectDetails(string projectCode)
        {
            InitializeComponent();
            SetupForm();
            this.projectCode = projectCode;
            LoadProjectDetails();
            LoadAddresses();
            LoadProponents();
        }

        private void SetupForm()
        {
            lblClientName = new Label
            {
                Text = "Client Name:",
                Location = new Point(20, 80),
                AutoSize = true
            };
            Controls.Add(lblClientName);

            txtClientName = new TextBox
            {
                Location = new Point(150, 80),
                Width = 350
            };
            Controls.Add(txtClientName);

            lblTin = new Label
            {
                Text = "TIN:",
                Location = new Point(20, 120),
                AutoSize = true
            };
            Controls.Add(lblTin);

            txtTin = new TextBox
            {
                Location = new Point(150, 120),
                Width = 350
            };
            Controls.Add(txtTin);

            lblDescription = new Label
            {
                Text = "Description:",
                Location = new Point(20, 160),
                AutoSize = true
            };
            Controls.Add(lblDescription);

            txtDescription = new TextBox
            {
                Location = new Point(150, 160),
                Width = 350,
                Height = 60,
                Multiline = true
            };
            Controls.Add(txtDescription);

            lblApproveBudget = new Label
            {
                Text = "Approved Budget:",
                Location = new Point(550, 80),
                AutoSize = true
            };
            Controls.Add(lblApproveBudget);

            txtApproveBudget = new TextBox
            {
                Location = new Point(700, 80),
                Width = 350
            };
            Controls.Add(txtApproveBudget);

            lblStatus = new Label
            {
                Text = "Status:",
                Location = new Point(550, 120),
                AutoSize = true
            };
            Controls.Add(lblStatus);

            txtStatus = new TextBox
            {
                Location = new Point(700, 120),
                Width = 350
            };
            Controls.Add(txtStatus);

            int yposition = 250;

            // Address Section
            lblHeaderAddress = new Label
            {
                Text = "Addresses",
                Location = new Point(20, yposition),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold)
            };
            Controls.Add(lblHeaderAddress);

            btnSelectAddress = new Button
            {
                Text = "Select Address",
                Location = new Point(950, yposition),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSelectAddress.FlatAppearance.BorderSize = 0;
            btnSelectAddress.Click += BtnSelectAddress_Click;
            Controls.Add(btnSelectAddress);

            yposition += 40;

            // Addresses DataGridView
            dgvAddresses = new DataGridView
            {
                Location = new Point(20, yposition),
                Width = 1050,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
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
            dgvAddresses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvAddresses.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvAddresses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvAddresses.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAddresses.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvAddresses.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvAddresses.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvAddresses.DefaultCellStyle.BackColor = Color.White;
            dgvAddresses.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvAddresses.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvAddresses.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAddresses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvAddresses.GridColor = Color.FromArgb(220, 220, 220);
            dgvAddresses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvAddresses.Columns.Add("AddressId", "ID");
            dgvAddresses.Columns["AddressId"].Visible = false;
            dgvAddresses.Columns.Add("HouseNumber", "House #");
            dgvAddresses.Columns.Add("Street", "Street");
            dgvAddresses.Columns.Add("Subdivision", "Subdivision");
            dgvAddresses.Columns.Add("Barangay", "Barangay");
            dgvAddresses.Columns.Add("City", "City");
            dgvAddresses.Columns.Add("Province", "Province");
            dgvAddresses.Columns.Add("Region", "Region");

            // Add delete button column
            DataGridViewButtonColumn btnDeleteAddress = new DataGridViewButtonColumn
            {
                Name = "DeleteAddress",
                HeaderText = "Actions",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvAddresses.Columns.Add(btnDeleteAddress);

            dgvAddresses.RowsAdded += (s, e) => AdjustAddressGridHeight();
            dgvAddresses.RowsRemoved += (s, e) => AdjustAddressGridHeight();

            Controls.Add(dgvAddresses);

            yposition += 210;

            // Proponents Section
            lblHeaderProponents = new Label
            {
                Text = "Proponents",
                Location = new Point(20, yposition),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold)
            };
            Controls.Add(lblHeaderProponents);

            btnSelectProponent = new Button
            {
                Text = "Select Proponent",
                Location = new Point(950, yposition),
                Size = new Size(130, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSelectProponent.FlatAppearance.BorderSize = 0;
            btnSelectProponent.Click += BtnSelectProponent_Click;
            Controls.Add(btnSelectProponent);

            yposition += 40;

            // Proponents DataGridView
            dgvProponents = new DataGridView
            {
                Location = new Point(20, yposition),
                Width = 1050,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
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
            dgvProponents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProponents.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProponents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvProponents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvProponents.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvProponents.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvProponents.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvProponents.DefaultCellStyle.BackColor = Color.White;
            dgvProponents.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvProponents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvProponents.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProponents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvProponents.GridColor = Color.FromArgb(220, 220, 220);
            dgvProponents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvProponents.Columns.Add("ProponentId", "ID");
            dgvProponents.Columns["ProponentId"].Visible = false;
            dgvProponents.Columns.Add("ProponentName", "Name");
            dgvProponents.Columns.Add("ProponentEmail", "Email");
            dgvProponents.Columns.Add("ProponentNumber", "Contact Number");

            // Add delete button column
            DataGridViewButtonColumn btnDeleteProponent = new DataGridViewButtonColumn
            {
                Name = "DeleteProponent",
                HeaderText = "Actions",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvProponents.Columns.Add(btnDeleteProponent);

            dgvProponents.RowsAdded += (s, e) => AdjustProponentGridHeight();
            dgvProponents.RowsRemoved += (s, e) => AdjustProponentGridHeight();

            Controls.Add(dgvProponents);

            // Initial adjustment
            AdjustAddressGridHeight();
            AdjustProponentGridHeight();
        }

        private void AdjustAddressGridHeight()
        {
            if (dgvAddresses.Rows.Count == 0)
            {
                dgvAddresses.Height = dgvAddresses.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvAddresses.ColumnHeadersHeight;
            int maxVisibleRows = 4;
            int rowsToCount = Math.Min(dgvAddresses.Rows.Count, maxVisibleRows);

            for (int i = 0; i < rowsToCount; i++)
            {
                totalHeight += dgvAddresses.Rows[i].Height;
            }

            totalHeight += 2;
            dgvAddresses.Height = totalHeight;

            // Adjust proponents section position
            int newYPosition = dgvAddresses.Location.Y + dgvAddresses.Height + 20;
            lblHeaderProponents.Location = new Point(20, newYPosition);
            btnSelectProponent.Location = new Point(950, newYPosition);
            dgvProponents.Location = new Point(20, newYPosition + 40);
        }

        private void AdjustProponentGridHeight()
        {
            if (dgvProponents.Rows.Count == 0)
            {
                dgvProponents.Height = dgvProponents.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvProponents.ColumnHeadersHeight;
            int maxVisibleRows = 4;
            int rowsToCount = Math.Min(dgvProponents.Rows.Count, maxVisibleRows);

            for (int i = 0; i < rowsToCount; i++)
            {
                totalHeight += dgvProponents.Rows[i].Height;
            }

            totalHeight += 2;
            dgvProponents.Height = totalHeight;
        }

        private void LoadProjectDetails()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Get company details
                string query = @"
                    SELECT company_id, company_name, tin, description, budget_allocation, status
                    FROM project_list
                    WHERE project_code = @project_code";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@project_code", projectCode);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            companyId = Convert.ToInt32(reader["company_id"]);
                            txtClientName.Text = reader["company_name"].ToString();
                            txtTin.Text = reader["tin"].ToString();
                            txtDescription.Text = reader["description"].ToString();
                            txtApproveBudget.Text = reader["budget_allocation"].ToString();
                            txtStatus.Text = reader["status"].ToString();
                        }
                    }
                }
            }
        }

        private void LoadAddresses()
        {
            dgvAddresses.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Load address based on address_id from project_list
                string query = @"
                    SELECT ca.id, ca.house_num, ca.street, ca.subdivision, ca.barangay, ca.city, ca.province, ca.region 
                    FROM company_address ca
                    INNER JOIN project_list pl ON ca.id = pl.address_id
                    WHERE pl.project_code = @projectCode AND pl.company_id = @companyId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@projectCode", projectCode);
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dgvAddresses.Rows.Add(
                                reader["id"].ToString(),
                                reader["house_num"].ToString(),
                                reader["street"].ToString(),
                                reader["subdivision"].ToString(),
                                reader["barangay"].ToString(),
                                reader["city"].ToString(),
                                reader["province"].ToString(),
                                reader["region"].ToString()
                            );
                        }
                    }
                }
            }

            AdjustAddressGridHeight();
            dgvAddresses.ClearSelection();
        }

        private void LoadProponents()
        {
            dgvProponents.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Load proponent based on proponent_id from project_list
                string query = @"
                    SELECT p.id, p.proponent_name, p.proponent_email, p.proponent_number 
                    FROM proponents p
                    INNER JOIN project_list pl ON p.id = pl.proponent_id
                    WHERE pl.project_code = @projectCode AND pl.company_id = @companyId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@projectCode", projectCode);
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dgvProponents.Rows.Add(
                                reader["id"].ToString(),
                                reader["proponent_name"].ToString(),
                                reader["proponent_email"].ToString(),
                                reader["proponent_number"].ToString()
                            );
                        }
                    }
                }
            }

            AdjustProponentGridHeight();
            dgvProponents.ClearSelection();
        }

        private void BtnSelectProponent_Click(object sender, EventArgs e)
        {
            // Load all available proponents for this company
            List<ProponentData> availableProponents = new List<ProponentData>();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT id, proponent_name, proponent_email, proponent_number 
                    FROM proponents 
                    WHERE company_id = @companyId
                    ORDER BY proponent_name";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableProponents.Add(new ProponentData
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["proponent_name"].ToString(),
                                Email = reader["proponent_email"].ToString(),
                                Number = reader["proponent_number"].ToString()
                            });
                        }
                    }
                }
            }

            if (availableProponents.Count == 0)
            {
                MessageBox.Show("No proponents found for this company.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create selection dialog
            using (Form selectForm = new Form())
            {
                selectForm.Text = "Select Proponent";
                selectForm.Size = new Size(600, 450);
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;

                Label lblInfo = new Label
                {
                    Text = "Select a proponent for this project:",
                    Location = new Point(20, 20),
                    Size = new Size(560, 20),
                    Font = new Font("Segoe UI", 10F)
                };

                ListBox listBox = new ListBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(540, 300),
                    Font = new Font("Segoe UI", 10F),
                    DisplayMember = "DisplayText"
                };

                // Add proponents to listbox with formatted display
                foreach (var proponent in availableProponents)
                {
                    listBox.Items.Add(new
                    {
                        DisplayText = $"{proponent.Name} - {proponent.Email} - {proponent.Number}",
                        ProponentId = proponent.Id
                    });
                }

                if (listBox.Items.Count > 0)
                    listBox.SelectedIndex = 0;

                Button btnSelect = new Button
                {
                    Text = "Select",
                    Location = new Point(340, 370),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(0, 120, 215),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F)
                };
                btnSelect.FlatAppearance.BorderSize = 0;

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(450, 370),
                    Size = new Size(100, 35),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F)
                };
                btnCancel.FlatAppearance.BorderSize = 0;

                btnSelect.Click += (s, ev) =>
                {
                    if (listBox.SelectedIndex >= 0)
                    {
                        dynamic selectedItem = listBox.SelectedItem;
                        int selectedProponentId = selectedItem.ProponentId;

                        // Update proponent_id in project_list table
                        using (MySqlConnection conn = new MySqlConnection(connString))
                        {
                            try
                            {
                                conn.Open();
                                string updateQuery = @"
                                    UPDATE project_list 
                                    SET proponent_id = @proponentId 
                                    WHERE project_code = @projectCode";

                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@proponentId", selectedProponentId);
                                    cmd.Parameters.AddWithValue("@projectCode", projectCode);

                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Proponent updated successfully.", "Success",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // Reload the proponents grid to show the selected proponent
                                        LoadProponents();
                                        selectForm.DialogResult = DialogResult.OK;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to update proponent.", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error updating proponent: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };

                btnCancel.Click += (s, ev) => selectForm.DialogResult = DialogResult.Cancel;

                selectForm.Controls.AddRange(new Control[] { lblInfo, listBox, btnSelect, btnCancel });
                selectForm.ShowDialog(this);
            }
        }

        private void BtnSelectAddress_Click(object sender, EventArgs e)
        {
            // Load all available addresses for this company
            List<AddressData> availableAddresses = new List<AddressData>();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT id, house_num, street, subdivision, barangay, city, province, region 
                    FROM company_address 
                    WHERE company_id = @companyId
                    ORDER BY city, barangay";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableAddresses.Add(new AddressData
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                HouseNum = reader["house_num"].ToString(),
                                Street = reader["street"].ToString(),
                                Subdivision = reader["subdivision"].ToString(),
                                Barangay = reader["barangay"].ToString(),
                                City = reader["city"].ToString(),
                                Province = reader["province"].ToString(),
                                Region = reader["region"].ToString()
                            });
                        }
                    }
                }
            }

            if (availableAddresses.Count == 0)
            {
                MessageBox.Show("No addresses found for this company.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create selection dialog
            using (Form selectForm = new Form())
            {
                selectForm.Text = "Select Address";
                selectForm.Size = new Size(700, 450);
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;

                Label lblInfo = new Label
                {
                    Text = "Select an address for this project:",
                    Location = new Point(20, 20),
                    Size = new Size(660, 20),
                    Font = new Font("Segoe UI", 10F)
                };

                ListBox listBox = new ListBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(640, 300),
                    Font = new Font("Segoe UI", 9F),
                    DisplayMember = "DisplayText"
                };

                // Add addresses to listbox with formatted display
                foreach (var address in availableAddresses)
                {
                    string addressDisplay = $"{address.HouseNum} {address.Street}";
                    if (!string.IsNullOrEmpty(address.Subdivision))
                        addressDisplay += $", {address.Subdivision}";
                    addressDisplay += $", {address.Barangay}, {address.City}, {address.Province}";

                    listBox.Items.Add(new
                    {
                        DisplayText = addressDisplay,
                        AddressId = address.Id
                    });
                }

                if (listBox.Items.Count > 0)
                    listBox.SelectedIndex = 0;

                Button btnSelect = new Button
                {
                    Text = "Select",
                    Location = new Point(440, 370),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(0, 120, 215),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F)
                };
                btnSelect.FlatAppearance.BorderSize = 0;

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(550, 370),
                    Size = new Size(100, 35),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F)
                };
                btnCancel.FlatAppearance.BorderSize = 0;

                btnSelect.Click += (s, ev) =>
                {
                    if (listBox.SelectedIndex >= 0)
                    {
                        dynamic selectedItem = listBox.SelectedItem;
                        int selectedAddressId = selectedItem.AddressId;

                        // Update address_id in project_list table
                        using (MySqlConnection conn = new MySqlConnection(connString))
                        {
                            try
                            {
                                conn.Open();
                                string updateQuery = @"
                                    UPDATE project_list 
                                    SET address_id = @addressId 
                                    WHERE project_code = @projectCode";

                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@addressId", selectedAddressId);
                                    cmd.Parameters.AddWithValue("@projectCode", projectCode);

                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Address updated successfully.", "Success",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // Reload the addresses grid to show the selected address
                                        LoadAddresses();
                                        selectForm.DialogResult = DialogResult.OK;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to update address.", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error updating address: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };

                btnCancel.Click += (s, ev) => selectForm.DialogResult = DialogResult.Cancel;

                selectForm.Controls.AddRange(new Control[] { lblInfo, listBox, btnSelect, btnCancel });
                selectForm.ShowDialog(this);
            }
        }

        private class ProponentData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Number { get; set; }
        }

        private class AddressData
        {
            public int Id { get; set; }
            public string HouseNum { get; set; }
            public string Street { get; set; }
            public string Subdivision { get; set; }
            public string Barangay { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Region { get; set; }
        }
    }
}
