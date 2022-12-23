namespace SportswearShop_Ver2.Models
{
    public class BillKhachHang
    {
        private int id;
        private int total_money;
        private int customer_id;
        private DateTime created_at;

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

        public int Id { get => id; set => id = value; }
        public int TotalMoney { get => total_money; set => total_money = value; }
        public int CustomerId { get => customer_id; set => customer_id = value; }
        public DateTime CreatedAt { get => created_at; set => created_at = value; }
    }
}
