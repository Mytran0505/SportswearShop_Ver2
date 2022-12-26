using SportswearShop_Ver2.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportswearShop_Ver2.Models
{
    public class SportswearShopLINQContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=sportshop_ver22;uid=root;password=;Convert Zero Datetime=True";
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseMySQL(connectionString);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hàm này để set primary key cho Entity Framework
            //modelBuilder.Entity<OrderDetail>().HasKey(od => new {od.OrderId, od.ProductId });
            modelBuilder.Entity<WishList>().HasKey(wl => new { wl.ProductId, wl.UserId });
            //modelBuilder.Entity<OrderTracking>().HasKey(ot => new { ot.OrderId, ot.OrderStatus });
            //modelBuilder.Entity<ProductRating>().HasKey(pt => new { pt.ProductId, pt.UserId });
            modelBuilder.Entity<LoginHistory>().HasKey(lh => new { lh.UserId });
            modelBuilder.Entity<devvn_quanhuyen>().HasKey(qh => new { qh.Maqh });
        }

        public DbSet<User> User { set; get; }   // Bảng User trong DataBase, <User> tên lớp
        public DbSet<Product> Product { set; get; }
        public DbSet<Menu> Menu { set; get; }
        //public DbSet<SubBrand> SubBrand { set; get; }
        public DbSet<Category> Category { set; get; }
        public DbSet<Banner> Banner { set; get; }
        //public DbSet<Campaign> Campaign { set; get; }
        public DbSet<BillKhachHang> Order { set; get; }
        public DbSet<CTHD> CTHD { set; get; }
        //public DbSet<Blog> Blog { set; get; }

        public DbSet<LoginHistory> LoginHistory { set; get; }
        //public DbSet<ShipMethod> ShipMethod { set; get; }
        //public DbSet<ProductGallary> ProductGallary { set; get; }
        //public DbSet<Comment> Comment { set; get; }
        public DbSet<devvn_quanhuyen> devvn_quanhuyen { set; get; }
        public DbSet<devvn_tinhthanhpho> devvn_tinhthanhpho { set; get; }
        public DbSet<devvn_xaphuongthitran> devvn_xaphuongthitran { set; get; }
        public DbSet<ShippingAddress> ShippingAddress { set; get; }
        public DbSet<Statistic> Statistic { set; get; }
        public DbSet<WishList> WishList { set; get; }
        //public DbSet<OrderTracking> OrderTracking { set; get; }
        //public DbSet<ProductRating> ProductRating { set; get; }

        //public List<Category>getAllCategory()
        //{
        //    var categories = Category.Where(c => c.Status == 1).ToList();
        //    return categories;
        //}
        //public List<Brand> getAllBrand()
        //{
        //    var brands = Brand.Where(c => c.Status == 1).ToList();
        //    return brands;
        //}
        //public List<SubBrand> getAllSubBrand()
        //{
        //    var subbrand = SubBrand.Where(c => c.Status == 1).ToList();
        //    return subbrand;
        //}

        //public List<SubBrand> getSubBrand(int brandId)
        //{
        //    var subbrand = SubBrand.Where(c => c.BrandId == brandId).ToList();
        //    return subbrand;
        //}
        //public void saveProduct(Product newProduct)
        //{
        //    newProduct.StartsAt = DateTime.Now;
        //    newProduct.CreatedAt = DateTime.Now;
        //    newProduct.UpdatedAt = DateTime.Now;
        //    Product.Add(newProduct);
        //    SaveChanges();
        //}
        public void saveCustomer(User newUser)
        {
            newUser.Admin = 0;
            newUser.UserImage = "default-user-icon.png";
            newUser.LastLogin = DateTime.Now;
            newUser.RegisteredAt = DateTime.Now;
            User.Add(newUser);
            SaveChanges();
        }
        //public void saveBlog(Blog newBlog)
        //{
        //    newBlog.DateCreate = DateTime.Now;
        //    Blog.Add(newBlog);
        //    SaveChanges();
        //}
        public void saveCategory(Category newCate)
        {
            Category.Add(newCate);
            SaveChanges();
        }
        //public void saveBrand(Brand newBrand)
        //{
        //    Brand.Add(newBrand);
        //    SaveChanges();
        //}

        //public void saveSubBrand(SubBrand newSubBrand)
        //{
        //    SubBrand.Add(newSubBrand);
        //    SaveChanges();
        //}
        //public void saveShipMethod(ShipMethod newShipMethod)
        //{
        //    //newShipMethod.CreatedAt = DateTime.Now; // Câu lệnh này bị lỗi
        //    ShipMethod.Add(newShipMethod);
        //    SaveChanges();
        //}

        //public void updateProduct(Product productInfo)
        //{
        //    var product = Product.Where(p => p.ProductId == productInfo.ProductId).FirstOrDefault();
        //    product.ProductName = productInfo.ProductName;
        //    product.Quantity = productInfo.Quantity;
        //    product.Cost = productInfo.Cost;
        //    product.Price = productInfo.Price;
        //    product.Content = productInfo.Content;
        //    product.Discount = productInfo.Discount;
        //    product.UpdatedAt = DateTime.Now;
        //    product.Status = productInfo.Status;
        //    product.BrandId = productInfo.BrandId;
        //    product.SliderId = productInfo.SliderId;
        //    product.CategoryId = productInfo.CategoryId;
        //    product.SubBrandId = productInfo.SubBrandId;
        //    if (!string.IsNullOrEmpty(productInfo.ProductImage))
        //    {
        //        product.ProductImage = productInfo.ProductImage;
        //        // Chưa làm xóa ảnh cũ ở đây
        //    }
        //    this.SaveChanges();
        //}
        //public void updateCate(Category cateInfo)
        //{
        //    var cate = Category.Where(p => p.CategoryId == cateInfo.CategoryId).FirstOrDefault();
        //    if (cate != null)
        //    {
        //        cate.CategoryName = cateInfo.CategoryName;
        //        cate.Status = cateInfo.Status;
        //    }
        //    SaveChanges();
        //}
        //public void updateBrand(Brand brandInfo)
        //{
        //    var brand = Brand.Where(p => p.BrandId == brandInfo.BrandId).FirstOrDefault();
        //    brand.BrandName = brandInfo.BrandName;
        //    brand.CategoryId = brandInfo.CategoryId;
        //    brand.Description = brandInfo.Description;
        //    brand.Status = brandInfo.Status;
        //    if (!string.IsNullOrEmpty(brandInfo.BrandLogo))
        //    {
        //        brand.BrandLogo = brandInfo.BrandLogo;
        //        // Chưa làm xóa ảnh cũ ở đây
        //    }
        //    SaveChanges();
        //}

        //public void updateSubBrand(SubBrand subbrandInfo)
        //{
        //    var subbrand = SubBrand.Where(p => p.SubBrandId == subbrandInfo.SubBrandId).FirstOrDefault();

        //    if (subbrand != null)
        //    {
        //        subbrand.SubBrandName = subbrandInfo.SubBrandName;
        //        subbrand.BrandId = subbrandInfo.BrandId;
        //        subbrand.Status = subbrandInfo.Status;
        //    }
        //    SaveChanges();
        //}

        //public void deleteProduct(int productId)
        //{
        //    var product = (from p in Product
        //                   where (p.ProductId == productId)
        //                   select p).FirstOrDefault();

        //    if (product != null)
        //    {
        //        Remove(product);
        //        SaveChanges();
        //    }
        //}


        //public void deleteBrand(int brandId)
        //{
        //    var subbrand = (from p in SubBrand
        //                where (p.BrandId == brandId)
        //                select p).ToList();
        //    if(subbrand.Count > 0)
        //    {
        //        foreach (var item in subbrand)
        //        {
        //            Remove(item);
        //            SaveChanges();
        //        }    
        //    }
        //    var brand = (from p in Brand
        //                where (p.BrandId == brandId)
        //                select p).FirstOrDefault();
        //    if (brand != null)
        //    {
        //        Remove(brand);
        //        SaveChanges();
        //    }
        //}

        //public List<BannerSlider> getAllBannerSliders()
        //{
        //    return BannerSlider.OrderByDescending(b => b.CreatedAt).ToList();
        //}

        //public List<Campaign> getAllCampaign()
        //{
        //    return Campaign.ToList();
        //}

        //public List<ShipMethod> getAllShipMethod()
        //{
        //    return ShipMethod.ToList();
        //}
        //public List<ShipMethod> getShipMethodToCheckout()
        //{
        //    return ShipMethod.Where(s => s.Status == 1).ToList();
        //}

        //public List<devvn_tinhthanhpho> getAllThanhPho()
        //{
        //    return devvn_tinhthanhpho.ToList();
        //}

        //public List<devvn_quanhuyen> getAllQuanHuyen()
        //{
        //    return devvn_quanhuyen.ToList();
        //}
        //public List<devvn_xaphuongthitran> getAllXaPhuong()
        //{
        //    return devvn_xaphuongthitran.ToList();
        //}

        ////1 bảng
        ////public List<Comment> getAllComments()
        ////{
        ////    return Comment.ToList();
        ////}

        ////join nhiều bảng
        ////public IQueryable getAllComments()
        ////{
        ////    //var commentList = Comment.Join(User,c => c.UserId,u => u.UserId, (c, u) => new
        ////    //{
        ////    //    CommentId = c.CommentId,
        ////    //    UserId = c.UserId,
        ////    //    UserName = u.LastName
        ////    //});
        ////    var commentList = from c in Comment
        ////                 join u in User on c.UserId equals u.UserId
        ////                 select new  MyCategory
        ////                 {
        ////                     CommentId = c.CommentId,
        ////                     UserId = c.UserId,
        ////                     UserName = u.LastName
        ////                 };
        ////    return commentList;
        ////}


        //public void updateSliderStatus(int sliderId, int status)
        //{
        //    var slider = BannerSlider.Where(p => p.SliderId == sliderId).FirstOrDefault();
        //    slider.SliderStatus = status;
        //    SaveChanges();
        //}

        //public void deleteSlider(int sliderId)
        //{
        //    System.Diagnostics.Debug.WriteLine("PID: " + sliderId);
        //    // Code thiếu chỗ xóa các bảng liên quan ở đây
        //    var slider = BannerSlider.Where(p => p.SliderId == sliderId).FirstOrDefault();

        //    if (slider != null)
        //    {
        //        Remove(slider);
        //        SaveChanges();
        //    }
        //}

        //public void saveSlider(BannerSlider newSlider)
        //{
        //    newSlider.CreatedAt = DateTime.Now;
        //    newSlider.UpdatedAt = DateTime.Now;
        //    BannerSlider.Add(newSlider);
        //    SaveChanges();
        //}


        public void updateLoginHistory(LoginHistory login)
        {
            //LoginHistory.Add(login);
            //SaveChanges();
        }

        //public void updateOrderStatus(int OrderId, string OrderStatus, string PaymentStatus)
        //{
        //    var order = Order.Where(p => p.OrderId == OrderId).FirstOrDefault();
        //    order.OrderStatus = OrderStatus;
        //    order.PaymentStatus = PaymentStatus;
        //    SaveChanges();
        //}

        //public void addOrderTracking(int OrderId, string OrderStatus)
        //{
        //    OrderTracking orderTracking = new OrderTracking(OrderId, OrderStatus, DateTime.Now);
        //    var ot = OrderTracking.Where(p => p.OrderId == OrderId && p.OrderStatus == OrderStatus).FirstOrDefault();
        //    if(ot == null) // nếu dữ liệu chưa tồn tài thì mới thêm
        //    {
        //        OrderTracking.Add(orderTracking);
        //        SaveChanges();
        //    }
        //}

        //public void updateOrderStatus(int OrderId, string OrderStatus)
        //{
        //    var order = Order.Where(p => p.OrderId == OrderId).FirstOrDefault();
        //    order.OrderStatus = OrderStatus;
        //    SaveChanges();
        //}

        //public void updateShipMethodStatus(int shipMethodId, int status)
        //{
        //    var shipmethod = ShipMethod.Where(p => p.ShipMethodId == shipMethodId).FirstOrDefault();
        //    shipmethod.Status = status;
        //    this.SaveChanges();
        //}
        //public void deleteShipMethod(int shipmethodId)
        //{
        //    var shipmethod = ShipMethod.Where(p => p.ShipMethodId == shipmethodId).FirstOrDefault();

        //    if (shipmethod != null)
        //    {
        //        Remove(shipmethod);
        //        SaveChanges();
        //    }
        //}
        //public List<Product> getGiamGiaSoc()
        //{
        //    return Product.Where(p => p.Status == 1 && p.Discount != 0).OrderByDescending(b => b.Discount).Take(6).ToList();
        //}

        //public IQueryable<Product> getBrandProduct(int brandId)
        //{
        //    return Product.Where(p => p.Status == 1 && p.BrandId == brandId );
        //}

        //public IQueryable<Product> getCateProduct(string categoryId)
        //{
        //    return Product.Where(p => p.Status == 1 && p.CategoryId == categoryId);
        //}

        //public IQueryable<Product> getSubProduct(string subbrandId)
        //{
        //    return Product.Where(p => p.Status == 1 && p.SubBrandId == subbrandId);
        //}

        //public IQueryable<Brand> getB(string categoryId)
        //{
        //    return Brand.Where(p => p.Status == 1 && p.CategoryId == categoryId);
        //}
        //public IQueryable<SubBrand> getSB(string subbrandId)
        //{
        //    var bi= SubBrand.Where(p => p.SubBrandId == subbrandId).FirstOrDefault();
        //    return SubBrand.Where(p => p.Status == 1 && p.BrandId == bi.BrandId);
        //}

        //public IQueryable<SubBrand> getSBN(string brandname)
        //{
        //    var bi = Brand.Where(p => p.BrandName == brandname).FirstOrDefault();
        //    return SubBrand.Where(p => p.Status == 1 && p.BrandId == bi.BrandId);
        //}
        //public IQueryable<Product> getBNProduct(string brandName)
        //{
        //    var ketqua = from product in Product
        //                 join brand in Brand on product.BrandId equals brand.BrandId into t
        //                 from brand in t.DefaultIfEmpty()
        //                 where brand.BrandName == brandName
        //                 select product ;

        //    return ketqua;
        //}

        //public List<ProductGallary> getProductGallary(int productId)
        //{
        //    return ProductGallary.Where(p => p.ProductId == productId && p.GallaryStatus == 1).ToList();
        //}

        //public void updateProductGallaryStatus(int GallaryId, int status)
        //{
        //    var productGallary = ProductGallary.Where(p => p.GallaryId == GallaryId).FirstOrDefault();
        //    productGallary.GallaryStatus = status;
        //    SaveChanges();
        //}

        //public void deleteProductGallary(int GallaryId)
        //{
        //    var productGallary = ProductGallary.Where(p => p.GallaryId == GallaryId).FirstOrDefault();

        //    if (productGallary != null)
        //    {
        //        Remove(productGallary);
        //        SaveChanges();
        //    }
        //}

        //public void saveProductGallary(ProductGallary productGallary)
        //{
        //    ProductGallary.Add(productGallary);
        //    SaveChanges();
        //}

        //public IQueryable<Product> getRelatedProduct(int productId, string categoryId, int brandId)
        //{
        //    var relatedProduct = Product.Where(p => p.ProductId != productId && p.CategoryId == categoryId && p.BrandId == brandId).Take(10);
        //    return relatedProduct;
        //}

        public void deleteShippingAddress(int ShippingAddressId)
        {
            var shippingAddress = ShippingAddress.Where(p => p.ShippingAddressId == ShippingAddressId).FirstOrDefault();

            if (shippingAddress != null)
            {
                Remove(shippingAddress);
                SaveChanges();
            }
        }

        public List<devvn_xaphuongthitran> load_xaphuongthitran_dropdownbox(string maqh)
        {
            var xaphuong = devvn_xaphuongthitran.Where(x => x.Maqh == maqh).ToList();
            return xaphuong;
        }
        public List<devvn_quanhuyen> load_quanhuyen_dropdownbox(string matp)
        {
            var quanhuyen = devvn_quanhuyen.Where(q => q.Matp == matp).ToList();
            return quanhuyen;
        }

        public void change_default_shipping_address(int ShippingAddressId, int customerId)
        {

            // Set các địa chỉ isDefault = 0
            var shippingAddressList = ShippingAddress.Where(s => s.UserId == customerId).ToList();
            foreach (var item in shippingAddressList)
            {
                System.Diagnostics.Debug.WriteLine("id=" + item.ShippingAddressId);
                item.IsDefault = 0;
            }
            SaveChanges();

            // Set isDefault = 1 đối với địa chỉ đã chọn
            var shippingAddress = ShippingAddress.Where(s => s.ShippingAddressId == ShippingAddressId).FirstOrDefault();
            shippingAddress.IsDefault = 1;
            System.Diagnostics.Debug.WriteLine("cus=" + shippingAddress.ReceiverName + shippingAddress.IsDefault);
            SaveChanges();
        }

        public void saveShippingAddress(ShippingAddress shippingAddress)
        {
            // Set các địa chỉ isDefault = 0
            var shippingAddressList = ShippingAddress.Where(s => s.UserId == shippingAddress.UserId).ToList();
            foreach (var item in shippingAddressList)
            {
                item.IsDefault = 0;
            }
            SaveChanges();

            shippingAddress.IsDefault = 1;
            shippingAddress.UpdatedAt = DateTime.Now;
            shippingAddress.CreatedAt = DateTime.Now;
            ShippingAddress.Add(shippingAddress);
            SaveChanges();
        }
        public void update_shipping_address(ShippingAddress shippingAddress)
        {
            var shippingAddressUpdate = ShippingAddress.Where(s => s.ShippingAddressId == shippingAddress.ShippingAddressId).FirstOrDefault();
            if (shippingAddressUpdate != null)
            {
                shippingAddressUpdate.ReceiverName = shippingAddress.ReceiverName;
                shippingAddressUpdate.ShippingAddressType = shippingAddress.ShippingAddressType;
                shippingAddressUpdate.Xaid = shippingAddress.Xaid;
                shippingAddressUpdate.Maqh = shippingAddress.Maqh;
                shippingAddressUpdate.Matp = shippingAddress.Matp;
                shippingAddressUpdate.Address = shippingAddress.Address;
                shippingAddressUpdate.Phone = shippingAddress.Phone;
                SaveChanges();
            }
        }

        //public int createOrder(BillKhachHang order)
        //{
        //    Order.Add(order);
        //    SaveChanges();
        //    return order.Id;
        //}

        ////public ShipMethod getShipMethod(int ShipMethodId)
        ////{
        ////    var shipMethod = ShipMethod.Where(s => s.ShipMethodId == ShipMethodId).FirstOrDefault();
        ////    return shipMethod;
        ////}
        //public void saveOrderDetail(CTHD orderDetail)
        //{
        //    CTHD.Add(orderDetail);
        //    SaveChanges();
        //}

        //public BillKhachHang getOrderInfo(int orderId)
        //{
        //    var orderInfo = BillKhachHang.Where(o => o.id == orderId).FirstOrDefault();
        //    return orderInfo;
        //}

        //public Statistic getStatistic(DateTime statisticDate)
        //{
        //    // Chuyển đổi như vầy để set Time = 00:00:00 để so sánh với statisticDate (kiểu Date, ko có Time) trong CSDL
        //    DateTime myDate = DateTime.ParseExact(statisticDate.ToString("yyyy-MM-dd"), "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);

        //    var statistic = Statistic.Where(s => s.StatisticDate == myDate).FirstOrDefault();
        //    //if(statistic != null)
        //    //    System.Diagnostics.Debug.WriteLine("SD: " + statistic.StatisticDate);
        //    return statistic;
        //}

        //public List<Statistic> getStatistic(DateTime tu_ngay, DateTime den_ngay)
        //{
        //    // Chuyển đổi như vầy để set Time = 00:00:00 để so sánh với statisticDate (kiểu Date, ko có Time) trong CSDL
        //    tu_ngay = DateTime.ParseExact(tu_ngay.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //    den_ngay = DateTime.ParseExact(den_ngay.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

        //    var statistic = Statistic.Where(s => s.StatisticDate >= tu_ngay && s.StatisticDate <= den_ngay).ToList();
        //    return statistic;
        //}

        //public void addStatistic(Statistic newStatisticLine)
        //{
        //    Statistic.Add(newStatisticLine);
        //    SaveChanges();
        //}

        //public void updateStatistic(Statistic newData)
        //{
        //    // Chuyển đổi như vầy để set Time = 00:00:00 để so sánh với statisticDate (kiểu Date, ko có Time) trong CSDL
        //    DateTime myDate = DateTime.ParseExact(newData.StatisticDate.ToString("yyyy-MM-dd"), "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);

        //    var statistic = Statistic.Where(s => s.StatisticDate == myDate).FirstOrDefault();
        //    statistic.Sales += newData.Sales;
        //    statistic.Profit += newData.Profit;
        //    SaveChanges();
        //}

        //public void updateSoldProduct(int productId, int newSold)
        //{
        //    var product = Product.Where(p => p.Id == productId).FirstOrDefault();
        //    product.Sold += newSold;
        //    product.Quantity -= newSold;
        //    SaveChanges();
        //}

        //public void updateSoldProduct(List<object> orderDetail)
        //{
        //    foreach(var item in orderDetail)
        //    {
        //        int productId = (int)item.GetType().GetProperty("ProductId").GetValue(item, null);
        //        var product = Product.Where(p => p.ProductId == productId).FirstOrDefault();
        //        product.Sold -= (int)item.GetType().GetProperty("OrderQuantity").GetValue(item, null);
        //        product.Quantity += (int)item.GetType().GetProperty("OrderQuantity").GetValue(item, null);
        //    }
        //    SaveChanges();
        //}

        //public list<BillKhachHang> getorderlistofcustomer(int userid)
        //{
        //    var orderlist = order.where(o => o.userid == userid).orderbydescending(o => o.orderid).tolist();
        //    return orderlist;
        //}

        public void remove_product_from_wishlist(int userId, int productId)
        {
            var wishlist = WishList.Where(p => p.UserId == userId && p.ProductId == productId).FirstOrDefault();

            if (wishlist != null)
            {
                Remove(wishlist);
                SaveChanges();
            }
        }
        public void add_product_to_wishlist(int userId, int productId)
        {
            WishList newItem = new WishList()
            {
                UserId = userId,
                ProductId = productId,
                CreatedAt = DateTime.Now
            };
            WishList.Add(newItem);
            SaveChanges();
        }
        public int isProductExistInWishlist(int userId, int productId)
        {
            var wishlist = WishList.Where(p => p.UserId == userId && p.ProductId == productId).FirstOrDefault();

            if (wishlist != null)
            {
                System.Diagnostics.Debug.WriteLine("Wish list: " + wishlist.UserId + " " + wishlist.ProductId);
                return 0; //Sản phẩm đã tồn tại trong wishlist
            }
            return 1;
        }

        //public void updateCommentStatus(int ParentComment)
        //{
        //    var comment = Comment.Where(c => c.CommentId == ParentComment).FirstOrDefault();
        //    comment.Reply = 1;
        //    SaveChanges();
        //}

        //public void addComment(Comment comment)
        //{
        //    Comment.Add(comment);
        //    SaveChanges();
        //}

        //public void deleteComment(int commentId)
        //{
        //    var subComment = Comment.Where(c => c.ParentComment == commentId).ToList();

        //    if (subComment.Count != 0)
        //    {
        //        foreach (var item in subComment)
        //        {
        //            Remove(item);
        //            SaveChanges();
        //        }
        //    }
        //    var comment = Comment.Where(c => c.CommentId == commentId).FirstOrDefault();
        //    if(comment != null)
        //    {
        //        Remove(comment);
        //        SaveChanges();
        //    }
        //}

        //public List<OrderTracking> getOrderTracking(int orderId)
        //{
        //    return OrderTracking.Where(ot => ot.OrderId == orderId).OrderByDescending(ot => ot.CreatedAt).ToList();
        //}

        //public int isRatingeExit(int productId, int userId)
        //{
        //    var productRating = ProductRating.Where(pr => pr.ProductId == productId && pr.UserId == userId).FirstOrDefault();
        //    if (productRating == null)
        //        return 0;
        //    return 1;
        //}

        //public void addRating(int productId, string title, string content, int rating, int userId)
        //{
        //    ProductRating productRating = new ProductRating() 
        //    { 
        //        ProductId = productId,
        //        Title = title,
        //        Content = content,
        //        Rating = rating,
        //        UserId = userId,
        //        CreatedAt = DateTime.Now,
        //        ProductRatingStatus = 1
        //    };
        //    ProductRating.Add(productRating);
        //    SaveChanges();
        //}
        //public void updateRatingStatus(int userId, int productId, int status)
        //{
        //    var rating = ProductRating.Where(p => p.UserId == userId && p.ProductId == productId).FirstOrDefault();
        //    rating.ProductRatingStatus = status;
        //    SaveChanges();
        //}

        //public void deleteRating(int userId, int productId)
        //{
        //    var rating = ProductRating.Where(p => p.UserId == userId && p.ProductId == productId).FirstOrDefault();

        //    if (rating != null)
        //    {
        //        Remove(rating);
        //        SaveChanges();
        //    }
        //}
        //public void updateExtraShipfee(string maqh, int newExtraShippingFee)
        //{
        //    var quanhuyenInfo = devvn_quanhuyen.Where(qh => qh.Maqh == maqh).FirstOrDefault();
        //    if (quanhuyenInfo != null)
        //    {
        //        quanhuyenInfo.ExtraShippingFee = newExtraShippingFee;
        //        SaveChanges();
        //    }
        //}

        //public void updateCommentStatus(int CommentId, int Status)
        //{
        //    var comment = Comment.Where(c => c.CommentId == CommentId).FirstOrDefault();
        //    if (comment != null)
        //    {
        //        comment.CommentStatus = Status;
        //        SaveChanges();
        //    }
        //}
        //public void updateCommentReply(int CommentId, int Reply)
        //{
        //    var comment = Comment.Where(c => c.CommentId == CommentId).FirstOrDefault();
        //    if (comment != null)
        //    {
        //        comment.Reply = Reply;
        //        SaveChanges();
        //    }
        //}

        //public Brand getBrand(int brandId)
        //{
        //    var brandInfo = Brand.Where(b => b.BrandId == brandId).FirstOrDefault();
        //    if (brandInfo != null)
        //        return brandInfo;
        //    return null;
        //}

        //public SubBrand getSubBrandInfo(string subbrandId)
        //{
        //    var subbrandInfo = SubBrand.Where(b => b.SubBrandId == subbrandId).FirstOrDefault();
        //    if (subbrandInfo != null)
        //        return subbrandInfo;
        //    return null;
        //}
        //public void updateSubBrandStatus(string SubBrandId, int Status)
        //{
        //    var subbrand = SubBrand.Where(p => p.SubBrandId == SubBrandId).FirstOrDefault();
        //    subbrand.Status = Status;
        //    SaveChanges();
        //}

        //public void deleteSubBrand(string SubBrandId)
        //{
        //    var subbrand = SubBrand.Where(p => p.SubBrandId == SubBrandId).FirstOrDefault();

        //    if (subbrand != null)
        //    {
        //        Remove(subbrand);
        //        SaveChanges();
        //    }
        //}
        //public void deleteCategory(string categoryId)
        //{
        //    var cate = Category.Where(p => p.CategoryId == categoryId).FirstOrDefault();
        //    if (cate != null)
        //    {
        //        Remove(cate);
        //        SaveChanges();
        //    }
        //}

        //public Category getCate(string categoryId)
        //{
        //    var cateInfo = Category.Where(b => b.CategoryId == categoryId).FirstOrDefault();
        //    if (cateInfo != null)
        //        return cateInfo;
        //    return null;
        //}
        //public void updateCateStatus(string CategoryId, int Status)
        //{
        //    var cate = Category.Where(p => p.CategoryId == CategoryId).FirstOrDefault();
        //    cate.Status = Status;
        //    SaveChanges();
        //}

        //public void updateBlogStatus(int BlogId, int Status)
        //{
        //    var blog = Blog.Where(p => p.BlogId == BlogId).FirstOrDefault();
        //    blog.Status = Status;
        //    SaveChanges();
        //}
        //public void deleteBlog(int BlogId)
        //{
        //    var blog = Blog.Where(p => p.BlogId == BlogId).FirstOrDefault();
        //    if (blog != null)
        //    {
        //        Remove(blog);
        //        SaveChanges();
        //    }
        //}
    }
}
