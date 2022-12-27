using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    public class OrderTracking
    {
        private int orderId;
        private string orderStatus;
        DateTime createdAt;

        public OrderTracking()
        {
        }

        public OrderTracking(int orderId, string orderStatus, DateTime createdAt)
        {
            this.orderId = orderId;
            this.orderStatus = orderStatus;
            this.createdAt = createdAt;
        }

        public int OrderId { get => orderId; set => orderId = value; }
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
