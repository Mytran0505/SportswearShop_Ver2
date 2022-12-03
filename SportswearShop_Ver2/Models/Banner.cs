using System;
namespace SportswearShop_Ver2.Models
{
	public class Banner
	{
		private int id;
		private string name;
		private string url;
		private string image;
		private int sort_by;
		private int active;

		public Banner()
		{
		}

		public Banner(int id, string name, string url, string image, int sort_by, int active)
		{
			this.id = id;
			this.name = name;
			this.url = url;
			this.image = image;
			this.sort_by = sort_by;
			this.active = active;
		}

		public int Id { get => id; set => id = value; }
		public string Name { get => name; set => name = value; }
		public string Url { get => url; set => url = value; }
		public string Image { get => image; set => image = value; }
		public int Sort_by { get => sort_by; set => sort_by = value; }
		public int Active { get => active; set => active = value; }
	}
}
