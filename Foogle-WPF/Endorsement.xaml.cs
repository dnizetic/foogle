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

        public void PopulateCollection()
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
            PopulateCollection();

            PopulateEndorsedCollection();

            InitializeComponent();
        }



        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            //get user id
            var usr = ((Button)sender).DataContext as FoogleUser;

            if (usr == null)
                throw new InvalidOperationException("Invalid DataContext");

            SelectCategory sc = new SelectCategory(Convert.ToInt32( usr.id ));

            sc.Show();


        }


        public class EndorsedView
        {
            public int id { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }

            public string catname { get; set; }

            public int rid { get; set; }
        }

        //endorsed collection
        public static ObservableCollection<EndorsedView> _EndorsedCollection =
            new ObservableCollection<EndorsedView>();

        public static void PopulateEndorsedCollection()
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

                List<Recommendation> list = recomms.ToList();
                
                foreach (var p in list)
                {
                    _EndorsedCollection.Add(new EndorsedView {
                        id = Convert.ToInt32(p.student.id),
                        email = p.student.email,
                        firstname = p.student.firstname,
                        lastname = p.student.lastname,
                        catname = p.category.name,
                        rid = Convert.ToInt32(p.id)
                    });
                }

            }
        }




        public ObservableCollection<EndorsedView> EndorsedCollection
        {
            get
            {

                return _EndorsedCollection;

            }
        }

        private void DeleteEndorse(object sender, RoutedEventArgs e)
        {
            var r = ((Button)sender).DataContext as EndorsedView;

            if (r == null)
                throw new InvalidOperationException("Invalid DataContext");

            try
            {

                using (var context = new FoogleContext())
                {

                    //MessageBox.Show(r.id.ToString());

                    Recommendation re = context.Recommendations.SingleOrDefault(c => c.id == r.rid);

                    context.Recommendations.Remove(re);
                    context.SaveChanges();
                }

                PopulateEndorsedCollection();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + err.InnerException);
            }


        }

        private void ShowProfile(object sender, RoutedEventArgs e)
        {
            var usr = ((Button)sender).DataContext as FoogleUser;

            if (usr == null)
                throw new InvalidOperationException("Invalid DataContext");

            System.Diagnostics.Process.Start(usr.linkedin);

        }
    }
}
