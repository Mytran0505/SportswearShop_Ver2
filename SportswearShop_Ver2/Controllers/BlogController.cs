using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SportswearShop_Ver2.Models;


namespace SportswearShop_Ver2.Controllers
{
    public class BlogController : Controller
    {

        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        public IActionResult all_blog()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            //ViewBag.AllSubBrand = context.getAllSubBrand();

            ViewBag.AllBlog = context.getListBlog();
            return View();
        }
        public IActionResult blog_detail(int blogId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            //ViewBag.AllSubBrand = context.getAllSubBrand();

            ViewBag.BlogDetail = context.getBlogDetail(blogId);
            ViewBag.BlogRelated = context.getBlogRelate(blogId);
            return View();
        }

        

    }
}
