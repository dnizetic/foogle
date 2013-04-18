using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for UserRegister.xaml
    /// </summary>
    public partial class UserRegisterWindow : Window
    {

        //http://developer.linkedin.com/thread/1190
        public UserRegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //create professor user
            //send confirmation email

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.google.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            //we need a foogle gmail created
            client.Credentials = new NetworkCredential("myemail@gmail.com", "password");

            String email = ProfessorEmail.Text;
            MailMessage mm = new MailMessage("donotreply@domain.com", email);
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RememberMeCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
