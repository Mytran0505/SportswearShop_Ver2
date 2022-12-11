using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    // Chú ý: class này không có trong database
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public CartItem()
        {
        }
    }
}
