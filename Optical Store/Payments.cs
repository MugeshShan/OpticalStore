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
    public partial class Payments : Form
    {
        public Payments()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();

            var command3 = "Select * from Payment";
            OleDbCommand command4 = new OleDbCommand(command3, connection);
            OleDbDataAdapter adapter1 = new OleDbDataAdapter();
            adapter1.SelectCommand = command4;
            var ds1 = new DataSet();
            adapter1.Fill(ds1);
            var dt1 = ds1.Tables[0];

            var payment = new List<object>();

            foreach (DataRow dr in dt1.Rows)
            {
                if (dr["Payment_Date"].ToString().Contains(this.dateTimePicker1.Text))
                {
                    var tempUser = new
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        PatientId = Convert.ToInt32(dr["Patient_Id"]),
                        Amount = Convert.ToInt32(dr["Amount"]),
                        PaymentType = dr["Payment_Type"].ToString(),
                        Date = dr["Payment_Date"].ToString()

                    };

                    payment.Add(tempUser);
                }
            }

            this.dataGridView1.DataSource = payment;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
