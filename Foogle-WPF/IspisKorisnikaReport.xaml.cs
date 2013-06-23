using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;


namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for IspisKorisnikaReport.xaml
    /// </summary>
    public partial class IspisKorisnikaReport : Window
    {
        public IspisKorisnikaReport()
        {
            InitializeComponent();
            ispisKorisnika.Load += ispisKorisnika_Load;
        }


        private bool _isReportViewerLoaded;
        void ispisKorisnika_Load(object sender, EventArgs e)
        {

            //MessageBox.Show("HI");
            
            //ispisKorisnika.LocalReport.ReportEmbeddedResource = "TheApp.Reports.MyReport.rdlc";
            //ispisKorisnika.LocalReport.ReportEmbeddedResource = "foogle.Foogle-WPF.IspisKorisnika_report.rdlc";

            try
            {


                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                FoogleDS dataset = new FoogleDS();
            
                //AdventureWorks2008R2DataSet dataset = new AdventureWorks2008R2DataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "ispis_korisnika_report"; //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.foogle_user;
                //reportDataSource1.Value = dataset.SalesOrderDetail;
                this.ispisKorisnika.LocalReport.DataSources.Add(reportDataSource1);

                //this.ispisKorisnika.LocalReport.ReportEmbeddedResource = "Foogle-WPF.IspisKorisnika_report.rdlc";

                //tvoja putanja (mozemo kasnije slozit ovo nije bitno)
                this.ispisKorisnika.LocalReport.ReportPath = "C:/Users/denis/foogle/Foogle-WPF/IspisKorisnika_report.rdlc";
                using (StreamReader rdlcSR = new StreamReader(@"C:/Users/denis/foogle/Foogle-WPF/IspisKorisnika_report.rdlc"))
                {
                    ispisKorisnika.LocalReport.LoadReportDefinition(rdlcSR);

                    //ispisKorisnika.LocalReport.Refresh();
                }
            

                //ispisKorisnika.LocalReport.ReportEmbeddedResource = "Foogle-WPF.IspisKorisnika_report.rdlc";
            
                dataset.EndInit();

                //fill data into adventureWorksDataSet

                FoogleDSTableAdapters.foogle_userTableAdapter adapter = new FoogleDSTableAdapters.foogle_userTableAdapter();
   
                adapter.ClearBeforeFill = true;
                adapter.Fill(dataset.foogle_user);
                ispisKorisnika.RefreshReport();

                _isReportViewerLoaded = true;
            }
            catch (Exception ee)
            {
            }
        }

     

        private void _reportViewer_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }


        private void PrepareReport()
        {
            
        }  


        private void viewerInstance_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

        }  

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                       
            

            this.ispisKorisnika.RefreshReport(); 
        }
    }
}
