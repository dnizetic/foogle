using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SelectCategory.xaml
    /// </summary>
    public partial class SelectCategory : Window
    {

        public ObservableCollection<Category> _CategoriesCollection =
            new ObservableCollection<Category>();

        private void populateCollection()
        {
            _CategoriesCollection.Clear();

            //get all professors 'p' that are unactivated
            using (var context = new FoogleContext())
            {
                //neaktivirani profesori
                var cats = from a in context.Categories
                                 select a;


                foreach (var p in cats)
                {
                    _CategoriesCollection.Add(p);

                }

            }
        }

        private int user_id = 0;


        public SelectCategory(int u_id)
        {

            populateCollection();
            user_id = u_id;

            InitializeComponent();
        }

        public ObservableCollection<Category> CategoriesCollection
        {
            get
            {
                return _CategoriesCollection;

            }
        }

        private void EndorseButton(object sender, RoutedEventArgs e)
        {
            //current professor_id (logged in)
            int prof_id = MainWindow.professor_id;

            if (prof_id == 0)
            {
                throw new NotImplementedException();
            }


            //student id
            int student_id = user_id;

            //category_id
            int cat_id = EndorseCombo.SelectedIndex + 1;

            //MessageBox.Show("prof_id: " + prof_id + ", student_id" + student_id + "cat_id " + cat_id);

            try
            {
                using (var context = new FoogleContext())
                {
                    Category cat = context.Categories.SingleOrDefault(c => c.id == cat_id);
                    FoogleUser student = context.Users.SingleOrDefault(c => c.id == student_id);
                    FoogleUser professor = context.Users.SingleOrDefault(c => c.id == prof_id);

                    context.Recommendations.Add(
                        new Recommendation
                        {
                            category = cat,
                            student = student,
                            teacher = professor
                        });

                    context.SaveChanges();
                }

                MessageBox.Show("Endorse uspjesan.");
                Endorsement.PopulateEndorsedCollection();
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + "\n" + err.InnerException);
            }


        }
    }
}
