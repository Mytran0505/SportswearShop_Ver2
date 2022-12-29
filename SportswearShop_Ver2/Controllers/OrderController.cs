using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportswearShop_Ver2.Models;
using MyCardSession.Helpers;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace SportswearShop_Ver2.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult my_orders()
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            if (customerId != 0) // Nếu customer đã đăng nhập
            {
                /*===Cái này để load layout ===*/
                SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
                ViewBag.AllCategory = context.getAllCategory();
                ViewBag.AllMenu = context.getAllMenu();
                //ViewBag.AllSubBrand = linqContext.getAllSubBrand();
                /*======*/

                ViewBag.OrderList = context.getOrderListOfCustomer(customerId);
                return View();
            }
            return RedirectToAction("login", "Home");
        }
        public IActionResult order_detail(int orderId)
        {
            /*===Cái này để load layout ===*/
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            //ViewBag.AllSubBrand = context.getAllSubBrand();
            /*======*/
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            ViewBag.DefaultShippingAddress = context.getShippingAddressOFOrder(orderId);
            ViewBag.OrderInfo = context.getOrderInfo(orderId);
            ViewBag.OrderDetail = context.getOrderDetail(orderId);
            return View();
        }

        public void cancel_order(int orderId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateOrderStatus(orderId, "Đã hủy");
            List<object> orderDetail = context.getOrderDetail(orderId);

            // Cập nhật số lượng tồn kho và đã bán của các sản phẩm trong đơn hàng
            context.updateSoldProduct(orderDetail);
        }

        public int is_rating_exit(int ProductId)
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            return context.isRatingeExit(ProductId, customerId);
        }

        public void add_rating(int ProductId, string Title, string Content, int Rating)
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.addRating(ProductId, Title, Content, Rating, customerId);
        }

        public string get_product(int ProductId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            Product product = context.getProductInfo(ProductId);
            string output = $"<p><b style='font-size:20px'>ĐÁNH GIÁ SẢN PHẨM #{product.Id}</b></p><img src='{product.Image}'  style='margin: auto; max-width: 100px; max-height: 80px; width: auto; height: auto;'/>";
            output += $"<p style='display:inline-block; margin-left:10px'>{product.Name}</p>";
            output += $"<input type='hidden' value='{product.Id}</p>";
            return output;
        }
    }
}
