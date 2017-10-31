using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Http;
using System.Net;
using System.Web.Http.ModelBinding;

namespace CoreCommerce.Models
{
    public class User : CommonFields
    {
        [Key]
        public int user_id { get; set; }

        public string stripe_id { get; set; }

        public long shopify_id { get; set; }

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

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostUser
    {
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

        [Required(ErrorMessage = "Zip is required")]
        public int zip { get; set; }
    }

    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User CreateUser(PostUser user);
        void DeleteUser(int user_id);
        void UpdateUser(User user);
        bool Authenticate(string email, string password);
        void Save();
    }

    public class UserRepository : IUserRepository
    {
        private ApplicationContext context;
        CompanyRepository cr;
        //int company_id;

        public UserRepository (ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
            //company_id = cr.GetCompanyIdFromApiUser();
        }

        public bool Authenticate(string email, string password)
        {
            User user = GetUserByEmail(email);
            return BCrypt.Net.BCrypt.Verify(password, user.password);
        }

        public User CreateUser(PostUser postUser)
        {
            User user = new User
            {
                active = true,
                address_1 = postUser.address_1,
                address_2 = postUser.address_2,
                city = postUser.city,
                email = postUser.email,
                first_name = postUser.first_name,
                last_name = postUser.last_name,
                password = postUser.last_name,
                state = postUser.state,
                zip = postUser.zip
            };

            user.created = DateTime.Now;
            user.updated = DateTime.Now;
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
            int company_id = cr.GetCompanyIdFromApiUser();
            User user = context.Users.Where(u => u.email == email).Where(x => x.company_id == company_id).FirstOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        public User GetUserById(int id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            User user = context.Users.Where(u => u.user_id == id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
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