using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportswearShop_Ver2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class ShippingAddressController : Controller
    {
        public IActionResult show_shipping_address()
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            if (customerId != 0) // Nếu customer đã đăng nhập
            {
                /*===Cái này để load layout ===*/
                SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
                ViewBag.AllCategory = context.getAllCategory();
                ViewBag.AllMenu = context.getAllMenu();
                //ViewBag.AllSubBrand = context.getAllSubBrand();
                /*======*/
                ViewBag.DefaultShippingAddress = context.getDefaultShippingAddress(customerId);
                ViewBag.ShippingAddressList = context.getShippingAddressOfCustomer(customerId);
                ViewBag.AllThanhPho = context.getAllThanhPho();
                ViewBag.AllQuanHuyen = context.getAllQuanHuyen();
                ViewBag.AllXaPhuong = context.getAllXaPhuong();
            }
            return View("Index");
        }
        public IActionResult add_shipping_address(ShippingAddress shippingAddress)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            shippingAddress.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            context.saveShippingAddress(shippingAddress);
            return RedirectToAction("Index", "Checkout");
        }
        public void delete_shipping_address(int ShippingAddressId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteShippingAddress(ShippingAddressId);
        }

        public string load_xaphuongthitran_dropdownbox(string maqh)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            List<devvn_xaphuongthitran> xaphuong = context.load_xaphuongthitran_dropdownbox(maqh);
            string output = "";
            foreach(var item in xaphuong)
            {
                output += "<option value=" + item.Xaid + ">"+ item.Name +"</option>";
            }
            return output;
        }
        public string load_quanhuyen_dropdownbox(string matp)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            List<devvn_quanhuyen> quanhuyen = context.load_quanhuyen_dropdownbox(matp);
            string output = "";
            foreach (var item in quanhuyen)
            {
                output += "<option value=" + item.Maqh + ">" + item.Name + "</option>";
            }
            return output;
        }

        public IActionResult change_default_shipping_address(int shippingAddressId)
        {
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("customerId"));
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.change_default_shipping_address(shippingAddressId, customerId);
            return RedirectToAction("Index", "Checkout");
        }
        public IActionResult update_shipping_address(ShippingAddress shippingAddress)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.update_shipping_address(shippingAddress);
            return RedirectToAction("show_shipping_address");
        }
    }
}
