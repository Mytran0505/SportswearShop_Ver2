using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    public class WishList
    {
        private int userId, productId;
        private DateTime createdAt;

        public WishList()
        {
        }

        public WishList(int userId, int productId, DateTime createdAt)
        {
            this.userId = userId;
            this.productId = productId;
            this.createdAt = createdAt;
        }

        public int UserId { get => userId; set => userId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
