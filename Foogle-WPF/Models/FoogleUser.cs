using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class FoogleUser
    {

        public int id { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string role { get; set; }
        public string activity { get; set; }
        public bool confirmed { get; set; }

        public string confirmed_textual
        {
            get { return this.confirmed == true ? "Da" : "Ne"; }
        }

        public string password { get; set; }

        public string linkedin { get; set; }

        public string linkedin_id { get; set; }

        public double exp { get; set; }

    }
}
