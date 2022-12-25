using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class devvn_quanhuyen
    {
        private string maqh;
        private string name, type, matp;
        private int extraShippingFee;

        public devvn_quanhuyen()
        {
        }

        public devvn_quanhuyen(string maqh, string name, string type, string matp, int extraShippingFee)
        {
            this.maqh = maqh;
            this.name = name;
            this.type = type;
            this.matp = matp;
            this.extraShippingFee = extraShippingFee;
        }

        public string Maqh { get => maqh; set => maqh = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Matp { get => matp; set => matp = value; }
        public int ExtraShippingFee { get => extraShippingFee; set => extraShippingFee = value; }
    }
}
