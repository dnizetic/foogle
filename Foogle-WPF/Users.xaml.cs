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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid() 
        {
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
            try
            {
                DataRowView row = (DataRowView)foogle_userDataGrid.SelectedItems[0];
                string id = row["id"].ToString();

                using (var context = new FoogleContext())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM foogle_user WHERE id =" + id + "");
                    context.SaveChanges();
                }

                RefreshDataGrid();
            }
            catch
            {
                RefreshDataGrid();
            }
        }

        private void unesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)foogle_userDataGrid.SelectedItems[0];
                string id = row["id"].ToString();
                string email = row["email"].ToString();
                string firstname = row["firstname"].ToString();
                string lastname = row["lastname"].ToString();
                string password = row["password"].ToString();
                string role = row["role"].ToString();
                if (row["confirmed"].ToString() == "") { row["confirmed"] = "False"; }
                bool confirmed = bool.Parse(row["confirmed"].ToString());               
                string linkedin = row["linkedin"].ToString();
                float exp = float.Parse(row["exp"].ToString());
                


                using (var context = new FoogleContext())
                {
                    context.Users.Add(
                        new FoogleUser
                        {
                            email = email,
                            confirmed = confirmed,
                            firstname = firstname,
                            lastname = lastname,
                            role = role,
                            activity = null,
                            password = password,
                            linkedin = linkedin,
                            exp = exp
                        });

                    context.SaveChanges();
                }

                RefreshDataGrid();
            }
            catch {
                MessageBox.Show("Neispravni podaci!");
                RefreshDataGrid();
            }

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)foogle_userDataGrid.SelectedItems[0];
                string id = row["id"].ToString();
                string email = row["email"].ToString();
                string firstname = row["firstname"].ToString();
                string lastname = row["lastname"].ToString();
                string password = row["password"].ToString();
                string role = row["role"].ToString();

                int confirmed = 0;
                if (row["confirmed"].ToString() == "" || row["confirmed"].ToString() == "False") { confirmed = 0; }
                else confirmed = 1;
                string linkedin = row["linkedin"].ToString();
                float exp = float.Parse(row["exp"].ToString());

                MessageBox.Show(confirmed.ToString());


                using (var context = new FoogleContext())
                {
                    context.Database.ExecuteSqlCommand("Update foogle_user set email='" + email + "', firstname='" + firstname + "', lastname='" + lastname + "', password='" + password + "', role='" + role + "', confirmed=" + confirmed.ToString() + ", linkedin='" + linkedin + "', exp=" + exp + " WHERE id =" + id + "");
                    context.SaveChanges();
                }

                RefreshDataGrid();
            }
            catch
            {
                MessageBox.Show("Neispravni podaci!");
                RefreshDataGrid();
            }

        }

        private void osvjezi_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
