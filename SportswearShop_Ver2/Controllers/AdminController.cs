using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Drawing;

namespace SportswearShop_Ver2.Controllers
{
    public class AdminController : Controller
    {
        string adminId = "";
        string adminLastName = "";
        string adminFirstName = "";
        string adminImage = "";

        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message)
        {
            if (HttpContext.Session.GetInt32("adminId") != null)
            {
                // Nếu admin đăng nhập rồi thì vô thẳng dashboard
                return RedirectToAction("Dashboard");
            }
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            return View();

        }

       

        public IActionResult Dashboard()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            // Chuyển dữ liệu admin qua
            DateTime homNay = DateTime.Now;
            DateTime dauThangNay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int soNgayThangTruoc = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month) - 1;
            DateTime cuoiThangTruoc = ((new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1)).AddDays(soNgayThangTruoc);
            DateTime dauThangTruoc = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);
            DateTime dauNamNay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddYears(-1);

            ViewBag.numberCustomer = context.countCustomer();
            ViewBag.numberProduct = context.countProduct();
            ViewBag.numberOrder = context.countOrder();
            ViewBag.totalRevenue = context.getTotalRevenue();
            ViewBag.totalRevenueThisMonth = context.getRevenueThisMonth(dauThangNay, homNay);
            ViewBag.numberOrderToday = context.countOrder(homNay, homNay);
            //ViewBag.numberLoginToday = context.countLogin(homNay, homNay);
            ViewBag.topProducts = context.getTopProduct(dauThangTruoc, homNay);
            //ViewBag.topBlogView = context.getTopBlogView();
            //ViewBag.topProductView = context.getTopProductView();
            //ViewBag.inventoryList = context.getInventoryList();
            //ViewBag.numberLoginThangNay = context.countLogin(dauThangNay, homNay);
            //ViewBag.numberLoginThangTruoc = context.countLogin(dauThangTruoc, cuoiThangTruoc);
            //ViewBag.numberLoginNamNay = context.countLogin(dauNamNay, homNay);
            return View();
        }

        public List<object> load_pie_chart()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            return context.countOrderByDate(DateTime.Now.AddDays(-30), DateTime.Now);
        }

        public List<object> load_default_chart()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            return context.getRevenueByDate(DateTime.Now.AddDays(-30), DateTime.Now);
        }

        public List<object> filter_by_time_span(string time_span)
        {
            DateTime homNay = DateTime.Now;
            DateTime dauThangNay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int soNgayThangTruoc = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month) - 1;
            DateTime cuoiThangTruoc = ((new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1)).AddDays(soNgayThangTruoc);
            DateTime dauThangTruoc = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);
            DateTime dauNamNay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddYears(-1);

            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            if (time_span == "7ngay")
                return context.getRevenueByDate(DateTime.Now.AddDays(-7), homNay);
            if (time_span == "thangnay")
                return context.getRevenueByDate(dauThangNay, homNay);
            if (time_span == "thangtruoc")
                return context.getRevenueByDate(dauThangTruoc, cuoiThangTruoc);
            return context.getRevenueByDate(DateTime.Now.AddDays(-365), DateTime.Now);
        }

        public List<object> filter_by_date(DateTime den_ngay, DateTime tu_ngay)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            return context.getRevenueByDate(tu_ngay, den_ngay);
        }
    }
}
