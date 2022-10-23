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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();
            if (this.radioButton1.Checked == true)
            {
                var command = String.Format("Insert INTO [Doctor] ([DoctorName], [Email], [Mobile], [UserName], [Password], [Address]) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, maskedTextBox1.Text, richTextBox1.Text);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                command2.ExecuteNonQuery();
                MessageBox.Show("Doctor details inserted!!!");
           }
            else
            {
                var command = String.Format("Insert INTO [Patient] ([Patient_Name], [Email], [UserName], [Password], [Address], [Mobile]) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", textBox1.Text, textBox2.Text, textBox4.Text, maskedTextBox1.Text, richTextBox1.Text, textBox3.Text);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                command2.ExecuteNonQuery();
                MessageBox.Show("Patient details inserted!!!");
            }
            ClearTextBoxes();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
    }
}
