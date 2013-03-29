using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Npgsql;

namespace foogle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //taken from http://stackoverflow.com/questions/1357853/autocomplete-textbox-control
        private void searchBox_TextChanged(object sender, EventArgs e)
        {

            TextBox t = sender as TextBox;

            if (t != null)
            {
                //say you want to do a search when user types 3 or more chars
                if (t.Text.Length >= 1)
                {

                    suggestSkills(t.Text);

                    //SuggestStrings will have the logic to return array of strings either from cache/db
                    
                    /*string[] arr = SuggestStrings(t.Text);

                    AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                    collection.AddRange(arr);

                    this.searchBox.AutoCompleteCustomSource = collection;*/
                }
            }

            //llOpenConnAndSelect_LinkClicked();
        }

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        private void suggestSkills(String current_text)
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "localhost", "5432", "postgres",
                    "alphaomega", "foogle");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "select * from skill where skill_tag LIKE '%" + current_text + "%';";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                dataGridView1.DataSource = dt;

                String[] suggs = new String[10];


                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString());
                throw;
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
