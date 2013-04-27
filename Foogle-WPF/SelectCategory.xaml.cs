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

        public SelectCategory()
        {

            populateCollection();
            InitializeComponent();
        }

        public ObservableCollection<Category> CategoriesCollection
        {
            get
            {
                return _CategoriesCollection;

            }
        }
    }
}
