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
        List<string> _itemDependencies = new List<string>();
        List<string> _itemDependents = new List<string>();
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
            _databaseItems.Clear();
            listBox2.Visible = true;
            label2.Visible = true;
            SqlConnection cnn;
            connectionString = (@"Data Source=PL8\MTCDB;Initial Catalog="+ comboBox1.SelectedItem +";Trusted_Connection=True;");
            cnn = new SqlConnection(connectionString);
            SqlCommand getDB = new SqlCommand((@"select Table_schema, Table_name from INFORMATION_SCHEMA.TABLES UNION Select SPECIFIC_SCHEMA, SPECIFIC_NAME from INFORMATION_SCHEMA.ROUTINES where ROUTINE_TYPE = 'Procedure'"), cnn);
            SqlDataAdapter SQLDA = new SqlDataAdapter(getDB);
            DataTable DBs = new DataTable();
            SQLDA.Fill(DBs);
            //opens connection to sql 

            for (int i = 0; i < DBs.Rows.Count; i++)
            {
                string dbName = DBs.Rows[i][0].ToString() + "." + DBs.Rows[i][1].ToString();


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
            connectionString = (@"Data Source=PL8\MTCDB;Initial Catalog=" + comboBox1.SelectedItem + ";Trusted_Connection=True;");
            cnn = new SqlConnection(connectionString);
            SqlCommand getDB = new SqlCommand((@"EXEC sp_depends @objname = N'" + listBox2.SelectedItem + "';"), cnn);
            SqlDataAdapter SQLDA = new SqlDataAdapter(getDB);
            DataSet DBs = new DataSet();
            SQLDA.Fill(DBs);
            //opens connection to sql 
            if (DBs.Tables[0].Rows.Count != 0)
            {


                for (int i = 0; i < DBs.Tables[0].Rows.Count; i++)
                {
                    _itemDependents.Add(DBs.Tables[0].Rows[i][0].ToString());
                }

                for (int i = 0; i < DBs.Tables[1].Rows.Count; i++)
                {

                    _itemDependencies.Add(DBs.Tables[0].Rows[i][0].ToString());

                }
            }
            else
            {
                _itemDependencies.Add("No references exist for this object.");
            }

            listBox1.DataSource = _itemDependencies;
            listBox3.DataSource = _itemDependents;
            
        }
    }
}
