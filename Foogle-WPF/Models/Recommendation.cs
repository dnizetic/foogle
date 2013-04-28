using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class Recommendation
    {
        public int id { get; set; }


        public virtual Category category { get; set; }
        public virtual FoogleUser teacher { get; set; }
        public virtual FoogleUser student { get; set; }
    }
}
