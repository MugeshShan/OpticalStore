﻿using System;
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
    public partial class PatientPage : Form
    {
        public PatientPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            this.Close();
            //PatientPage patientPage = new PatientPage();
            //patientPage.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AppointmentStatusPage appointmentStatusPage =   new AppointmentStatusPage(); ;
            appointmentStatusPage.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookProductsPage bookProductsPage = new BookProductsPage(); ;
            bookProductsPage.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetPrescriptionPage prescriptionPage = new GetPrescriptionPage();
            prescriptionPage.Show();
            this.Close();
        }
    }
}
