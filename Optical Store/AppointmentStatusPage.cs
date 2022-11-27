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
    public partial class AppointmentStatusPage : Form
    {
        OleDbConnection connection;
        List<Doctor> Doctors = new List<Doctor>();
        List<Appointment> Appointments = new List<Appointment>();
        public AppointmentStatusPage()
        {
            InitializeComponent();
            connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();

            var command = "Select * from Doctor";
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Doctor
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["DoctorName"].ToString(),
                    Email = dr["Email"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString(),
                    Mobile = dr["Mobile"].ToString(),
                    Address = dr["Address"].ToString(),
                };
                if (tempUser != null)
                {
                    Doctors.Add(tempUser);
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
                var patientId = Convert.ToInt32(dr["Patient_Id"]);
                if (patientId == Utility.Utility.Patient.Id)
                {
                    var doctorId = Convert.ToInt32(dr["Doctor_Id"]);
                    var doctor = Doctors.Find(x => x.Id == doctorId);
                    var remarks = dr["Status"].ToString();// == "Booked" ? "Awaiting for Doctors Approval" : "Booking Confirmed. Please reach opticals before 30mins of appointment time";
                    var tempUser = new Appointment
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = doctor.Name,
                        Time = dr["Appointment_Date"].ToString(),
                        Status = dr["Status"].ToString(),
                        Remarks = remarks,
                        PatientId = Convert.ToInt32(dr["Patient_Id"])
                    };
                    if (tempUser != null)
                    {
                        Appointments.Add(tempUser);
                    }
                }

            }

            this.dataGridView1.DataSource = Appointments;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            PatientPage patientPage = new PatientPage();
            patientPage.Show();
            this.Close();
        }
    }
}
