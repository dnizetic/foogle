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
    /// Interaction logic for SearchSelectWindow.xaml
    /// </summary>
    public partial class SearchSelectCategoryWindow : Window
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

        public ObservableCollection<Category> CategoriesCollection
        {
            get
            {
                return _CategoriesCollection;

            }
        }


        public SearchSelectCategoryWindow()
        {

            populateCollection();

            InitializeComponent();
            trazi.IsDefault = true;
            
        }


        private void SearchButton(object sender, RoutedEventArgs e)
        {


            //get all students, calculate # of matched skills
            var user_info = new List<UserMatch>();

            int cat_id = CategoryCombo.SelectedIndex + 1;

            try
            {
                using (var context = new FoogleContext())
                {
                    var students = from b in context.Users
                                   where b.role.Equals("s")
                                   select b;


                    foreach (FoogleUser f in students)
                    {

                        //foreach student, we have to calculate exp_amount
                        double exp_translated = f.exp;

                        using (var context2 = new FoogleContext())
                        {
                            var matched_categories = from d in context2.Recommendations
                                                     where d.student.id.Equals(f.id)
                                                     where d.category.id.Equals(cat_id)
                                                     select d;

                            var usr_skills = from d in context2.UserSkills
                                                     where d.user.id.Equals(f.id)
                                                     select d;

                            //MessageBox.Show("Matched cats: " + matched_categories.Count().ToString());

                            //recommendations adding
                            exp_translated += (matched_categories.Count() * 1.5);


                            //# of skills adding
                            exp_translated += (usr_skills.Count() / 10);


                        }


                        //store in an array
                        user_info.Add(new UserMatch
                        {
                            user = f,
                            email = f.email,
                            firstname = f.firstname,
                            lastname = f.lastname,
                            linkedin = f.linkedin,
                            exp = exp_translated,
                            id = f.id
                        });
                        

                    }

                    var sortedList = user_info.OrderByDescending(si => si.exp).ToList();

                    foreach (UserMatch u in sortedList)
                    {
                        //MessageBox.Show("User: " + u.user.firstname + ", exp: " + u.exp);
                    }

                    SearchBySkillResults ssr = new SearchBySkillResults(sortedList);

                    ssr.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }

            

        }
    }
}
