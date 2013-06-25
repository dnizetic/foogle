using System;
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

        //Welcome welcome = new Welcome();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
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
                        MainWindow.authLevel = 1337; // TODO: Postavi prema tipu korisnika!

                        FoogleUser f = usrs.First();
                        MainWindow.userID = Convert.ToInt32(f.id);

                        Close();
                        return;
                    }
                    else
                    {
                        errormessage.Text = "Krivi e-mail i/ili lozinka!";
                    }
                }
                //con.Close();
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

    }
}
