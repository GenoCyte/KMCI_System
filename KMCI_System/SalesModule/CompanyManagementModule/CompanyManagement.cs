using KMCI_System.SalesModule.CompanyManagementModule;
using KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule;
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

namespace KMCI_System.SalesModule
{
    public partial class CompanyManagement : UserControl
    {
        private DataGridView dgvCompany;
        private Panel detailsPanel;
        private ProjectOverview projectDetailsControl;
        private UserControl currentUserControl;

        public CompanyManagement()
        {
            InitializeComponent();
            //SetupDetailsPanel();
            SetupDataGridView();
            LoadCompany();
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            AddCompany addCompanyForm = new AddCompany();
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
                ScrollBars = ScrollBars.Both,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false,
                Margin = new Padding(0, 0, 0, 30) // Add 30px padding at the bottom
            };

            dgvCompany.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvCompany.Location = new Point(20, 160);
            dgvCompany.Width = this.ClientSize.Width - 40;

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
            dgvCompany.Columns.Add("CompanyName", "Company Name");
            dgvCompany.Columns.Add("Tin", "TIN");
            dgvCompany.Columns.Add("Roles", "Roles");
            dgvCompany.Columns.Add("Projects", "Projects");
            dgvCompany.Columns.Add("Addresses", "Addresses");
            dgvCompany.Columns.Add("Proponents", "Proponents");

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
            dgvCompany.Columns.Add(btnDelete);

            // Set column widths
            dgvCompany.Columns["CompanyName"].MinimumWidth = 200;
            dgvCompany.Columns["CompanyName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCompany.Columns["CompanyName"].FillWeight = 100;

            dgvCompany.Columns["Tin"].MinimumWidth = 150;
            dgvCompany.Columns["Tin"].Width = 150;

            dgvCompany.Columns["Roles"].MinimumWidth = 80;
            dgvCompany.Columns["Roles"].Width = 100;

            dgvCompany.Columns["Projects"].MinimumWidth = 80;
            dgvCompany.Columns["Projects"].Width = 100;
            dgvCompany.Columns["Projects"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCompany.Columns["Addresses"].MinimumWidth = 80;
            dgvCompany.Columns["Addresses"].Width = 100;
            dgvCompany.Columns["Addresses"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCompany.Columns["Proponents"].MinimumWidth = 80;
            dgvCompany.Columns["Proponents"].Width = 100;
            dgvCompany.Columns["Proponents"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCompany.Columns["Actions"].MinimumWidth = 80;
            dgvCompany.Columns["Actions"].Width = 80;
            dgvCompany.Columns["Actions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            // Handle button click
            dgvCompany.CellContentClick += dgvCompany_CellContentClick;
            dgvCompany.CellClick += dgvCompany_CellClick;
            dgvCompany.RowsAdded += DgvCompanys_RowsChanged;
            dgvCompany.RowsRemoved += DgvCompanys_RowsChanged;

            this.Controls.Add(dgvCompany);
        }

        private void DgvCompanys_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void DgvCompanys_RowsChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void AdjustDataGridViewHeight()
        {
            if (dgvCompany.Rows.Count == 0)
            {
                dgvCompany.Height = dgvCompany.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvCompany.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dgvCompany.Rows)
            {
                totalHeight += row.Height;
            }

            // Add small buffer for borders
            totalHeight += 2;

            dgvCompany.Height = totalHeight;
        }

        private void dgvCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvCompany.Columns["Actions"].Index)
            {
                String companyName = dgvCompany.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString();
                LoadUserControl(new CompanyDetails(companyName));
            }
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

        private void dgvCompany_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCompany.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this company?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    String companyName = dgvCompany.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString();
                    DeleteCompany(companyName);
                    dgvCompany.Rows.RemoveAt(e.RowIndex);

                    // Hide details panel when row is deleted
                    //detailsPanel.Visible = false;

                    MessageBox.Show("Company deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadCompany()
        {
            dgvCompany.Rows.Clear();  // Add this line to clear existing rows first

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    c.id,
                    c.company_name,
                    c.tin,
                    COUNT(DISTINCT cl.id) AS roles,
                    COUNT(DISTINCT pj.id) AS project_count,
                    COUNT(DISTINCT a.id) AS address_count,
                    COUNT(DISTINCT p.id) AS proponent_count
                FROM company_list AS c
                LEFT JOIN company_role AS cl 
                    ON c.id = cl.company_id
                LEFT JOIN project_list AS pj 
                    ON c.id = pj.company_id
                LEFT JOIN company_address AS a 
                    ON c.id = a.company_id
                LEFT JOIN proponents AS p 
                    ON c.id = p.company_id
                GROUP BY 
                    c.id, 
                    c.company_name, 
                    c.tin;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvCompany.Rows.Add(
                            reader["company_name"].ToString(),
                            reader["tin"].ToString(),
                            reader["roles"].ToString(),
                            reader["project_count"].ToString(),
                            reader["address_count"].ToString(),
                            reader["proponent_count"].ToString()
                        );
                    }
                }
            }

            AdjustDataGridViewHeight();
            dgvCompany.ClearSelection();  // Optional: prevents first row from being selected
        }

        private void DeleteCompany(String companyName)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Delete items
                string deleteItems = "DELETE FROM company_list WHERE company_name = @company_name";
                using (MySqlCommand cmd = new MySqlCommand(deleteItems, conn))
                {
                    cmd.Parameters.AddWithValue("@company_name", companyName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
