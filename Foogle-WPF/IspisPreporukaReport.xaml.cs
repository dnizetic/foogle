using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for IspisPreporukaReport.xaml
    /// </summary>
    public partial class IspisPreporukaReport : Window
    {
        public IspisPreporukaReport()
        {
            InitializeComponent();
            ispisPreporuka.Load += ispisPreporuka_Load;
        }

        void ispisPreporuka_Load(object sender, EventArgs e)
        {

            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            FoogleDS dataset = new FoogleDS();

            dataset.BeginInit();

            reportDataSource1.Name = "ispis_preporuka_report"; //Name of the report dataset in our .RDLC file
            reportDataSource1.Value = dataset.recommendation_view;

            this.ispisPreporuka.LocalReport.DataSources.Add(reportDataSource1);
            this.ispisPreporuka.LocalReport.ReportPath = "../../IspisPreporuka_report.rdlc";
            dataset.EndInit();

            FoogleDSTableAdapters.recommendation_viewTableAdapter
            recommendation_view_table_adapter = new
            FoogleDSTableAdapters.recommendation_viewTableAdapter();

            recommendation_view_table_adapter.ClearBeforeFill = true;
            recommendation_view_table_adapter.Fill(dataset.recommendation_view);  
        }

        private void viewerInstance_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           this.ispisPreporuka.RefreshReport();
        }

    }
}
