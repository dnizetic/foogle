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

using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;

namespace Foogle_WPF
{

    //24.4.
    //code derived from:
    //http://tech.pro/tutorial/742/wpf-tutorial-using-the-listview-part-1

    /// <summary>
    /// Interaction logic for AdminUserConfirmationWindow.xaml
    /// </summary>
    public partial class AdminUserConfirmationWindow : Window
    {

        public ObservableCollection<FoogleUser> _KorisnikCollection =
            new ObservableCollection<FoogleUser>();

        private void populateCollection()
        {
            _KorisnikCollection.Clear();

            //get all professors 'p' that are unactivated
            using (var context = new FoogleContext())
            {
                //neaktivirani profesori
                var professors = from a in context.Users
                                 where a.confirmed.Equals(false)
                                 where a.role.Equals("p")
                                 select a;


                foreach (var p in professors)
                {
                    _KorisnikCollection.Add(p);

                }

            }
        }

        public ObservableCollection<FoogleUser> KorisnikCollection
        {
            get
            {


                return _KorisnikCollection;

            }
        }

        public AdminUserConfirmationWindow()
        {

            populateCollection();

            InitializeComponent();
        }




        //http://stackoverflow.com/questions/765984/wpf-listboxitems-with-datatemplates-how-do-i-reference-the-clr-object-bound-to
        private void ConfirmButton(object sender, RoutedEventArgs e)
        {
            
            var usr  = ((Button)sender).DataContext as FoogleUser;

            if (usr == null)
                throw new InvalidOperationException("Invalid DataContext");


            var user = new FoogleUser() { id = usr.id, confirmed = true };
            using (var db = new FoogleContext())
            {
                db.Users.Attach(user);
                db.Entry(user).Property(x => x.confirmed).IsModified = true;
                db.SaveChanges();
            }


            //send email
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

            mail.To.Add(new MailAddress(usr.email));
            mail.From = new MailAddress("fooglefoi@gmail.com");
            mail.Subject = "Foogle - potvrdjen racun";
            mail.Body = "Vas racun je potvrdjen. Mozete poceti koristiti aplikaciju.";

            try
            {
                //client.Send(mail);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
            //update listview
            populateCollection();
            
            MessageBox.Show("Uspjeh.");
        }


    }
}
