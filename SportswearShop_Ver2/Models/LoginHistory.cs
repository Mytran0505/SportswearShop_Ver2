using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportswearShop_Ver2.Models
{
    public class LoginHistory
    {
        private int loginId, userId;
        private DateTime loginDate, loginTime;

        public LoginHistory()
        {
        }

        public LoginHistory(int userId, DateTime loginDate, DateTime loginTime)
        {
            this.userId = userId;
            this.loginDate = loginDate;
            this.loginTime = loginTime;
        }

        [Key]
        public int LoginId { get => loginId; set => loginId = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateTime LoginDate { get => loginDate; set => loginDate = value; }
        public DateTime LoginTime { get => loginTime; set => loginTime = value; }
    }
}
