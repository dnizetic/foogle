using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class UserSkills
    {
        public int id { get; set; }

        public virtual Skill skill { get; set; }
        public virtual FoogleUser user { get; set; }

    }
}
