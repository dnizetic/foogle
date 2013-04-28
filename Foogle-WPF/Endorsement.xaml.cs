using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Endorsement.xaml
    /// </summary>
    public partial class Endorsement : Window
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
                                 where a.role.Equals("s")
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

        public Endorsement()
        {
            populateCollection();

            InitializeComponent();
        }



        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            //get user id
            var usr = ((Button)sender).DataContext as FoogleUser;

            if (usr == null)
                throw new InvalidOperationException("Invalid DataContext");

            SelectCategory sc = new SelectCategory(usr.id);

            sc.Show();


        }

    }
}
