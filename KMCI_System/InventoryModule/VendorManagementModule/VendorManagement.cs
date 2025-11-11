using KMCI_System.InventoryModule;
using MySql.Data.MySqlClient;

namespace KMCI_System.LogisticsModule
{
    public partial class VendorManagement : UserControl
    {
        private DataGridView dgvCompany;
        private Panel detailsPanel;
        private ProjectOverview projectDetailsControl;
        private UserControl currentUserControl;

        public VendorManagement()
        {
            InitializeComponent();
            //SetupDetailsPanel();
            SetupDataGridView();
            LoadCompany();
        }

        private void btnAddVendor_Click(object sender, EventArgs e)
        {
            AddVendor addCompanyForm = new AddVendor();
            if (addCompanyForm.ShowDialog() == DialogResult.OK)
            {
                LoadCompany();
            }
        }

        private void SetupDataGridView()
        {
            dgvCompany = new DataGridView
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
                RowTemplate = { Height = 50 },
                ScrollBars = ScrollBars.Vertical,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false,
                Margin = new Padding(0, 0, 0, 30) // Add 30px padding at the bottom
            };

            dgvCompany.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCompany.Location = new Point(20, 160);
            dgvCompany.Width = this.ClientSize.Width - 40;
            dgvCompany.Height = this.ClientSize.Height - 180; // Fixed height with space for scrolling

            // Style headers
            dgvCompany.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvCompany.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCompany.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvCompany.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCompany.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCompany.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvCompany.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvCompany.DefaultCellStyle.BackColor = Color.White;
            dgvCompany.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvCompany.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCompany.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCompany.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvCompany.GridColor = Color.FromArgb(220, 220, 220);
            dgvCompany.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvCompany.Columns.Add("VendorName", "Vendor Name");
            dgvCompany.Columns.Add("Phone", "Phone");
            dgvCompany.Columns.Add("Email", "Email");

            // Add delete button column
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Actions",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.DefaultCellStyle.ForeColor = Color.Red;
            btnDelete.DefaultCellStyle.SelectionForeColor = Color.Red;
            dgvCompany.Columns.Add(btnDelete);

            // Set column widths
            dgvCompany.Columns["VendorName"].MinimumWidth = 250;
            dgvCompany.Columns["VendorName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCompany.Columns["VendorName"].FillWeight = 100;

            dgvCompany.Columns["Phone"].MinimumWidth = 150;
            dgvCompany.Columns["Phone"].Width = 180;

            dgvCompany.Columns["Email"].MinimumWidth = 200;
            dgvCompany.Columns["Email"].Width = 250;

            dgvCompany.Columns["Actions"].MinimumWidth = 80;
            dgvCompany.Columns["Actions"].Width = 80;
            dgvCompany.Columns["Actions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Handle button click
            dgvCompany.CellContentClick += dgvCompany_CellContentClick;
            dgvCompany.CellClick += dgvCompany_CellClick;

            this.Controls.Add(dgvCompany);
        }

        private void dgvCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvCompany.Columns["Actions"].Index)
            {
                String vendorName = dgvCompany.Rows[e.RowIndex].Cells["VendorName"].Value.ToString();
                LoadUserControl(new VendorDetails(vendorName));
            }
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

        private void dgvCompany_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCompany.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this vendor?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    String vendorName = dgvCompany.Rows[e.RowIndex].Cells["VendorName"].Value.ToString();
                    DeleteCompany(vendorName);
                    dgvCompany.Rows.RemoveAt(e.RowIndex);

                    MessageBox.Show("Vendor deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadCompany()
        {
            dgvCompany.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    vendor_name,
                    vendor_phone,
                    vendor_email
                FROM vendor_list
                ORDER BY vendor_name ASC;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvCompany.Rows.Add(
                            reader["vendor_name"].ToString(),
                            reader["vendor_phone"].ToString(),
                            reader["vendor_email"].ToString()
                        );
                    }
                }
            }

            dgvCompany.ClearSelection();
        }

        private void DeleteCompany(String vendorName)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM vendor_list WHERE vendor_name = @vendor_name";
                using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}