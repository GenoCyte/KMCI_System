using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.UserManagementModule
{
    public partial class UpdateUser : Form
    {
        private int userId; // store the user ID to be updated
        private string email;
        private string password;
        private string department;
        private string position;
        private string contact;

        public UpdateUser(string name, string email, string password, string department, string position, string contact)
        {
            InitializeComponent();


            // Split name into first and last name if possible
            var parts = name.Split(' ');
            if (parts.Length >= 2)
            {
                tb_fname.Text = parts[0];
            }
            else
            {
                tb_fname.Text = name;
            }

            tb_email.Text = email;
            tb_password.Text = password;
            cb_department.Text = department;
            cb_position.Text = position;
            tb_contact.Text = contact;
        }



        private void UpdateUser_Load(object sender, EventArgs e)
        {
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(tb_fname.Text) ||
                string.IsNullOrWhiteSpace(tb_email.Text) ||
                string.IsNullOrWhiteSpace(tb_password.Text) ||
                cb_department.SelectedIndex == -1 ||
                cb_position.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(tb_contact.Text))
            {
                MessageBox.Show("Please fill out all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!long.TryParse(tb_contact.Text, out _))
            {
                MessageBox.Show("Contact number must be numeric.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string conString = "datasource=localhost;username=root;password=;database=kmci_database;";
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string query = "UPDATE user SET name=@name, email=@email, password=@password, department=@department, position=@position, contact=@contact WHERE id=@id";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        string name = tb_fname.Text + " ";

                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", tb_email.Text);
                        cmd.Parameters.AddWithValue("@password", tb_password.Text);
                        cmd.Parameters.AddWithValue("@department", cb_department.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@position", cb_position.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@contact", tb_contact.Text);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No record was updated. Check the user ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(tb_fname.Text) ||
                string.IsNullOrWhiteSpace(tb_email.Text) ||
                string.IsNullOrWhiteSpace(tb_password.Text) ||
                cb_department.SelectedIndex == -1 ||
                cb_position.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(tb_contact.Text))
            {
                MessageBox.Show("Please fill out all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!long.TryParse(tb_contact.Text, out _))
            {
                MessageBox.Show("Contact number must be numeric.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string conString = "datasource=localhost;username=root;password=;database=kmci_database;";
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string query = "UPDATE  user SET name=@name, email=@email, password=@password, department=@department, position=@position, contact=@contact WHERE email=@email";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))

                    {
                        string name = tb_fname.Text;

                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", tb_email.Text);
                        cmd.Parameters.AddWithValue("@password", tb_password.Text);
                        cmd.Parameters.AddWithValue("@department", cb_department.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@position", cb_position.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@contact", tb_contact.Text);
                        MySqlDataReader Myreader;
                        con.Open();
                        Myreader = cmd.ExecuteReader();
                        con.Close();

                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tb_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


