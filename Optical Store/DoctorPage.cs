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
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ConfirmAppoinments confirmAppoinments = new ConfirmAppoinments();
            confirmAppoinments.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ProvidePrescriptions providePrescriptions = new ProvidePrescriptions();
            providePrescriptions.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form1 doctorPage = new Form1();
            doctorPage.Show();
            this.Close();
        }
    }
}
