using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule
{
    public partial class AddProjectForm : Form
    {
        private TextBox txtProjectCode;
        private ComboBox cboCompanyName;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;
        private Label lblProjectCode;
        private Label lblCompanyName;
        private Label lblDescription;

        public AddProjectForm()
        {
            InitializeComponent();
            SetupForm();
            GenerateProjectCode();
            LoadClientCompanies();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // Form properties
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 350);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Project";
            BackColor = Color.White;

            ResumeLayout(false);
        }

        private void SetupForm()
        {
            // Project Code Label
            lblProjectCode = new Label
            {
                Text = "Project Code:",
                Location = new Point(30, 30),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblProjectCode);

            // Project Code TextBox
            txtProjectCode = new TextBox
            {
                Location = new Point(30, 55),
                Size = new Size(440, 25),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(txtProjectCode);

            // Company Name Label
            lblCompanyName = new Label
            {
                Text = "Company Name:",
                Location = new Point(30, 95),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblCompanyName);

            // Company Name ComboBox (Changed from TextBox)
            cboCompanyName = new ComboBox
            {
                Location = new Point(30, 120),
                Size = new Size(440, 25),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboCompanyName);

            // Description Label
            lblDescription = new Label
            {
                Text = "Description:",
                Location = new Point(30, 160),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(lblDescription);

            // Description TextBox
            txtDescription = new TextBox
            {
                Location = new Point(30, 185),
                Size = new Size(440, 60),
                Font = new Font("Segoe UI", 10F),
                Multiline = true
            };
            Controls.Add(txtDescription);

            // Save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(280, 300),
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
                Location = new Point(380, 300),
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
        }

        private void GenerateProjectCode()
        {
            try
            {
                string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Get last 2 digits of current year
                    string yearSuffix = DateTime.Now.Year.ToString().Substring(2);

                    // Get the highest project number for the current year
                    string maxQuery = @"
                        SELECT MAX(CAST(SUBSTRING(project_code, 8) AS UNSIGNED)) 
                        FROM project_list 
                        WHERE project_code LIKE @yearPattern";

                    using (MySqlCommand cmd = new MySqlCommand(maxQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@yearPattern", $"PROJ{yearSuffix}-%");

                        object result = cmd.ExecuteScalar();
                        int nextNumber = (result == null || result == DBNull.Value) ? 1 : Convert.ToInt32(result) + 1;

                        // Format: PROJ + Last 2 digits of year + 5-digit number
                        string projectCode = $"PROJ{yearSuffix}-{nextNumber:D5}";

                        txtProjectCode.Text = projectCode;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating project code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProjectCode.Text = "ERROR";
            }
        }

        private void LoadClientCompanies()
        {
            cboCompanyName.Items.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT DISTINCT c.company_name 
                    FROM company_list c
                    INNER JOIN company_role cr ON c.id = cr.company_id
                    WHERE cr.role = 'Client'
                    ORDER BY c.company_name";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboCompanyName.Items.Add(reader["company_name"].ToString());
                    }
                }
            }

            if (cboCompanyName.Items.Count > 0)
            {
                cboCompanyName.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtProjectCode.Text) || txtProjectCode.Text == "ERROR")
            {
                MessageBox.Show("Invalid project code. Please restart the form.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCompanyName.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboCompanyName.Text))
            {
                MessageBox.Show("Please select a company name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCompanyName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }

            // Save to database
            try
            {
                SaveProject();
                MessageBox.Show("Project added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCompanyId()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT id
                    FROM company_list
                    WHERE company_name = @companyName";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyName", cboCompanyName.Text.Trim());

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    throw new Exception("Company not found in database.");
                }
            }
        }

        private string GetCompanyTin()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT tin
                    FROM company_list
                    WHERE company_name = @companyName";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@companyName", cboCompanyName.Text.Trim());

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }

                    throw new Exception("Company not found in database.");
                }
            }
        }

        private void SaveProject()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO project_list (project_code, company_id, company_name, tin,  description, budget_allocation, status)
                    VALUES (@projectCode, @company_id, @companyName, @tin, @description, @budgetAllocation, @status)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@projectCode", txtProjectCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@company_id", GetCompanyId());
                    cmd.Parameters.AddWithValue("@companyName", cboCompanyName.Text.Trim());
                    cmd.Parameters.AddWithValue("@tin", GetCompanyTin());
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@budgetAllocation", 0);
                    cmd.Parameters.AddWithValue("@status", "Under Negotiation");
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