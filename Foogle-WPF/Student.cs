using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    class Student
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        //public int index_num { get; set; }

        public virtual ICollection<PortfolioProjects> PortfolioProjects { get; set; }

    }
}
