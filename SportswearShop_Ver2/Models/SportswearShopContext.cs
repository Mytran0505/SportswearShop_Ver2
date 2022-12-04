using MySql.Data.MySqlClient;

namespace SportswearShop_Ver2.Models
{
    public class SportswearShopContext
    {
		public string ConnectionString { get; set; }

		public SportswearShopContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public SportswearShopContext()
		{
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}

		public List<Banner> getSliderForHomePage()
		{
			List<Banner> sliders = new List<Banner>();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				string str = "SELECT * FROM banners where active = 1 order by sort_by desc";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
                    {
						//System.Diagnostics.Debug.WriteLine("1");
						sliders.Add(new Banner()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Url = reader["url"].ToString(),
                            Image = reader["image"].ToString(),
                            Sort_by = Convert.ToInt32(reader["sort_by"]),
							Active = Convert.ToInt32(reader["active"])
                        });
                    }
					reader.Close();
				}

				conn.Close();

			}
			return sliders;
		}
		public List<Menu> getBannerForHomePage()
		{
			List<Menu> banners = new List<Menu>();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				string str = "SELECT * FROM menus where parent_id = 0";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						//System.Diagnostics.Debug.WriteLine("1");
						banners.Add(new Menu()
						{
							Id = Convert.ToInt32(reader["id"]),
							Name = reader["name"].ToString(),
							Image = reader["image"].ToString(),
						});
					}
					reader.Close();
				}

				conn.Close();

			}
			return banners;
		}

		public List<Product> getProductForHomePage()
		{
			List<Product> products = new List<Product>();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				string str = "SELECT * FROM products";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						//System.Diagnostics.Debug.WriteLine("1");
						products.Add(new Product()
						{
							Id = Convert.ToInt32(reader["id"]),
							Name = reader["name"].ToString(),
							Image = reader["image"].ToString(),
							Original_price = Convert.ToInt32(reader["original_price"]),
							Price_sale = Convert.ToInt32(reader["price_sale"])

						});
					}
					reader.Close();
				}

				conn.Close();

			}
			return products;
		}

		public User getUserInfo(string email, string password, int isAdmin)
		{
			User userInfo = new User();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				var str = "select * from User where Admin = @admin AND Email = @email AND Password = @password";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				cmd.Parameters.AddWithValue("email", email);
				cmd.Parameters.AddWithValue("password", password);
				cmd.Parameters.AddWithValue("admin", isAdmin);
				using (var reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						userInfo.UserId = Convert.ToInt32(reader["UserId"]);
						userInfo.FirstName = reader["FirstName"].ToString();
						userInfo.LastName = reader["LastName"].ToString();
						userInfo.Mobile = reader["Mobile"].ToString();
						userInfo.UserImage = reader["UserImage"].ToString();
						userInfo.Email = reader["Email"].ToString();
						userInfo.Admin = Convert.ToInt32(reader["Admin"]);
					}
					else
						return null;
				}
			}
			return userInfo;
		}
		public void updateLastLogin(int userId)
		{
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				var str = "UPDATE USER SET LastLogin = SYSDATE() where UserId = @userId";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				cmd.Parameters.AddWithValue("userId", userId);
				cmd.ExecuteNonQuery();
			}
		}
	}
}
