using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Xml.Linq;
using System.Collections.Generic;

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
        public Category getNameOfCategory(int CategoryId)
        {
            Category category = new Category();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT ID, NAME FROM CATEGORY WHERE ID = @categoryId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("categoryId", CategoryId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        category.Id = Convert.ToInt32(reader["ID"]);
                        category.Name = reader["NAME"].ToString();  

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return category;
        }

        public Menu getNameOfMenu(int MenuId)
        {
            Menu menu = new Menu();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT ID, NAME FROM MENUS WHERE ID = @menuId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuId", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        menu.Id = Convert.ToInt32(reader["ID"]);
                        menu.Name = reader["NAME"].ToString();

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return menu;
        }
        public object getMenuOfCategory(int CategoryId)
        {
            object MenuInfo = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT C.ID AS CID, M.ID AS MID, C.NAME AS CNAME, M.NAME AS MNAME  " +
                    "FROM MENUS M, CATEGORY C " +
                    "WHERE M.PARENT_ID = C.ID AND C.ID = @categoryId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("categoryId", CategoryId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        MenuInfo = new
                        {
           
                            CategoryId = Convert.ToInt32(reader["CID"]),
                            MenuId = Convert.ToInt32(reader["MID"]),
                            CategoryName = reader["CNAME"].ToString(),
                            MenuName = reader["MNAME"].ToString(),
                        };

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return MenuInfo;
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

        public List<object> getTopProduct(DateTime startDate, DateTime endDate)
        {
            List<object> products = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT id, image, name, price_sale, original_price, DoanhThuDemLai, SUM(SoLuongBanRa) AS SoLuongBanRa, SoLuongConTon " +
                          "FROM " +
                          "((SELECT pd.id, pd.image, pd.name, pd.price_sale, pd.original_price, (pd.price_sale - pd.original_price) * SUM(cthd.amount) AS DoanhThuDemLai, SUM(cthd.amount) as SoLuongBanRa, pd.Quantity as SoLuongConTon " +
                          "FROM products pd, c_t_h_d_s cthd, bill_khachhangs bkh " +
                          "WHERE pd.id = cthd.product_id AND cthd.id = bkh.id " +
                          "AND bkh.created_at BETWEEN @startdate and @enddate " +
                          "GROUP BY pd.id) " +
                          "UNION " +
                          "(SELECT pd.id, pd.image, pd.name, pd.price_sale, pd.original_price, (pd.price_sale - pd.original_price) * SUM(cthd.amount) AS DoanhThuDemLai, SUM(cthd.amount) as SoLuongBanRa, pd.Quantity as SoLuongConTon " +
                          "FROM products pd, c_t_h_d_s cthd, bill_vanglais bvl " +
                          "WHERE pd.id = cthd.product_id AND cthd.id = bvl.id " +
                          "AND bvl.created_at BETWEEN @startdate and @enddate " +
                          "GROUP BY pd.id)) x " +
                          "GROUP BY id " +
                          "ORDER BY SUM(SoLuongBanRa) DESC " +
                          "LIMIT 5";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            ProductName = reader["name"].ToString(),
                            ProductImage = reader["image"].ToString(),
                            SoLuongConTon = Convert.ToInt32(reader["SoLuongConTon"]),
                            OriginalPrice = Convert.ToInt32(reader["original_price"]),
                            SalePrice = Convert.ToInt32(reader["price_sale"]),
                            SoLuongBanRa = Convert.ToInt32(reader["SoLuongBanRa"]),
                            DoanhThuDemLai = Convert.ToInt64(reader["DoanhThuDemLai"])
                        };
                        products.Add(obj);
                    }
                }
            }
            return products;
        }

        public List<object> getTopProductNoLimit(DateTime startDate, DateTime endDate)
        {
            List<object> products = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT id, image, name, price_sale, original_price, DoanhThuDemLai, SUM(SoLuongBanRa) AS SoLuongBanRa, SoLuongConTon " +
                          "FROM " +
                          "((SELECT pd.id, pd.image, pd.name, pd.price_sale, pd.original_price, (pd.price_sale - pd.original_price) * SUM(cthd.amount) AS DoanhThuDemLai, SUM(cthd.amount) as SoLuongBanRa, pd.Quantity as SoLuongConTon " +
                          "FROM products pd, c_t_h_d_s cthd, bill_khachhangs bkh " +
                          "WHERE pd.id = cthd.product_id AND cthd.id = bkh.id " +
                          "AND bkh.created_at BETWEEN @startdate and @enddate " +
                          "GROUP BY pd.id) " +
                          "UNION " +
                          "(SELECT pd.id, pd.image, pd.name, pd.price_sale, pd.original_price, (pd.price_sale - pd.original_price) * SUM(cthd.amount) AS DoanhThuDemLai, SUM(cthd.amount) as SoLuongBanRa, pd.Quantity as SoLuongConTon " +
                          "FROM products pd, c_t_h_d_s cthd, bill_vanglais bvl " +
                          "WHERE pd.id = cthd.product_id AND cthd.id = bvl.id " +
                          "AND bvl.created_at BETWEEN @startdate and @enddate " +
                          "GROUP BY pd.id)) x " +
                          "GROUP BY id " +
                          "ORDER BY SUM(SoLuongBanRa) DESC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("startdate", startDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("enddate", endDate.ToString("yyyy-MM-dd"));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            ProductName = reader["name"].ToString(),
                            ProductImage = reader["image"].ToString(),
                            SoLuongConTon = Convert.ToInt32(reader["SoLuongConTon"]),
                            OriginalPrice = Convert.ToInt32(reader["original_price"]),
                            SalePrice = Convert.ToInt32(reader["price_sale"]),
                            SoLuongBanRa = Convert.ToInt32(reader["SoLuongBanRa"]),
                            DoanhThuDemLai = Convert.ToInt64(reader["DoanhThuDemLai"])
                        };
                        products.Add(obj);
                    }
                }
            }
            return products;
        }

        public int getMenuCurrentMaxId()
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT id FROM menus ORDER BY id DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
            }
            return id;
        }

        public void saveMenu(Menu newMenu)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO menus(id, name, parent_id, description, active, created_at, updated_at, image) VALUES (@Id,@Name,@Parent_id,@Description,@Active,@NgayTao,@NgayUpdate,@Image)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", newMenu.Id);
                cmd.Parameters.AddWithValue("Name", newMenu.Name);
                cmd.Parameters.AddWithValue("Parent_id", newMenu.Parent_id);
                cmd.Parameters.AddWithValue("Description", newMenu.Description);
                cmd.Parameters.AddWithValue("Active", newMenu.Active);
                cmd.Parameters.AddWithValue("Image", newMenu.Image);
                cmd.Parameters.AddWithValue("NgayTao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<Menu> getAllMenuForMenuManagement()
        {
            List<Menu> list = new List<Menu>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from menus";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Menu()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
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

        public void updateMenuStatus(int id, int status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE menus SET active=@status WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteMenu(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM menus WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Menu getMenuById(int id)
        {
            Menu menu = new Menu();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from menus where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    menu.Id = Convert.ToInt32(reader["id"]);
                    menu.Name = reader["name"].ToString();
                    menu.Description = reader["description"].ToString();
                    menu.Parent_id = Convert.ToInt32(reader["parent_id"]);
                    menu.Active = Convert.ToInt32(reader["active"]);
                    menu.Image = reader["image"].ToString();
                    reader.Close();
                }
                conn.Close();
            }
            return menu;
        }

        public void saveUpdateMenu(Menu menu)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE menus SET name=@Name, parent_id=@Parent_id, description=@Description, active=@Active, updated_at=@NgayUpdate, image=@Image WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", menu.Id);
                cmd.Parameters.AddWithValue("Name", menu.Name);
                cmd.Parameters.AddWithValue("Parent_id", menu.Parent_id);
                cmd.Parameters.AddWithValue("Description", menu.Description);
                cmd.Parameters.AddWithValue("Active", menu.Active);
                cmd.Parameters.AddWithValue("Image", menu.Image);
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public int getUserCurrentMaxId()
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT UserId FROM user ORDER BY UserId DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["UserId"]);
                    }
                }
            }
            return id;
        }

        public void saveNewAdminUser(User newUser)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO user(UserId, FirstName, LastName, Mobile, Email, Password, Admin, UserImage, RegisteredAt, LastLogin) VALUES (@UserId, @FirstName, @LastName, @Mobile, @Email, @Password, @Admin, @UserImage, @RegisteredAt, @LastLogin)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", newUser.UserId);
                cmd.Parameters.AddWithValue("FirstName", newUser.FirstName);
                cmd.Parameters.AddWithValue("LastName", newUser.LastName);
                cmd.Parameters.AddWithValue("Mobile", newUser.Mobile);
                cmd.Parameters.AddWithValue("Email", newUser.Email);
                cmd.Parameters.AddWithValue("Password", newUser.Password);
                cmd.Parameters.AddWithValue("Admin", 1);
                cmd.Parameters.AddWithValue("UserImage", newUser.UserImage);
                cmd.Parameters.AddWithValue("RegisteredAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> getAllAdminUser()
        {
            List<User> list = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user where Admin=1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<User> getAllClientUser()
        {
            List<User> list = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user where Admin=0";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public void deleteClientUser(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM user WHERE UserId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public User getClientUserById(int id)
        {
            User clientuser = new User();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user where UserId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    clientuser.UserId = Convert.ToInt32(reader["UserId"]);
                    clientuser.FirstName = reader["FirstName"].ToString();
                    clientuser.LastName = reader["LastName"].ToString();
                    clientuser.Mobile = reader["Mobile"].ToString();
                    clientuser.Email = reader["Email"].ToString();
                    clientuser.Password = reader["Password"].ToString();
                    reader.Close();
                }
                conn.Close();
            }
            return clientuser;
        }

        public void saveUpdateClientUser(User clientuser)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE user SET FirstName=@FirstName, LastName=@LastName, Mobile=@Mobile, Email=@Email, Password=@Password WHERE UserId=@UserId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", clientuser.UserId);
                cmd.Parameters.AddWithValue("FirstName", clientuser.FirstName);
                cmd.Parameters.AddWithValue("LastName", clientuser.LastName);
                cmd.Parameters.AddWithValue("Mobile", clientuser.Mobile);
                cmd.Parameters.AddWithValue("Email", clientuser.Email);
                cmd.Parameters.AddWithValue("Password", clientuser.Password);
                cmd.ExecuteNonQuery();
            }
        }

    }
}