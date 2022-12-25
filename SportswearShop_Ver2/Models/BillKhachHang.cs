namespace SportswearShop_Ver2.Models
{
    public class BillKhachHang
    {
        private int id;
        private int total_money;
        private int customer_id;
        private DateTime created_at;
        private string status;
        private string payment_status;
        private int shippingAddressId, shipFee;
        private DateTime estimatedDeliveryTime;
        private string shipMethod;
        private string orderStatus;
        private string paymentMethod;
        private string description;
        private DateTime? dateUpdate;

        public BillKhachHang()
        {
        }

        public BillKhachHang(int id, int total_money, int customer_id, DateTime created_at)
        {
            this.id = id;
            this.total_money = total_money;
            this.customer_id = customer_id;
            this.created_at = created_at;
        }

        public BillKhachHang(int id, int total_money, int customer_id, DateTime created_at, string status)
        {
            this.id = id;
            this.total_money = total_money;
            this.customer_id = customer_id;
            this.created_at = created_at;
            this.status = status;
        }

        public BillKhachHang(int id, int total_money, int customer_id, DateTime created_at, string status, string payment_status)
        {
            this.id = id;
            this.total_money = total_money;
            this.customer_id = customer_id;
            this.created_at = created_at;
            this.status = status;
            this.payment_status = payment_status;
        }

        public int Id { get => id; set => id = value; }
        public int TotalMoney { get => total_money; set => total_money = value; }
        public int CustomerId { get => customer_id; set => customer_id = value; }
        public DateTime CreatedAt { get => created_at; set => created_at = value; }
        public string Status { get => status; set => status = value; }
        public string Payment_status { get => payment_status; set => payment_status = value; }
        public int ShippingAddressId { get => shippingAddressId; set => shippingAddressId = value; }
        public int ShipFee { get => shipFee; set => shipFee = value; }
        public DateTime EstimatedDeliveryTime { get => estimatedDeliveryTime; set => estimatedDeliveryTime = value; }
        public string ShipMethod { get => shipMethod; set => shipMethod = value; }
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }
        public string PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
        public string Description { get => description; set => description = value; }
    }
}
