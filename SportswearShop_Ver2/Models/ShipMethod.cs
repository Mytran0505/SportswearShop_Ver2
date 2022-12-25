using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class ShipMethod
    {
        private int shipMethodId;
        private string shipMethodName;
        private int shipFee;
        private int estimatedDeliveryTime;
        private int status;
        //private DateTime createdAt;

        public ShipMethod()
        {
        }

        public ShipMethod(int shipMethodId, string shipMethodName, int shipFee, int estimatedDeliveryTime, int status, DateTime createdAt)
        {
            this.shipMethodId = shipMethodId;
            this.shipMethodName = shipMethodName;
            this.shipFee = shipFee;
            this.estimatedDeliveryTime = estimatedDeliveryTime;
            this.status = status;
            //this.createdAt = createdAt;
        }

        public int ShipMethodId { get => shipMethodId; set => shipMethodId = value; }
        public string ShipMethodName { get => shipMethodName; set => shipMethodName = value; }
        public int ShipFee { get => shipFee; set => shipFee = value; }
        public int EstimatedDeliveryTime { get => estimatedDeliveryTime; set => estimatedDeliveryTime = value; }
        public int Status { get => status; set => status = value; }
       // public DateTime CreatedAt { get => CreatedAt; set => CreatedAt = value; }
    }
}
