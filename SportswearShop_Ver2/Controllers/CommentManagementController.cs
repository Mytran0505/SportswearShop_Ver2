using SportswearShop_Ver2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Controllers
{
    public class CommentManagementController : Controller
    {
        public IActionResult view_comment()
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            ViewBag.AllComments = context.getAllComments();
            return View();
        }

        public string load_comment()
        {
            string output = "";
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            var allComments = context.getAllComments();
            foreach (var item in allComments)
            {
                output += "<tr>";
                output += $"<td>{item.GetType().GetProperty("CreatedAt").GetValue(item, null)}</td>";
                output += $"<td>{item.GetType().GetProperty("CommentContent").GetValue(item, null)}<input type='text' class='CommentId' value='{item.GetType().GetProperty("CommentId").GetValue(item, null) }' hidden></td>";
                output += $"<td>{item.GetType().GetProperty("UserName").GetValue(item, null)}</td>";
                output += $"<td>{item.GetType().GetProperty("ProductName").GetValue(item, null)}</td><td style='text-align:center;'>";

                if ((int)item.GetType().GetProperty("Reply").GetValue(item, null) == 1)
                    output += "<input class='form-check-input reply-check-box' type='checkbox' value='' checked>";
                else
                    output += "<input class='form-check-input reply-check-box' type='checkbox' value=''></td>";
                output += @$"<td>
                            <div class='form-button-action'>
                                <button type = 'button' data-toggle='tooltip' title='' class='btn btn-link btn-danger' data-original-title='Trả lời bình luận'>
                                 <a target='_blank' rel='noopener noreferrer' href = '/ProductDetail/product_detail?productId={item.GetType().GetProperty("ProductId").GetValue(item, null)}' class='active' ui-toggle-class=''>
                                 <i class='fa fa-reply' aria-hidden='true'></i></a>
                            </button>"; // Chỗ này chưa code onclick='return window.open()'
                //System.Diagnostics.Debug.WriteLine((int)item.GetType().GetProperty("CommentStatus").GetValue(item, null));
                if ((int)item.GetType().GetProperty("CommentStatus").GetValue(item, null) == 1)
                {
                    output += @$"
                                <button type = 'button' data-toggle='tooltip' title='' class='btn btn-link btn-primary' data-original-title='Hiển thị bình luận'>
                                 <i class='fa-thumb-styling fa fa-eye' aria-hidden='true'></i>
                            </button>";
                    output += @$"
                                <button type = 'button' data-toggle='tooltip' title='' class='btn btn-link btn-danger' data-original-title='Ẩn bình luận' hidden>
                                 <i class='fa-thumb-styling fa fa-eye-slash' aria-hidden='true'></i>
                            </button>";
                }
                else
                {
                    output += @$"
                                <button type = 'button' data-toggle='tooltip' title='' class='btn btn-link btn-primary' data-original-title='Hiển thị bình luận' hidden>
                                 <i class='fa-thumb-styling fa fa-eye' aria-hidden='true'></i>
                            </button>";
                    output += @$"
                                <button type = 'button' data-toggle='tooltip' title='' class='btn btn-link btn-danger' data-original-title='Ẩn bình luận'>
                                 <i class='fa-thumb-styling fa fa-eye-slash' aria-hidden='true'></i>
                            </button>";
                }
                output += @"<button type='button' data-toggle='tooltip' title='' class='btn btn-link btn-danger' data-original-title='Xóa bình luận'>
                               <a href = 'javascript:void(0)' class= 'active' ui-toggle-class= ''>
                                    <i class= 'fa fa-times text-danger text'></i>
                                </a>
                            </button></div></td></tr>";
            }
            return output;
        }
        public void update_comment_status(int CommentId, int Status)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateCommentStatus(CommentId, Status);
        }

        public void reply_comment(int CommentId, int Reply)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.updateCommentReply(CommentId, Reply);
        }

        public void delete_comment(int CommentId)
        {
            SportswearShopContext context = HttpContext.RequestServices.GetService(typeof(SportswearShop_Ver2.Models.SportswearShopContext)) as SportswearShopContext;
            context.deleteComment(CommentId);
        }
    }
}
