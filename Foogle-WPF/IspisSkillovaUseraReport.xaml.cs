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

            reportDataSource1.Name = "ispis_skillova__usera_report"; //Name of the report dataset in our .RDLC file

            reportDataSource1.Value = dataset.category;

            

            ObservableCollection<Skill> _SkillCollection =
          new ObservableCollection<Skill>();
            _SkillCollection.Clear();


            using (var context = new FoogleContext())
            {

                var skill_name = from a in context.Skills
                            select a;

                foreach (var s in skill_name)
                {
                    _SkillCollection.Add(s);
                }

            }

            ObservableCollection<FoogleUser> _UserCollection =
          new ObservableCollection<FoogleUser>();
            _UserCollection.Clear();


            using (var context = new FoogleContext())
            {

                var usrs = from u in context.Users
                                 select u;

                foreach (var u in usrs)
                {
                    _UserCollection.Add(u);
                }

            }

            ObservableCollection<UserSkills> _UserSkillCollection =
            new ObservableCollection<UserSkills>();
            _UserSkillCollection.Clear();


            using (var context = new FoogleContext())
            {

                var skill = from c in context.UserSkills
                            select c;

                foreach (var p in skill)
                {
                    _UserSkillCollection.Add(p);
                }

            }

            ObservableCollection<UserSkillView> _UsersSkillsViewCollection = new ObservableCollection<UserSkillView>();
            _UsersSkillsViewCollection.Clear();

            UserSkillView usk_view = new UserSkillView();

            //for (int i = 0; i < _UserSkillCollection.Count; i++) {               
            //    for (int j = 0; j < _UserCollection.Count; j++) {
            //        if (_UserSkillCollection[i].user.id == _UserCollection[j].id) { usk_view.ime = _UserCollection[j].firstname; usk_view.prezime = _UserCollection[j].lastname; }
            //    }
            //    for (int z = 0; z < _SkillCollection.Count; z++)
            //    {
            //        if (_UserSkillCollection[i].skill.id == _SkillCollection[z].id) { usk_view.naziv = _SkillCollection[z].name; }
            //    }
            //    _UsersSkillsViewCollection.Add(usk_view);
            //}

            usk_view.ime = "Ljiljana";
            usk_view.prezime = "Pintaric";
            usk_view.naziv = "jhgflghj";

            _UsersSkillsViewCollection.Add(usk_view);


            ReportDataSource reportDataSource = new ReportDataSource("ispis_skillova_usera_report", _UsersSkillsViewCollection);

            this.ispisSkillovaUsera.LocalReport.DataSources.Add(reportDataSource);


            //correct path
            this.ispisSkillovaUsera.LocalReport.ReportPath = "../../IspisSkillovaUsera_report.rdlc";

            using (StreamReader rdlcSR = new StreamReader("../../IspisSkillovaUsera_report.rdlc"))
            {
                ispisSkillovaUsera.LocalReport.LoadReportDefinition(rdlcSR);

            }

            dataset.EndInit();
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
