using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryManegmentSystem
{
    public partial class ShowBill : Form
    {
        public ShowBill()
        {
            InitializeComponent();
            
        }
        public string  OpenPDF(string s)

        {
            //MessageBox.Show("Client name:");
            axAcroPDF1.src = "C:\\Users\\sandeepa\\Desktop\\System 1\\GroceryManegmentSystem\\GroceryManegmentSystem\\bin\\Debug\\"+ s +".pdf";
            axAcroPDF1.Show();
            return null;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}
