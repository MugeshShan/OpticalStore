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
    public partial class PaymentPage : Form
    {

        OleDbConnection connection;
        public PaymentPage()
        {
            connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();
            InitializeComponent();
            var type = new List<String>();
            type.Add("Card");
            type.Add("Cash");

            this.comboBox1.DataSource = type;

            this.dataGridView1.DataSource = Utility.Utility.Bookings;

            this.label4.Text = Utility.Utility.Bookings.Sum(x => x.Amount).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var command = String.Format("Insert INTO [Payment] ([Patient_Id], [Amount], [Payment_Type], [Payment_Date]) VALUES ({0}, {1}, '{2}', '{3}')", Utility.Utility.Patient.Id, Convert.ToInt32(this.label4.Text), this.comboBox1.Text, DateTime.Now.ToString());
            OleDbCommand command2 = new OleDbCommand(command, connection);
            command2.ExecuteNonQuery();
            MessageBox.Show("Payment Received !!!");
        }
    }
}
