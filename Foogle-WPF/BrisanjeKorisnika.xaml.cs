using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    /// Interaction logic for BrisanjeKorisnika.xaml
    /// </summary>
    public partial class BrisanjeKorisnika : Window
    {
        public BrisanjeKorisnika()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid() {
            Foogle_WPF.FoogleDS foogleDS = ((Foogle_WPF.FoogleDS)(this.FindResource("foogleDS")));
            // Load data into the table foogle_user. You can modify this code as needed.
            Foogle_WPF.FoogleDSTableAdapters.foogle_userTableAdapter foogleDSfoogle_userTableAdapter = new Foogle_WPF.FoogleDSTableAdapters.foogle_userTableAdapter();
            foogleDSfoogle_userTableAdapter.Fill(foogleDS.foogle_user);
            System.Windows.Data.CollectionViewSource foogle_userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("foogle_userViewSource")));
            foogle_userViewSource.View.MoveCurrentToFirst();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
           
        }

        private void obrisi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)foogle_userDataGrid.SelectedItems[0];
            string id = row["id"].ToString(); 
            
            using (var context = new FoogleContext())
                        {
                            context.Database.ExecuteSqlCommand("DELETE FROM foogle_user WHERE id ="+id+"");
                            context.SaveChanges();
                        }

            RefreshDataGrid();
        }
    }
}
