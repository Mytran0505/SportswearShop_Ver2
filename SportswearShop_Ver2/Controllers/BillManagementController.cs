using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace SportswearShop_Ver2.Controllers
{
    public class BillManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public BillManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult view_bill_khachhangs()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllBill = context.getAllBillKhachHang();
            return View();
        }

        public IActionResult view_bill_vanglais()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllBill = context.getAllBillVangLai();
            return View();
        }

        public void update_order_status(int OrderId, string OrderStatus, string PaymentStatus)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateOrderStatus(OrderId, OrderStatus, PaymentStatus);
            context.addOrderTracking(OrderId, OrderStatus);
        }

        public IActionResult order_detail(int orderId)
        {
            /*===Cái này để load layout ===*/
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            //ViewBag.AllSubBrand = context.getAllSubBrand();
            /*======*/

            var OrderInfo = context.getOrderInfo(orderId);
            ViewBag.DefaultShippingAddress = context.getDefaultShippingAddress((int)OrderInfo.GetType().GetProperty("CustomerId").GetValue(OrderInfo, null));
            ViewBag.OrderInfo = OrderInfo;
            ViewBag.OrderDetail = context.getOrderDetail(orderId);
            return View();
        }
    }
}
