using MySql.Data.MySqlClient;

namespace KMCI_System.LogisticsModule
{
    public partial class AddProponents : Form
    {
        private String companyName;

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

        public AddProponents(String companyName)
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
            if (string.IsNullOrWhiteSpace(txtProponentName.Text))
            {
                MessageBox.Show("Please enter a proponent name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProponentName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProponentEmail.Text))
            {
                MessageBox.Show("Please enter a proponent email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProponentEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProponentNumber.Text))
            {
                MessageBox.Show("Please enter a proponent contact number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProponentNumber.Focus();
                return;
            }

            // Save to database
            try
            {
                SaveProponents();
                MessageBox.Show("Address added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveProponents()
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
                            INSERT INTO proponents
                            (company_id, proponent_name, proponent_email, proponent_number)
                            VALUES 
                            (@companyId, @proponentName, @proponentEmail, @proponentNumber)";

                        using (MySqlCommand cmd = new MySqlCommand(addressQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@companyId", companyId);
                            cmd.Parameters.AddWithValue("@proponentName", txtProponentName.Text.Trim());
                            cmd.Parameters.AddWithValue("@proponentEmail", txtProponentEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@proponentNumber", txtProponentNumber.Text.Trim());
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
