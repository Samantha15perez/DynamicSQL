using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DynamicSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> _databases = new List<string>();



        private void Form1_Load(object sender, EventArgs e)
        { 
            string connectionString = null;
            SqlConnection cnn;
            connectionString = @"Data Source=PL8\MTCDB;Database=master;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionString);
            //opens connection to sql 



            //fills box
            try
            {
                cnn.Open();
                listBox2.Visible = true;
                label2.Visible = true;
                cnn.Close();
                //get list of databases from server & put them in the listbox

                comboBox1.DataSource = _databases;
            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
