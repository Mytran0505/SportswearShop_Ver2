using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    public class Blog
    {
        private int blogId;
        private string author;
        private DateTime dateCreate, datePost;
        private string title, summary, content;
        private int status;
        private string image;
        private int view;

        public Blog()
        {
        }

        public Blog(int blogId, string author, DateTime dateCreate, DateTime datePost, string title, string summary, string content, int status, string image, int view)
        {
            this.blogId = blogId;
            this.author = author;
            this.dateCreate = dateCreate;
            this.datePost = datePost;
            this.title = title;
            this.summary = summary;
            this.content = content;
            this.status = status;
            this.image = image;
            this.view = view;
            
        }

        public int BlogId { get => blogId; set => blogId = value; }
        public string Author { get => author; set => author = value; }
        public DateTime DateCreate { get => dateCreate; set => dateCreate = value; }
        public DateTime DatePost { get => datePost; set => datePost = value; }
        public string Title { get => title; set => title = value; }
        public string Summary { get => summary; set => summary = value; }
        public string Content { get => content; set => content = value; }
        public int Status { get => status; set => status = value; }
        public string Image { get => image; set => image = value; }
        public int View { get => view; set => view = value; }
    }
}
