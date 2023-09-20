using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace GroceryManegmentSystem
{
    public partial class Cashier : Form
    {
        String findQuantity = "0";
        String findId = "0";
        public string ClientBillName;

       
        public Cashier()
        {
            InitializeComponent();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                findId = row.Cells["Id"].Value.ToString();
                findQuantity = row.Cells["Quantity"].Value.ToString();



                txtItemNameCas.Text = row.Cells["Item"].Value.ToString();
                txtQuantityCas.Text = row.Cells["Quantity"].Value.ToString();
                txtPriceCas.Text = row.Cells["Price"].Value.ToString();

                if (findQuantity == "0")
                {
                    MessageBox.Show("Out of the stock");
                    ClearTextBoxs();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }
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
                dataGridView2.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }

        }

        public void ClearTextBoxs()
        {

            txtItemNameCas.Text = " ";
            txtQuantityCas.Text = " ";
            txtPriceCas.Text = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string indexId = sendFindId(findId);
            string Quantity = sendQuantity(findQuantity);
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
            connection.Open();

            OleDbConnection connection1 = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
            connection1.Open();

            if (txtClientCas.Text == " ")
            {
                MessageBox.Show("Enter Client Name");
            }
            else
            {
                try
                {

                    String client = txtClientCas.Text.ToString();
                    String billing = txtBilingCas.Text.ToString();
                    String item = txtItemNameCas.Text.ToString();
                    String quantity = txtQuantityCas.Text.ToString();
                    String price = txtPriceCas.Text.ToString();
                    string bdate = DateTime.Now.ToString("M/d/yyyy");
                    
                    int newQuantity = 0;
                    newQuantity = Int32.Parse(Quantity) - Int32.Parse(quantity);
                    double newQuantity1 = Convert.ToDouble(price)  * Int32.Parse(quantity);

                    //MessageBox.Show(indexId + " " + Quantity + " " + newQuantity);


                    String my_querry = "INSERT INTO Cashier(ClientName,BDate,BillingDate,Item,Quantity,Price)VALUES('" + client + "','" + bdate + "','" + billing + "','" + item + "','" + quantity + "','" + newQuantity1 + "')";

                    String my_querry1 = "UPDATE Stock set Quantity='" + newQuantity + "' where Id=" + indexId + " ";


                    OleDbCommand cmd = new OleDbCommand(my_querry, connection);
                    cmd.ExecuteNonQuery();

                    OleDbCommand cmd1 = new OleDbCommand(my_querry1, connection1);
                    cmd1.ExecuteNonQuery();




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
                    connection1.Close();
                }

                LoadCashierTableData();
                indexId = "0";
                Quantity = "0";

            }

            txtClientCas.Enabled = false;

        }
        public void LoadCashierTableData()
        {
            try
            {
                String client = txtClientCas.Text.ToString();
                String billing = txtBilingCas.Text.ToString();
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\sandeepa\Desktop\System 1\DB.accdb");
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;



                String query = "select Item,Quantity,Price from Cashier where ClientName=@client and BillingDate=@billing";

                command.Parameters.AddWithValue("@client", SqlDbType.VarChar).Value = client;
                command.Parameters.AddWithValue("@billing", SqlDbType.VarChar).Value = billing;


                command.CommandText = query;


                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCashierbill.DataSource = dt;


                double TotalPrice = Convert.ToDouble(dt.Compute("SUM(Price)", string.Empty));

                lblamount.Text = "Rs." + TotalPrice.ToString();
                CreatePDF(client, billing, TotalPrice.ToString());
                ClientBillName = client;

                connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);

            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            txtBilingCas.Text = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            txtBilingCas.Enabled = false;
            txtClientCas.Text = " ";
            groupBox1.Enabled = true;
            dataGridView2.Enabled = true;
            txtClientCas.Enabled = true;
            BtnFinish.Enabled = true;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();


        }

        private void Cashier_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            dataGridView2.Enabled = false;
            BtnFinish.Enabled = false;
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            dataGridView2.Enabled = false;
            dgvCashierbill.DataSource = " ";
            lblamount.Text = "-------------- ";
            ShowBill myobg = new ShowBill();
            myobg.Show();
            myobg.OpenPDF(ClientBillName);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void CreatePDF(String name, String billdate, String tot)

        {
            string Billname = name;

            try
            {
                MessageBox.Show(dgvCashierbill.Columns.Count.ToString());
                PdfPTable pdfTable = new PdfPTable(dgvCashierbill.Columns.Count);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                foreach (DataGridViewColumn column in dgvCashierbill.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    pdfTable.AddCell(cell);
                }

                foreach (DataGridViewRow row in dgvCashierbill.Rows)
                {

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //MessageBox.Show("Cell value:"+cell.Value.ToString());


                        //MessageBox.Show("Cell value:" + cell.Value.ToString());
                        if (cell.Value != null)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                            // MessageBox.Show("Not null");
                        }



                    }
                }



                using (FileStream stream = new FileStream(Billname + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A6, 20, 10, 10, 10);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Paragraph pdfTitle = new Paragraph("                         Neel’s Grocery (Pvt) Ltd        \n INVOICE\n------------------------------------------------------------------\n");
                    Paragraph pdfclentdetails = new Paragraph("\nClient Name:   " + name + "\nDate & Time:       " + billdate + "\n\n");

                    Paragraph para1 = new Paragraph("     \nTotal Amount:   Rs." + tot + "\n\n------------------------ Thank You -------------------------");

                    //Paragraph para2 = new Paragraph("Good Day");



                    pdfDoc.Add(pdfTitle);
                    pdfDoc.Add(pdfclentdetails);

                    pdfDoc.Add(pdfTable);

                    pdfDoc.Add(para1);
                    // pdfDoc.Add(para2);

                    pdfDoc.Close();
                    stream.Close();


                }



                MessageBox.Show("Data Exported Successfully !!!", "Info");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }
        public string sendFindId(string s)
        {

            return s;
        }

        public string sendQuantity(string s)
        {

            return s;
        }

        private void dgvCashierbill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}







        



