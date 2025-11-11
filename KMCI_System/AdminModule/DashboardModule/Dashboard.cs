using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace KMCI_System.AdminModule.DashboardModule
{
    public partial class Dashboard : UserControl
    {
        private Panel pnlKPICards;
        private Panel pnlCharts;
        private Chart chartQuotationStatus;
        private Chart chartMonthlySales;
        private Chart chartProjectStatus;
        private Chart chartBudgetUtilization;
        private Chart chartTopClients;
        private Chart chartPOTrend;
        private bool isInitialized = false;

        // Fixed layout dimensions
        private const int CARD_WIDTH = 225;
        private const int CARD_HEIGHT = 120;
        private const int CHART_WIDTH = 500;
        private const int CHART_HEIGHT = 400;
        private const int SPACING = 50;
        private const int LEFT_MARGIN = 20;

        public Dashboard()
        {
            InitializeComponent();
            this.Load += Dashboard_Load;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (!isInitialized)
            {
                SetupDashboard();
                LoadDashboardData();
                isInitialized = true;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible && isInitialized)
            {
                // Refresh data when becoming visible again
                LoadDashboardData();
            }
        }

        private void SetupDashboard()
        {
            // Set up auto-scroll for the entire UserControl
            this.AutoScroll = true;
            this.BackColor = Color.White;

            int yPosition = 130; // Start below the header2 label

            // KPI Cards Panel - Fixed size
            pnlKPICards = new Panel
            {
                Location = new Point(LEFT_MARGIN, yPosition),
                Size = new Size((CARD_WIDTH * 4) + (SPACING * 3), CARD_HEIGHT + SPACING),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlKPICards);
            pnlKPICards.BringToFront();
            CreateKPICards();

            yPosition += CARD_HEIGHT + SPACING + 20;

            // Charts Panel - Fixed size
            int chartsPanelWidth = (CHART_WIDTH * 2) + SPACING;
            int chartsPanelHeight = (CHART_HEIGHT * 3) + (SPACING * 2);
            
            pnlCharts = new Panel
            {
                Location = new Point(LEFT_MARGIN, yPosition),
                Size = new Size(chartsPanelWidth, chartsPanelHeight),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlCharts);
            pnlCharts.BringToFront();
            CreateCharts();

            // Set AutoScrollMinSize to ensure all content is scrollable
            this.AutoScrollMinSize = new Size(chartsPanelWidth + (LEFT_MARGIN * 2), yPosition + chartsPanelHeight + 50);
        }

        private void CreateKPICards()
        {
            // Card 1: Total Quotations
            CreateKPICard("Total Quotations", "0", "📋", Color.FromArgb(0, 120, 215), 0, 0, "quotations");

            // Card 2: Active Projects
            CreateKPICard("Active Projects", "0", "📊", Color.FromArgb(16, 124, 16), CARD_WIDTH + SPACING, 0, "projects");

            // Card 3: Pending POs
            CreateKPICard("Pending POs", "0", "📦", Color.FromArgb(255, 140, 0), (CARD_WIDTH + SPACING) * 2, 0, "pos");

            // Card 4: Total Budget
            CreateKPICard("Budget Allocated", "₱0", "💰", Color.FromArgb(156, 39, 176), (CARD_WIDTH + SPACING) * 3, 0, "budget");
        }

        private void CreateKPICard(string title, string value, string icon, Color accentColor, int x, int y, string tag)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(CARD_WIDTH, CARD_HEIGHT),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = tag
            };

            // Icon Label
            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 28),
                ForeColor = accentColor,
                Location = new Point(15, 10),
                AutoSize = true
            };
            card.Controls.Add(lblIcon);

            // Title Label
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(15, 60),
                Size = new Size(CARD_WIDTH - 30, 20)
            };
            card.Controls.Add(lblTitle);

            // Value Label
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, 78),
                Size = new Size(CARD_WIDTH - 30, 25),
                Tag = "value" // Tag to identify value labels for updating
            };
            card.Controls.Add(lblValue);

            pnlKPICards.Controls.Add(card);
        }

        private void CreateCharts()
        {
            // Row 1: Quotation Status & Monthly Sales
            chartQuotationStatus = CreateChart("Quotation Status Distribution", 0, 0, SeriesChartType.Doughnut);
            chartMonthlySales = CreateChart("Monthly Sales Trend (Last 6 Months)", CHART_WIDTH + SPACING, 0, SeriesChartType.Line);

            // Row 2: Project Status & Budget Utilization
            chartProjectStatus = CreateChart("Projects by Status", 0, CHART_HEIGHT + SPACING, SeriesChartType.Bar);
            chartBudgetUtilization = CreateChart("Budget Utilization by Category", CHART_WIDTH + SPACING, CHART_HEIGHT + SPACING, SeriesChartType.Column);

            // Row 3: Top Clients & PO Trend
            chartTopClients = CreateChart("Top 5 Clients by Quote Value", 0, (CHART_HEIGHT + SPACING) * 2, SeriesChartType.Bar);
            chartPOTrend = CreateChart("Purchase Orders Trend", CHART_WIDTH + SPACING, (CHART_HEIGHT + SPACING) * 2, SeriesChartType.Line);
        }

        private Chart CreateChart(string title, int x, int y, SeriesChartType chartType)
        {
            Chart chart = new Chart
            {
                Location = new Point(x, y),
                Size = new Size(CHART_WIDTH, CHART_HEIGHT),
                BackColor = Color.White,
                BorderlineColor = Color.FromArgb(220, 220, 220),
                BorderlineWidth = 1,
                BorderlineDashStyle = ChartDashStyle.Solid,
                Visible = true
            };

            // Chart Area
            ChartArea chartArea = new ChartArea("MainArea")
            {
                BackColor = Color.White,
                BorderWidth = 0
            };

            // Configure axes
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            chartArea.AxisX.LineColor = Color.FromArgb(200, 200, 200);
            chartArea.AxisY.LineColor = Color.FromArgb(200, 200, 200);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);

            // Better label handling for axes
            if (chartType == SeriesChartType.Bar || chartType == SeriesChartType.Column)
            {
                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.WordWrap;
                chartArea.AxisX.LabelStyle.Angle = -45;
            }

            chart.ChartAreas.Add(chartArea);

            // Series
            Series series = new Series("Data")
            {
                ChartType = chartType,
                Font = new Font("Segoe UI", 8),
                IsValueShownAsLabel = true,
                LabelForeColor = Color.FromArgb(64, 64, 64)
            };

            // Customize based on chart type
            if (chartType == SeriesChartType.Doughnut)
            {
                series["PieLabelStyle"] = "Outside";
                series["DoughnutRadius"] = "60";
                Legend legend = new Legend
                {
                    Docking = Docking.Right,
                    Font = new Font("Segoe UI", 8),
                    BackColor = Color.Transparent,
                    IsDockedInsideChartArea = false
                };
                chart.Legends.Add(legend);
            }
            else if (chartType == SeriesChartType.Line)
            {
                series.BorderWidth = 3;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 8;
                series.IsValueShownAsLabel = false;
            }
            else if (chartType == SeriesChartType.Bar || chartType == SeriesChartType.Column)
            {
                series["PointWidth"] = "0.6";
            }

            chart.Series.Add(series);

            // Title
            Title chartTitle = new Title
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Alignment = ContentAlignment.TopLeft,
                Docking = Docking.Top
            };
            chart.Titles.Add(chartTitle);

            pnlCharts.Controls.Add(chart);
            return chart;
        }

        private void LoadDashboardData()
        {
            string connString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Load KPI data
                    LoadKPIData(conn);

                    // Load chart data
                    LoadQuotationStatusData(conn);
                    LoadMonthlySalesData(conn);
                    LoadProjectStatusData(conn);
                    LoadBudgetUtilizationData(conn);
                    LoadTopClientsData(conn);
                    LoadPOTrendData(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadKPIData(MySqlConnection conn)
        {
            // Total Quotations
            string quotationQuery = "SELECT COUNT(*) FROM quotation";
            using (MySqlCommand cmd = new MySqlCommand(quotationQuery, conn))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                UpdateKPICard("quotations", count.ToString());
            }

            // Active Projects
            string projectQuery = @"SELECT COUNT(*) FROM project_list 
                                   WHERE LOWER(TRIM(COALESCE(status, ''))) NOT IN ('completed', 'complete')";
            using (MySqlCommand cmd = new MySqlCommand(projectQuery, conn))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                UpdateKPICard("projects", count.ToString());
            }

            // Pending POs
            string poQuery = @"SELECT COUNT(*) FROM purchase_order 
                              WHERE status NOT IN ('Delivered to Client', 'Completed')";
            using (MySqlCommand cmd = new MySqlCommand(poQuery, conn))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                UpdateKPICard("pos", count.ToString());
            }

            // Total Budget Allocated
            string budgetQuery = "SELECT COALESCE(SUM(budget_allocation), 0) FROM project_list";
            using (MySqlCommand cmd = new MySqlCommand(budgetQuery, conn))
            {
                decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                UpdateKPICard("budget", "₱" + total.ToString("N0"));
            }
        }

        private void UpdateKPICard(string tag, string value)
        {
            foreach (Control card in pnlKPICards.Controls)
            {
                if (card.Tag?.ToString() == tag)
                {
                    foreach (Control control in card.Controls)
                    {
                        if (control.Tag?.ToString() == "value")
                        {
                            control.Text = value;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void LoadQuotationStatusData(MySqlConnection conn)
        {
            chartQuotationStatus.Series["Data"].Points.Clear();

            string query = @"SELECT status, COUNT(*) as count 
                           FROM quotation 
                           GROUP BY status";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Color[] colors = {
                    Color.FromArgb(0, 120, 215),    // Blue
                    Color.FromArgb(255, 185, 0),    // Yellow
                    Color.FromArgb(232, 17, 35),    // Red
                    Color.FromArgb(16, 124, 16),    // Green
                    Color.FromArgb(153, 153, 153)   // Gray
                };

                int colorIndex = 0;
                while (reader.Read())
                {
                    string status = reader["status"].ToString();
                    int count = Convert.ToInt32(reader["count"]);

                    var point = chartQuotationStatus.Series["Data"].Points.Add(count);
                    point.LegendText = $"{status} ({count})";
                    point.Label = count.ToString();
                    point.Color = colors[colorIndex % colors.Length];

                    colorIndex++;
                }
            }
        }

        private void LoadMonthlySalesData(MySqlConnection conn)
        {
            chartMonthlySales.Series["Data"].Points.Clear();
            chartMonthlySales.Series["Data"].Color = Color.FromArgb(0, 120, 215);

            string query = @"SELECT 
                               DATE_FORMAT(quotation_date, '%Y-%m') as month,
                               SUM(total_cost) as total
                           FROM quotation
                           WHERE quotation_date >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
                           GROUP BY DATE_FORMAT(quotation_date, '%Y-%m')
                           ORDER BY month";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string month = reader["month"].ToString();
                    decimal total = Convert.ToDecimal(reader["total"]);

                    DateTime date = DateTime.ParseExact(month, "yyyy-MM", null);
                    string monthLabel = date.ToString("MMM yyyy");

                    chartMonthlySales.Series["Data"].Points.AddXY(monthLabel, total);
                }
            }

            chartMonthlySales.ChartAreas[0].AxisY.LabelStyle.Format = "₱#,##0";
        }

        private void LoadProjectStatusData(MySqlConnection conn)
        {
            chartProjectStatus.Series["Data"].Points.Clear();

            string query = @"SELECT 
                               COALESCE(NULLIF(TRIM(status), ''), 'No Status') as status,
                               COUNT(*) as count 
                           FROM project_list 
                           GROUP BY status
                           ORDER BY count DESC";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Color[] colors = {
                    Color.FromArgb(0, 120, 215),
                    Color.FromArgb(16, 124, 16),
                    Color.FromArgb(255, 140, 0),
                    Color.FromArgb(232, 17, 35),
                    Color.FromArgb(156, 39, 176)
                };

                int index = 0;
                while (reader.Read())
                {
                    string status = reader["status"].ToString();
                    int count = Convert.ToInt32(reader["count"]);

                    chartProjectStatus.Series["Data"].Points.AddXY(status, count);
                    chartProjectStatus.Series["Data"].Points[index].Color = colors[index % colors.Length];

                    index++;
                }
            }
        }

        private void LoadBudgetUtilizationData(MySqlConnection conn)
        {
            chartBudgetUtilization.Series["Data"].Points.Clear();

            string query = @"SELECT 
                               category_name,
                               SUM(category_expenses) as spent
                           FROM budget_category
                           GROUP BY category_name
                           ORDER BY spent DESC
                           LIMIT 10";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string category = reader["category_name"].ToString();
                    decimal spent = Convert.ToDecimal(reader["spent"]);

                    chartBudgetUtilization.Series["Data"].Points.AddXY(category, spent);
                }
            }

            foreach (var point in chartBudgetUtilization.Series["Data"].Points)
            {
                point.Color = Color.FromArgb(0, 120, 215);
            }

            chartBudgetUtilization.ChartAreas[0].AxisY.LabelStyle.Format = "₱#,##0";
        }

        private void LoadTopClientsData(MySqlConnection conn)
        {
            chartTopClients.Series["Data"].Points.Clear();

            string query = @"SELECT 
                               c.company_name,
                               SUM(q.total_cost) as total_value
                           FROM quotation q
                           JOIN company_list c ON q.company_id = c.id
                           GROUP BY c.company_name
                           ORDER BY total_value DESC
                           LIMIT 5";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string company = reader["company_name"].ToString();
                    decimal value = Convert.ToDecimal(reader["total_value"]);

                    chartTopClients.Series["Data"].Points.AddXY(company, value);
                }
            }

            foreach (var point in chartTopClients.Series["Data"].Points)
            {
                point.Color = Color.FromArgb(16, 124, 16);
            }

            chartTopClients.ChartAreas[0].AxisX.LabelStyle.Format = "₱#,##0";
        }

        private void LoadPOTrendData(MySqlConnection conn)
        {
            chartPOTrend.Series["Data"].Points.Clear();
            chartPOTrend.Series["Data"].Color = Color.FromArgb(255, 140, 0);

            string query = @"SELECT 
                               DATE_FORMAT(po_date, '%Y-%m') as month,
                               COUNT(*) as count
                           FROM purchase_order
                           WHERE po_date >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
                           GROUP BY DATE_FORMAT(po_date, '%Y-%m')
                           ORDER BY month";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string month = reader["month"].ToString();
                    int count = Convert.ToInt32(reader["count"]);

                    DateTime date = DateTime.ParseExact(month, "yyyy-MM", null);
                    string monthLabel = date.ToString("MMM yyyy");

                    chartPOTrend.Series["Data"].Points.AddXY(monthLabel, count);
                }
            }
        }
    }
}