using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;

namespace SportswearShop_Ver2.Controllers
{
    public class PromotionManagement : Controller
    {
        public IActionResult add_promotion()
        {
            return View();
        }

        public IActionResult SavePromotion(Promotion newPromotion, List<IFormFile> Image)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.savePromotion(newPromotion);
            return RedirectToAction("add_promotion");
        }

        public IActionResult view_promotion()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllPromotion = context.getAllPromtionForPromtionManagement();
            return View();
        }

        public void unactive_promotion(int PromotionId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updatePromotionStatus(PromotionId, 0);
        }
        public void active_promotion(int PromotionId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updatePromotionStatus(PromotionId, 1);
        }
    }
}
