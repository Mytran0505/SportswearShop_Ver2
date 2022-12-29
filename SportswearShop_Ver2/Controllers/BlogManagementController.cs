using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SportswearShop_Ver2.Controllers
{
    public class BlogManagementController : Controller
    {
        public IActionResult add_content()
        {
            return View();
        }
        [Obsolete]
        public IActionResult SaveContent(Blog newBlog, List<IFormFile> Image)
        {
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			context.saveBlog(newBlog);
            return RedirectToAction("add_content");

        }
        public IActionResult view_content()
        {
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			ViewBag.AllBlog = context.getAllBlogForBlogManagement();
            return View();
        }
        public void unactive_blog(int BlogId)
        {
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			context.updateBlogStatus(BlogId, 0);
        }
        public void active_blog(int BlogId)
        {
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			context.updateBlogStatus(BlogId, 1);
        }

        public void delete_blog(int BlogId)
        {
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			context.deleteBlog(BlogId);
        }
    }
}
