using KMCI_System.AdminModule;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KMCI_System.Login
{
    public partial class Login : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        public Login()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 10F);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Focus email
            txtEmail.Focus();

            // Set placeholder/cue banner (works on Vista+)
            if (txtEmail.IsHandleCreated)
                SendMessage(txtEmail.Handle, EM_SETCUEBANNER, (IntPtr)1, "Enter your email");
            else
                txtEmail.HandleCreated += (s, ev) => SendMessage(txtEmail.Handle, EM_SETCUEBANNER, (IntPtr)1, "Enter your email");

            if (txtPassword.IsHandleCreated)
                SendMessage(txtPassword.Handle, EM_SETCUEBANNER, (IntPtr)1, "Enter your password");
            else
                txtPassword.HandleCreated += (s, ev) => SendMessage(txtPassword.Handle, EM_SETCUEBANNER, (IntPtr)1, "Enter your password");

            txtPassword.UseSystemPasswordChar = true;

            // Apply rounded corners for panels here is optional; actual regions applied in Shown
            // Keep regions updated on resize
            panelMain.SizeChanged += (s, ev) => ApplyRoundedRegion(panelMain, 20);
            panelShadow.SizeChanged += (s, ev) => ApplyRoundedRegion(panelShadow, 20);
            btnLogin.SizeChanged += (s, ev) => ApplyRoundedRegion(btnLogin, Math.Max(10, btnLogin.Height / 2));

            // Style buttons
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.BackColor = Color.Black;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.MouseEnter += (s, ev) => btnLogin.BackColor = Color.FromArgb(40, 40, 40);
            btnLogin.MouseLeave += (s, ev) => btnLogin.BackColor = Color.Black;

            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Cursor = Cursors.Hand;

            // Center the form
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Soft layered gradient background
            using (var brush = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(246, 247, 251), Color.White, LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            base.OnPaintBackground(e);
        }

        private void ApplyRoundedRegion(Control ctrl, int radius)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;
            using (var path = CreateRoundRectPath(new Rectangle(0, 0, ctrl.Width, ctrl.Height), radius))
            {
                // dispose previous region if any
                if (ctrl.Region != null)
                {
                    try { ctrl.Region.Dispose(); } catch { }
                }
                ctrl.Region = new Region(path);
            }
        }

        private GraphicsPath CreateRoundRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            string department = Authenticate(email, password);

            if (!string.IsNullOrEmpty(department))
            {
                // Store session info
                DialogResult = DialogResult.OK;
                Session.CurrentUserEmail = email;
                Session.CurrentUserDepartment = department;

                // Hide login form
                this.Hide();

                // Open the appropriate form based on department
                Form departmentForm = null;

                switch (department.ToLower())
                {
                    case "sales":
                        departmentForm = new SalesForm();
                        break;
                    case "purchasing":
                        departmentForm = new PurchasingForm();
                        break;
                    case "admin":
                        departmentForm = new AdminForm();
                        break;
                    default:
                        MessageBox.Show($"No form configured for department: {department}",
                            "Access Denied",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        this.Show();
                        return;
                }

                // Show the department form
                if (departmentForm != null)
                {
                    departmentForm.FormClosed += (s, args) => this.Close(); // Close login when department form closes
                    departmentForm.Show();
                }
            }
            else
            {
                MessageBox.Show(this, "Invalid email or password.", "Login failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private string Authenticate(string email, string password)
        {
            string conString = "datasource=localhost;username=root;password=;database=kingland;";
            string query = "SELECT department FROM user WHERE email = @email AND password = @password;";

            try
            {
                using (var con = new MySqlConnection(conString))
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    con.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string department = result.ToString();

                        // Update user status to Active
                        using (var upd = new MySqlCommand("UPDATE kingland.user SET status = @status WHERE email = @email;", con))
                        {
                            upd.Parameters.AddWithValue("@status", "Active");
                            upd.Parameters.AddWithValue("@email", email);
                            upd.ExecuteNonQuery();
                        }

                        return department;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword fp = new ForgotPassword();
            fp.ShowDialog();
            this.Hide();
        }
    }
}

// Add this to your Session class (or create it if it doesn't exist)
public static class Session
{
    public static string CurrentUserEmail { get; set; }
    public static string CurrentUserDepartment { get; set; }
}