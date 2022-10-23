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
    public partial class CheckAppoinmentsPage : Form
    {
        OleDbConnection connection;
        List<Patient> Patients = new List<Patient>();
        List<Appointment> Appointments = new List<Appointment>();
        public CheckAppoinmentsPage()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();

            var command = "Select * from Patient";
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Patient
                {
                    Id = Convert.ToInt32(dr["Patient_Id"]),
                    Name = dr["Patient_Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString(),
                    Mobile = dr["Mobile"].ToString(),
                    Address = dr["Address"].ToString(),
                };
                if (tempUser != null)
                {
                    Patients.Add(tempUser);
                }

            }

            var command3 = "Select * from Appointment";
            OleDbCommand command4 = new OleDbCommand(command3, connection);
            OleDbDataAdapter adapter1 = new OleDbDataAdapter();
            adapter1.SelectCommand = command4;
            var ds1 = new DataSet();
            adapter1.Fill(ds1);
            var dt1 = ds1.Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                var doctorId = Convert.ToInt32(dr["Doctor_Id"]);
                if (doctorId == Utility.Utility.Doctor.Id)
                {
                    var patientId = Convert.ToInt32(dr["Patient_Id"]);
                    var patient = Patients.Find(x => x.Id == patientId);
                    var remarks = dr["Remarks"].ToString();// == "Booked" ? "Awaiting for Doctors Approval" : "Booking Confirmed. Please reach opticals before 30mins of appointment time";
                    var tempUser = new Appointment
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        PatientId = patientId,
                        Name = patient.Name,
                        Time = dr["Appointment_Date"].ToString(),
                        Status = dr["Status"].ToString(),
                        Remarks = remarks
                    };
                    if (tempUser != null && tempUser.Time.Contains(this.dateTimePicker1.Text))
                    {
                        Appointments.Add(tempUser);
                    }
                }

            }

            this.dataGridView1.DataSource = Appointments;
        }
    }
}
