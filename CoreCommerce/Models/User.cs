using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        [Index(IsUnique = true)]
        [MaxLength(255)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        public string password { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address_1 { get; set; }

        public string address_2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int zip { get; set; }

        public int provider_id { get; set; }

        public string initial_url { get; set; }

        public ApiCompany company { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User CreateUser(User user);
        void DeleteUser(int user_id);
        void UpdateUser(User user);
        bool Authenticate(string email, string password);
        void Save();
    }

    public class UserRepository : IUserRepository
    {
        private ApplicationContext context;

        public UserRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public bool Authenticate(string email, string password)
        {
            User user = GetUserByEmail(email);
            return BCrypt.Net.BCrypt.Verify(password, user.password);
        }

        public User CreateUser(User user)
        {
            user.created = DateTime.Now;
            user.updated = DateTime.Now;

            ApiCompanyRepository acr = new ApiCompanyRepository(context);
            user.company = acr.GetApiCompany(HttpContext.Current.User.Identity.Name);
            

            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            context.Users.Add(user);
            Save();
            return user;
        }

        public void DeleteUser(int user_id)
        {
            context.Users.Remove(context.Users.Find(user_id));
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.Where(u => u.email == email).FirstOrDefault();
        }

        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            user.updated = DateTime.Now;

            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}