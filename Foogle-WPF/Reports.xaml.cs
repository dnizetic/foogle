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

        private void usersReport_Click(object sender, RoutedEventArgs e)
        {
            IspisKorisnikaReport usersReportWindow = new IspisKorisnikaReport();
            usersReportWindow.Show();
        }

        private void categoryReport_Click(object sender, RoutedEventArgs e)
        {
            IspisKategorijaReport categoryReportWindow = new IspisKategorijaReport();
            categoryReportWindow.Show();
        }

        private void skillsReport_Click(object sender, RoutedEventArgs e)
        {
            IspisSkillovaReport skillsReportWindow = new IspisSkillovaReport();
            skillsReportWindow.Show();
        }

        private void userSkillsReport_Click(object sender, RoutedEventArgs e)
        {
            IspisSkillovaUseraReport userSkillsReportWindow = new IspisSkillovaUseraReport();
            userSkillsReportWindow.Show();
        }

        private void endorsementsReport_Click(object sender, RoutedEventArgs e)
        {
            IspisPreporukaReport endoresementsReportWindow = new IspisPreporukaReport();
            endoresementsReportWindow.Show();
        }
    }
}
