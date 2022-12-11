using SportswearShop_Ver2.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SportswearShop_Ver2.Models
{
	public class SportswearShopLINQContext
	{
		public DbSet<User> User { set; get; }
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
    }
}
