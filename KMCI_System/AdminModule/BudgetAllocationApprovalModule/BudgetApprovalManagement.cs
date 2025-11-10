using KMCI_System.PurchasingModule.PurchaseOrderModule;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.AdminModule.BudgetAllocationApprovalModule
{
    public partial class BudgetApprovalManagement : UserControl
    {
        private DataGridView dgvProject;
        private Panel detailsPanel;
        //private ProjectOverview projectDetailsControl;
        private UserControl currentUserControl;
        public BudgetApprovalManagement()
        {
            InitializeComponent();
            //SetupDetailsPanel();
            SetupDataGridView();
            LoadProject();
        }

        private void SetupDataGridView()
        {
            dgvProject = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 50 },
                ScrollBars = ScrollBars.Both,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false
            };

            dgvProject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvProject.Location = new Point(20, 160);
            dgvProject.Width = this.ClientSize.Width - 40;

            // Style headers
            dgvProject.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvProject.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProject.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvProject.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvProject.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvProject.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvProject.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Style cells
            dgvProject.DefaultCellStyle.BackColor = Color.White;
            dgvProject.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvProject.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvProject.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProject.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Enable grid lines
            dgvProject.GridColor = Color.FromArgb(220, 220, 220);
            dgvProject.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add columns
            dgvProject.Columns.Add("BudgetId", "Id");
            dgvProject.Columns.Add("ProjectCode", "Project Code");
            dgvProject.Columns.Add("QuotationId", "Quotation ID");
            dgvProject.Columns.Add("BidPrice", "Bid Price");
            dgvProject.Columns.Add("TotalCost", "Total Cost");

            // Set column widths
            dgvProject.Columns["BudgetId"].Visible = false;
            dgvProject.Columns["ProjectCode"].Width = 150;
            dgvProject.Columns["QuotationId"].Width = 300;
            dgvProject.Columns["BidPrice"].Width = 395;
            dgvProject.Columns["TotalCost"].Width = 260;

            // Handle button click
            dgvProject.CellClick += dgvProject_CellClick;
            dgvProject.RowsAdded += dgvProjects_RowsChanged;
            dgvProject.RowsRemoved += dgvProjects_RowsChanged;

            this.Controls.Add(dgvProject);
        }

        private void dgvProject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // ✅ Get the budget ID from the hidden column
                int budgetId = Convert.ToInt32(dgvProject.Rows[e.RowIndex].Cells["BudgetId"].Value);
                
                // ✅ Get the project code from the visible column
                string projectCode = dgvProject.Rows[e.RowIndex].Cells["ProjectCode"].Value.ToString();
                
                // ✅ Pass both values to BudgetAllocationDetails
                LoadUserControl(new BudgetAllocationDetails(budgetId, projectCode));
            }
        }

        private void LoadUserControl(UserControl userControl)
        {
            var adminForm = this.FindForm() as AdminForm;
            // Clear existing controls in panel
            adminForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            adminForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void dgvProjects_RowsChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void dgvProjects_RowsChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AdjustDataGridViewHeight();
        }

        private void AdjustDataGridViewHeight()
        {
            if (dgvProject.Rows.Count == 0)
            {
                dgvProject.Height = dgvProject.ColumnHeadersHeight + 2;
                return;
            }

            int totalHeight = dgvProject.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dgvProject.Rows)
            {
                totalHeight += row.Height;
            }

            // Add small buffer for borders
            totalHeight += 2;

            dgvProject.Height = totalHeight;
        }

        private void LoadProject()
        {
            dgvProject.Rows.Clear();

            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"
                SELECT id, project_code, quotation_id, bid_price, total_cost
                FROM budget_allocation
                WHERE status = 'Pending'
                ORDER BY id DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        decimal bidPrice = Convert.ToDecimal(reader["bid_price"]);
                        string bidPriceValue = bidPrice == 0 ? "On Approval" : "₱" + bidPrice.ToString("N2");
                        decimal totalCost = Convert.ToDecimal(reader["total_cost"]);
                        string totalCostValue = totalCost == 0 ? "On Approval" : "₱" + totalCost.ToString("N2");

                        dgvProject.Rows.Add(
                            reader["id"].ToString(),
                            reader["project_code"].ToString(),
                            reader["quotation_id"].ToString(),
                            bidPriceValue,
                            totalCostValue
                        );
                    }
                }
            }

            AdjustDataGridViewHeight();
            dgvProject.ClearSelection();
        }
    }
}
