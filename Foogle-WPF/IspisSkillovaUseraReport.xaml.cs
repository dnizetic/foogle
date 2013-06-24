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
    /// Interaction logic for IspisSkillovaUseraReport.xaml
    /// </summary>
    public partial class IspisSkillovaUseraReport : Window
    {
        public IspisSkillovaUseraReport()
        {
            InitializeComponent();
            ispisSkillovaUsera.Load += ispisSkillovaUsera_Load;
        }

        void ispisSkillovaUsera_Load(object sender, EventArgs e)
        {
            
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            FoogleDS dataset = new FoogleDS();

            dataset.BeginInit();

            reportDataSource1.Name = "ispis_skillova_usera_report"; //Name of the report dataset in our .RDLC file
            reportDataSource1.Value = dataset.user_skills_view;

            this.ispisSkillovaUsera.LocalReport.DataSources.Add(reportDataSource1);
            this.ispisSkillovaUsera.LocalReport.ReportPath = "../../IspisSkillovaUsera_report.rdlc";
            dataset.EndInit();

            FoogleDSTableAdapters.user_skills_viewTableAdapter            
            user_skills_view_ta = new
            FoogleDSTableAdapters.user_skills_viewTableAdapter();

            user_skills_view_ta.ClearBeforeFill = true;
            user_skills_view_ta.Fill(dataset.user_skills_view);  


        }

        private void viewerInstance_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.ispisSkillovaUsera.RefreshReport();
            
        }

       
    }
}
