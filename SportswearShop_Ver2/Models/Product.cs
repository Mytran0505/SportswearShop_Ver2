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

		public Product()
		{
		}

		public Product(int id, string name, string description, string content, int menu_id, int original_price, int price_sale, int active, string image)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.Content = content;
			this.Menu_id = menu_id;
			this.Original_price = original_price;
			this.Price_sale = price_sale;
			this.Active = active;
			this.Image = image;
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
	}
}
