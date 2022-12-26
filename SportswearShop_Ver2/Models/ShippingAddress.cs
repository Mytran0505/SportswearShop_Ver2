using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class ShippingAddress
    {
        private int shippingAddressId;
        private string receiverName;
        private string phone, address;
        private string matp, maqh, xaid;
        private string shippingAddressType;
        private int userId, isDefault;
        private DateTime updatedAt;
        private DateTime createdAt;

        public ShippingAddress()
        {
        }

        public ShippingAddress(int shippingAddressId, string receiverName, string phone, string address, string matp, string maqh, string xaid, string shippingAddressType, int userId, int isDefault, DateTime updatedAt)
        {
            this.shippingAddressId = shippingAddressId;
            this.receiverName = receiverName;
            this.phone = phone;
            this.address = address;
            this.matp = matp;
            this.maqh = maqh;
            this.xaid = xaid;
            this.shippingAddressType = shippingAddressType;
            this.userId = userId;
            this.isDefault = isDefault;
            this.updatedAt = updatedAt;
        }

        public int ShippingAddressId { get => shippingAddressId; set => shippingAddressId = value; }
        public string ReceiverName { get => receiverName; set => receiverName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Matp { get => matp; set => matp = value; }
        public string Maqh { get => maqh; set => maqh = value; }
        public string Xaid { get => xaid; set => xaid = value; }
        public string ShippingAddressType { get => shippingAddressType; set => shippingAddressType = value; }
        public int UserId { get => userId; set => userId = value; }
        public int IsDefault { get => isDefault; set => isDefault = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
