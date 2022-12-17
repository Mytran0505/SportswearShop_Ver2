using Microsoft.AspNetCore.Mvc;
using MyCardSession.Helpers;
using SportswearShop_Ver2.Models;

namespace SportswearShop_Ver2.Controllers
{
    public class ProductDetailController : Controller
    {
        public IActionResult product_detail(int productId)
        {
            //System.Diagnostics.Debug.WriteLine("Chạy product detail");
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMẹnu();

            var productDetail = context.getProductDetail(productId);
            int categoryId = (int)(productDetail.GetType().GetProperty("CategoryId").GetValue(productDetail, null));
            int brandId = (int)(productDetail.GetType().GetProperty("MenuId").GetValue(productDetail, null));

            ViewBag.ProductDetail = productDetail;
            ViewBag.RelatedProduct = context.getRelatedProduct(productId);
            //ViewBag.ProductGallary = linqContext.getProductGallary(productId);
            //List<object> ratingList = context.getRating(productId);
            //ViewBag.RatingList = ratingList;
            //if (ratingList.Count > 0)
            //    ViewBag.AvgRating = Math.Round(ratingList.Average(item => (int)item.GetType().GetProperty("Rating").GetValue(item, null)), 1);
            return View("Index");
        }



    }
}
