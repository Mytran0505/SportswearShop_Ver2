using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class OrderTrackingController : Controller
    {
        public IActionResult Index(int orderId)
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            if (customerId != 0) // Nếu customer đã đăng nhập
            {
                /*===Cái này để load layout ===*/
                SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
                ViewBag.AllCategory = context.getAllCategory();
                ViewBag.AllMenu = context.getAllMenu();
                //ViewBag.AllSubBrand = context.getAllSubBrand();
                /*======*/

                ViewBag.OrderInfo = context.getOrderInfo(orderId);
                ViewBag.OrderDetail = context.getOrderDetail(orderId);
                return View();
            }
            return RedirectToAction("login", "Home");
        }
        public string load_order_tracking(int OrderId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            List<OrderTracking> orderTracking = context.getOrderTracking(OrderId);
            string output = @"<table class='track_tbl'><tbody>";
            foreach (var item in orderTracking)
            { 
                output += @$"<tr><td class='track_dot'>
                                            <span class='track_line'></span>
                                        </td>
                                        <td>
                                            <b>{item.OrderStatus}</b><br>
                                            {item.CreatedAt.ToString("HH:mm dd-MM-yyyy")}
                                        </td>
                                    </tr>";
            }
            output += "</tbody></table>";
            return output; 
        }

        public string load_order_status(int OrderId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            List<OrderTracking> orderTracking = context.getOrderTracking(OrderId);
            if (orderTracking.Count == 0)
                return "";
            return $"<b style='font-size:18px; color:red'>{orderTracking[0].OrderStatus}</b>";
        }
    }
}
