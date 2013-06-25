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
        public Login()
        {
            InitializeComponent();
            this.Loaded += Login_Loaded;
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            LoginButton.IsDefault = true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Unesite e-mail!";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Unesite valjani e-mail!";
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
                    var users = from user in context.Users
                               where user.email.Equals(email)
                               where user.password.Equals(password)
                               where user.confirmed.Equals(true)
                               select user;

                    if (users.Count() == 1)
                    {
                        FoogleUser user = users.First();
                        MainWindow.userID = Convert.ToInt32(user.id);
                        switch (user.role[0])
                        {
                            case 's':
                                MainWindow.authLevel = 1;
                                break;
                            case 'p':
                                MainWindow.authLevel = 666;
                                break;
                            case 'a':
                                MainWindow.authLevel = 1337;
                                break;
                            default:
                                MainWindow.authLevel = 0;
                                break;
                        }

                        Close();
                        return;
                    }
                    else
                    {
                        errormessage.Text = "Krivi e-mail i/ili lozinka!";
                    }
                }
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
