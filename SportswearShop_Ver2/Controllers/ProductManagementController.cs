using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace SportswearShop_Ver2.Controllers
{
    public class ProductManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public ProductManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult view_product()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllProduct = context.getAllProductForProductManagement();
            ViewBag.AllMenu = context.getAllMenuForMenuManagement();
            return View();
        }

        public void unactive_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateProductStatus(Id, 0);
        }

        public void active_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateProductStatus(Id, 1);
        }

        public void delete_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteProduct(Id);
        }
    }
}
