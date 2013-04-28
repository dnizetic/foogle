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

        public void populateCollection()
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

            populateEndorsedCollection();

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


        //endorsed collection
        public static ObservableCollection<Recommendation> _EndorsedCollection =
            new ObservableCollection<Recommendation>();

        public static void populateEndorsedCollection()
        {
            _EndorsedCollection.Clear();

            int prof_id = MainWindow.professor_id;

            //get all professors 'p' that are unactivated
            using (var context = new FoogleContext())
            {
                //neaktivirani profesori
                var recomms = from a in context.Recommendations
                                 where a.teacher.id.Equals(prof_id)
                                 select a;


                foreach (var p in recomms)
                {
                    _EndorsedCollection.Add(p);
                }

            }
        }




        public ObservableCollection<Recommendation> EndorsedCollection
        {
            get
            {

                return _EndorsedCollection;

            }
        }

        private void DeleteEndorse(object sender, RoutedEventArgs e)
        {
            var r = ((Button)sender).DataContext as Recommendation;

            if (r == null)
                throw new InvalidOperationException("Invalid DataContext");

            try
            {

                using (var context = new FoogleContext())
                {

                    //MessageBox.Show(r.id.ToString());

                    Recommendation re = context.Recommendations.SingleOrDefault(c => c.id == r.id);

                    context.Recommendations.Remove(re);
                    context.SaveChanges();
                }

                populateEndorsedCollection();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + err.InnerException);
            }


        }
    }
}
