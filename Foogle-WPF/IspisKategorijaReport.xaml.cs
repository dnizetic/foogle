using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for IspisKategorijaReport.xaml
    /// </summary>
    public partial class IspisKategorijaReport : Window
    {
        public IspisKategorijaReport()
        {
            InitializeComponent();
            ispisKategorija.Load += ispisKategorija_Load;
        }

        void ispisKategorija_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            FoogleDS dataset = new FoogleDS();

            dataset.BeginInit();

            reportDataSource1.Name = "ispis_kategorija_report"; //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataset.category;

            ObservableCollection<Category> _KategorijaCollection =
            new ObservableCollection<Category>();
            _KategorijaCollection.Clear();

            using (var context = new FoogleContext())
            {
                var cat = from a in context.Categories
                          select a;

                //var cat = context.Categories.SqlQuery("Select cat.id, cat.name, (select c.name from category c where c.id=cat.master_category) from category cat").ToList();

                foreach (var p in cat)
                {
                    _KategorijaCollection.Add(p);
                }

            }
            ReportDataSource reportDataSource = new ReportDataSource("ispis_kategorija_report", _KategorijaCollection);

            this.ispisKategorija.LocalReport.DataSources.Add(reportDataSource);


            //correct path
            this.ispisKategorija.LocalReport.ReportPath = "../../IspisKategorija_report.rdlc";

            using (StreamReader rdlcSR = new StreamReader("../../IspisKategorija_report.rdlc"))
            {
                ispisKategorija.LocalReport.LoadReportDefinition(rdlcSR);

            }

            dataset.EndInit();
        }

        private void viewerInstance_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ispisKategorija.RefreshReport(); 
        }  
    }
}
