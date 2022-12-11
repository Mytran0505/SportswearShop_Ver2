namespace SportswearShop_Ver2.Models
{
	public class Product
	{
		private int id;
		private string name;
		private string description;
		private string content;
		private int menu_id;
		private int original_price;
		private int price_sale;
		private int active;
		private string image;
		private int quantity;

		public Product()
		{
		}
		public Product(int id, string name, string description, string content, int menu_id, int original_price, int price_sale, int active, string image, int quantity)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.content = content;
			this.menu_id = menu_id;
			this.original_price = original_price;
			this.price_sale = price_sale;
			this.active = active;
			this.image = image;
			this.Quantity = quantity;
		}

		public int Id { get => id; set => id = value; }
		public string Name { get => name; set => name = value; }
		public string Description { get => description; set => description = value; }
		public string Content { get => content; set => content = value; }
		public int Menu_id { get => menu_id; set => menu_id = value; }
		public int Original_price { get => original_price; set => original_price = value; }
		public int Price_sale { get => price_sale; set => price_sale = value; }
		public int Active { get => active; set => active = value; }
		public string Image { get => image; set => image = value; }
		public int Quantity { get => quantity; set => quantity = value; }
	}
}
