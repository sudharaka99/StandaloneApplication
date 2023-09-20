﻿using System; 
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

    public partial class Stock_Keeper : Form
    {
        String findId = "0";
        
        public Stock_Keeper()
        {
            InitializeComponent();
        }

        private void Stock_Keeper_Load(object sender, EventArgs e)
        {





        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sendFindId(findId) == "0")

            {

                //MessageBox.Show(sendFindId(findId)); 


                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
                connection.Open();

                try
                {

                    String category = cmbCategories.SelectedItem.ToString();
                    String item = txtItemName.Text.ToString();
                    String quantity = txtQuantity.Text.ToString();
                    String price = txtPrice.Text.ToString();
                    //String categoryid=" ";

                 

                    String my_querry = "INSERT INTO Stock(Category,Item,Quantity,Price)VALUES('" + category + "','" + item + "','" + quantity + "','" + price + "')";

                    OleDbCommand cmd = new OleDbCommand(my_querry, connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data saved successfuly...!");
                    //this.stockTableAdapter.Fill(this.dBDataSet.Stock); 
                    //Load all data from table 
                    LoadStockTableData();

                    ClearTextBoxs();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed due to" + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                //Update database 
                string indexId = sendFindId(findId);
                //string categoryId = sendCategoryId(findcategoryId);
                MessageBox.Show(indexId);

                OleDbConnection connection2 = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
                connection2.Open();

                try
                {

                    String category = cmbCategories.SelectedItem.ToString();
                    String item = txtItemName.Text.ToString();
                    String quantity = txtQuantity.Text.ToString();
                    String price = txtPrice.Text.ToString();



                    String my_querry = "UPDATE Stock set  Category='" + category + "', Item='" + item + "',Quantity='" + quantity + "',Price='" + price + "' where Id=" + indexId + " ";



                    OleDbCommand cmd = new OleDbCommand(my_querry, connection2);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data saved successfuly...!");
                    findId = "0";
                    //findcategoryId = "0";

                    //Load all data from table 
                    LoadStockTableData();

                    ClearTextBoxs();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed due to" + ex.Message);
                }

                
                finally
                {
                    connection2.Close();
                    findId = "0";
                    //findcategoryId = "0";
                }

            }
        }
        public void Delete_Stock_Field()
        {
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
            connection.Open();

            try
            {



                string deleteId = sendFindId(findId);
                MessageBox.Show(deleteId);

                String my_querry = "DELETE * FROM Stock WHERE Id =" + deleteId + " ";



                OleDbCommand cmd = new OleDbCommand(my_querry, connection);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data deleted successfuly...!");

                //Load all data from table 
                findId = "0";
                LoadStockTableData();

                ClearTextBoxs();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed due to" + ex.Message);
            }
            finally
            {
                connection.Close();
                findId = "0";
            }



        }
        public void ClearTextBoxs()
        {
            cmbCategories.ResetText();
            txtItemName.Text = " ";
            txtQuantity.Text = " ";
            txtPrice.Text = " ";
        }
        private void button1_Click(object sender, EventArgs e)
        {


        }
        public void LoadStockTableData()
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String query = "select * from Stock";
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }

        }

       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //MessageBox.Show("Error "); 
                //MessageBox.Show(row.Cells["Id"].Value.ToString()); 



                findId = row.Cells["Id"].Value.ToString();
                //findcategoryId = row.Cells["Categoryid"].Value.ToString();


                cmbCategories.Text = row.Cells["Category"].Value.ToString();
                txtItemName.Text = row.Cells["Item"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }



        }
        public string sendFindId(string s)
        {

            return s;
        }
        public string sendCategoryId(string s)
        {

            return s;
        }

       
              
                  

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Delete_Stock_Field();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearTextBoxs();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();

        }
    }
}