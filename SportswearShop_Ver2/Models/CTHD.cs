namespace SportswearShop_Ver2.Models
{
    public class CTHD
    {
        private int orderId, productId, orderQuantity;
        private long unitPrice;

        public CTHD()
        {
        }

        public CTHD(int orderId, int productId, int orderQuantity, long unitPrice)
        {
            this.orderId = orderId;
            this.productId = productId;
            this.orderQuantity = orderQuantity;
            this.unitPrice = unitPrice;
        }
        public int OrderId { get => orderId; set => orderId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int OrderQuantity { get => orderQuantity; set => orderQuantity = value; }
        public long UnitPrice { get => unitPrice; set => unitPrice = value; }
    }
}
