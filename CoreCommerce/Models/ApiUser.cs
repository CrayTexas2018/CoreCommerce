using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class ApiUser 
    {
        [Key]
        public int api_user_id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(255)]
        public string Username { get; set; }

        public string password { get; set; }

        [JsonIgnore]
        [Column("company_id")]
        public Company company { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
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

            user.created = DateTime.Now;
            user.updated = DateTime.Now;

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
            user.updated = DateTime.Now;

            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}