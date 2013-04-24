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

        public ObservableCollection<Korisnik> _KorisnikCollection =
            new ObservableCollection<Korisnik>();


        public AdminUserConfirmationWindow()
        {
            //get all professors 'p' that are unactivated
            using (var context = new FoogleContext())
            {
                //neaktivirani profesori
                var professors = from a in context.Korisnici
                                 where a.aktiviran.Equals(false)
                                 where a.tip_korisnika.Equals("p")
                                 select a;


                foreach (var p in professors)
                {
                    _KorisnikCollection.Add(p);

                }

            }

            InitializeComponent();

        }

        public ObservableCollection<Korisnik> KorisnikCollection
        { get { return _KorisnikCollection; } }



    }
}
