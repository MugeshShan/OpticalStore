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
    public partial class ConfirmAppoinments : Form
    {
        OleDbConnection connection;
        List<Patient> Patients = new List<Patient>();
        List<Appointment> Appointments = new List<Appointment>();
        List<Patient> BookedPatients = new List<Patient>();
        List<Appointment> AppointmentOnMentionedDate = new List<Appointment>();

        public ConfirmAppoinments()
        {
            InitializeComponent();
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
                    var remarks = dr["Status"].ToString() == "Booked" ? "Awaiting for Doctors Approval" : "Booking Confirmed. Please reach opticals before 30mins of appointment time";
                    var tempUser = new Appointment
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = patient.Name,
                        Time = dr["Appointment_Date"].ToString(),
                        Status = dr["Status"].ToString(),
                        Remarks = remarks,
                        PatientId = patientId
                    };
                    if (tempUser != null)
                    {
                        Appointments.Add(tempUser);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var date = this.dateTimePicker1.Text;
            
            var appoinment = Appointments.FindAll(x => x.Time.Contains(date));
            foreach (var app in appoinment)
            {
                var patient = Patients.Find(x => x.Id == app.PatientId);
                if (patient != null)
                {
                    BookedPatients.Add(patient);
                    AppointmentOnMentionedDate.Add(app);
                }
            }
            this.comboBox1.DataSource = BookedPatients.Select(x => x.Name).ToList();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Utility.Utility.Appointment.Remarks = this.richTextBox1.Text;
            var patient = BookedPatients.Find(x => x.Name == this.comboBox1.Text);
            var appointment = AppointmentOnMentionedDate.Find(x => x.PatientId == patient.Id);
            var command = String.Format("Update Appointment SET [Remarks]='{0}' Where [ID]=@Id", this.richTextBox1.Text);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            cmd.Parameters.Clear();
            cmd.CommandText = command;
            cmd.Parameters.AddWithValue("@Id", appointment.Id);
            cmd.ExecuteNonQuery();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var patient = BookedPatients.Find(x => x.Name == this.comboBox1.Text);
            var appointment = AppointmentOnMentionedDate.Find(x => x.PatientId == patient.Id);
            this.label4.Text = appointment.Time;
            this.richTextBox1.Text = appointment.Remarks;
        }
    }
}
