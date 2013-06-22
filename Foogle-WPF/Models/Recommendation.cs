using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class Recommendation
    {
        public Int64 id { get; set; }


        public virtual Category category { get; set; }
        public virtual FoogleUser teacher { get; set; }
        public virtual FoogleUser student { get; set; }

        public string student_email { get { return student.email; } }
        public string student_fname { get { return student.firstname; } }
        public string student_lname { get { return student.lastname; } }
    }
}
