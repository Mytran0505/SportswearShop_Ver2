using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class devvn_tinhthanhpho
    {
        private string matp, name, type, slug;

        public devvn_tinhthanhpho()
        {
        }

        public devvn_tinhthanhpho(string matp, string name, string type, string slug)
        {
            this.matp = matp;
            this.name = name;
            this.type = type;
            this.slug = slug;
        }
        [Key]
        public string Matp { get => matp; set => matp = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Slug { get => slug; set => slug = value; }
    }
}
