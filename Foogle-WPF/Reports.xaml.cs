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

namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void korisnici_report_Click(object sender, RoutedEventArgs e)
        {
            IspisKorisnikaReport ik = new IspisKorisnikaReport();
            ik.Show();
        }

        private void kategorije_Click(object sender, RoutedEventArgs e)
        {
            IspisKategorijaReport ikat = new IspisKategorijaReport();
            ikat.Show();
        }

        private void skillovi_Click(object sender, RoutedEventArgs e)
        {
            IspisSkillovaReport iskill = new IspisSkillovaReport();
            iskill.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IspisSkillovaUseraReport isu = new IspisSkillovaUseraReport();
            isu.Show();
        }
    }
}
