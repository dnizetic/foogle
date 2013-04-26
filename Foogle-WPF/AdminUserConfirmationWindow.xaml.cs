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

        public ObservableCollection<FoogleUser> _KorisnikCollection =
            new ObservableCollection<FoogleUser>();


        public AdminUserConfirmationWindow()
        {
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

            InitializeComponent();

        }

        public ObservableCollection<FoogleUser> KorisnikCollection
        { get { 

            
            return _KorisnikCollection; 
        
        } }



    }
}
