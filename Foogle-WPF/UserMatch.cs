﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class UserMatch
    {
        public FoogleUser user;
        public int id { get; set; }

        public int num_matches { get; set; }

        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string linkedin { get; set; }

        public double exp { get; set; }

    }
}