using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class UserLogin
    {
        [Key]
        public int login_id { get; set; }

        public User user { get; set; }

        public string ip_address { get; set; }

        public string url { get; set; }

        public DateTime login_date { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IUserLoginRepository
    {
        IEnumerable<UserLogin> GetLogins();
        IEnumerable<UserLogin> GetUserLogins(int user_id);
        UserLogin GetUserLogin(int user_id);
        UserLogin CreateUserLogin(UserLogin login);
        void Save();
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private ApplicationContext context;

        public UserLoginRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public UserLogin CreateUserLogin(UserLogin login)
        {
            login.created = DateTime.Now;
            login.updated = DateTime.Now;

            context.UserLogins.Add(login);
            Save();

            return login;
        }

        public IEnumerable<UserLogin> GetLogins()
        {
            return context.UserLogins.ToList();
        }

        public UserLogin GetUserLogin(int user_id)
        {
            return context.UserLogins.Find(user_id);
        }

        public IEnumerable<UserLogin> GetUserLogins(int user_id)
        {
            return context.UserLogins.Where(u => u.user.user_id == user_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}