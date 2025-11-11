using KMCI_System.InventoryModule;

namespace KMCI_System.LogisticsModule
{
    public partial class ProjectOverview : UserControl
    {
        private UserControl currentUserControl;
        private String projectCode;
        private List<Button> groupBoxButtons;

        public ProjectOverview(String projectCode)
        {
            InitializeComponent();
            SetupButton();
            LoadProjectdetails(new ProjectDetails(projectCode));
            this.projectCode = projectCode;
            header.Text = projectCode;
        }

        private void SetupButton()
        {
            groupBoxButtons = new List<Button>();

            GroupBox groupBox = new GroupBox
            {
                Size = new Size(510, 60),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            groupBox.Location = new Point(
                (this.ClientSize.Width - groupBox.Width) / 2,
                (70)
            );
            groupBox.Anchor = AnchorStyles.Top;

            Button btn1 = new Button
            {
                Text = "Project Details",
                Location = new Point(10, 20),
                Size = new Size(160, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btn1.FlatAppearance.BorderSize = 0;
            btn1.Click += btnProjectDetailsButton_Click;
            groupBoxButtons.Add(btn1);

            Button btn2 = new Button
            {
                Text = "Budget Allocation",
                Location = new Point(175, 20),
                Size = new Size(160, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btn2.FlatAppearance.BorderSize = 0;
            btn2.Click += btnBudgetAllocationButton_Click;
            groupBoxButtons.Add(btn2);

            Button btn4 = new Button
            {
                Text = "Project Directory",
                Location = new Point(340, 20),
                Size = new Size(160, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btn4.FlatAppearance.BorderSize = 0;
            btn4.Click += btnProjectDirectoryButton_Click;
            groupBoxButtons.Add(btn4);

            groupBox.Controls.AddRange(new Control[] { btn1, btn2, btn4 });
            this.Controls.Add(groupBox);
        }

        private void GroupBoxButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Reset all buttons to white
                foreach (Button btn in groupBoxButtons)
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                }

                // Set clicked button to blue
                clickedButton.BackColor = Color.FromArgb(0, 120, 215);
                clickedButton.ForeColor = Color.White;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ProjectManagement());
        }

        private void btnProjectDetailsButton_Click(object sender, EventArgs e)
        {
            GroupBoxButton_Click(sender, e);
            LoadUserControlPanel(new ProjectDetails(projectCode));
        }

        private void btnBudgetAllocationButton_Click(object sender, EventArgs e)
        {
            GroupBoxButton_Click(sender, e);
            LoadUserControlPanel(new BudgetAllocation(projectCode));
        }

        private void btnProjectDirectoryButton_Click(object sender, EventArgs e)
        {
            GroupBoxButton_Click(sender, e);
            LoadUserControlPanel(new ProjectDirectory(projectCode));
        }

        private void LoadProjectdetails(UserControl userControl)
        {
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

        private void LoadUserControlPanel(UserControl userControl)
        {
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

        private void LoadUserControl(UserControl userControl)
        {
            var inventoryForm = this.FindForm() as InventoryForm;
            // Clear existing controls in panel
            inventoryForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            inventoryForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }


    }
}
