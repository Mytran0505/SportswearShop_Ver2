using Microsoft.AspNetCore.Mvc;
using MyCardSession.Helpers;
using SportswearShop_Ver2.Models;

namespace SportswearShop_Ver2.Controllers
{
	public class CartController : Controller
	{
		public IActionResult show_cart()
		{
			/*===Cái này để load layout ===*/
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
			var linqContext = new SportswearShopLINQContext();
			ViewBag.AllCategory = context.getAllCategory();
			ViewBag.AllMenu = context.getAllMenu();
			//ViewBag.AllSubBrand = linqContext.getAllSubBrand();
			/*======*/

			var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			ViewBag.cart = cart;
			if (cart == null)
			{
				ViewBag.total = 0;
				ViewBag.numberItem = 0;
			}
			else
			{
				ViewBag.numberItem = cart.Sum(item => item.Quantity);
				ViewBag.total = cart.Sum(item => item.Product.Price_sale * item.Quantity);
			}
			return View("Index");
		}

		public void add_to_cart(int ProductId, int Quantity)
		{
			//System.Diagnostics.Debug.WriteLine("h: " + ProductId+" " + Quantity);
			SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

			if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
			{
				List<CartItem> cart = new List<CartItem>(); //mảng các item
				CartItem newCartItem = new CartItem { Product = context.getProductInfo(ProductId), Quantity = Quantity };
				cart.Add(newCartItem);
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}
			else
			{
				List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
				int index = isExist(ProductId);
				if (index != -1)
				{
					cart[index].Quantity++;
				}
				else
				{
					cart.Add(new CartItem { Product = context.getProductInfo(ProductId), Quantity = Quantity });
				}
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}
		}
		public void remove_item(int id)
		{
			List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			int index = isExist(id);
			cart.RemoveAt(index);
			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
		}

		public void update_quantity(int ProductId, int newQuantity)
		{
			List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
			int index = isExist(ProductId);
			cart[index].Quantity = newQuantity;
			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
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
	}
}
