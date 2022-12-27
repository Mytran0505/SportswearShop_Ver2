using ITGoShop_F_Ver2.Controllers;
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
            ViewBag.AllMenu = context.getAllMenu();

            var productDetail = context.getProductDetail(productId);
            int categoryId = (int)(productDetail.GetType().GetProperty("CategoryId").GetValue(productDetail, null));
            int brandId = (int)(productDetail.GetType().GetProperty("MenuId").GetValue(productDetail, null));

            ViewBag.ProductDetail = productDetail;
            ViewBag.RelatedProduct = context.getRelatedProduct(productId, brandId);
            //ViewBag.ProductGallary = linqContext.getProductGallary(productId);
            //List<object> ratingList = context.getRating(productId);
            //ViewBag.RatingList = ratingList;
            //if (ratingList.Count > 0)
            //    ViewBag.AvgRating = Math.Round(ratingList.Average(item => (int)item.GetType().GetProperty("Rating").GetValue(item, null)), 1);
            return View("Index");
        }

        private int isExist(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult save_cart(int productId, int quantity)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>(); //mảng các item
                CartItem newCartItem = new CartItem { Product = context.getProductInfo(productId), Quantity = quantity };
                cart.Add(newCartItem);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem { Product = context.getProductInfo(productId), Quantity = quantity });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("show_cart", "Cart");
        }

        public void send_comment(int ProductId, string CommentContent, int ParentComment)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            int adminId = Convert.ToInt32(HttpContext.Session.GetInt32("adminId"));
            if (customerId != 0 || adminId != 0)
            {
                Comment comment = new Comment();
                comment.CommentContent = CommentContent;
                comment.ProductId = ProductId;

                comment.CreatedAt = DateTime.Now;
                comment.UpdatedAt = DateTime.Now;
                if (customerId != 0)
                {
                    comment.UserId = customerId;
                }
                else
                {
                    comment.UserId = adminId;
                    if (ParentComment != 0)
                    {
                        context.updateCommentStatus(ParentComment);
                    }
                }
                comment.CommentStatus = 1;
                if (ParentComment != 0)
                    comment.ParentComment = ParentComment;
                else
                    comment.ParentComment = null;
                context.addComment(comment);
            }
        }

    }
}
