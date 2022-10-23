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
    public partial class AppointmentPage : Form
    {
        OleDbConnection connection;
        List<Doctor> Doctors = new List<Doctor>();
        public AppointmentPage()
        {
            InitializeComponent();
            var hour = new List<string>();
            hour.Add("10");
            hour.Add("11");
            hour.Add("12");
            hour.Add("01");
            hour.Add("05");
            hour.Add("06");
            hour.Add("07");
            hour.Add("08");
            hour.Add("09");
            this.comboBox2.DataSource = hour;

            var min = new List<string>();
            min.Add("00");
            min.Add("15");
            min.Add("30");
            min.Add("45");
            this.comboBox3.DataSource = min;

            var sec = new List<string>();
            sec.Add("AM");
            sec.Add("PM");
            this.comboBox4.DataSource = sec;
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

            var name = Doctors.Select(x => x.Name).ToList();
            this.comboBox1.DataSource = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var doctor = Doctors.Find(x=>x.Name == this.comboBox1.SelectedItem.ToString());
            var appointment_date = this.dateTimePicker1.Text + ':' + this.comboBox2.SelectedItem.ToString() + ':' + this.comboBox3.SelectedItem.ToString() + ':' + this.comboBox4.SelectedItem.ToString();
            var command = String.Format("Insert INTO [Appointment] ([Type], [Status], [Patient_Id], [Doctor_Id], [Appointment_Date]) VALUES ('{0}', '{1}', {2}, {3}, '{4}')", "Online", "Booked", Utility.Utility.Patient.Id, doctor.Id, appointment_date);

            OleDbCommand command2 = new OleDbCommand(command, connection);
            command2.ExecuteNonQuery();
            MessageBox.Show("Appoinment Booked!!! Please check appointment status one day before in Appointment Status page !!!");

        }
    }
}
