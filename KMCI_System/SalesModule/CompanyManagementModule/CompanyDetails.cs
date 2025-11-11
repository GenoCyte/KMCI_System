using MySql.Data.MySqlClient;

namespace KMCI_System.SalesModule
{
    public partial class CompanyDetails : UserControl
    {
        private UserControl currentUserControl;
        private String companyName;
        private int companyId;
        private Label lblTin;
        private TextBox txtTin;
        private Label lblRoles;
        private FlowLayoutPanel pnlRoles; // Changed from ListBox to FlowLayoutPanel
        private Label lblHeaderAddress;
        private Button btnAddAddress;
        private DataGridView dgvAddresses;
        private Label lblHeaderProponents;
        private Button btnAddProponent;
        private DataGridView dgvProponents;

        public CompanyDetails(String companyName)
        {
            InitializeComponent();
            SetupForm();
            header.Text = companyName;
            this.companyName = companyName;
            LoadCompanyDetails();
            LoadAddresses();
            LoadProponents();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new CompanyManagement());
        }

        private void LoadUserControl(UserControl userControl)
        {
            var salesForm = this.FindForm() as SalesForm;
            // Clear existing controls in panel
            salesForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            salesForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupForm()
        {
            int yposition = 150;
            int xposition = 30;

            lblTin = new Label
            {
                Text = "TIN:",
                Location = new Point(xposition, yposition),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblTin);

            txtTin = new TextBox
            {
                Location = new Point(xposition, yposition + 20),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.White
            };
            Controls.Add(txtTin);

            xposition += 300;

            lblRoles = new Label
            {
                Text = "Roles:",
                Location = new Point(xposition, yposition),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblRoles);

            pnlRoles = new FlowLayoutPanel
            {
                Location = new Point(xposition, yposition + 20),
                Size = new Size(400, 60), // Wider to accommodate horizontal layout
                FlowDirection = FlowDirection.LeftToRight, // Arrange items left to right
                WrapContents = true, // Wrap to next line if needed
                AutoScroll = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(5)
            };
            Controls.Add(pnlRoles);

            yposition += 80;

            // Address Section
            lblHeaderAddress = new Label
            {
                Text = "Addresses",
                Location = new Point(20, yposition),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold)
            };
            Controls.Add(lblHeaderAddress);

            btnAddAddress = new Button
            {
                Text = "Add Address",
                Location = new Point(this.ClientSize.Width - 150, yposition),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Cursor = Cursors.Hand
            };
            btnAddAddress.FlatAppearance.BorderSize = 0;
            btnAddAddress.Click += BtnAddAddress_Click;
            Controls.Add(btnAddAddress);

            yposition += 40;

            // Addresses DataGridView
            dgvAddresses = new DataGridView
            {
                Location = new Point(20, yposition),
                Width = this.ClientSize.Width - 40,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
                RowTemplate = { Height = 40 },
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = true,
                EnableHeadersVisualStyles = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Style headers
            dgvAddresses.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
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
            btnDeleteAddress.DefaultCellStyle.ForeColor = Color.Red;
            btnDeleteAddress.DefaultCellStyle.SelectionForeColor = Color.Red;
            dgvAddresses.Columns.Add(btnDeleteAddress);

            dgvAddresses.CellContentClick += DgvAddresses_CellContentClick;
            dgvAddresses.RowsAdded += (s, e) => AdjustAddressGridHeight();
            dgvAddresses.RowsRemoved += (s, e) => AdjustAddressGridHeight();

            Controls.Add(dgvAddresses);

            yposition += 150; // Initial height, will be adjusted

            // Proponents Section
            lblHeaderProponents = new Label
            {
                Text = "Proponents",
                Location = new Point(20, yposition),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold)
            };
            Controls.Add(lblHeaderProponents);

            btnAddProponent = new Button
            {
                Text = "Add Proponent",
                Location = new Point(this.ClientSize.Width - 150, yposition),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Cursor = Cursors.Hand
            };
            btnAddProponent.FlatAppearance.BorderSize = 0;
            btnAddProponent.Click += BtnAddProponent_Click;
            Controls.Add(btnAddProponent);

            yposition += 40;

            // Proponents DataGridView
            dgvProponents = new DataGridView
            {
                Location = new Point(20, yposition),
                Width = this.ClientSize.Width - 40,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
                RowTemplate = { Height = 40 },
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = true,
                EnableHeadersVisualStyles = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
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
            btnDeleteProponent.DefaultCellStyle.ForeColor = Color.Red;
            btnDeleteProponent.DefaultCellStyle.SelectionForeColor = Color.Red;
            dgvProponents.Columns.Add(btnDeleteProponent);

            dgvProponents.CellContentClick += DgvProponents_CellContentClick;
            dgvProponents.RowsAdded += (s, e) => AdjustProponentGridHeight();
            dgvProponents.RowsRemoved += (s, e) => AdjustProponentGridHeight();

            Controls.Add(dgvProponents);
        }

        private void LoadCompanyDetails()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Get company details
                string query = "SELECT id, tin FROM company_list WHERE company_name = @companyName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyName", companyName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            companyId = Convert.ToInt32(reader["id"]);
                            txtTin.Text = reader["tin"].ToString();
                        }
                    }
                }

                // Get all roles for this company and display horizontally
                pnlRoles.Controls.Clear();
                string roleQuery = "SELECT role FROM company_role WHERE company_id = @companyId ORDER BY role";
                using (MySqlCommand cmd = new MySqlCommand(roleQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool hasRoles = false;
                        while (reader.Read())
                        {
                            hasRoles = true;

                            // Create a label for each role with badge-like styling
                            Label roleLabel = new Label
                            {
                                Text = reader["role"].ToString(),
                                AutoSize = true,
                                Padding = new Padding(8, 4, 8, 4),
                                Margin = new Padding(3),
                                BackColor = Color.FromArgb(230, 240, 250),
                                ForeColor = Color.FromArgb(0, 120, 215),
                                Font = new Font("Segoe UI", 9F),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            pnlRoles.Controls.Add(roleLabel);
                        }

                        // If no roles found, show a message
                        if (!hasRoles)
                        {
                            Label noRolesLabel = new Label
                            {
                                Text = "No roles assigned",
                                AutoSize = true,
                                Padding = new Padding(5),
                                ForeColor = Color.Gray,
                                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
                            };
                            pnlRoles.Controls.Add(noRolesLabel);
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
                string query = @"
                    SELECT id, house_num, street, subdivision, barangay, city, province, region 
                    FROM company_address 
                    WHERE company_id = @companyId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
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
                string query = @"
                    SELECT id, proponent_name, proponent_email, proponent_number 
                    FROM proponents 
                    WHERE company_id = @companyId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
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

        private void AdjustAddressGridHeight()
        {
            if (dgvAddresses.Rows.Count == 0)
            {
                dgvAddresses.Height = dgvAddresses.ColumnHeadersHeight + 50;
            }
            else
            {
                int totalHeight = dgvAddresses.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvAddresses.Rows)
                {
                    totalHeight += row.Height;
                }
                dgvAddresses.Height = Math.Min(totalHeight + 2, 300); // Max height 300px
            }

            // Adjust proponents section position
            int newYPosition = dgvAddresses.Location.Y + dgvAddresses.Height + 30;
            lblHeaderProponents.Location = new Point(lblHeaderProponents.Location.X, newYPosition);
            btnAddProponent.Location = new Point(btnAddProponent.Location.X, newYPosition);
            dgvProponents.Location = new Point(dgvProponents.Location.X, newYPosition + 40);
        }

        private void AdjustProponentGridHeight()
        {
            if (dgvProponents.Rows.Count == 0)
            {
                dgvProponents.Height = dgvProponents.ColumnHeadersHeight + 50;
            }
            else
            {
                int totalHeight = dgvProponents.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvProponents.Rows)
                {
                    totalHeight += row.Height;
                }
                dgvProponents.Height = Math.Min(totalHeight + 2, 300); // Max height 300px
            }
        }

        private void DgvAddresses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAddresses.Columns["DeleteAddress"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this address?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    int addressId = Convert.ToInt32(dgvAddresses.Rows[e.RowIndex].Cells["AddressId"].Value);
                    DeleteAddress(addressId);
                    dgvAddresses.Rows.RemoveAt(e.RowIndex);
                    MessageBox.Show("Address deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DgvProponents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProponents.Columns["DeleteProponent"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this proponent?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    int proponentId = Convert.ToInt32(dgvProponents.Rows[e.RowIndex].Cells["ProponentId"].Value);
                    DeleteProponent(proponentId);
                    dgvProponents.Rows.RemoveAt(e.RowIndex);
                    MessageBox.Show("Proponent deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DeleteAddress(int addressId)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "DELETE FROM company_address WHERE id = @addressId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteProponent(int proponentId)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "DELETE FROM proponents WHERE id = @proponentId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@proponentId", proponentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void BtnAddAddress_Click(object sender, EventArgs e)
        {
            AddAddress addCompanyForm = new AddAddress(companyName);
            if (addCompanyForm.ShowDialog() == DialogResult.OK)
            {
                LoadAddresses();
            }
        }

        private void BtnAddProponent_Click(object sender, EventArgs e)
        {
            AddProponents addCompanyForm = new AddProponents(companyName);
            if (addCompanyForm.ShowDialog() == DialogResult.OK)
            {
                LoadProponents();
            }
        }
    }
}
