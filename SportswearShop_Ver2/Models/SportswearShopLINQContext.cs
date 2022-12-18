using SportswearShop_Ver2.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using MySQL.Data.EntityFrameworkCore;


namespace SportswearShop_Ver2.Models
{
    public class SportswearShopLINQContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=sportshop_ver22;uid=root;password=;Convert Zero Datetime=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);
        }
        public DbSet<User> User { set; get; }

        public void updateLoginHistory(LoginHistory login)
        {
<<<<<<< HEAD
            //LoginHistory.Add(login);
            //SaveChanges();
        }

        /*public List<Category> getAllCategory()
        {
            var categories = Category.Where(c => c.Active == 1).ToList();
            return categories;
        }
        public List<Menu> getAllMenu()
        {
            var Menus = Menu.Where(c => c.Active == 1).ToList();
            return Menus;
        }

        public void saveMenu(Menu newMenu)
        {
            Menu.Add(newMenu);
            SaveChanges();
        }*/

        //<<<<<<< HEAD
=======
            LoginHistory.Add(login);
            SaveChanges();
        }
>>>>>>> a49b6610134879c5fd8726caed5a57ec92b277aa

        //public List<Category> getAllCategory()
        //{
        //    var categories = Category.Where(c => c.Active == 1).ToList();
        //    return categories;
        //}
        //public List<Menu> getAllMenu()
        //{
        //    var Menus = Menu.Where(c => c.Active == 1).ToList();
        //    return Menus;
        //}

        //public void saveMenu(Menu newMenu)
        //{
        //    Menu.Add(newMenu);
        //    SaveChanges();
        //}

        //public List<Category> getAllCategory()
        //{
        //    var categories = Category.Where(c => c.Active == 1).ToList();
        //    return categories;
        //}
        //public List<Menu> getAllMenu()
        //{
        //    var Menus = Menu.Where(c => c.Active == 1).ToList();
        //    return Menus;
        //}

        //public void saveMenu(Menu newMenu)
        //{
        //    Menu.Add(newMenu);
        //    SaveChanges();
        //}
<<<<<<< HEAD
        //=======
        //>>>>>>> ec17cd3de0626fb9be37f61897866ae8f15285ab
=======
>>>>>>> a49b6610134879c5fd8726caed5a57ec92b277aa
    }
}
