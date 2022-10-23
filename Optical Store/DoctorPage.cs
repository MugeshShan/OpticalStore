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
    public partial class DoctorPage : Form
    {
        public DoctorPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckAppoinmentsPage checkAppoinmentsPage = new CheckAppoinmentsPage();
            checkAppoinmentsPage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfirmAppoinments confirmAppoinments = new ConfirmAppoinments();
            confirmAppoinments.Show();
        }
    }
}
