using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace KMCI_System.Login
{
    // Main partial class
    public partial class Main : Form
    {
        private System.Windows.Forms.Timer submenuTimer;
        private int submenuTargetHeight;
        private int submenuAnimationStep = 24;

        private System.Windows.Forms.Timer contentTimer;
        private UserControl? animatedControl;
        private int contentAnimationStep = 40; // will be updated per-load for speed

        public Main()
        {
            InitializeComponent();

            // initialize timers with faster intervals for snappier animation
            submenuTimer = new System.Windows.Forms.Timer { Interval = 8 }; // shorter interval -> smoother/faster
            submenuTimer.Tick += SubmenuTimer_Tick;

            contentTimer = new System.Windows.Forms.Timer { Interval = 8 };
            contentTimer.Tick += ContentTimer_Tick;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Basic styling
            this.BackColor = Color.FromArgb(246, 247, 251);

            // Style primary sidebar buttons
            btnDashboard.FillColor = Color.Black;
            btnDashboard.ForeColor = Color.White;
            btnDashboard.MouseEnter += (s, ev) => btnDashboard.FillColor = Color.FromArgb(64, 64, 64);
            btnDashboard.MouseLeave += (s, ev) => btnDashboard.FillColor = Color.Black;

            btnManagement.FillColor = Color.White;
            btnManagement.ForeColor = Color.Black;
            btnManagement.MouseEnter += (s, ev) => btnManagement.FillColor = Color.FromArgb(245, 245, 245);
            btnManagement.MouseLeave += (s, ev) => btnManagement.FillColor = Color.White;
            btnLogout.FillColor = Color.White;
            btnLogout.MouseEnter += (s, ev) => btnLogout.FillColor = Color.FromArgb(245, 245, 245);
            btnLogout.MouseLeave += (s, ev) => btnLogout.FillColor = Color.White;

            // Submenu buttons hover
            foreach (Control c in panelManagementSubmenu.Controls)
            {
                if (c is Guna2Button b)
                {
                    b.FillColor = Color.White;
                    b.ForeColor = Color.Black;
                    b.MouseEnter += (s, ev) => b.FillColor = Color.FromArgb(240, 240, 240);
                    b.MouseLeave += (s, ev) => b.FillColor = Color.White;
                }
            }


        }

        private void SubmenuTimer_Tick(object sender, EventArgs e)
        {
            if (panelManagementSubmenu.Height < submenuTargetHeight)
            {
                panelManagementSubmenu.Height = Math.Min(submenuTargetHeight, panelManagementSubmenu.Height + submenuAnimationStep);
                if (panelManagementSubmenu.Height == submenuTargetHeight) submenuTimer.Stop();
            }
            else if (panelManagementSubmenu.Height > submenuTargetHeight)
            {
                panelManagementSubmenu.Height = Math.Max(submenuTargetHeight, panelManagementSubmenu.Height - submenuAnimationStep);
                if (panelManagementSubmenu.Height == submenuTargetHeight)
                {
                    submenuTimer.Stop();
                    // hide when collapsed to keep tab order consistent
                    if (submenuTargetHeight == 0) panelManagementSubmenu.Visible = false;
                }
            }
            else
            {
                submenuTimer.Stop();
            }
        }

        private void ContentTimer_Tick(object sender, EventArgs e)
        {
            if (animatedControl == null)
            {
                contentTimer.Stop();
                return;
            }

            // move control left until it reaches 0 (adaptive step already set)
            if (animatedControl.Left > 0)
            {
                int next = animatedControl.Left - contentAnimationStep;
                if (next <= 0)
                {
                    animatedControl.Left = 0;
                    contentTimer.Stop();
                }
                else
                {
                    animatedControl.Left = next;
                }
            }
            else
            {
                contentTimer.Stop();
            }
        }

        private void LoadControl(UserControl uc)
        {
            // stop any running content animation
            contentTimer.Stop();

            // prepare new control off-screen to the right
            panelMain.Controls.Clear();
            animatedControl = uc;
            uc.Width = panelMain.ClientSize.Width;
            uc.Height = panelMain.ClientSize.Height;
            uc.Top = 0;
            uc.Left = panelMain.ClientSize.Width; // start off-screen
            uc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.Controls.Add(uc);
            uc.BringToFront();

            // compute adaptive content step based on width so larger screens slide faster
            try
            {
                contentAnimationStep = Math.Max(96, panelMain.ClientSize.Width / 8); // larger -> faster
            }
            catch
            {
                contentAnimationStep = 160;
            }

            // start slide animation
            contentTimer.Start();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
        }

        private void btnManagement_Click(object sender, EventArgs e)
        {
            // animate submenu expand/collapse
            if (panelManagementSubmenu.Visible && panelManagementSubmenu.Height > 0)
            {
                // collapse: choose step based on current height for consistent duration
                submenuTargetHeight = 0;
                submenuAnimationStep = Math.Max(12, panelManagementSubmenu.Height / 6);
                submenuTimer.Start();
            }
            else
            {
                // expand: compute target height based on child controls and set step for speed
                int h = 0;
                foreach (Control c in panelManagementSubmenu.Controls)
                    h += c.Height;
                submenuTargetHeight = h;
                panelManagementSubmenu.Visible = true;
                submenuAnimationStep = Math.Max(12, Math.Max(32, submenuTargetHeight / 6));
                submenuTimer.Start();
            }
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
        }





        private void btnUsers_Click(object sender, EventArgs e)
        {
            LoadControl(new UserManagementUC());
        }



        private void btnLogout_Click(object sender, EventArgs e)
        {
            // mark current user inactive (if any), then return to login UI
            try
            {
                if (!string.IsNullOrWhiteSpace(Session.CurrentUserEmail))
                {
                    string conString = "datasource=localhost;username=root;password=;database=kingland;";
                    using (var con = new MySqlConnection(conString))
                    using (var cmd = new MySqlCommand("UPDATE kingland.user SET status = @status WHERE email = @email;", con))
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
                Login login = new Login();
                login.Show();
            }
            catch { }
            this.Close();
        }



        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
