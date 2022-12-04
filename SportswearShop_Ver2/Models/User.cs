using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace SportswearShop_Ver2.Models
{
    public class User
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string mobile;
        private string email;
        private string password;
        private int admin;
        private string userImage;
        private DateTime registeredAt;
        private DateTime lastLogin;

        public User()
        {

        }

        public User(string mobile, string email, string password, int admin, string userImage, DateTime registeredAt, DateTime lastLogin)
        {
            this.mobile = mobile;
            this.email = email;
            this.password = password;
            this.admin = admin;
            this.userImage = userImage;
            this.registeredAt = registeredAt;
            this.lastLogin = lastLogin;
        }


        public int UserId { get => userId; set => userId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Admin { get => admin; set => admin = value; }
        public string UserImage { get => userImage; set => userImage = value; }
        public DateTime RegisteredAt { get => registeredAt; set => registeredAt = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
    }
}
