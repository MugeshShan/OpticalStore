using Optical_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optical_Store
{
    public partial class Appointments : Form
    {
        public Appointments()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();

            var command3 = "Select * from Appointment";
            OleDbCommand command4 = new OleDbCommand(command3, connection);
            OleDbDataAdapter adapter1 = new OleDbDataAdapter();
            adapter1.SelectCommand = command4;
            var ds1 = new DataSet();
            adapter1.Fill(ds1);
            var dt1 = ds1.Tables[0];

            var appointmentList = new List<object>();

            foreach(DataRow dr in dt1.Rows)
            {
                if (dr["Appointment_Date"].ToString().Contains(this.dateTimePicker1.Text))
                {
                    var tempUser = new
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        PatientId = Convert.ToInt32(dr["Patient_Id"]),
                        DoctorId = Convert.ToInt32(dr["Doctor_Id"]),
                        Time = dr["Appointment_Date"].ToString(),
                        Status = dr["Status"].ToString(),
                        Remarks = dr["Remarks"].ToString(),

                    };

                    appointmentList.Add(tempUser);
                }
            }

            this.dataGridView1.DataSource = appointmentList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
