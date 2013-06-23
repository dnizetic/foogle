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
    /// Interaction logic for IspisSkillovaReport.xaml
    /// </summary>
    public partial class IspisSkillovaReport : Window
    {
        public IspisSkillovaReport()
        {
            InitializeComponent();
            ispisSkillova.Load += ispisSkillova_Load;
        }

        void ispisSkillova_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            FoogleDS dataset = new FoogleDS();

            dataset.BeginInit();

            reportDataSource1.Name = "ispis_skillova_report"; //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataset.category;

            ObservableCollection<Skill> _SkillCollection =
            new ObservableCollection<Skill>();
            _SkillCollection.Clear();

            using (var context = new FoogleContext())
            {
                var skill = from a in context.Skills
                          select a;


                foreach (var p in skill)
                {
                    _SkillCollection.Add(p);
                }

            }
            ReportDataSource reportDataSource = new ReportDataSource("ispis_skillova_report", _SkillCollection);

            this.ispisSkillova.LocalReport.DataSources.Add(reportDataSource);


            //correct path
            this.ispisSkillova.LocalReport.ReportPath = "../../IspisSkillova_report.rdlc";

            using (StreamReader rdlcSR = new StreamReader("../../IspisSkillova_report.rdlc"))
            {
                ispisSkillova.LocalReport.LoadReportDefinition(rdlcSR);

            }

            dataset.EndInit();
        }

        private void viewerInstance_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ispisSkillova.RefreshReport(); 
        }
    }
}
