using KMCI_System.InventoryModule.InventoryManagementModule;
using KMCI_System.Login; // ✅ Add to access Session class
using KMCI_System.LogisticsModule;
using MySql.Data.MySqlClient;

namespace KMCI_System.InventoryModule
{
    public partial class InventoryForm : Form
    {
        private UserControl currentUserControl;
        public InventoryForm()
        {
            InitializeComponent();

            // ✅ Display logged-in user's name in greeting
            if (!string.IsNullOrEmpty(Session.CurrentUserName))
            {
                lblGreeting.Text = $"Good Day, {Session.CurrentUserName}";
            }
            else
            {
                lblGreeting.Text = "Good Day, Employee";
            }

            LoadUserControl(new ProjectManagement());
        }

        private void LoadUserControl(UserControl userControl)
        {
            // Clear existing controls in panel
            panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnProjectManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ProjectManagement());
        }

        private void btnInventoryManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new InventoryManagement());
        }


        private void btnProductManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ProductManagement());
        }

        private void btnVendorManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new VendorManagement());
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            // mark current user inactive (if any), then return to login UI
            try
            {
                if (!string.IsNullOrWhiteSpace(Session.CurrentUserEmail))
                {
                    string conString = "datasource=localhost;username=root;password=;database=kmci_database;";
                    using (var con = new MySqlConnection(conString))
                    using (var cmd = new MySqlCommand("UPDATE user SET status = @status WHERE email = @email;", con))
                    {
                        cmd.Parameters.AddWithValue("@status", "Inactive");
                        cmd.Parameters.AddWithValue("@email", Session.CurrentUserEmail);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // clear session
                    Session.CurrentUserEmail = null!;
                    Session.CurrentUserName = null!; // ✅ Clear user name
                    Session.CurrentUserDepartment = null!; // ✅ Clear department
                }
            }
            catch
            {
                // ignore DB update errors on logout
            }

            // Show login form and close main.
            try
            {
                Login.Login login = new Login.Login();
                login.Show();
            }
            catch { }
            this.Hide();
        }
    }
}
