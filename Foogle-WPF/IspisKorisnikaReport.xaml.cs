using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;


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
            ispisKorisnika.LocalReport.EnableHyperlinks = true;
        }

        private bool _isReportViewerLoaded;
        void ispisKorisnika_Load(object sender, EventArgs e)// ispisKorisnika = ReportViewer
        {


            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            FoogleDS dataset = new FoogleDS();
            
            dataset.BeginInit();

            reportDataSource1.Name = "ispis_korisnika_report"; //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataset.foogle_user;
                
            ObservableCollection<FoogleUser> _KorisnikCollection =
            new ObservableCollection<FoogleUser>();
            _KorisnikCollection.Clear();

            using (var context = new FoogleContext())
            {
                var usrs = from a in context.Users
                                select a;

                foreach (var p in usrs)
                {
                    _KorisnikCollection.Add(p);
                }

            }
            ReportDataSource reportDataSource = new ReportDataSource("ispis_korisnika_report", _KorisnikCollection);

            this.ispisKorisnika.LocalReport.DataSources.Add(reportDataSource);

        
            //correct path
            this.ispisKorisnika.LocalReport.ReportPath = "../../IspisKorisnika_report.rdlc";

            using (StreamReader rdlcSR = new StreamReader("../../IspisKorisnika_report.rdlc"))
            {
                ispisKorisnika.LocalReport.LoadReportDefinition(rdlcSR);

            }
            
            dataset.EndInit();

            _isReportViewerLoaded = true;
            
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
