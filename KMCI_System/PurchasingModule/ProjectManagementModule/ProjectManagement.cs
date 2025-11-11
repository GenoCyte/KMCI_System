using MySql.Data.MySqlClient;

namespace KMCI_System.PurchasingModule
{
    public partial class ProjectManagement : UserControl
    {
        private DataGridView dgvProject;
        private Panel detailsPanel;
        private ProjectOverview projectDetailsControl;
        private UserControl currentUserControl;

        public ProjectManagement()
        {
            InitializeComponent();
            SetupDetailsPanel();
            SetupDataGridView();
            LoadProject();
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            // ✅ FIXED: Use PurchasingModule's AddProjectForm instead of SalesModule
            using (var addProjectForm = new PurchasingModule.AddProjectForm())
            {
                if (addProjectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProject();
                }
            }
        }

        private void SetupDetailsPanel()
        {
            detailsPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 400,
                Visible = false,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(detailsPanel);
        }

        private void SetupDataGridView()
        {
            dgvProject = new DataGridView
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
                EnableHeadersVisualStyles = false
            };

            dgvProject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvProject.Location = new Point(20, 160);
            dgvProject.Width = this.ClientSize.Width - 40;

            // Style headers
            dgvProject.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProject.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProject.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvProject.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvProject.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvProject.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvProject.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvProject.DefaultCellStyle.BackColor = Color.White;
            dgvProject.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvProject.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvProject.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProject.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvProject.GridColor = Color.FromArgb(220, 220, 220);
            dgvProject.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvProject.Columns.Add("ProjectCode", "Project Code");
            dgvProject.Columns.Add("Company", "Company");
            dgvProject.Columns.Add("Description", "Description");
            dgvProject.Columns.Add("ApprovedBudget", "Approved Budget Cost");

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
            dgvProject.Columns.Add(btnDelete);

            // Set column widths
            dgvProject.Columns["ProjectCode"].Width = 150;
            dgvProject.Columns["Company"].Width = 300;
            dgvProject.Columns["Description"].Width = 395;
            dgvProject.Columns["ApprovedBudget"].Width = 180;
            dgvProject.Columns["Actions"].Width = 80;

            // Handle button click
            dgvProject.CellContentClick += dgvProject_CellContentClick;
            dgvProject.CellClick += dgvProject_CellClick;
            dgvProject.RowsAdded += dgvProjects_RowsChanged;
            dgvProject.RowsRemoved += dgvProjects_RowsChanged;

            this.Controls.Add(dgvProject);
        }

        private void dgvProject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dgvProject.Columns["Actions"].Index)
            {
                String projectCode = dgvProject.Rows[e.RowIndex].Cells["ProjectCode"].Value.ToString();
                // ✅ Using PurchasingModule's ProjectOverview
                LoadUserControl(new ProjectOverview(projectCode));
            }
        }

        private void LoadUserControl(UserControl userControl)
        {
            var purchasingForm = this.FindForm() as PurchasingForm;
            // Clear existing controls in panel
            purchasingForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            purchasingForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void dgvProjects_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void dgvProjects_RowsChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void AdjustDataGridViewHeight()
        {
            if (dgvProject.Rows.Count == 0)
            {
                dgvProject.Height = dgvProject.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvProject.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dgvProject.Rows)
            {
                totalHeight += row.Height;
            }

            // Add small buffer for borders
            totalHeight += 2;

            dgvProject.Height = totalHeight;
        }

        private void dgvProject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProject.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this project?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    String projectCode = dgvProject.Rows[e.RowIndex].Cells["ProjectCode"].Value.ToString();
                    DeleteProject(projectCode);
                    dgvProject.Rows.RemoveAt(e.RowIndex);

                    // Hide details panel when row is deleted
                    detailsPanel.Visible = false;

                    MessageBox.Show("Project deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadProject()
        {
            dgvProject.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT project_code, company_name, description, budget_allocation
                FROM project_list
                ORDER BY id DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        decimal budgetAllocation = Convert.ToDecimal(reader["budget_allocation"]);
                        string displayValue = budgetAllocation == 0 ? "On Approval" : "₱" + budgetAllocation.ToString("N2");

                        dgvProject.Rows.Add(
                            reader["project_code"].ToString(),
                            reader["company_name"].ToString(),
                            reader["description"].ToString(),
                            displayValue
                        );
                    }
                }
            }

            AdjustDataGridViewHeight();
            dgvProject.ClearSelection();
        }

        private void DeleteProject(String projectCode)
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Delete project
                string deleteQuery = "DELETE FROM project_list WHERE project_code = @projectCode";
                using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@projectCode", projectCode);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}