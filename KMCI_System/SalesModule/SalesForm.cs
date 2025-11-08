namespace KMCI_System
{
    public partial class SalesForm : Form
    {

        private UserControl currentUserControl;
        public SalesForm()
        {
            InitializeComponent();
            LoadUserControl(new SalesModule.ProjectManagement());
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
            LoadUserControl(new SalesModule.ProjectManagement());
        }

        private void btnCompanyManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new SalesModule.CompanyManagement());
        }

        private void btnSupplierManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new SalesModule.SupplierManagement());
        }

        private void btnProductManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new SalesModule.ProductManagementModule.ProductManagement());
        }
        private void BtnLogOut_Click(object sender, EventArgs e)
        {
            Login.Login loginForm = new Login.Login();  
            loginForm.Show();
            this.Close();

        }
    }
}
