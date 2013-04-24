using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    class PortfolioProjects
    {
        public int id { get; set; }

        public string project_name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string project_description { get; set; }

        public int student_id { get; set; }

        public virtual Student Student { get; set; }
    }
}
