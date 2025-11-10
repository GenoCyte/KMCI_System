using KMCI_System.PurchasingModule;
using KMCI_System.PurchasingModule.PurchaseOrderModule;
using MySql.Data.MySqlClient;

namespace KMCI_System
{
    public partial class PurchasingForm : Form
    {
        private UserControl currentUserControl;
        
        public PurchasingForm()
        {
            InitializeComponent();
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
            LoadUserControl(new PurchasingModule.ProjectManagement());
        }
        
        private void btnPrManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PurchasingModule.PurchaseRequestModule.PurchaseRequest());
        }

        private void btnPoManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PurchaseOrder());
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
