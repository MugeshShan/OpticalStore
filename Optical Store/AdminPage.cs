using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optical_Store
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Appointments appointments = new Appointments();
            appointments.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Payments payments = new Payments();
            payments.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.Show();
        }
    }
}
