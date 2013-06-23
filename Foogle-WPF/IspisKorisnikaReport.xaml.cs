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
    /// Interaction logic for IspisKorisnikaReport.xaml
    /// </summary>
    public partial class IspisKorisnikaReport : Window
    {
        public IspisKorisnikaReport()
        {
            InitializeComponent();
            ispisKorisnika.Load += ispisKorisnika_Load;
        }

        void ispisKorisnika_Load(object sender, EventArgs e)
        {
            
            //ispisKorisnika.LocalReport.ReportEmbeddedResource = "TheApp.Reports.MyReport.rdlc";
            ispisKorisnika.LocalReport.ReportEmbeddedResource = "foogle.Foogle-WPF.IspisKorisnika_report.rdlc";
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
