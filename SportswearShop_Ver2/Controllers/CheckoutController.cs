using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using MyCardSession.Helpers;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using Rn.NetCore.MailUtils;
namespace SportswearShop_Ver2.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            ViewBag.AllThanhPho = context.getAllThanhPho();
            ViewBag.AllQuanHuyen = context.getAllQuanHuyen();
            ViewBag.AllXaPhuong = context.getAllXaPhuong();

            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            if (customerId != 0) // Nếu customer đã đăng nhập
            {
                ViewBag.DefaultShippingAddress = context.getDefaultShippingAddress(customerId);
                
            }
            ViewBag.AllShipMethod = context.getShipMethodToCheckout();
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart == null)
            {
                ViewBag.total = 0;
                ViewBag.numberItem = 0;
            }
            else
            {
                ViewBag.numberItem = cart.Sum(item => item.Quantity);
                ViewBag.total = cart.Sum(item => item.Product.Price_sale * item.Quantity);
            }

            //return RedirectToAction("login_to_checkout");
            return View();

        }
        public IActionResult login_to_checkout(string message)
        {
            if (!string.IsNullOrEmpty(ViewBag.message))
            {
                ViewBag.message = message;
            }
            return View();
        }

        public IActionResult checkout_after_login(User userInput)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            User userInfo = context.getUserInfo(userInput.Email, userInput.Password, 0);
            if (userInfo != null)
            {
                HttpContext.Session.SetInt32("customerId", userInfo.UserId);
                HttpContext.Session.SetString("customerLastName", userInfo.LastName);
                HttpContext.Session.SetString("customerFirstName", userInfo.FirstName);
                HttpContext.Session.SetString("customerImage", userInfo.UserImage);
                HttpContext.Session.SetString("customerEmail", userInfo.Email);

                //var LINQContext = new SportswearShopLINQContext();
                //LoginHistory login = new LoginHistory(userInfo.UserId, DateTime.Now, DateTime.Now);
                //LINQContext.updateLoginHistory(login);
                // Update last login
                context.updateLastLogin(userInfo.UserId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("login_to_checkout", new { message = "Mật khẩu hoặc tài khoản sai. Xin nhập lại!" });
        }

        public async Task<IActionResult> create_orderAsync(BillKhachHang order)
        {
            // Lấy thông tin từ cart để thêm thông tin đơn hàng vào bảng ORDER
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            order.TotalMoney = order.ShipFee + cart.Sum(item => item.Product.Price_sale * item.Quantity);
            order.CustomerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            //order.DateUpdate = DateTime.Now;
            order.CreatedAt = DateTime.Now;
            order.Status = "Đặt hàng thành công";
            order.Payment_status = "Chờ thanh toán";
            int numberOfProduct = cart.Sum(item => item.Quantity);
            order.Description = cart.First().Product.Name;
            if (numberOfProduct > 1)
                order.Description += " và " + (numberOfProduct - 1) + " sản phẩm khác";
            int orderId = context.createOrder(order);

            //// Thêm theo dõi đơn hàng
            context.addOrderTracking(orderId, "Đặt hàng thành công");

            foreach (var item in cart)
            {
                // Thêm các thông tin vào bảng chi tiết hóa đơn
                CTHD orderDetail = new CTHD()
                {
                    OrderQuantity = item.Quantity,
                    ProductId = item.Product.Id,
                    UnitPrice = item.Product.Price_sale,
                    OrderId = orderId
                };
                context.saveOrderDetail(orderDetail);

                Product productInfo = context.getProductInfo(item.Product.Id);

                ////Update thông tin doanh thu lên bảng statistic
                //Statistic statistic = linqContext.getStatistic(DateTime.Now);
                //Statistic newStatisticInfo = new Statistic()
                //{
                //    StatisticDate = DateTime.Now,
                //    Sales = (int)(item.Quantity * item.Product.Price),
                //    Profit = (int)((item.Product.Price - item.Product.Cost) * item.Quantity)
                //};
                //if (statistic != null)
                //{
                //    // Nếu đã có dòng thống kê doanh thu cho hôm nay thì cập nhật dữ liệu
                //    linqContext.updateStatistic(newStatisticInfo);
                //}
                //else
                //{
                //    // Nếu chưa có dòng thống kê cho hôm nay thì tạo mới
                //    linqContext.addStatistic(newStatisticInfo);
                //}

                // Trừ số lượng tồn kho
                context.updateSoldProduct(productInfo.Id, item.Quantity);
            }
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            // Gửi mail

            string customerFirstName = HttpContext.Session.GetString("customerFirstName");
            string customerLastName = HttpContext.Session.GetString("customerLastName");
            string customerEmail = HttpContext.Session.GetString("customerEmail");
            string mailContent = getMailContent(order, customerFirstName, customerLastName);
            await MailUtils.SendMailGoogleSmtp("mytran0505itlker@gmail.com", customerEmail, $"Chào {customerFirstName}, SportswearShop đã nhận được đơn hàng của bạn", mailContent,
                                          "mytran0505itlker@gmail.com", "SportswearShop");
            return RedirectToAction("order_detail", "Order", new { orderId = orderId });
        }
        public string getMailContent(BillKhachHang orderInfo, string customerFirstName, string customerLastName)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string ShipFee = orderInfo.ShipFee.ToString("#,###", cul.NumberFormat);
            string ToTal = orderInfo.TotalMoney.ToString("#,###", cul.NumberFormat);
            string EstimatedDeliveryTime = orderInfo.EstimatedDeliveryTime.ToString("dd-MM-yyyy");
            string OrderDate = orderInfo.CreatedAt.ToString("HH:mm dd-MM-yyyy");
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            object shippingAddress = context.getShippingAddress(orderInfo.ShippingAddressId);

            string output = @$"<body>
                <div class='card' style='margin: 40px 100px;'>
                        <div class='card-body' style='font-size:16px'>
                            <h2>Cảm ơn quý khách {customerLastName} {customerFirstName} đã đặt hàng tại SportswearShop,</h2>
                            <p class='card-text'>SportswearShop rất vui thông báo đơn hàng #{orderInfo.Id} của quý khách đã được tiếp nhận và đang trong quá trình xử lý. SportswearShop sẽ thông báo đến quý khách ngay khi hàng chuẩn bị được giao.</p>
                            <p class='card-text' style='color:#77ACF1;'><b>THÔNG TIN ĐƠN HÀNG #{orderInfo.Id}</b>  (Thời gian đặt hàng: {OrderDate})</p>
                            <hr>
                            <p class='card-text'><b>Mô tả đơn hàng:</b> {orderInfo.Description}</p>
                            <p class= 'card-text'><b> Địa chỉ giao hàng:</b></p>
                            <p> Tên người nhận: {shippingAddress.GetType().GetProperty("ReceiverName").GetValue(shippingAddress, null)}</p>
                            <p> Địa chỉ: {shippingAddress.GetType().GetProperty("Address").GetValue(shippingAddress, null)}, {shippingAddress.GetType().GetProperty("XaPhuong").GetValue(shippingAddress, null)}, {shippingAddress.GetType().GetProperty("QuanHuyen").GetValue(shippingAddress, null)}, {shippingAddress.GetType().GetProperty("ThanhPho").GetValue(shippingAddress, null)}</p>
                            </p> Điện thoại: {shippingAddress.GetType().GetProperty("Phone").GetValue(shippingAddress, null)}</p>
                            <p class= 'card-text'><b> Phương thức thanh toán:</b> {orderInfo.ShipMethod}</p>
                            <p class= 'card-text'><b> Thời gian giao hàng dự kiến: </b> dự kiến giao hàng vào ngày EstimatedDeliveryTime</p>
                            <p class= 'card-text'><b> Phí vận chuyển: </b>{ShipFee} ₫</p>
                            <p class= 'card-text'><b> TỔNG TRỊ GIÁ ĐƠN HÀNG: </b><b style = 'color:red; font-size: 20px' >{ToTal} ₫</b></p>
                            <p class= 'card-text'> Trân trọng,</p>
                            <p class='card-text'> Đội ngũ SportswearShop.</p>
                            <p class= 'card-text'><i> Lưu ý: Với những đơn hàng thanh toán trả trước, xin vui lòng đảm bảo người nhận hàng đúng thông tin đã đăng kí trong đơn hàng, và chuẩn bị giấy tờ tùy thân để đơn vị giao nhận có thể xác thực thông tin khi giao hàng</i></p>
                        </div>
                  </div>
            </body>";
            return output;
        }
    }
}
