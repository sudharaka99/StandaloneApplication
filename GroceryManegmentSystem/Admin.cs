using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryManegmentSystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void billingCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cashier cashier = new Cashier();
            cashier.Show();
            cashier.LoadStockTableData();
        }

        private void manegeStockToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Stock_Keeper stock_Keeper = new Stock_Keeper();
            stock_Keeper.Show();
            stock_Keeper.LoadStockTableData();
        }

        private void staffRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff_registration staff_Registration = new Staff_registration();
            staff_Registration.Show();
            staff_Registration.LoadStaffRegistrationTableData();
        }

        private void btnFillter_Click_1(object sender, EventArgs e)
        {

            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String query = "select Sum(Price) from Cashier where BDate  Between @date1 and @date2 ";

                dateTimePicker1.CustomFormat = "MM/dd/yyyy";
                dateTimePicker2.CustomFormat = "MM/dd/yyyy";

                MessageBox.Show(dateTimePicker1.Text);

                command.Parameters.AddWithValue("@date1", dateTimePicker1.Text);
                command.Parameters.AddWithValue("@date2", dateTimePicker2.Text);
                command.CommandText = query;

                OleDbDataReader reader = null;
                reader = command.ExecuteReader();
                while (reader.Read())

                {

                    label1.Text = reader[0].ToString(); ;
                }



                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

        

    
    