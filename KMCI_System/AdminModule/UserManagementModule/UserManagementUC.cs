using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KMCI_System.AdminModule.UserManagementModule
{
    public class UserManagementUC : UserControl
    {
        private Panel headerPanel;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2DataGridView dgv;
        private Panel topPanel;
        private Guna2Button btnBottomRight;
        private Guna2Button btnAddUser;
        private Guna2Button btnEdit;
        private Guna2HtmlLabel tbid;



        public UserManagementUC()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            btnEdit = new Guna2Button()
            {
                Text = "Edit",
                Size = new Size(120, 36),
                FillColor = Color.Black,
                ForeColor = Color.White,
                BorderRadius = 10,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

                btnEdit.Location = new Point(
                this.ClientSize.Width - btnEdit.Width - 150,
                this.ClientSize.Height - btnEdit.Height - 24
    );

            this.SizeChanged += (s, e) =>
            {
                btnEdit.Location = new Point(
                    this.ClientSize.Width - btnEdit.Width - 150,
                    this.ClientSize.Height - btnEdit.Height - 24
                );
            };

            btnEdit.Click += (s, e) =>
            {
                try
                {
                    edit();
                }
                catch (Exception ex)
                {
                }
            };

            btnBottomRight = new Guna2Button()
            {
                Text = "Delete",
                Size = new Size(120, 36),
                FillColor = Color.Black,
                ForeColor = Color.White,
                BorderRadius = 10,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            // Position will be adjusted dynamically when the control resizes
            btnBottomRight.Location = new Point(
                this.ClientSize.Width - btnBottomRight.Width - 24,
                this.ClientSize.Height - btnBottomRight.Height - 24
    );

            // Keep it in position when the UserControl resizes
            this.SizeChanged += (s, e) =>
            {
                btnBottomRight.Location = new Point(
                    this.ClientSize.Width - btnBottomRight.Width - 24,
                    this.ClientSize.Height - btnBottomRight.Height - 24
                );
            };

            // Optional: Click action (reload data)
            btnBottomRight.Click += (s, e) =>
            {
                try
                {
                   remove();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error refreshing data: " + ex.Message);
                }
            };
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White
            };

            lblTitle = new Guna2HtmlLabel()
            {
                Text = "User Management",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(24, 16)
            };
            tbid = new Guna2HtmlLabel()
            {
                Text = "sdsad",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Visible = true,
                Location = new Point(24,30)
            };

            lblSubtitle = new Guna2HtmlLabel()
            {
                Text = "Manage users, departments and roles",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(24, 48)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);

            topPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.White
            };

            btnAddUser = new Guna2Button()
            {
                Text = "Add User",
                Size = new Size(120, 36),
            };
            btnAddUser.FillColor = Color.Black;
            btnAddUser.ForeColor = Color.White;
            btnAddUser.BorderRadius = 10;
            btnAddUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // Wire the click event so the button actually opens the AddUser form.
            btnAddUser.Click += (s, e) =>
            {
                var owner = this.FindForm();
                using (var addUser = new AddUser())
                {
                    addUser.StartPosition = FormStartPosition.CenterParent;
                    if (owner != null)
                    {
                        addUser.ShowDialog(owner);
                    }
                    else
                    {
                        addUser.ShowDialog();
                    }
                }


                // Refresh data after the AddUser dialog closes (ignore refresh errors).
                try
                {
                    LoadData();
                }
                catch
                {
                }
            };

            topPanel.SizeChanged += (s, e) =>
            {
                btnAddUser.Location = new Point(Math.Max(24, topPanel.ClientSize.Width - btnAddUser.Width - 24), (topPanel.Height - btnAddUser.Height) / 2);
            };

            this.Controls.Add(btnBottomRight);
            btnBottomRight.BringToFront();
            this.Controls.Add(btnEdit);
            btnEdit.BringToFront();
            topPanel.Controls.Add(btnAddUser);

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                ReadOnly = true,
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Name", HeaderText = "Name" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Email", HeaderText = "Email" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Password", HeaderText = "Password" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Department", HeaderText = "Department" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Position", HeaderText = "Position" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Contact", HeaderText = "Contact" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status" });

            this.Controls.Add(dgv);
            this.Controls.Add(topPanel);
            this.Controls.Add(headerPanel);
        }
        public void LoadData()
        {
            dgv.Rows.Clear();
            dgv.Refresh();

            string conString = "datasource=localhost;username=root;password=;database=kingland;";
            string query = "SELECT * FROM kingland.user";
            MySqlConnection con = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader Myreader;

            try
            {
                con.Open();
                Myreader = cmd.ExecuteReader();

                while (Myreader.Read())
                {
                    int id = (int)Myreader.GetInt64("id");
                    string name = Myreader.GetString("name");
                    string email = Myreader.GetString("email");
                    string password = Myreader.GetString("password");
                    string department = Myreader.GetString("department");
                    string position = Myreader.GetString("position");
                    int contact = (int)Myreader.GetInt64("contact");
                    string status = Myreader.GetString("status");
                    int rowIndex = dgv.Rows.Add(name, email, password, department, position, contact, status);

                    // Color the Status cell: green for Active, red for others (case-insensitive).
                    var statusCell = dgv.Rows[rowIndex].Cells["Status"];
                    if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        statusCell.Style.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }



        }

        public void remove ()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user you want to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected user ID and name
            string userName = dgv.SelectedRows[0].Cells["Name"].Value.ToString();

            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete user \"{userName}\"?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string conString = "datasource=localhost;username=root;password=;database=kingland;";
                    using (MySqlConnection con = new MySqlConnection(conString))
                    {
                        con.Open();
                        string query = "DELETE FROM user WHERE name = @name";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@name", userName);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"User \"{userName}\" was deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void edit()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected user data
            string name = dgv.SelectedRows[0].Cells["Name"].Value.ToString();
            string email = dgv.SelectedRows[0].Cells["Email"].Value.ToString();
            string password = dgv.SelectedRows[0].Cells["Password"].Value.ToString();
            string department = dgv.SelectedRows[0].Cells["Department"].Value.ToString();
            string position = dgv.SelectedRows[0].Cells["Position"].Value.ToString();
            string contact = dgv.SelectedRows[0].Cells["Contact"].Value.ToString();

            // Open the UpdateUser form
            var owner = this.FindForm();
            using (var updateForm = new UpdateUser(name, email, password, department, position, contact))
            {
                updateForm.StartPosition = FormStartPosition.CenterParent;
                updateForm.ShowDialog(owner);
            }

            LoadData();
        }
    } } 