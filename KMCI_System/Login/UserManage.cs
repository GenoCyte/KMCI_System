using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMCI_System.Login
{
    public partial class UserManage : UserControl
    {
        private Panel headerPanel;
        private Label title;
        private Label subtitle;
        public UserManage()
        {
            InitializeCustomComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void add_user_Click(object sender, EventArgs e)
        {

        }

        private void UserManage_Load(object sender, EventArgs e)
        {

        }
        public void InitializeCustomComponent()
        {

            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White
            };
            {
                this.Dock = DockStyle.Fill;
                this.BackColor = Color.White;

                headerPanel = new Panel()
                {
                    Dock = DockStyle.Top,
                    Height = 88,
                    BackColor = Color.White
                };

            }
        }
    }
}
