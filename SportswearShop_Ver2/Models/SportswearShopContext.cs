using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using System.Xml.Linq;

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
		public List<Category> getBannerForHomePage()
		{
			List<Category> banners = new List<Category>();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				string str = "SELECT * FROM category where active = 1";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						//System.Diagnostics.Debug.WriteLine("1");
						banners.Add(new Category()
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
							Price_sale = Convert.ToInt32(reader["price_sale"]),
							Quantity = Convert.ToInt32(reader["Quantity"])

						});
					}
					reader.Close();
				}

				conn.Close();

			}
			return products;
		}

		public Product getProductInfo(int productId)
		{
			Product productInfo = new Product();
			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				var str = "SELECT * FROM PRODUCTs WHERE Id = @productId";
				//"SELECT * FROM (Product P JOIN category C ON P.CategoryId = C.CategoryId) " +
				//"JOIN brand B ON B.BrandId = P.BrandId " +
				//"JOIN subbrand S ON S.SubBrandId = P.SubBrandId " +
				//"where ProductId = @productId";
				MySqlCommand cmd = new MySqlCommand(str, conn);
				cmd.Parameters.AddWithValue("ProductId", productId);
				using (var reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						productInfo.Id = Convert.ToInt32(reader["id"]);
						productInfo.Name = reader["name"].ToString();
						productInfo.Image = reader["image"].ToString();
						productInfo.Original_price = Convert.ToInt32(reader["original_price"]);
						productInfo.Price_sale = Convert.ToInt32(reader["price_sale"]);
						productInfo.Quantity = Convert.ToInt32(reader["Quantity"]);
						
						if (reader["menu_id"] != DBNull.Value)
							productInfo.Menu_id = Convert.ToInt32(reader["menu_id"]);
						productInfo.Content = reader["Content"].ToString();
					}
					else
						return null;
				}
			}
			return productInfo;
		}

        public List<Category> getAllCategory()
        {
            List<Category> list = new List<Category>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from category where active = 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Category()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Active = Convert.ToInt32(reader["Active"]),
                            Image = reader["image"].ToString()

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<Menu> getAllMẹnu()
        {
            List<Menu> list = new List<Menu>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from menus where active = 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Menu()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Parent_id = Convert.ToInt32(reader["parent_id"]),
                            Active = Convert.ToInt32(reader["active"]),
                            Image = reader["image"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public object getProductDetail(int productId)
        {
            object productInfo = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT P.ID AS PID, Quantity, P.NAME AS PNAME, P.IMAGE AS PIMAGE, PRICE_SALE, C.ID AS CID, M.ID AS MID, content, C.NAME AS CNAME, M.NAME AS MNAME  " +
                    "FROM MENUS M, PRODUCTS P, CATEGORY C " +
                    "WHERE M.PARENT_ID = C.ID AND P.MENU_ID=M.ID AND P.ID = @ProductId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("ProductId", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        productInfo = new
                        {
                            ProductId = Convert.ToInt32(reader["PID"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductName = reader["PNAME"].ToString(),
                            ProductImage = reader["PIMAGE"].ToString(),
                            Price = Convert.ToInt32(reader["price_sale"]),
                            CategoryId = Convert.ToInt32(reader["CID"]),
                            MenuId = Convert.ToInt32(reader["MID"]),
                            Content = reader["content"].ToString(),
                            CategoryName = reader["CNAME"].ToString(),
                            MenuName = reader["MNAME"].ToString(),
                        };

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return productInfo;
        }
        public object getRelatedProduct(int productId)
        {
            object productInfo = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT P.ID AS PID, Quantity, P.NAME AS PNAME, P.IMAGE AS PIMAGE, PRICE_SALE, C.ID AS CID, M.ID AS MID, content, C.NAME AS CNAME, M.NAME AS MNAME  " +
                    "FROM MENUS M, PRODUCTS P, CATEGORY C " +
                    "WHERE M.PARENT_ID = C.ID AND P.MENU_ID=M.ID AND P.ID = @ProductId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("ProductId", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        productInfo = new
                        {
                            ProductId = Convert.ToInt32(reader["PID"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductName = reader["PNAME"].ToString(),
                            ProductImage = reader["PIMAGE"].ToString(),
                            Price = Convert.ToInt32(reader["price_sale"]),
                            CategoryId = Convert.ToInt32(reader["CID"]),
                            MenuId = Convert.ToInt32(reader["MID"]),
                            Content = reader["content"].ToString(),
                            CategoryName = reader["CNAME"].ToString(),
                            MenuName = reader["MNAME"].ToString(),
                        };

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return productInfo;
        }

        //public object getRelatedProduct(int productId, int categoryId, int menuId)
        //{
        //    object productInfo = new object();
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        var str = "SELECT P.ID AS PID, Quantity, P.NAME AS PNAME, P.IMAGE AS PIMAGE, PRICE_SALE, C.ID AS CID, M.ID AS MID, content, C.NAME AS CNAME, M.NAME AS MNAME  " +
        //            "FROM MENUS M, PRODUCTS P, CATEGORY C " +
        //            "WHERE M.PARENT_ID = C.ID AND P.MENU_ID=M.ID AND P.ID != @ProductId " +
        //            "and C.ID = @CategoryId AND M.ID = @MenuId";
        //        MySqlCommand cmd = new MySqlCommand(str, conn);
        //        cmd.Parameters.AddWithValue("ProductId", productId);
        //        cmd.Parameters.AddWithValue("CategoryId", categoryId);
        //        cmd.Parameters.AddWithValue("MenuId", menuId);

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
        //                productInfo = new
        //                {
        //                    ProductId = Convert.ToInt32(reader["PID"]),
        //                    Quantity = Convert.ToInt32(reader["Quantity"]),
        //                    ProductName = reader["PNAME"].ToString(),
        //                    ProductImage = reader["PIMAGE"].ToString(),
        //                    Price = Convert.ToInt32(reader["price_sale"]),
        //                    CategoryId = Convert.ToInt32(reader["CID"]),
        //                    MenuId = Convert.ToInt32(reader["MID"]),
        //                    Content = reader["content"].ToString(),
        //                    CategoryName = reader["CNAME"].ToString(),
        //                    MenuName = reader["MNAME"].ToString(),
        //                };

        //            }
        //            reader.Close();

        //        }
        //        conn.Close();

        //    }
        //    return productInfo;
        //}
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

        public long getTotalRevenue()
        {
            long revenue = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT SUM(DoanhThu) AS DoanhThu " +
                          "FROM( " +
                                "SELECT NgayBan, SUM(DoanhThu) AS DoanhThu " +
                                "FROM( " +
                                      "SELECT bkh.id, bkh.created_at as NgayBan, bkh.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                      "FROM c_t_h_d_s cthd, products pd, bill_khachhangs bkh " +
                                      "WHERE cthd.product_id = pd.id AND bkh.id = cthd.id " +
                                      "GROUP by bkh.id, bkh.created_at " +
                                      "UNION " +
                                      "SELECT bvl.id, bvl.created_at as NgayBan, bvl.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                      "FROM c_t_h_d_s cthd, products pd, bill_vanglais bvl " +
                                      "WHERE cthd.product_id = pd.id AND bvl.id = cthd.id " +
                                      "GROUP by bvl.id, bvl.created_at) x " +
                                      "GROUP BY NgayBan) x";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (DBNull.Value.Equals(reader["DoanhThu"])) revenue = 0;
                        else
                            revenue = Convert.ToInt32(reader["DoanhThu"]);

                    }
                }
            }
            return revenue;
        }

        public long getRevenueThisMonth(DateTime startDate, DateTime endDate)
        {
            long revenue = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
				var str = "SELECT SUM(DoanhThu) AS DoanhThu " +
                          "FROM( " +
                                "SELECT NgayBan, SUM(DoanhThu) AS DoanhThu " +
                                "FROM( " +
                                      "SELECT bkh.id, bkh.created_at as NgayBan, bkh.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                      "FROM c_t_h_d_s cthd, products pd, bill_khachhangs bkh " +
                                      "WHERE cthd.product_id = pd.id AND bkh.id = cthd.id " +
                                      "AND bkh.created_at BETWEEN @startdate AND @enddate " +
                                      "GROUP by bkh.id, bkh.created_at " +
                                      "UNION " +
                                      "SELECT bvl.id, bvl.created_at as NgayBan, bvl.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                      "FROM c_t_h_d_s cthd, products pd, bill_vanglais bvl " +
                                      "WHERE cthd.product_id = pd.id AND bvl.id = cthd.id " +
                                      "AND bvl.created_at BETWEEN @startdate AND @enddate " +
                                      "GROUP by bvl.id, bvl.created_at) x " +
                                      "GROUP BY NgayBan) x";

                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (DBNull.Value.Equals(reader["DoanhThu"])) revenue = 0;
                        else
                            revenue = Convert.ToInt32(reader["DoanhThu"]);

                    }
                }
            }
            return revenue;
        }

        public List<object> getRevenueByDate(DateTime startDate, DateTime endDate)
        {
            List<object> revenueInfo = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT NgayBan, SUM(DoanhThu) AS DoanhThu " +
                          "FROM( " +
                                "SELECT bkh.id, bkh.created_at as NgayBan, bkh.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                "FROM c_t_h_d_s cthd, products pd, bill_khachhangs bkh " +
                                "WHERE cthd.product_id = pd.id AND bkh.id = cthd.id " +
                                "AND bkh.created_at BETWEEN @startdate AND @enddate " +
                                "GROUP by bkh.id, bkh.created_at " +
                                "UNION " +
                                "SELECT bvl.id, bvl.created_at as NgayBan, bvl.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                "FROM c_t_h_d_s cthd, products pd, bill_vanglais bvl " +
                                "WHERE cthd.product_id = pd.id AND bvl.id = cthd.id " +
                                "AND bvl.created_at BETWEEN @startdate AND @enddate " +
                                "GROUP by bvl.id, bvl.created_at) x " +
                                "GROUP BY NgayBan";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            period = ((DateTime)reader["NgayBan"]).ToString("dd-MM-yyyy"),
                            profit = Convert.ToInt32(reader["DoanhThu"]),
                        };
                        revenueInfo.Add(obj);
                    }
                }
            }
            return revenueInfo;
        }

        public List<object> countOrderByDate(DateTime startDate, DateTime endDate)
        {
            List<object> ordersInfo = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
				var str = "SELECT COUNT(DISTINCT cthd.id) AS TongDonHang " +
						  "from bill_khachhangs bkh, bill_vanglais bvl, c_t_h_d_s cthd " +
                          "WHERE (bkh.id = cthd.id and bkh.created_at BETWEEN @startdate and @enddate) " +
                          "OR (bvl.id = cthd.id and bvl.created_at BETWEEN @startdate and @enddate)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("1");
                        var obj = new
                        {
                            label = "Đặt hàng thành công",
                            value = Convert.ToInt32(reader["TongDonHang"]),
                        };
                        ordersInfo.Add(obj);
                    }
                }
            }
            return ordersInfo;
        }

        public int countCustomer()
        {
            int numberCustomer = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(*) AS NUMBER_CUSTOMER FROM USER WHERE ADMIN = 0";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        numberCustomer = Convert.ToInt32(reader["NUMBER_CUSTOMER"]);
                    }
                }
            }
            return numberCustomer;
        }

        public int countProduct()
        {
            int numberProduct = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(*) AS NUMBER_PRODUCT FROM PRODUCTS";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        numberProduct = Convert.ToInt32(reader["NUMBER_PRODUCT"]);
                    }
                }
            }
            return numberProduct;
        }

        public int countOrder(DateTime startDate, DateTime endDate)
        {
            int numberOrder = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(DISTINCT cthd.id) AS TongDonHang " +
                          "from bill_khachhangs bkh, bill_vanglais bvl, c_t_h_d_s cthd " +
                          "WHERE (bkh.id = cthd.id and bkh.created_at BETWEEN @startdate and @enddate) " +
                          "OR (bvl.id = cthd.id and bvl.created_at BETWEEN @startdate and @enddate)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        numberOrder = Convert.ToInt32(reader["TongDonHang"]);
                    }
                }
            }
            return numberOrder;
        }

        public int countOrder()
        {
            int numberOrder = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(DISTINCT cthd.id) AS TongDonHang " +
                          "from bill_khachhangs bkh, bill_vanglais bvl, c_t_h_d_s cthd " +
                          "WHERE bkh.id = cthd.id " +
                          "OR bvl.id = cthd.id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        numberOrder = Convert.ToInt32(reader["TongDonHang"]);
                    }
                }
            }
            return numberOrder;
        }


        public List<Statistic> getStatistic(DateTime startDate, DateTime endDate)
        {
            List<Statistic> revenueInfo = new List<Statistic>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT NgayBan, SUM(DoanhThu) AS DoanhThu " +
                          "FROM( " +
                                "SELECT bkh.id, bkh.created_at as NgayBan, bkh.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                "FROM c_t_h_d_s cthd, products pd, bill_khachhangs bkh " +
                                "WHERE cthd.product_id = pd.id AND bkh.id = cthd.id " +
                                "AND bkh.created_at BETWEEN @startdate AND @enddate " +
                                "GROUP by bkh.id, bkh.created_at " +
                                "UNION " +
                                "SELECT bvl.id, bvl.created_at as NgayBan, bvl.total_money - SUM(pd.original_price * cthd.amount) AS DoanhThu " +
                                "FROM c_t_h_d_s cthd, products pd, bill_vanglais bvl " +
                                "WHERE cthd.product_id = pd.id AND bvl.id = cthd.id " +
                                "AND bvl.created_at BETWEEN @startdate AND @enddate " +
                                "GROUP by bvl.id, bvl.created_at) x " +
                                "GROUP BY NgayBan";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        revenueInfo.Add(new Statistic()
                        {
                            StatisticDate = ((DateTime)reader["NgayBan"]),
                            Profit = Convert.ToInt32(reader["DoanhThu"])
                        });
                       
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return revenueInfo;
        }


    }
}