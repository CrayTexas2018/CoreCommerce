using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class UserLogin
    {
        [Key]
        public int login_id { get; set; }

        public int user_id { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        public User user { get; set; }

        public string ip_address { get; set; }

        public string url { get; set; }

        public DateTime login_date { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostUserLogin
    {
        public int user_id { get; set; }

        public string ip_address { get; set; }

        public string url { get; set; }
    }

    public interface IUserLoginRepository
    {
        IEnumerable<UserLogin> GetLogins();
        IEnumerable<UserLogin> GetUserLogins(int user_id);
        UserLogin GetUserLogin(int user_id);
        UserLogin CreateUserLogin(PostUserLogin login);
        void Save();
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private ApplicationContext context;

        public UserLoginRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public UserLogin CreateUserLogin(PostUserLogin postLogin)
        {
            UserLogin login = new UserLogin
            {
                active = true,
                ip_address = postLogin.ip_address,
                url = postLogin.url,
                user_id = postLogin.user_id
            };

            login.login_date = DateTime.Now;
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