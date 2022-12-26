using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace SportswearShop_Ver2.Controllers
{
    public class ProductManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public ProductManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult add_product()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.NextId = context.getProductCurrentMaxId() + 1;
            ViewBag.AllMenu = context.getAllMenuForMenuManagement();
            return View();
        }

        [Obsolete]
        public IActionResult save_new_product(Product newProduct)
        {

            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.saveNewProduct(newProduct);
            return RedirectToAction("add_product");

        }

        public IActionResult view_product()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllProduct = context.getAllProductForProductManagement();
            ViewBag.AllMenu = context.getAllMenuForMenuManagement();
            return View();
        }

        public void unactive_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateProductStatus(Id, 0);
        }

        public void active_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateProductStatus(Id, 1);
        }

        public void delete_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteProduct(Id);
        }

        public IActionResult update_product(int Id)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.ProductInfo = context.getProductById(Id);
            ViewBag.AllMenu = context.getAllMenuForMenuManagement();
            return View();
        }

        [Obsolete]
        public IActionResult save_update_product(Product product)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;

            context.saveUpdateProduct(product);
            return RedirectToAction("view_product");

        }

        public ActionResult ExportProduct()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var data = new DataTable();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add($"id");
                sheet.Cells["A1:H99"].Style.Font.Name = "Times New Roman";
                sheet.Cells["A1:C1"].Merge = true;
                sheet.Column(3).Width = 25;
                sheet.Column(1).Width = 5.33;
                sheet.Column(2).Width = 11.67;
                sheet.Column(5).Width = 20;
                //sheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:H1"].Value = "SPORTSWEAR SHOP";
                sheet.Cells["A1:H1"].Style.Font.Bold = true;
                sheet.Cells["A1:H1"].Style.Font.Size = 12;
                sheet.Cells["A1:H1"].Merge = true;
                //sheet.Cells["A2:C2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A2:C2"].Value = "Trường ĐH Công nghệ Thông tin, khu phố 6, Linh Trung, Thủ Đức, TP Hồ Chí Minh";
                sheet.Cells["A2:H2"].Style.Font.Size = 12;
                sheet.Cells["A2:H2"].Merge = true;

                sheet.Cells["A3:H3"].Merge = true;

                sheet.Cells["A4:D4"].Value = "PHIẾU KIỂM KHO";
                sheet.Cells["A4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A4:D4"].Style.Font.Bold = true;
                sheet.Cells["A4:D4"].Style.Font.Size = 16;
                sheet.Cells["A4:H4"].Merge = true;

                string ngayKiemKho = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                sheet.Cells["A5:D5"].Value = $"(Ngày kiểm kho: {ngayKiemKho})";
                sheet.Cells["A5:D5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A5:D5"].Style.Font.Italic = true;
                sheet.Cells["A5:H5"].Merge = true;

                sheet.Cells["A6:H6"].Merge = true;

                sheet.Row(7).Height = 25;
                sheet.Cells["A7:H7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A7:H7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells["A7:H7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A7:H7"].Style.Fill.BackgroundColor.SetColor(0, 186, 248, 255);
                sheet.Cells["A7:H7"].Style.Font.Bold = true;
                sheet.Cells["A7:H7"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A7:H7"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A7:H7"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A7:H7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A7:A7"].Value = "Mã sản phẩm";
                sheet.Cells["B7:B7"].Value = "Tên sản phẩm";
                sheet.Cells["C7:C7"].Value = "Danh mục sản phẩm";
                sheet.Cells["D7:D7"].Value = "Mô tả sơ lược";
                sheet.Cells["E7:E7"].Value = "Giá vốn";
                sheet.Cells["F7:F7"].Value = "Giá bán";
                sheet.Cells["G7:G7"].Value = "Số lượng bán được";
                sheet.Cells["H7:H7"].Value = "Số lượng còn tồn";

                SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
                List<Product> products = context.getAllProductForExport();

                int count = products.Count + 7;
                string productArea = $"A7:H{count}";
                sheet.Cells[productArea].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[productArea].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[productArea].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells[productArea].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells[productArea].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[productArea].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                int j = 8;
                foreach (var item in products)
                {
                    sheet.Row(j).Height = 25;
                    string masp = $"A{j}:A{j}";
                    string tensp = $"B{j}:B{j}";
                    string danhmucsp = $"C{j}:C{j}";
                    string mota = $"D{j}:D{j}";
                    string giavon = $"E{j}:E{j}";
                    string giaban = $"F{j}:F{j}";
                    string daban = $"G{j}:G{j}";
                    string conton = $"H{j}:H{j}";
                    sheet.Cells[masp].Value = item.GetType().GetProperty("Id").GetValue(item, null);
                    sheet.Cells[tensp].Value = item.GetType().GetProperty("Name").GetValue(item, null);
                    sheet.Cells[danhmucsp].Value = item.GetType().GetProperty("Menu_id").GetValue(item, null);
                    sheet.Cells[mota].Value = item.GetType().GetProperty("Description").GetValue(item, null);
                    sheet.Cells[giavon].Value = item.GetType().GetProperty("Original_price").GetValue(item, null);
                    sheet.Cells[giaban].Value = item.GetType().GetProperty("Price_sale").GetValue(item, null);
                    sheet.Cells[daban].Value = item.GetType().GetProperty("Sold").GetValue(item, null);
                    sheet.Cells[conton].Value = item.GetType().GetProperty("Quantity").GetValue(item, null);
                    j++;
                }
                sheet.Cells[$"G{count + 3}:G{count + 3}"].Value = "Ngày . . .  tháng . . . năm . . .";
                sheet.Cells[$"G{count + 3}:G{count + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"G{count + 3}:G{count + 3}"].Style.Font.Size = 12;

                sheet.Cells[$"G{count + 4}:G{count + 4}"].Value = "Nhân viên kiểm kho";
                sheet.Cells[$"G{count + 4}:G{count + 4}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"G{count + 4}:G{count + 4}"].Style.Font.Bold = true;
                sheet.Cells[$"G{count + 4}:G{count + 4}"].Style.Font.Size = 12;

                sheet.Cells[$"G{count + 5}:G{count + 5}"].Value = "(Kí, ghi rõ họ tên)";
                sheet.Cells[$"G{count + 5}:G{count + 5}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"G{count + 5}:G{count + 5}"].Style.Font.Italic = true;
                sheet.Cells[$"G{count + 5}:G{count + 5}"].Style.Font.Size = 12;

                sheet.Cells[productArea].AutoFitColumns();
                sheet.Name = "Product";
                package.Save();
            }
            stream.Position = 0;

            var tenfile = $"product.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", tenfile);
        }

    }
}
