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
		public void updateLoginHistory(LoginHistory login)
		{
			//LoginHistory.Add(login);
			//SaveChanges();
		}
	}
}
