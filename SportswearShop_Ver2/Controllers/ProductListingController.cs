﻿using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;

namespace SportswearShop_Ver2.Controllers
{
	public class ProductListingController : Controller
	{
        public object Product_Listing1(int categoryId)
        {
            //System.Diagnostics.Debug.WriteLine("Chạy product detail");
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            ViewBag.NameOfCategory = context.getNameOfCategory(categoryId);
            ViewBag.AllProduct = context.getAlllProduct();
            ViewBag.MenuOfCategory = context.getMenuOfCategory(categoryId);
            ViewBag.ProductForHomePage = context.getProductForHomePage();
            ViewBag.ProductOfMenu1 = context.getProductOfMenu1(categoryId);
            ViewBag.ProductOfMenu2 = context.getProductOfMenu2(categoryId);
            ViewBag.ProductOfMenu3 = context.getProductOfMenu3(categoryId);
            //ViewBag.ProductOfMenu = context.getProductOfMenu(menuId);

            //ViewBag.LTProduct = context.getAoProduct(categoryId);
            //ViewBag.PCProduct = context.getQuanProduct(int categoryId);
            //ViewBag.PKProduct = context.getGiayProduct(int categoryId);
            //ViewBag.ProductGallary = linqContext.getProductGallary(productId);
            //List<object> ratingList = context.getRating(productId);
            //ViewBag.RatingList = ratingList;
            //if (ratingList.Count > 0)
            //    ViewBag.AvgRating = Math.Round(ratingList.Average(item => (int)item.GetType().GetProperty("Rating").GetValue(item, null)), 1);
            return View();
        }

        public object Product_Listing2(int menuId)
        {
            //System.Diagnostics.Debug.WriteLine("Chạy product detail");
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.MenuOfCategory = context.getMenuOfCategory(menuId);
            ViewBag.NameOfMenu = context.getNameOfMenu(menuId);
            ViewBag.AllCategory = context.getAllCategory();
            ViewBag.AllMenu = context.getAllMenu();
            ViewBag.ProductOfMenu = context.getProductOfMenu(menuId);
            
            //ViewBag.LTProduct = context.getAoProduct(categoryId);
            //ViewBag.PCProduct = context.getQuanProduct(int categoryId);
            //ViewBag.PKProduct = context.getGiayProduct(int categoryId);
            //ViewBag.ProductGallary = linqContext.getProductGallary(productId);
            //List<object> ratingList = context.getRating(productId);
            //ViewBag.RatingList = ratingList;
            //if (ratingList.Count > 0)
            //    ViewBag.AvgRating = Math.Round(ratingList.Average(item => (int)item.GetType().GetProperty("Rating").GetValue(item, null)), 1);
            return View();
        }
    }
}

