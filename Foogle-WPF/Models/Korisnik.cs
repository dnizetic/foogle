using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foogle_WPF
{
    public class Korisnik
    {

        public int id { get; set; }
        public string email { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string tip_korisnika { get; set; }
        public bool aktiviran { get; set; }

    }
}
