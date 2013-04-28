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

        
        public ObservableCollection<UserMatch> _KorisnikCollection =
            new ObservableCollection<UserMatch>();


        public ObservableCollection<UserMatch> KorisnikCollection
        {
            get
            {


                return _KorisnikCollection;

            }
        }



        public SearchBySkillResults(List<UserMatch> l)
        {

            foreach (UserMatch u in l)
            {
                _KorisnikCollection.Add(u);
            }


            InitializeComponent();
        }
    }
}
