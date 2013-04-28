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
    /// Interaction logic for SearchBySkillResults.xaml
    /// </summary>
    public partial class SearchBySkillResults : Window
    {

        /*
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
        } */



        public SearchBySkillResults()
        {
            InitializeComponent();
        }
    }
}
