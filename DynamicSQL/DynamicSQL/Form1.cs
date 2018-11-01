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

        List<string> _databaseItems = new List<string>();
        List<string> _itemDependancies = new List<string>();
        string connectionString = null;


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

            
            SqlConnection cnn;
            connectionString = @"Data Source=PL8\MTCDB;Initial Catalog=master;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionString);
            SqlCommand getDB = new SqlCommand("SELECT name FROM sys.Databases", cnn);
            SqlDataAdapter SQLDA = new SqlDataAdapter(getDB);
            DataTable DBs = new DataTable();
            SQLDA.Fill(DBs);
            //opens connection to sql 

            for (int i = 0; i < DBs.Rows.Count; i++)
            {
                string dbName = DBs.Rows[i][0].ToString();
                comboBox1.Items.Add(dbName);
                //get list of databases from server & put them in the listbox
            }


               //if connection is successful, it shows the other boxes. 


                //listBox2.Visible = true;
                //label2.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select item
            listBox2.Visible = true;
            label2.Visible = true;
            SqlConnection cnn;
            connectionString = @"Data Source=PL8\MTCDB;Initial Catalog=master;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionString);
            SqlCommand getDB = new SqlCommand((@"select name from sysobjects where xtype IN ('IT', 'V', 'P') AND uid = " + (comboBox1.SelectedIndex + 1)), cnn);
            SqlDataAdapter SQLDA = new SqlDataAdapter(getDB);
            DataTable DBs = new DataTable();
            SQLDA.Fill(DBs);
            //opens connection to sql 

            for (int i = 0; i < DBs.Rows.Count; i++)
            {
                string dbName = DBs.Rows[i][0].ToString();
                _databaseItems.Add(dbName);
                //get list of databases from server & put them in the listbox
            }

            //on select, use stored procedure to get related items from SQL server


            //add those items to a list

            //fill listbox with items
            listBox2.DataSource = _databaseItems;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select item
            listBox1.Visible = true;
            label3.Visible = true;
            SqlConnection cnn;
            connectionString = @"Data Source=PL8\MTCDB;Initial Catalog=master;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionString);
            SqlCommand getDB = new SqlCommand((@"EXEC sp_depends @objname = N'" + listBox2.SelectedItem + "';"), cnn);
            SqlDataAdapter SQLDA = new SqlDataAdapter(getDB);
            DataTable DBs = new DataTable();
            SQLDA.Fill(DBs);
            //opens connection to sql 
            if (DBs.Rows.Count != 0)
            {

            for (int i = 0; i < DBs.Rows.Count; i++)
            {
                string dbName = DBs.Rows[i][0].ToString();
                _itemDependancies.Add(dbName);
                //get list of databases from server & put them in the listbox
            }
            }
            else
            {
                _itemDependancies.Add("No references exist for this object.");
            }

            listBox1.DataSource = _itemDependancies;

        }
    }
}
