using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class ShipMethodManagementController : Controller
    {
        public IActionResult view_shipmethod()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllShipMethod = context.getAllShipMethod();
            return View();
        }
        public void update_shipmethod_status(int ShipMethodId, int Status)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateShipMethodStatus(ShipMethodId, Status);
        }

        public void delete_shipmethod(int ShipMethodId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteShipMethod(ShipMethodId);
        }
        public IActionResult save_shipmethod(ShipMethod newShipMethod)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveShipMethod(newShipMethod);
            return RedirectToAction("view_shipmethod");
        }
        public IActionResult add_shipmethod()
        {
            return View();
        }
        public IActionResult view_extra_shipfee()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.ExtraShipfee = context.getAllExtraShipfee();
            return View();
        }
        public void update_extra_shipfee(string maqh, int ExtraShippingFee)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateExtraShipfee(maqh, ExtraShippingFee);
        }
    }
}
