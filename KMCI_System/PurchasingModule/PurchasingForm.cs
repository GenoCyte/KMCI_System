using KMCI_System.PurchasingModule;
using KMCI_System.PurchasingModule.PurchaseOrderModule;

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
    }
}
