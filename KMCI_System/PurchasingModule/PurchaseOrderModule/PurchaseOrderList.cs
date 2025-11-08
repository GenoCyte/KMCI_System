using KMCI_System.PurchasingModule.PurchaseRequestModule;
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

namespace KMCI_System.PurchasingModule.PurchaseOrderModule
{
    public partial class PurchaseOrderList : UserControl
    {
        private string projectCode;
        private string vendorName;
        private Label lblheader2;
        private DataGridView dgvProject;
        private Panel detailsPanel;
        //private ProjectOverview projectDetailsControl;
        private UserControl currentUserControl;

        public PurchaseOrderList(string projectCode)
        {
            this.projectCode = projectCode;
            InitializeComponent();
            header.Text = projectCode;
            //SetupDetailsPanel();
            SetupDetails();
            LoadProject();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PurchaseOrder());
        }

        private void LoadUserControl(UserControl userControl)
        {
            var salesForm = this.FindForm() as PurchasingForm;
            // Clear existing controls in panel
            salesForm.panel1.Controls.Clear();

            // Dispose previous UserControl if exists
            if (currentUserControl != null)
            {
                currentUserControl.Dispose();
            }

            // Set the new UserControl
            currentUserControl = userControl;
            userControl.Dock = DockStyle.Fill; // Fill the entire panel
            salesForm.panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void SetupDetails()
        {
            lblheader2 = new Label
            {
                Text = "Purchase Order List",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                Location = new Point(20, 100),
                AutoSize = true
            };
            this.Controls.Add(lblheader2);

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
            dgvProject.Columns.Add("PrId", "PR ID");
            dgvProject.Columns.Add("PrName", "PR Name");
            dgvProject.Columns.Add("SupplierName", "Supplier Name");
            dgvProject.Columns.Add("Quantity", "Quantity");
            dgvProject.Columns.Add("GrandTotal", "Grand Total");

            // Set column widths
            dgvProject.Columns["PrId"].Width = 150;
            dgvProject.Columns["PrName"].Width = 205;
            dgvProject.Columns["SupplierName"].Width = 400;
            dgvProject.Columns["Quantity"].Width = 150;
            dgvProject.Columns["GrandTotal"].Width = 200;


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
                vendorName = dgvProject.Rows[e.RowIndex].Cells["SupplierName"].Value.ToString();
                string prName = dgvProject.Rows[e.RowIndex].Cells["PrName"].Value.ToString();
                LoadUserControl(new CreatePurchaseOrder(vendorName, prName, projectCode));
            }
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
                SELECT id, pr_name, vendor_name, quantity, grand_total
                FROM purchase_request
                WHERE project_code = @project_code AND status = 'Approved'
                ORDER BY id DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@project_code", projectCode);  // Move this BEFORE ExecuteReader
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal budgetAllocation = Convert.ToDecimal(reader["grand_total"]);
                            string displayValue = "₱" + budgetAllocation.ToString("N2");

                            dgvProject.Rows.Add(
                                reader["id"].ToString(),
                                reader["pr_name"].ToString(),
                                reader["vendor_name"].ToString(),
                                reader["quantity"].ToString(),
                                displayValue
                            );
                        }
                    }
                }
            }

            AdjustDataGridViewHeight();
            dgvProject.ClearSelection();
        }
    }
}
