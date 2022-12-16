namespace SportswearShop_Ver2.Models
{
	public class Category
    {
		private int id;
		private string name;
		private string description;
		private int active;
		private string image;

		public int Id { get => id; set => id = value; }
		public string Name { get => name; set => name = value; }
		public string Description { get => description; set => description = value; }
		public int Active { get => active; set => active = value; }
		public string Image { get => image; set => image = value; }



		public Category(int id, string name, string description, int active, string image)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.active = active;
			this.image = image;
		}

		public Category()
		{
		}

        
    }
}
