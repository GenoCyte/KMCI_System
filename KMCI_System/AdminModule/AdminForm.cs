using KMCI_System.AdminModule;
using KMCI_System.AdminModule.DashboardModule;
using KMCI_System.AdminModule.PurchaseRequestApprovalModule;
using KMCI_System.AdminModule.UserManagementModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.AdminModule
{
    public partial class AdminForm : Form
    {
        private UserControl currentUserControl;
        public AdminForm()
        {
            InitializeComponent();
            LoadUserControl(new Dashboard());
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

        private void btnDashboardManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Dashboard());
        }

        private void btnProjectManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ProjectManagement());
        }

        private void btnBudgetAllocationManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new BudgetAllocationApprovalModule.BudgetApprovalManagement());
        }

        private void btnPurchaseRequestManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PurchaseRequestManagement());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UserManagement());
        }
    }
}
