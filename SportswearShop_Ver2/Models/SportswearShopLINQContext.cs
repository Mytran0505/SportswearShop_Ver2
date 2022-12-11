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
        public DbSet<Statistic> Statistic { set; get; }

        public void updateLoginHistory(LoginHistory login)
        {
                //LoginHistory.Add(login);
                //SaveChanges();
        }

        public List<Statistic> getStatistic(DateTime tu_ngay, DateTime den_ngay)
        {
                // Chuyển đổi như vầy để set Time = 00:00:00 để so sánh với statisticDate (kiểu Date, ko có Time) trong CSDL
            tu_ngay = DateTime.ParseExact(tu_ngay.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            den_ngay = DateTime.ParseExact(den_ngay.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            var statistic = Statistic.Where(s => s.StatisticDate >= tu_ngay && s.StatisticDate <= den_ngay).ToList();
            return statistic;
        }
        public List<Category> getAllCategory()
        {
            var categories = Category.Where(c => c.Active == 1).ToList();
            return categories;
        }
        public List<Menu> getAllMenu()
        {
            var Menus = Menu.Where(c => c.Active == 1).ToList();
            return Menus;
        }
    }
}
