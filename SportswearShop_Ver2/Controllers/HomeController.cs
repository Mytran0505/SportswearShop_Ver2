using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;
using System.Diagnostics;

namespace SportswearShop_Ver2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public async Task<IActionResult> IndexAsync()
		{
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			var linqContext = new SportswearShopLINQContext();
			//ViewBag.AllCategory = linqContext.getAllCategory();
			//ViewBag.AllBrand = linqContext.getAllBrand();
			//ViewBag.AllSubBrand = linqContext.getAllSubBrand();
			//ViewBag.AllBlog = context.getAllBlog();
			//ViewBag.AllBannerSlider = context.getAllBannerSlider();
			//ViewBag.Top3ProductView = context.getTop3ProductView();
			//ViewBag.Top3Product = context.get3Product();
			//ViewBag.Blog = context.getBlog();
			//ViewBag.New = context.get2Blog();
			//ViewBag.LTProduct = context.getLTProduct();
			//ViewBag.PCProduct = context.getPCProduct();
			//ViewBag.PKProduct = context.getPKProduct();

			ViewBag.SliderForHomePage = context.getSliderForHomePage();
			ViewBag.BannerForHomePage = context.getBannerForHomePage();
			ViewBag.ProductForHomePage = context.getProductForHomePage();
			//ViewBag.GiamGiaSoc = context.getGiamGiaSoc();
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

				var LINQContext = new SportswearShopLINQContext();
				LoginHistory login = new LoginHistory(userInfo.UserId, DateTime.Now, DateTime.Now);
				LINQContext.updateLoginHistory(login);
				// Update last login
				context.updateLastLogin(userInfo.UserId);
				return RedirectToAction("Index");
			}
			return RedirectToAction("login", new { message = "Mật khẩu hoặc tài khoản sai. Xin nhập lại!" });
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
	}
}