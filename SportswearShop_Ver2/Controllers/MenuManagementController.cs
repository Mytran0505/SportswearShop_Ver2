using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SportswearShop_Ver2.Controllers
{
    public class MenuManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public MenuManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult add_product_menu()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.NextId = context.getMenuCurrentMaxId() + 1;
            return View();
        }

        [Obsolete]
        public IActionResult SaveMenu(Menu newMenu)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveMenu(newMenu);
            return RedirectToAction("add_product_menu");
        }

        public IActionResult view_product_menu()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllMenu = context.getAllMenuForMenuManagement();
            return View();
        }

        public void unactive_menu(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateMenuStatus(Id, 0);
        }

        public void active_menu(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateMenuStatus(Id, 1);
        }

        public void delete_menu(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteMenu(Id);
        }

        public IActionResult update_product_menu(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.MenuInfo = context.getMenuById(Id);
            return View();
        }

        public IActionResult save_update_menu(Menu menu)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveUpdateMenu(menu);
            return RedirectToAction("view_product_menu");
        }
    }
}
