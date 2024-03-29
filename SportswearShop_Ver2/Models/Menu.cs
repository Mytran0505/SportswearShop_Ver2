﻿namespace SportswearShop_Ver2.Models
{
	public class Menu
	{
		private int id;
		private string name;
		private int parent_id;
		private string description;
		private int active;
		private string image;

		public int Id { get => id; set => id = value; }
		public string Name { get => name; set => name = value; }
		public int Parent_id { get => parent_id; set => parent_id = value; }
		public string Description { get => description; set => description = value; }
		public int Active { get => active; set => active = value; }
		public string Image { get => image; set => image = value; }



		public Menu(int id, string name, int parent_id, string description, int active, string image)
		{
			this.id = id;
			this.name = name;
			this.parent_id = parent_id;
			this.description = description;
			this.active = active;
			this.image = image;
		}

		public Menu()
		{
		}

        internal static object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
