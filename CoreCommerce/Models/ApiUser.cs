using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ApiUser 
    {
        [Key]
        public int api_user_id { get; set; }

        public string Username { get; set; }

        public string password { get; set; }

        public ApiCompany company { get; set; }
    }

    public interface IApiUserRepository
    {
        IEnumerable<ApiUser> GetApiUsers();
        ApiUser GetApiUserById(int user_id);
        ApiUser CreateApiUser(ApiUser user);
        ApiUser AuthAndFindApiUser(string username, string password);
        void UpdateApiUser(ApiUser user);
        void DeleteApiUser(int user_id);
        void Save();
    }

    public class ApiUserRepository : IApiUserRepository
    {
        private ApplicationContext context;

        public ApiUserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public ApiUser AuthAndFindApiUser(string username, string password)
        {
            ApiUser user = context.ApiUsers.Where(u => u.Username == username).FirstOrDefault();
            if (BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                return user;
            }
            return null;
        }

        public ApiUser CreateApiUser(ApiUser user)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

            context.ApiUsers.Add(user);
            Save();

            return user;
        }

        public void DeleteApiUser(int user_id)
        {
            context.ApiUsers.Remove(context.ApiUsers.Find(user_id));
        }

        public ApiUser GetApiUserById(int user_id)
        {
            return context.ApiUsers.Find(user_id);
        }

        public IEnumerable<ApiUser> GetApiUsers()
        {
            return context.ApiUsers.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateApiUser(ApiUser user)
        {
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}