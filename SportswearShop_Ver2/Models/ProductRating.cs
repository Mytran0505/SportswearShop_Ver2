using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITGoShop_F_Ver2.Models
{
    public class ProductRating
    {
        private int userId, productId, rating;
        private string title, content;
        private int  productRatingStatus;
        private DateTime createdAt;

        public ProductRating()
        {
        }

        public ProductRating(int userId, int productId, int rating, string title, string content, int productRatingStatus, DateTime createdAt)
        {
            this.userId = userId;
            this.productId = productId;
            this.rating = rating;
            this.title = title;
            this.content = content;
            this.productRatingStatus = productRatingStatus;
            this.createdAt = createdAt;
        }

        public int UserId { get => userId; set => userId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public int ProductRatingStatus { get => productRatingStatus; set => productRatingStatus = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
