using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    public class Promotion
    {
        private int promotionId;
        private DateTime startDate, endDate;
        private string title;
        private int status;

        public Promotion()
        {
        }

        public Promotion(int promotionId, DateTime startDate, DateTime endDate, string title, int status)
        {
            this.PromotionId = promotionId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Title = title;
            this.Status = status;
        }

        public int PromotionId { get => promotionId; set => promotionId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Title { get => title; set => title = value; }
        public int Status { get => status; set => status = value; }
    }
}
