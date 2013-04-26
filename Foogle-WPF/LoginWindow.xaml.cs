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
using System.Windows.Shapes;

namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static string Hash(string value)
        {
            var sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(value));
            return BytesToHex(hash).ToLower();
        }

        private static string BytesToHex(byte[] bytes)
        {
            return String.Concat(Array.ConvertAll(bytes, x => x.ToString("X2")));
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //get user email: if !exists, return false

            String email = Email.Text;
            String pass = Password.Text;


            using (var context = new FoogleContext())
            {
                var user = from a in context.Users
                                where a.email.Equals(email)
                                select a;

                
                if (user.Count() == 0)
                {
                    LoginError.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {

                    FoogleUser k = user.ElementAt(0);

                    if (pass.Equals(k.password))
                    {
                        MessageBox.Show("Uspjesno.");
                        this.Close();
                    }
                    else
                    {
                        LoginError.Visibility = System.Windows.Visibility.Visible;
                    }

                }
            }

        }

        private void RememberMeCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
