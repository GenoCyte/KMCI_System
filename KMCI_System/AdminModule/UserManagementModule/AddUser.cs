using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace KMCI_System.AdminModule.UserManagementModule
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private void AddUser_Load(object sender, EventArgs e)
        { }
        private void btn_add_Click(object sender, EventArgs e)

        {
            if (string.IsNullOrWhiteSpace(tb_fname.Text) || string.IsNullOrWhiteSpace(tb_lname.Text) || string.IsNullOrWhiteSpace(tb_email.Text) || string.IsNullOrWhiteSpace(tb_password.Text) || string.IsNullOrWhiteSpace(cb_department.SelectedItem.ToString())
                || string.IsNullOrWhiteSpace(cb_position.SelectedItem.ToString()) || !long.TryParse(tb_contact.Text, out _))
            {
                MessageBox.Show("Fill out all information ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string conString = "datasource=localhost;username=root;password,=;database=kmci_database;";
                    string name = tb_fname.Text + " " + tb_lname.Text;
                    string query = "INSERT INTO user (name, email, password, department, position, contact, status) VALUES ('" + name + "', '" + tb_email.Text + "', '" + tb_password.Text + "', '" + cb_department.SelectedItem.ToString() + "', '" + cb_position.SelectedItem.ToString() + "', '" + tb_contact.Text + "', '" + "Inactive" + "')";
                    MySqlConnection con = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader Myreader;

                    con.Open();
                    Myreader = cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("User Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }


        private string GenerateStrongPassword(int length)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "@$!%*?&";

            string allChars = upper + lower + digits + special;
            Random random = new Random();

            // Ensure password has at least one of each type
            StringBuilder password = new StringBuilder();
            password.Append(upper[random.Next(upper.Length)]);
            password.Append(lower[random.Next(lower.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(special[random.Next(special.Length)]);

            // Fill remaining with random characters
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle to randomize order
            return new string(password.ToString().OrderBy(_ => random.Next()).ToArray());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tb_password.Text = GenerateStrongPassword(8);
        }

        private void cb_department_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cb_position_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}

