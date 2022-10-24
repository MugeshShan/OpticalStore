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
    public partial class Login : Form
    {
        List<Doctor> Doctors = new List<Doctor>();
        List<Patient> Patients  =  new List<Patient>();
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if(control is MaskedTextBox)
                        (control as MaskedTextBox).Clear();
                    else if(control is RichTextBox)
                        (control as RichTextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();
            OleDbCommand command2;
            if (this.textBox1.Text == "Admin" && this.maskedTextBox1.Text == "admin@123")
            {
                AdminPage adminPage = new AdminPage();
                adminPage.Show();
                this.Close();
            }
            else
            {
                if (this.radioButton1.Checked == true)
                {
                    var command = "Select * from Doctor";
                    command2 = new OleDbCommand(command, connection);
                }
                else
                {
                    var command = "Select * from Patient";
                    command2 = new OleDbCommand(command, connection);
                }

                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command2;
                var ds = new DataSet();
                adapter.Fill(ds);
                var dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.radioButton1.Checked == true)
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
                    else
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
                }

                if (this.radioButton1.Checked == true)
                {
                    var doctor = Doctors.Find(x => x.UserName == textBox1.Text && x.Password == maskedTextBox1.Text);
                    if (doctor != null)
                    {
                        ClearTextBoxes();
                        MessageBox.Show("Welcome Doctor " + doctor.Name + " !!!");
                        Utility.Utility.Doctor = doctor;
                        DoctorPage doctorPage = new DoctorPage();
                        doctorPage.Show();
                    }
                    else
                    {
                        ClearTextBoxes();
                        MessageBox.Show("Username or Password is incorrect !!!");
                    }
                }
                else
                {
                    var patient = Patients.Find(x => x.UserName == textBox1.Text && x.Password == maskedTextBox1.Text);
                    if (patient != null)
                    {
                        ClearTextBoxes();
                        MessageBox.Show("Welcome " + patient.Name + " !!!");
                        Utility.Utility.Patient = patient;
                        PatientPage patientPage = new PatientPage();
                        patientPage.Show();
                    }
                    else
                    {
                        ClearTextBoxes();
                        MessageBox.Show("Username or Password is incorrect !!!");
                    }
                }
            }
        }
    }
}
