using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class devvn_xaphuongthitran
    {
        private string xaid, name, type, maqh;

        public devvn_xaphuongthitran()
        {
        }

        public devvn_xaphuongthitran(string xaid, string name, string type, string maqh)
        {
            this.xaid = xaid;
            this.name = name;
            this.type = type;
            this.maqh = maqh;
        }

        [Key]
        public string Xaid { get => xaid; set => xaid = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Maqh { get => maqh; set => maqh = value; }
    }
}
