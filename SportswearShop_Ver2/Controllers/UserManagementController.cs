using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SportswearShop_Ver2.Controllers
{
    public class UserManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public UserManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult add_admin_user()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.NextId = context.getUserCurrentMaxId() + 1;
            return View();
        }

        public IActionResult save_admin_user(User newUser)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveNewAdminUser(newUser);
            return RedirectToAction("add_admin_user");
        }

        public IActionResult view_admin_user()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllAdminUser = context.getAllAdminUser();
            return View();
        }

        public IActionResult view_client_user()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllClientUser = context.getAllClientUser();
            return View();
        }
        public void delete_client_user(int UserId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteClientUser(UserId);
        }

        public IActionResult update_client_user(int UserId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.ClientUserInfo = context.getClientUserById(UserId);
            return View();
        }

        public IActionResult save_update_client_user(User clientuser)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveUpdateClientUser(clientuser);
            return RedirectToAction("view_client_user");
        }
    }
}
