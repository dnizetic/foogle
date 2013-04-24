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

        //!Important
        //Replaced by Registration.xaml.cs

        //http://developer.linkedin.com/thread/1190
        public UserRegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            if (ProfIme.Text.Equals("")
            || ProfPrezime.Text.Equals(""))
            {
                MessageBox.Show("Unesite ime i prezime.");
                return;
            }
            if (!IsValidEmail(ProfessorEmail.Text))
            {
                MessageBox.Show("Uneseni mail nije validan.");
                return;
            }

            //create professor user
            //send confirmation email

            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            //we need a foogle gmail created
            client.Credentials = new NetworkCredential("fooglefoi@gmail.com",
                "alphaomega851");

            mail.To.Add(new MailAddress(ProfessorEmail.Text));
            mail.From = new MailAddress("fooglefoi@gmail.com");
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";

            try
            {
                client.Send(mail);
                MessageBox.Show("Zahtjev za registracijom je primljen. Kad vas racun bude verificiran od strane administratora, o tome cete biti obavijesteni emailom.");


                using (var context = new FoogleContext())
                {
                    context.Korisnici.Add(
                        new Korisnik
                        {
                            //id = 1,
                            aktiviran = false,
                            email = ProfessorEmail.Text,
                            ime = ProfIme.Text,
                            prezime = ProfPrezime.Text,
                            tip_korisnika = "p"
                        });

                    context.SaveChanges();
                }

                using (var context = new FoogleContext())
                {
                    context.Korisnici.Add(
                        new Korisnik
                        {
                            //id = 1,
                            aktiviran = false,
                            email = ProfessorEmail.Text,
                            ime = ProfIme.Text,
                            prezime = ProfPrezime.Text,
                            tip_korisnika = "p"
                        });

                    context.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }



        private void RememberMeCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
