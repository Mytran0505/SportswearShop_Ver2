using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCardSession.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
namespace SportswearShop_Ver2.Controllers
{
    public class HomeController : Controller
    {
        string customerId = "";
        string customerLastName = "";
        string customerFirstName = "";
        string customerImage = "";
        string customerEmail = "";
        string total1 = "", total2 = "";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public async Task<IActionResult> IndexAsync()
		{
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            ViewBag.Blog = context.getBlog();
            ViewBag.SliderForHomePage = context.getSliderForHomePage();
			ViewBag.BannerForHomePage = context.getBannerForHomePage();
			ViewBag.ProductForHomePage = context.getProductForHomePage();
			ViewBag.GiamGiaSoc = context.getGiamGiaSoc();
			ViewBag.GiamGia = context.getAllPromtionForPromtionManagement();
            return View();
		}

		public IActionResult All_product()
		{
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			ViewBag.AllCategory = context.getAllCategory();
			ViewBag.AllMenu = context.getAllMenu();
			ViewBag.Allproduct = context.getAlllProduct();
			return View();
		}

		public IActionResult login(string message)
		{
			if (!string.IsNullOrEmpty(ViewBag.message))
			{
				ViewBag.message = message;
			}
			return View();
		}
        private IActionResult View(List<Blog> blogs, List<Product> products)
        {
            throw new NotImplementedException();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public int load_cart_quantity()
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                return 0;
            }
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            return cart.Sum(item => item.Quantity);
        }

        public string load_cart()
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                return @"<img style='display: block; width: auto; height: 150px; margin-left: auto; margin-right:auto;' src='/public/client/Images/empty-cart.png'>
                <p> Bạn chưa có sản phẩm nào trong giỏ hàng</p>";
            }

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int numberProduct = cart.Sum(item => item.Quantity);
            if (numberProduct == 0)
            {
                return @"<img style='display: block; width: auto; height: 150px; margin-left: auto; margin-right:auto;' src='/public/client/Images/empty-cart.png'>
                <p> Bạn chưa có sản phẩm nào trong giỏ hàng</p>";
            }

            string output = "<div class='ps-cart__content'>";
            foreach (var cartItem in cart)
            {
                string image = cartItem.Product.Image;
                output += @$"<div class='ps-cart-item'>
                <input type = 'hidden' class='item-id-for-cart' value='{cartItem.Product.Id}'/>
                <a class='ps-cart-item__close delete-button-in-nav' href='javascript:void(0)'></a>
                <div class='ps-cart-item__thumbnail'>
                    <a href = '/ProductDetail/product_detail?productId={cartItem.Product.Id}'></a><img src='{cartItem.Product.Image}' alt=''>
                </div>
                <div class='ps-cart-item__content'>
                    <a class='ps-cart-item__title' href='/ProductDetail/product_detail?productId={cartItem.Product.Id}'>{cartItem.Product.Name}</a>
                    <p>{cartItem.Product.Price_sale.ToString("#,###", cul.NumberFormat)}₫ x{cartItem.Quantity}</p>
                </div>
            </div>";
            }
            output += @$"</div><div class='ps-cart__total'>
            <p>Số sản phẩm:<span>{numberProduct}</span></p>
            <p>Tổng tiền:<span>{(cart.Sum(item => item.Product.Price_sale * item.Quantity)).ToString("#,###", cul.NumberFormat)} ₫</span></p></div>
            <div class='ps-cart__footer'>
            <a href = 'javascript:void(0)' class='ps-btn btn-thanh-toan'>THANH TOÁN</a>";
            return output;
        }
        public IActionResult check_password(User userInput)
		{
			System.Diagnostics.Debug.WriteLine(userInput.Email);
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			User userInfo = context.getUserInfo(userInput.Email, userInput.Password, 0);
			if (userInfo != null)
			{
				HttpContext.Session.SetInt32("customerId", userInfo.UserId);
				HttpContext.Session.SetString("customerLastName", userInfo.LastName);
				HttpContext.Session.SetString("customerFirstName", userInfo.FirstName);
				HttpContext.Session.SetString("customerImage", userInfo.UserImage);
				HttpContext.Session.SetString("customerEmail", userInfo.Email);

				//var LINQContext = new SportswearShopLINQContext();
				//LoginHistory login = new LoginHistory(userInfo.UserId, DateTime.Now, DateTime.Now);
				//LINQContext.updateLoginHistory(login);
				// Update last login
				context.updateLastLogin(userInfo.UserId);
				return RedirectToAction("Index");
			}
			return RedirectToAction("login", new { message = "Mật khẩu hoặc tài khoản sai. Xin nhập lại!" });
		}
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("customerId", "");
            HttpContext.Session.SetString("customerLastName", "");
            HttpContext.Session.SetString("customerFirstName", "");
            HttpContext.Session.SetString("customerImage", "");
            return RedirectToAction("Index");
        }
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public IActionResult customer_register(User newCustomer)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveCustomer(newCustomer);
            System.Diagnostics.Debug.WriteLine(newCustomer.Email);
            return RedirectToAction("check_password", "Home", new RouteValueDictionary(newCustomer));
        }
        public ActionResult search_result(string kw_submit)
        {

            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            @ViewData["CurrentFilter"] = kw_submit;
            ViewBag.Result1 = context.getAllProductSearch(kw_submit);
            return View();
        }
    }
}