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
    public partial class GetPrescriptionPage : Form
    {
        List<Prescription> Prescriptions = new List<Prescription>();
        List<Doctor> Doctors = new List<Doctor>();
        public GetPrescriptionPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
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
            OleDbCommand command3;
            var command4= "Select * from Prescription where Patient_Id=" + Utility.Utility.Patient.Id;
            command3 = new OleDbCommand(command4, connection);
            OleDbDataAdapter adapter1 = new OleDbDataAdapter();
            adapter1.SelectCommand = command3;
            var ds1 = new DataSet();
            adapter1.Fill(ds1);
            var dt1 = ds1.Tables[0];
            foreach(DataRow dr in dt1.Rows)
            {
                var doctor = Doctors.Find(x => x.Id == Convert.ToInt32(dr["Doctor_Id"]));
                var temp = new Prescription
                {
                    Date = dr["Prescription_Date"].ToString(),
                    Description = dr["Prescription"].ToString(),
                    ProvidedBy = doctor.Name
                };
                Prescriptions.Add(temp);
            }

            var prescription = Prescriptions.FindAll(x=>x.Date ==  this.dateTimePicker1.Text);
            this.dataGridView1.DataSource = prescription;
        }
    }
}
