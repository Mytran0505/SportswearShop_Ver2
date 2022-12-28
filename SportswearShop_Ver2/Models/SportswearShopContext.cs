using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Xml.Linq;
using System.Collections.Generic;
using SportswearShop_Ver2.Controllers;
using System;
using ITGoShop_F_Ver2.Controllers;
using System.ComponentModel.Design;

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
				string str = "SELECT * FROM products where active = 1";
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
							Quantity = Convert.ToInt32(reader["Quantity"]),
                            Menu_id = Convert.ToInt32(reader["menu_id"])

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
        public List<object> getMenuOfCategory(int CategoryId)
        {
            List<object> MenuInfo = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT C.ID AS CID, M.ID AS MID, C.NAME AS CNAME, M.NAME AS MNAME  " +
                    "FROM MENUS M, CATEGORY C " +
                    "WHERE M.active =1 AND M.PARENT_ID = C.ID AND C.ID = @categoryId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("categoryId", CategoryId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        var menuinfo = new
                        {
 
                            CategoryId = Convert.ToInt32(reader["CID"]),
                            MenuId = Convert.ToInt32(reader["MID"]),
                            CategoryName = reader["CNAME"].ToString(),
                            MenuName = reader["MNAME"].ToString(),
                        };
                        MenuInfo.Add(menuinfo);
                    }
                    reader.Close();

                }
                conn.Close();

            }
            return MenuInfo;
        }

        public List<Product> getProductOfMenu1(int categoryId)
        {
            List<Product> Products = new List<Product>();
			int MenuId = 0;
			if (categoryId == 1)
				MenuId = 1;
			else
				MenuId = 4;
			using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM products where active =1 and menu_id = @menuId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuId", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        Products.Add(new Product()
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
            return Products;
        }
        public List<Product> getProductOfMenu2(int categoryId)
        {
            List<Product> Products = new List<Product>();
			int MenuId = 0;
			if (categoryId == 1)
				MenuId = 2;
			else
				MenuId = 5;
			using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM products where active =1 and menu_id = @menuId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuId", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        Products.Add(new Product()
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
            return Products;
        }
        public List<Product> getProductOfMenu3(int categoryId)
        {
           
			List<Product> Products = new List<Product>();
			int MenuId = 0;
			if (categoryId == 1)
                MenuId = 3;
            else
                MenuId = 6;

			using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM products where active =1 and menu_id = @menuId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuId", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        Products.Add(new Product()
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
            return Products;
        }
        public List<Product> getProductOfMenu(int MenuId)
        {
            List<Product> Products = new List<Product>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM products where active =1 and menu_id = @menuid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuid", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        Products.Add(new Product()
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
            return Products;
        }

        public List<Product> getProductByMenu(int MenuId)
        {
            List<Product> Products = new List<Product>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM products where active =1 and menu_id = @menuid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("menuid", MenuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        Products.Add(new Product()
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
            return Products;
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

        public List<Menu> getAllMenu()
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

        public List<object> getAllComments()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM (COMMENT C " +
                    "JOIN USER U ON C.UserId = U.UserId) " +
                    "JOIN PRODUCTs P ON P.Id = C.ProductId " +
                    "WHERE ParentComment IS NULL " +
                    "ORDER BY Reply ASC, CommentId DESC;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            CommentId = Convert.ToInt32(reader["CommentId"]),
                            CommentContent = reader["CommentContent"].ToString(),
                            UserName = reader["LastName"].ToString() + " " + reader["FirstName"].ToString(),
                            ProductName = reader["Name"].ToString(),
                            CreatedAt = ((DateTime)reader["CreatedAt"]).ToString("dd-MM-yyyy"),
                            Reply = Convert.ToInt32(reader["Reply"]),
                            CommentStatus = Convert.ToInt32(reader["CommentStatus"]),
                            ProductId = Convert.ToInt32(reader["Id"]),
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void updateCommentStatus(int CommentId, int Status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE comment SET CommentStatus = @status WHERE CommentId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", Status);
                cmd.Parameters.AddWithValue("id", CommentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateCommentStatus(int ParentComment)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE comment set Reply =1  WHERE CommentId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", ParentComment);
                cmd.ExecuteNonQuery();
            }
        }
        public void addComment(Comment comment)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO comment(CommentId, CommentContent, UserId, ProductId, CommentStatus, Reply, ParentComment, CreatedAt, UpdatedAt) " +
                    "VALUES (@comId, @comcont,@userId, @ProID, @comsta, @rep, @parentcom, @CreateAt, @UpdateAt)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("comId", comment.CommentId);
                cmd.Parameters.AddWithValue("comcont", comment.CommentContent);
                cmd.Parameters.AddWithValue("userId", comment.UserId);
                cmd.Parameters.AddWithValue("ProID", comment.ProductId);
                cmd.Parameters.AddWithValue("comsta", comment.CommentStatus);
                cmd.Parameters.AddWithValue("rep", comment.Reply);
                cmd.Parameters.AddWithValue("parentcom", comment.ParentComment);
                cmd.Parameters.AddWithValue("CreateAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("UpdateAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();
            }
        }
        public void updateCommentReply(int CommentId, int Reply)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE comment SET Reply = @rep WHERE CommentId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("rep", Reply);
                cmd.Parameters.AddWithValue("id", CommentId);
                cmd.ExecuteNonQuery();
            }
        }
        public void deleteComment(int commentId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from comment where ParentComment = @Parcomt";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Parcomt", commentId);
                cmd.ExecuteNonQuery();

                var str1 = "Delete from comment where CommentId = @comt";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("comt", commentId);
                cmd1.ExecuteNonQuery();
            }
        }

        public List<object> getCommentParentCommentForProductDetail(int productId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT CommentId, ProductId, C.UserId, UserImage, LastName, FirstName, CommentContent, Admin, C.CreatedAt
                            FROM COMMENT C JOIN user P ON C.UserId = P.UserId 
                            WHERE ProductId = @productid
                            AND ParentComment IS NULL 
                            AND CommentStatus = 1 
                            ORDER BY CommentId DESC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("productid", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            CommentId = Convert.ToInt32(reader[0]),
                            ProductId = Convert.ToInt32(reader[1]),
                            UserId = Convert.ToInt32(reader[2]),
                            UserImage = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            FirstName = reader[5].ToString(),
                            CommentContent = reader[6].ToString(),
                            Admin = Convert.ToInt32(reader[7]),
                            CreatedAt = (DateTime)reader[8],
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public List<object> getSubCommentForProductDetail(int commendId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT CommentId, ProductId, C.UserId, UserImage, LastName, FirstName, CommentContent, Admin, C.CreatedAt
                            FROM COMMENT C JOIN user P ON C.UserId = P.UserId 
                            AND ParentComment = @parentcomment
                            ORDER BY CommentId DESC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("parentcomment", commendId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            CommentId = Convert.ToInt32(reader[0]),
                            ProductId = Convert.ToInt32(reader[1]),
                            UserId = Convert.ToInt32(reader[2]),
                            UserImage = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            FirstName = reader[5].ToString(),
                            CommentContent = reader[6].ToString(),
                            Admin = Convert.ToInt32(reader[7]),
                            CreatedAt = (DateTime)reader[8],
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public object getProductDetail(int productId)
        {
            object productInfo = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT P.ID AS PID, Quantity, P.NAME AS PNAME, P.IMAGE AS PIMAGE, SOLD, PRICE_SALE, C.ID AS CID, M.ID AS MID, content, C.NAME AS CNAME, M.NAME AS MNAME  " +
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
                            Sold = Convert.ToInt32(reader["SOLD"]),
                        };

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return productInfo;
        }
        public List<object> getMenuOfCatgory(int CategoryId)
        {
            List<object> MenuInfo = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT C.ID AS CID, M.ID AS MID, C.NAME AS CNAME, M.NAME AS MNAME  " +
                    "FROM MENUS M, CATEGORY C " +
                    "WHERE M.active =1 and M.PARENT_ID = C.ID AND C.ID = @categoryId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("categoryId", CategoryId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        var menuinfo = new
                        {

                            CategoryId = Convert.ToInt32(reader["CID"]),
                            MenuId = Convert.ToInt32(reader["MID"]),
                            CategoryName = reader["CNAME"].ToString(),
                            MenuName = reader["MNAME"].ToString(),
                        };
                        MenuInfo.Add(menuinfo);
                    }
                    reader.Close();

                }
                conn.Close();

            }
            return MenuInfo;
        }
        public List<object> getRelatedProduct(int productId, int menuId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT P.ID AS PID, Quantity, discount, P.NAME AS PNAME, P.IMAGE AS PIMAGE, PRICE_SALE, C.ID AS CID, M.ID AS MID, content, C.NAME AS CNAME, M.NAME AS MNAME " +
                    "FROM MENUS M, PRODUCTS P, CATEGORY C" +
                    " WHERE P.active =1 and M.PARENT_ID = C.ID AND P.MENU_ID = M.ID AND P.id != @ProId AND M.ID = @MenuId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("ProId", productId);
                cmd.Parameters.AddWithValue("MenuId", menuId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        var productInfo = new
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
                            Discount = Convert.ToInt32(reader["discount"]),
                        };
                        list.Add(productInfo);

                    }
                    reader.Close();

                }
                conn.Close();

            }
            return list;
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
        public List<object> getAllRating()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT U.UserId as UserId, FirstName, LastName, Title, PR.Content, Rating, PR.CreatedAt, UserImage, Name, PR.ProductId as ProductId, ProductRatingStatus
                            FROM (`productrating` PR JOIN `user` U ON PR.UserId = U.UserId )
                                JOIN `products` P ON P.Id = PR.ProductId
                            ORDER BY DATE(PR.CreatedAt) DESC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            UserImage = reader["UserImage"].ToString(),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["Name"].ToString(),
                            ProductRatingStatus = Convert.ToInt32(reader["ProductRatingStatus"]),
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void updateRatingStatus(int userId, int productId, int status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE productrating SET ProductRatingStatus = @status WHERE UserId = @userId and ProductId = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("id", productId);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteRating(int userId, int productId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from productrating WHERE UserId = @userId and ProductId = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("id", productId);
                cmd.ExecuteNonQuery();
            }
        }

        public void addRating(int productId, string title, string content, int rating, int userId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO productrating(UserId, ProductId, Rating, Title, Content, ProductRatingStatus, CreatedAt) VALUES " +
                    "(@userId,@ProId,@Rating, @Title, @Content, 1 ,@Create)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("ProId", productId);
                cmd.Parameters.AddWithValue("Rating", rating);
                cmd.Parameters.AddWithValue("Title", title);
                cmd.Parameters.AddWithValue("Content", content);
                cmd.Parameters.AddWithValue("Create", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public int isRatingeExit(int productId, int userId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                int count = 0;
                conn.Open();
                var str = "SELECT count(*) as SL from productrating where UserId = @userId and ProductId = @ProId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("ProId", productId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = Convert.ToInt32(reader["SL"]);
                    }
                }
                if (count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Productrating " + userId + " " + productId);
                    return 0; //ranting chưa tồn tại trong productrating
                }
            }
            return 1;
        }

        public List<object> getRating(int ProductId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT U.UserId as UserId, FirstName, LastName, Title, Content, Rating, CreatedAt, UserImage, ProductRatingStatus
                            FROM `productrating` PR JOIN `user` U ON PR.UserId = U.UserId 
                            WHERE PR.ProductId = @productid
                            AND ProductRatingStatus = 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("productid", ProductId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            UserImage = reader["UserImage"].ToString(),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ProductRatingStatus = reader["ProductRatingStatus"].ToString(),
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public object getDefaultShippingAddress(int userId)
        {
            object item = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT ShippingAddressId, Address, Phone, TT.name, QH.name, XP.name, ReceiverName, ShippingAddressType, ExtraShippingFee
                        FROM shippingaddress SA, devvn_quanhuyen QH, devvn_tinhthanhpho TT, devvn_xaphuongthitran XP
                        WHERE SA.matp = TT.matp
                        AND SA.maqh = QH.maqh
                        AND SA.xaid = XP.xaid
                        AND UserId = @UserId
                        AND IsDefault = 1;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new
                        {
                            ShippingAddressId = Convert.ToInt32(reader[0].ToString()),
                            Address = reader[1].ToString(),
                            Phone = reader[2].ToString(),
                            ThanhPho = reader[3].ToString(),
                            QuanHuyen = reader[4].ToString(),
                            XaPhuong = reader[5].ToString(),
                            ReceiverName = reader[6].ToString(),
                            ShippingAddressType = reader[7].ToString(),
                            ExtraShippingFee = Convert.ToInt32(reader[8].ToString()),
                        };
                        return item;
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return null;
        }

        public List<object> getWishList(int customerId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM WISHLIST W JOIN PRODUCTs P ON P.Id = W.ProductId WHERE UserId = @userId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", customerId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            ProductImage = reader["image"].ToString(),
                            ProductName = reader["name"].ToString(),
                            Price = Convert.ToInt32(reader["price_sale"]),
                        };
                        list.Add(obj);
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public User getProfile(int customerId)
        {
            User Info = new User();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM user " +
                    "where UserId = @UserId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", customerId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Info.UserId = Convert.ToInt32(reader["UserId"]);
                        Info.Email = reader["Email"].ToString();
                        Info.FirstName = reader["FirstName"].ToString();
                        Info.LastName = reader["LastName"].ToString();
                        Info.Mobile = reader["Mobile"].ToString();
                        Info.UserImage = reader["UserImage"].ToString();
                    }
                    else
                        return null;
                }
            }
            return Info;
        }

        public List<BillKhachHang> getOrderListOfCustomer(int CusId)
        {
            List<BillKhachHang> list = new List<BillKhachHang>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from bill_khachhangs where customer_id = @cusId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("cusId", CusId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BillKhachHang()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            TotalMoney = Convert.ToInt32(reader["total_money"]),
                            Status = reader["status"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            Payment_status = reader["payment_status"].ToString(),
                            ShipMethod = reader["ShipMethod"].ToString(),
                            ShipFee = Convert.ToInt32(reader["ShipFee"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<ShipMethod> getShipMethodToCheckout()
        {
            List<ShipMethod> list = new List<ShipMethod>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from ShipMethod where status = 1";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ShipMethod()
                        {
                            ShipMethodId = Convert.ToInt32(reader["ShipMethodId"]),
                            Status = Convert.ToInt32(reader["Status"]),
                            ShipFee = Convert.ToInt32(reader["ShipFee"]),
                            ShipMethodName = reader["ShipMethodName"].ToString(),
                            
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<devvn_tinhthanhpho> getAllThanhPho()
        {
            List<devvn_tinhthanhpho> list = new List<devvn_tinhthanhpho>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from devvn_tinhthanhpho";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new devvn_tinhthanhpho()
                        {
                            Matp = reader["matp"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            Slug = reader["slug"].ToString(),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<devvn_quanhuyen> getAllQuanHuyen()
        {
            List<devvn_quanhuyen> list = new List<devvn_quanhuyen>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from devvn_quanhuyen";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new devvn_quanhuyen()
                        {
                            Matp = reader["matp"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            Maqh = reader["maqh"].ToString(),
                            ExtraShippingFee = Convert.ToInt32(reader["ExtraShippingFee"]),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<devvn_xaphuongthitran> getAllXaPhuong()
        {
            List<devvn_xaphuongthitran> list = new List<devvn_xaphuongthitran>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from devvn_xaphuongthitran";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new devvn_xaphuongthitran()
                        {
                            Xaid = reader["xaid"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            Maqh = reader["maqh"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public object getOrderInfo(int oderId)
        {
            object billtInfo = new object();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM bill_khachhangs B join user U on B.customer_id = U.UserId WHERE Id = @billId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("billId", oderId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        billtInfo = new
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            TotalMoney = Convert.ToInt32(reader["total_money"]),
                            Status = reader["status"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            Payment_status = reader["payment_status"].ToString(),
                            ShipFee = Convert.ToInt32(reader["ShipFee"]),
                            ShipMethod = reader["ShipMethod"].ToString(),
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),

                        };
                    }
                }
            }
            return billtInfo;
        }

        public int getCustomerIdFromOrderInfo(int oderId)
        {
            int cusID = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT customer_id FROM bill_khachhangs WHERE Id = @billId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("billId", oderId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cusID = Convert.ToInt32(reader["customer_id"]);
                    }
                }
            }
            return cusID;
        }

        public List<object> getOrderDetail(int orderId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT P.id as ProductId, P.image as ProductImage, OD.UnitPrice as UnitPrice, OD.amount as OrderQuantity, P.name as ProductName
                            FROM c_t_h_d_s OD join Products P ON P.id = OD.product_id 
                            WHERE OD.id = @orderid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("orderid", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductImage = reader["ProductImage"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            UnitPrice = Convert.ToInt32(reader["UnitPrice"]),
                            OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public void updateOrderStatus(int OrderId, string OrderStatus, string PaymentStatus)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE bill_khachhangs SET status = @status, payment_status = @paytus WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", OrderStatus);
                cmd.Parameters.AddWithValue("paytus", PaymentStatus);
                cmd.Parameters.AddWithValue("id", OrderId);

                cmd.ExecuteNonQuery();
            }
        }
        public void updateOrderStatus(int OrderId, string OrderStatus)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE bill_khachhangs SET status = @status WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", OrderStatus);
                cmd.Parameters.AddWithValue("id", OrderId);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateSoldProduct(int productId, int newSold)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE products SET sold = sold + @new , quantity = quantity - @new1 WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("new", newSold);
                cmd.Parameters.AddWithValue("new1", newSold);
                cmd.Parameters.AddWithValue("id", productId);
                cmd.ExecuteNonQuery();
            }
        }

        public void change_default_shipping_address(int ShippingAddressId, int customerId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                var str1 = "UPDATE shippingaddress SET IsDefault = 1 where ShippingAddressId = @addrIdd and UserId = @cusIdd";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("addrIdd", ShippingAddressId);
                cmd1.Parameters.AddWithValue("cusIdd", customerId);
                cmd1.ExecuteNonQuery();

                var str = "UPDATE shippingaddress SET IsDefault = 0 where ShippingAddressId != @addrId and UserId = @cusId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("addrId", ShippingAddressId);
                cmd.Parameters.AddWithValue("cusId", customerId);
                cmd.ExecuteNonQuery();

                
            }
        }
        public void deleteShippingAddress(int ShippingAddressId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from shippingaddress WHERE ShippingAddressId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", ShippingAddressId);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateSoldProduct(List<object> orderDetail)
        {
            foreach (var item in orderDetail)
            {
                int productId = (int)item.GetType().GetProperty("ProductId").GetValue(item, null);
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var str = "UPDATE products SET Quantity = Quantity + @quantity, Sold =Sold - @sold WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("quantity", (int)item.GetType().GetProperty("OrderQuantity").GetValue(item, null));
                    cmd.Parameters.AddWithValue("sold", (int)item.GetType().GetProperty("OrderQuantity").GetValue(item, null));
                    cmd.Parameters.AddWithValue("id", productId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void saveOrderDetail(CTHD orderDetail)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO c_t_h_d_s(id, product_id, amount, UnitPrice) VALUES (@Id, @proId, @Amount, @unitPrice)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", orderDetail.OrderId);
                cmd.Parameters.AddWithValue("proId", orderDetail.ProductId);
                cmd.Parameters.AddWithValue("Amount", orderDetail.OrderQuantity);
                cmd.Parameters.AddWithValue("unitPrice", orderDetail.UnitPrice);
                cmd.ExecuteNonQuery();
            }
        }

        

        public void addOrderTracking(int OrderId, string OrderStatus)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO ordertracking(OrderId, OrderStatus, CreatedAt) VALUES (@OrderId,@OderStatus,@CreateAt)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("OrderId", OrderId);
                cmd.Parameters.AddWithValue("OderStatus", OrderStatus);
                cmd.Parameters.AddWithValue("CreateAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }
        public List<OrderTracking> getOrderTracking(int orderId)
        {
            List<OrderTracking> list = new List<OrderTracking>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from ordertracking where OrderId = @OrderId order by CreatedAt desc";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("OrderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderTracking()
                        {
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            OrderStatus = reader["OrderStatus"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public int createOrder(BillKhachHang order)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO bill_khachhangs (id, total_money, customer_id, created_at, status, payment_status, PaymentMethod, ShipMethod, ShippingAddressId, ShipFee, Description) " +
                    "VALUES (@Id, @total, @cusId, @date, @tus, @paytus, @PayMet, @ShipMet, @shipAdd, @shipFee, @Des)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", order.Id);
                cmd.Parameters.AddWithValue("total", order.TotalMoney);
                cmd.Parameters.AddWithValue("cusId", order.CustomerId);
                cmd.Parameters.AddWithValue("date", order.CreatedAt);
                cmd.Parameters.AddWithValue("tus", order.Status);
                cmd.Parameters.AddWithValue("paytus", order.Payment_status);
                cmd.Parameters.AddWithValue("PayMet", order.PaymentMethod);
                cmd.Parameters.AddWithValue("ShipMet", order.ShipMethod);
                cmd.Parameters.AddWithValue("shipAdd", order.ShippingAddressId);
                cmd.Parameters.AddWithValue("shipFee", order.ShipFee);
                cmd.Parameters.AddWithValue("Des", order.Description);
                cmd.ExecuteNonQuery();

                int id = 0;
                var str1 = "SELECT id FROM bill_khachhangs ORDER BY id DESC LIMIT 1";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                using (var reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
                return id;
            }
        }

        public List<object> getShippingAddressOfCustomer(int userId)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT ShippingAddressId, Address, Phone, TT.name, QH.name, XP.name, ReceiverName, ShippingAddressType
                        FROM shippingaddress SA, devvn_quanhuyen QH, devvn_tinhthanhpho TT, devvn_xaphuongthitran XP
                        WHERE SA.matp = TT.matp
                        AND SA.maqh = QH.maqh
                        AND SA.xaid = XP.xaid
                        AND IsDefault = 0
                        AND UserId = @UserId;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //System.Diagnostics.Debug.WriteLine("hI: " + reader["CategoryName"].ToString());
                        var obj = new
                        {
                            ShippingAddressId = Convert.ToInt32(reader[0].ToString()),
                            Address = reader[1].ToString(),
                            Phone = reader[2].ToString(),
                            ThanhPho = reader[3].ToString(),
                            QuanHuyen = reader[4].ToString(),
                            XaPhuong = reader[5].ToString(),
                            ReceiverName = reader[6].ToString(),
                            ShippingAddressType = reader[7].ToString(),
                        };
                        list.Add(obj);
                    }
                    reader.Close();
                }
            }
            return list;
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
        public List<devvn_quanhuyen> load_quanhuyen_dropdownbox(string matp) {
            List<devvn_quanhuyen> list = new List<devvn_quanhuyen>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from devvn_quanhuyen where matp = @matp";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("matp", matp);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new devvn_quanhuyen()
                        {
                            Matp = reader["matp"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            Maqh = reader["maqh"].ToString(),
                            ExtraShippingFee = Convert.ToInt32(reader["ExtraShippingFee"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public List<devvn_xaphuongthitran> load_xaphuongthitran_dropdownbox(string maqh)
        {
            List<devvn_xaphuongthitran> list = new List<devvn_xaphuongthitran>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from devvn_xaphuongthitran where maqh = @maqh";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maqh", maqh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new devvn_xaphuongthitran()
                        {
                            Xaid = reader["xaid"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            Maqh = reader["maqh"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public int isProductExistInWishlist(int userId, int productId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                int wishlist = 0;
                conn.Open();
                var str = "SELECT count(*) as SL from wishlist where UserId = @userId and ProductId = @ProId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("ProId", productId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        wishlist = Convert.ToInt32(reader["SL"]);
                    }
                }
                if (wishlist > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Wish list: " + userId + " " + productId);
                    return 0; //Sản phẩm đã tồn tại trong wishlist
                }
            }
            return 1;
        }
        public void remove_product_from_wishlist(int userId, int productId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                int wishlist = 0;
                conn.Open();
                var str = "Delete from wishlist where UserId = @userId and ProductId = @ProId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("ProId", productId);
                cmd.ExecuteNonQuery();
            }
        }
        public void add_product_to_wishlist(int userId, int productId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO wishlist(userId, productId, CreatedAt) VALUES (@Id, @proId,@CreateAt)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", userId);
                cmd.Parameters.AddWithValue("proId", productId);
                cmd.Parameters.AddWithValue("CreateAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }
        public void saveShippingAddress(ShippingAddress shippingAddress)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE `shippingAddress` set `IsDefault` = 0 where UserId =@userId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("UserId", shippingAddress.UserId);
                cmd.ExecuteNonQuery();
            }
            // Set các địa chỉ isDefault = 0
            shippingAddress.IsDefault = 1;
            shippingAddress.UpdatedAt = DateTime.Now;
            shippingAddress.CreatedAt = DateTime.Now;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO `shippingaddress`(`ShippingAddressId`, `ReceiverName`, `Phone`, `Address`, `matp`, `maqh`, `xaid`, `ShippingAddressType`, `UserId`, `IsDefault`, `UpdatedAt`, `CreatedAt`) " +
                    "VALUES (@shipId,@RecName,@Phone,@Addr, @matp,@maqh,@xaid,@shipType,@Userid,@isdefault,@updated,@createdat)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("shipId", shippingAddress.ShippingAddressId);
                cmd.Parameters.AddWithValue("RecName", shippingAddress.ReceiverName);
                cmd.Parameters.AddWithValue("Phone", shippingAddress.Phone);
                cmd.Parameters.AddWithValue("Addr", shippingAddress.Address);
                cmd.Parameters.AddWithValue("matp", shippingAddress.Matp);
                cmd.Parameters.AddWithValue("maqh", shippingAddress.Maqh);
                cmd.Parameters.AddWithValue("xaid", shippingAddress.Xaid);
                cmd.Parameters.AddWithValue("shipType", shippingAddress.ShippingAddressType);
                cmd.Parameters.AddWithValue("Userid", shippingAddress.UserId);
                cmd.Parameters.AddWithValue("isdefault", shippingAddress.IsDefault);
                cmd.Parameters.AddWithValue("updated", shippingAddress.UpdatedAt);
                cmd.Parameters.AddWithValue("createdat", shippingAddress.CreatedAt);
                cmd.ExecuteNonQuery();
            }

            
        }
        public List<ShipMethod> getAllShipMethod()
        {
            List<ShipMethod> list = new List<ShipMethod>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from shipmethod";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ShipMethod()
                        {
                            ShipMethodId = Convert.ToInt32(reader["ShipMethodId"]),
                            ShipMethodName = reader["ShipMethodName"].ToString(),
                            ShipFee = Convert.ToInt32(reader["ShipFee"]),
                            EstimatedDeliveryTime = Convert.ToInt32(reader["EstimatedDeliveryTime"]),
                            Status = Convert.ToInt32(reader["Status"])
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public void updateShipMethodStatus(int shipMethodId, int status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE shipmethod SET Status = @status WHERE ShipMethodId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("id", shipMethodId);
                cmd.ExecuteNonQuery();
            }
        }
        public void deleteShipMethod(int shipmethodId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from shipmethod WHERE ShipMethodId=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", shipmethodId);
                cmd.ExecuteNonQuery();
            }
        }

        public void saveShipMethod(ShipMethod newShipMethod)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO shipmethod(ShipMethodId, ShipMethodName, ShipFee, EstimatedDeliveryTime, Status, CreatedAt) VALUES (@MethobId,@methobName,@fee,@Estimate,@tus,@createAt)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MethobId", newShipMethod.ShipMethodId);
                cmd.Parameters.AddWithValue("methobName", newShipMethod.ShipMethodName);
                cmd.Parameters.AddWithValue("fee", newShipMethod.ShipFee);
                cmd.Parameters.AddWithValue("Estimate", newShipMethod.EstimatedDeliveryTime);
                cmd.Parameters.AddWithValue("tus", newShipMethod.Status);
                cmd.Parameters.AddWithValue("createAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<object> getAllExtraShipfee()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"SELECT QH.name as quanhuyen , TP.name as tinhtp, maqh, ExtraShippingFee 
                            FROM devvn_quanhuyen QH JOIN devvn_tinhthanhpho TP ON QH.matp = TP.matp;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            quanhuyen = reader["quanhuyen"].ToString(),
                            maqh = reader["maqh"].ToString(),
                            ExtraShippingFee = Convert.ToInt32(reader["ExtraShippingFee"]),
                            tinhtp = reader["tinhtp"].ToString(),
                        };
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void updateExtraShipfee(string maqh, int newExtraShippingFee)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE devvn_quanhuyen SET ExtraShippingFee = @ExtraFee WHERE maqh=@ma";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("ExtraFee", newExtraShippingFee);
                cmd.Parameters.AddWithValue("ma", maqh);
                cmd.ExecuteNonQuery();
            }
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

        public void update_shipping_address(ShippingAddress shippingAddress)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE shippingaddress SET ReceiverName = @RecName, Phone = @phone ,Address = @addr, matp = @matp , maqh= @maqh , xaid = @xaid , ShippingAddressType = @shiptype " +
                    "WHERE  ShippingAddressId = @shipAddrId";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("RecName", shippingAddress.ReceiverName);
                cmd.Parameters.AddWithValue("phone", shippingAddress.Phone);
                cmd.Parameters.AddWithValue("addr", shippingAddress.Address);
                cmd.Parameters.AddWithValue("matp", shippingAddress.Matp);
                cmd.Parameters.AddWithValue("maqh", shippingAddress.Maqh);
                cmd.Parameters.AddWithValue("xaid", shippingAddress.Xaid);
                cmd.Parameters.AddWithValue("shiptype", shippingAddress.ShippingAddressType);
                cmd.Parameters.AddWithValue("shipAddrId", shippingAddress.ShippingAddressId);

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

        public void saveCustomer(User newUser)
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
                cmd.Parameters.AddWithValue("Admin", 0);
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

        public int getBannerCurrentMaxId()
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT id FROM banners ORDER BY id DESC LIMIT 1";
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

        public void saveNewBanner(Banner newBanner)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO banners(id, name, url, image, sort_by, active, created_at, updated_at) VALUES (@Id,@Name,@Url,@Image,@Sort_by,@Active,@NgayTao,@NgayUpdate)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", newBanner.Id);
                cmd.Parameters.AddWithValue("Name", newBanner.Name);
                cmd.Parameters.AddWithValue("Url", newBanner.Url);
                cmd.Parameters.AddWithValue("Sort_by", newBanner.Sort_by);
                cmd.Parameters.AddWithValue("Active", newBanner.Active);
                cmd.Parameters.AddWithValue("Image", newBanner.Image);
                cmd.Parameters.AddWithValue("NgayTao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<Banner> getAllBannerForBannerManagement()
        {
            List<Banner> list = new List<Banner>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from banners";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Banner()
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
            return list;
        }

        public void updateBannerStatus(int id, int status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE banners SET active=@status WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteBanner(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM banners WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Banner getBannerById(int id)
        {
            Banner banner = new Banner();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from banners where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    banner.Id = Convert.ToInt32(reader["id"]);
                    banner.Name = reader["name"].ToString();
                    banner.Url = reader["url"].ToString();
                    banner.Image = reader["image"].ToString();
                    banner.Sort_by = Convert.ToInt32(reader["sort_by"]);
                    banner.Active = Convert.ToInt32(reader["active"]);
                    reader.Close();
                }
                conn.Close();
            }
            return banner;
        }

        public void saveUpdateBanner(Banner banner)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE banners SET name=@Name, url=@Url, image=@Image, sort_by=@Sort_by, active=@Active, updated_at=@NgayUpdate WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", banner.Id);
                cmd.Parameters.AddWithValue("Name", banner.Name);
                cmd.Parameters.AddWithValue("Url", banner.Url);
                cmd.Parameters.AddWithValue("Image", banner.Image);
                cmd.Parameters.AddWithValue("Sort_by", banner.Sort_by);
                cmd.Parameters.AddWithValue("Active", banner.Active);
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public void saveUpdateBannerWithoutImage(Banner banner)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE banners SET name=@Name, url=@Url, sort_by=@Sort_by, active=@Active, updated_at=@NgayUpdate WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", banner.Id);
                cmd.Parameters.AddWithValue("Name", banner.Name);
                cmd.Parameters.AddWithValue("Url", banner.Url);
                //cmd.Parameters.AddWithValue("Image", banner.Image);
                cmd.Parameters.AddWithValue("Sort_by", banner.Sort_by);
                cmd.Parameters.AddWithValue("Active", banner.Active);
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<object> getAllBillKhachHang()
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from bill_khachhangs BKH join user U on BKH.customer_id = U.UserID order by BKH.ID DESC";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            OrderId = Convert.ToInt32(reader["id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            OrderStatus = reader["status"].ToString(),
                            Total = Convert.ToInt32(reader["total_money"]),
                            PaymentStatus = reader["payment_status"].ToString(),
                            OrderDate = (DateTime)reader["created_at"]
                        };
                        list.Add(obj);
                    }
                    reader.Close();
                }

                conn.Close();
            }
            return list;
        }

        public List<BillVangLai> getAllBillVangLai()
        {
            List<BillVangLai> list = new List<BillVangLai>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from bill_vanglais";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BillVangLai()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            TotalMoney = Convert.ToInt32(reader["total_money"]),
                            CustomerPhoneNumber = reader["customer_phone_number"].ToString(),
                            CreatedAt = ((DateTime)reader["created_at"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int getProductCurrentMaxId()
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT id FROM products ORDER BY id DESC LIMIT 1";
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

        public void saveNewProduct(Product newProduct)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO products(id, name, description, content, menu_id, original_price, price_sale, active, image, quantity, sold, discount, created_at, updated_at) VALUES (@Id, @Name, @Description, @Content, @Menu_id, @Original_price, @Price_sale, @Active, @Image, @Quantity, @Sold, @Discount, @NgayTao, @NgayUpdate)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", newProduct.Id);
                cmd.Parameters.AddWithValue("Name", newProduct.Name);
                cmd.Parameters.AddWithValue("Description", newProduct.Description);
                cmd.Parameters.AddWithValue("Content", newProduct.Content);
                cmd.Parameters.AddWithValue("Menu_id", newProduct.Menu_id);
                cmd.Parameters.AddWithValue("Original_price", newProduct.Original_price);
                cmd.Parameters.AddWithValue("Price_sale", newProduct.Price_sale);
                cmd.Parameters.AddWithValue("Active", newProduct.Active);
                cmd.Parameters.AddWithValue("Image", newProduct.Image);
                cmd.Parameters.AddWithValue("Quantity", newProduct.Quantity);
                cmd.Parameters.AddWithValue("Sold", 0);
                cmd.Parameters.AddWithValue("Discount", newProduct.Discount);
                cmd.Parameters.AddWithValue("NgayTao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> getAllProductForProductManagement()
        {
            List<Product> list = new List<Product>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from products";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
                            Content = reader["content"].ToString(),
                            Menu_id = Convert.ToInt32(reader["menu_id"]),
                            Original_price = Convert.ToInt32(reader["original_price"]),
                            Price_sale = Convert.ToInt32(reader["price_sale"]),
                            Image = reader["image"].ToString(),
                            Active = Convert.ToInt32(reader["active"]),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            Sold = Convert.ToInt32(reader["sold"]),
                            Discount = Convert.ToInt32(reader["discount"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public void updateProductStatus(int id, int status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE products SET active=@status WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteProduct(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM products WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Product getProductById(int id)
        {
            Product product = new Product();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from products where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    product.Id = Convert.ToInt32(reader["id"]);
                    product.Name = reader["name"].ToString();
                    product.Description = reader["description"].ToString();
                    product.Content = reader["content"].ToString();
                    product.Menu_id = Convert.ToInt32(reader["menu_id"]);
                    product.Original_price = Convert.ToInt32(reader["original_price"]);
                    product.Price_sale = Convert.ToInt32(reader["price_sale"]);
                    product.Quantity = Convert.ToInt32(reader["quantity"]);
                    product.Discount = Convert.ToInt32(reader["discount"]);
                    product.Image = reader["image"].ToString();
                    product.Active = Convert.ToInt32(reader["active"]);
                    reader.Close();
                }
                conn.Close();
            }
            return product;
        }

        public void saveUpdateProduct(Product product)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE products SET name=@Name, description=@Description, content=@Content, menu_id=@Menu_id, original_price=@Original_price, price_sale=@Price_sale, active=@Active, image=@Image, quantity=@Quantity, discount=@Discount, updated_at=@NgayUpdate WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("Id", product.Id);
                cmd.Parameters.AddWithValue("Name", product.Name);
                cmd.Parameters.AddWithValue("Description", product.Description);
                cmd.Parameters.AddWithValue("Content", product.Content);
                cmd.Parameters.AddWithValue("Menu_id", product.Menu_id);
                cmd.Parameters.AddWithValue("Original_price", product.Original_price);
                cmd.Parameters.AddWithValue("Price_sale", product.Price_sale);
                cmd.Parameters.AddWithValue("Active", product.Active);
                cmd.Parameters.AddWithValue("Image", product.Image);
                cmd.Parameters.AddWithValue("Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("Discount", product.Discount);
                cmd.Parameters.AddWithValue("NgayUpdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> getAllProductForExport()
        {
            List<Product> list = new List<Product>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from products";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
                            Menu_id = Convert.ToInt32(reader["menu_id"]),
                            Original_price = Convert.ToInt32(reader["original_price"]),
                            Price_sale = Convert.ToInt32(reader["price_sale"]),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            Sold = Convert.ToInt32(reader["sold"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

    }
}