using Newtonsoft.Json;
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
    public partial class BookProductsPage : Form
    {
        OleDbConnection connection;
        List<Product> Products = new List<Product>();
        List<Booking> Bookings = new List<Booking>();
        public BookProductsPage()
        {
            InitializeComponent();
            connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["OpticalStore"];
            connection.Open();

            var command = "Select * from Product";
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Product
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductName = dr["Product_Name"].ToString(),
                    Amount = Convert.ToInt32(dr["Amount"]),
                    Description = dr["Description"].ToString()
                };
                if (tempUser != null)
                {
                    Products.Add(tempUser);
                }

            }

            var name = Products.Select(x => x.ProductName).ToList();
            this.comboBox1.DataSource = name;
            //this.dataGridView1.DataSource = Bookings;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var product = Products.Find(x => x.ProductName == this.comboBox1.Text);
            var booking = new Booking
            {
                ProductId = product.Id,
                ProductName = this.comboBox1.Text,
                Quantity = Convert.ToInt32(this.textBox1.Text),
                Amount = product.Amount * Convert.ToInt32(this.textBox1.Text),
            };
            var book = JsonConvert.SerializeObject(booking);
            Bookings.Add(booking);
            var command = String.Format("Insert INTO [Booking] ([Status], [Patient_Id], [Products], [Amount], [Booking_Date]) VALUES ('{0}', {1}, '{2}', {3}, '{4}')", "Booked", Utility.Utility.Patient.Id, product.ProductName, booking.Amount, DateTime.Now.ToString());
            OleDbCommand command2 = new OleDbCommand(command, connection);
            command2.ExecuteNonQuery();
            //this.InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var products = JsonConvert.SerializeObject(Bookings);
            var quantity = Bookings.Count();
            var total = Bookings.Sum(x => x.Amount);
            Utility.Utility.Bookings.AddRange(Bookings);
            var date = DateTime.Now.ToString();
            foreach (var booking in Bookings)
            {
                var command = String.Format("Insert INTO [Purchase] ([Products], [Patient_Id], [Quantity], [Amount], [Purchase_Date]) VALUES ('{0}', {1}, {2}, {3}, '{4}')", booking.ProductName, Utility.Utility.Patient.Id, booking.Quantity, total, date);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                command2.ExecuteNonQuery();
            }
            MessageBox.Show("Items Purchased !!!");
            PaymentPage paymentPage = new PaymentPage();
            paymentPage.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
