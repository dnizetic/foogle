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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            /* TODO: 
             *      Korisnik nije prijavljen:
             *          1. otvori formu za prijavu i ponudi:
             *                                      + registraciju
             *                                      + povezivanje s LinkedIn računom
             *                                      + prijavu sa postoječim podacima
             *          2. poveži se s bazom
             *          3. promijeni naziv u "Odjava" i dodaj korisničko ime u UserLabel
             *          
             *      Korisnik je prijavljen:
             *          1. prekini vezu sa bazom
             *          2. Promjeni naziv u "Prijava" i makni korisničko ime iz UserLabel
             */
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
