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
            return View();
        }

        [Obsolete]
        public IActionResult SaveMenu(Menu newMenu)
        {

            var context = new SportswearShopLINQContext();
            context.saveMenu(newMenu);
            return RedirectToAction("add_product_menu");
        }
    }
}
