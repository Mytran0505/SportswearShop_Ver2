using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class RatingManagementController : Controller
    {
        public IActionResult Index()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.allRating = context.getAllRating();
            return View();
        }
        public void update_rating_status(int UserId, int ProductId, int Status)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateRatingStatus(UserId, ProductId, Status);
        }

        public void delete_rating(int UserId, int ProductId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteRating(UserId, ProductId);
        }
    }
}
