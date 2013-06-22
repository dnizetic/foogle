using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{

    //used for display query results
    public class UserMatch
    {
        public FoogleUser user;
        public Int64 id { get; set; }

        public int num_matches { get; set; }

        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string linkedin { get; set; }

        public double exp { get; set; }

    }
}
