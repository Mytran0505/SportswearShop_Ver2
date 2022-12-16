﻿using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportswearShop_Ver2.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult print_revenue_report(DateTime tu_ngay, DateTime den_ngay)
        {
            if (tu_ngay == DateTime.MinValue || den_ngay == DateTime.MinValue) // DateTime is null
            {
                tu_ngay = DateTime.Now.AddDays(-30);
                den_ngay = DateTime.Now;
            }
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.tuNgay = tu_ngay;
            ViewBag.denNgay = den_ngay;
            ViewBag.statisticInfo = context.getStatistic(tu_ngay, den_ngay);
            return View();
        }
    }
}
