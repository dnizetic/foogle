using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

using Npgsql;


namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitialiseDatabaseConnection();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            /* TODO: 
             *      Korisnik nije prijavljen:
             *          1. otvori formu za prijavu i ponudi:
             *                                      + registraciju
             *                                      + povezivanje s LinkedIn računom
             *                                      + prijavu sa postoječim podacima
             *          2. poveži se s bazom
             *          3. promijeni naziv u "Odjava" i dodaj korisničko ime u UserLabel
             *          
             *      Korisnik je prijavljen:
             *          1. prekini vezu sa bazom
             *          2. Promjeni naziv u "Prijava" i makni korisničko ime iz UserLabel
             */
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }



        /*
         * SuggestSkills()
         *
         * Based on current text in the search box, this
         * function returns a set of suggestions for the
         * user.
         *
         * @param (String current_text) current text typed in the search box
         * //@return (String[]) array of suggestions (strings)
         */
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        private void SuggestSkills(String current_text)
        {
            try
            {
                
                string sql = "select * from skill where skill_tag LIKE '%" + current_text + "%';";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                ds.Reset();

                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                //dataGridView1.DataSource = dt;

                String[] suggs = new String[10];

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }



        private void SearchCombo_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("I'm changed");
        }



        public static NpgsqlConnection conn = null;
        private void InitialiseDatabaseConnection()
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "localhost", "5432", "postgres",
                    "alphaomega", "foogle");
                
                conn = new NpgsqlConnection(connstring);
                conn.Open();
                
            }  catch (Exception msg) {
               
                MessageBox.Show(msg.ToString());
                throw;
            }
        }

    }
}
