using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SportswearShop_Ver2.Controllers
{
    public class BannerManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public BannerManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult add_banner()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.NextId = context.getBannerCurrentMaxId() + 1;
            return View();
        }

        [Obsolete]
        public IActionResult save_new_banner(Banner newBanner, List<IFormFile> Image)
        {
            // Lưu ảnh sản phẩm vào trước
            string path = Path.Combine(this.Environment.WebRootPath, "/public/images/");
            foreach (IFormFile postedFile in Image)
            {
                // Lấy tên file
                newBanner.Image = "/public/images/" + postedFile.FileName;
                // Lưu file vào project
                //string fileName = Path.GetFileName(DateTime.Now.ToString("yyyy_MM_dd_HHmmss_") + postedFile.FileName);
                //using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                //{
                //    postedFile.CopyTo(stream);
                //}
            }

            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveNewBanner(newBanner);
            return RedirectToAction("add_banner");

        }

        public IActionResult view_banner()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllBanner = context.getAllBannerForBannerManagement();
            return View();
        }

        public void unactive_banner(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateBannerStatus(Id, 0);
        }

        public void active_banner(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateBannerStatus(Id, 1);
        }

        public void delete_banner(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteBanner(Id);
        }

    }
}
