namespace SportswearShop_Ver2.Models
{
    public class BillVangLai
    {
        private int id;
        private int total_money;
        private string customer_phone_number;
        private DateTime created_at;

        public BillVangLai()
        {
        }

        public BillVangLai(int id, int total_money, string customer_phone_number, DateTime created_at)
        {
            this.id = id;
            this.total_money = total_money;
            this.customer_phone_number = customer_phone_number;
            this.created_at = created_at;
        }

        public int Id { get => id; set => id = value; }
        public int TotalMoney { get => total_money; set => total_money = value; }
        public string CustomerPhoneNumber { get => customer_phone_number; set => customer_phone_number = value; }
        public DateTime CreatedAt { get => created_at; set => created_at = value; }
    }
}
