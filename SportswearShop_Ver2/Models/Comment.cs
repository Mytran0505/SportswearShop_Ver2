using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITGoShop_F_Ver2.Controllers
{
    public class Comment
    {
        private int commentId;
        private string commentContent;
        private int userId, productId, commentStatus, reply;
        private int? parentComment;
        private DateTime createdAt, updatedAt;

        public Comment()
        {
        }

        public Comment(int commentId, string commentContent, int userId, int productId, int commentStatus, int reply, int parentComment, DateTime createdAt, DateTime updatedAt)
        {
            this.commentId = commentId;
            this.commentContent = commentContent;
            this.userId = userId;
            this.productId = productId;
            this.commentStatus = commentStatus;
            this.reply = reply;
            this.parentComment = parentComment;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }

        public int CommentId { get => commentId; set => commentId = value; }
        public string CommentContent { get => commentContent; set => commentContent = value; }
        public int UserId { get => userId; set => userId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int CommentStatus { get => commentStatus; set => commentStatus = value; }
        public int Reply { get => reply; set => reply = value; }
        public int? ParentComment { get => parentComment; set => parentComment = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
    }
}
