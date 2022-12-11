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
        private const string connectionString = "server=localhost;port=3306;database=sportshop_ver2;uid=root;password=;Convert Zero Datetime=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(connectionString);
        }
        public DbSet<User> User { set; get; }
        public DbSet<Menu> Menu { set; get; }
        public DbSet<Category> Category { set; get; }

        public void updateLoginHistory(LoginHistory login)
		{
			//LoginHistory.Add(login);
			//SaveChanges();
		}
        public List<Category> getAllCategory()
        {
            var categories = Category.Where(c => c.Active == 1).ToList();
            return categories;
        }
        public List<Menu> getAllMenu()
        {
            var Menus = Menu.Where(c => c.Active==1).ToList();
            return Menus;
        }
    }
}
