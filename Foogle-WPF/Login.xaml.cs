﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class Login : Window
    {

        Label lab;
        public Login(Label l)
        {
            InitializeComponent();
            lab = l;
        }
        Registration registration = new Registration();
        Welcome welcome = new Welcome();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                /* SqlConnection con = new SqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Registration where Email='" + email + "'  and password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet); */



                //check if lid already exists
                using (var context = new FoogleContext())
                {
                    var usrs = from b in context.Users
                               where b.email.Equals(email)
                               where b.password.Equals(password)
                               where b.confirmed.Equals(true)
                               select b;


                    

                    if (usrs.Count() == 1)
                    {
                        MessageBox.Show("Dobrodosli!");

                        //Label l = this.FindName("LoggedInLabel") as Label;
                        //l.Content = "Dobrodosli!";
                        lab.Content = "Dobrodosli!";

                        MainWindow.logged_in = true;

                        FoogleUser f = usrs.First();
                        MainWindow.professor_id = f.id;

                        Close();

                        return;
                    }
                    else
                    {
                        errormessage.Text = "Sorry! Please enter existing emailid/password.";
                    }
                }
                //con.Close();
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }

    }
}
