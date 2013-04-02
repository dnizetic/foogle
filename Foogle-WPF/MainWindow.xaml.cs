﻿using System;
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

        private bool g_bConnectedTestDebug = false;
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Primjer funkcionalnosti, obrisati!
            if (g_bConnectedTestDebug)
            {
                UserLabel.Content = "Prijavite se! ->";
                UserLabel.Foreground = Brushes.Black;
                UserLabel.FontWeight = FontWeights.Bold;

                LoginButton.Content = "Prijava";
                g_bConnectedTestDebug = false;
            }
            else
            {
                UserLabel.Content = "John Doe";
                UserLabel.Foreground = Brushes.Blue;
                UserLabel.FontWeight = FontWeights.Normal;

                LoginButton.Content = "Odjava";
                g_bConnectedTestDebug = true;
            }
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
            // TODO: Provjeri login status, dozvoli samo ako je prijavljen
            // Tražilica bi trebala raditi i na način da se mogu pretražiti korisnici po imenu ili bilo čemu drugome
           
            SuggestSkills(SearchCombo.Text);
        }


        /*
         * SuggestSkills()
         *
         * Based on current text in the search box, this
         * function returns a set of suggestions for the
         * user.
         * 
         * Example: user types "p", suggestions: "php, ..."
         * Example: user types "q", suggestion: "jquery, ..."
         * Examples are listed in a ListBox below the TextBox
         *
         * @param (String current_text) current text typed in the search box
         * //TODO: @return (String[]) array of suggestions (strings)
         */
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        private String[] SuggestSkills(String searchText)
        {
            try
            {
                
                string sql = "select * from skill where skill_tag LIKE '%" + searchText + "%' limit 10;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, sqlConnection);

                ds.Reset();

                da.Fill(ds);
                // since C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                //dataGridView1.DataSource = dt;  


                // TODO: Mislim da bi trebalo tu izbacit van tablicu s imenima, popisom skillova i bodovima =)
                String[] suggestions = new String[10];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    String s = row["skill_tag"] as String;
                    suggestions[i++] = s;

                    //if(i == 1)
                    //    MessageBox.Show(s);
                }

                return suggestions;

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }


        //@zvjerka
        //promjenil sam iz combo u textbox (ima razloga koristit combobox? 
        //ovo ne bu biranje skillova vec autocomplete.
        private void SearchCombo_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("DEBUG: I'm changed"); // ovo ne radi kad se upisuje tekst, mislim da samo kad se bira
        }





        //http://stackoverflow.com/questions/950770/autocomplete-textbox-in-wpf

        //this.SearchBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //this.SearchBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        private void OnSearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("Text changed");

            TextBox t = sender as TextBox;
            if (t != null)
            {
                //say you want to do a search when user types 1 or more chars
                if (t.Text.Length >= 1)
                {
                    //SuggestStrings will have the logic to return array of strings either from cache/db
                    string[] arr = SuggestSkills(t.Text);

                    foreach (String s in arr)
                    {
                        //if(s.Length > 0)
                        MessageBox.Show(s);
                    }

                    //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                    //collection.AddRange(arr);

                    //this.textBox1.AutoCompleteCustomSource = collection;
                }
            }

        }


        public static NpgsqlConnection sqlConnection = null;
        private void InitialiseDatabaseConnection()
        {
            try
            {
                // PostgeSQL-style connection string
                string connectionString = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "localhost", "5432", "postgres", // TODO: postaviti bazu online i promijeniti adresu.
                    "alphaomega", "foogle");

                sqlConnection = new NpgsqlConnection(connectionString);
                sqlConnection.Open();

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                MessageBox.Show("Ako ne radi povezivanje, onda connection string nije dobar, provjerite isti! " +
                                "Isto tako, problem može biti da aplikacija nema pristup bazi jer baza ili nije na svome mjestu ili ne radi server.");
                throw; // nema baze ili veze - nema koristi od aplikacije.
            }
        }

    }
}