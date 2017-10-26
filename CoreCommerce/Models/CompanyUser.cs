using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class CompanyUser : CommonFields
    {
        [Key]
        public int company_user_id { get; set; }

        [Column("company_id")]
        [JsonIgnore]
        public Company company { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(255)]
        public string email { get; set; }

        public string password { get; set; }

        public bool admin { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostCompanyUser
    {
        public string email { get; set; }

        public string password { get; set; }

        public bool is_admin { get; set; }
    }

    public interface ICompanyUserRepository
    {
        IEnumerable<CompanyUser> GetCompanyUsers(int company_id);
        CompanyUser GetCompanyUser(int user_id);
        CompanyUser CreateCompanyUser(PostCompanyUser user);
        void UpdateCompanyUser(CompanyUser user);
        void DeleteCompanyUser(int user_id);
        void Save();
    }

    public class CompanyUserRepository : ICompanyUserRepository
    {
        private ApplicationContext context;

        public CompanyUserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public CompanyUser CreateCompanyUser(PostCompanyUser postUser)
        {
            CompanyRepository cr = new CompanyRepository(context);

            CompanyUser user = new CompanyUser();
            user.active = true;
            user.admin = postUser.is_admin;
            user.email = postUser.email;
            user.password = BCrypt.Net.BCrypt.HashPassword(postUser.password);
            user.created = DateTime.Now;
            user.updated = DateTime.Now;
            user.company = cr.GetCompanyFromApiUser();

            context.CompanyUsers.Add(user);
            Save();

            return user;
        }

        public void DeleteCompanyUser(int user_id)
        {
            context.CompanyUsers.Remove(context.CompanyUsers.Find(user_id));
        }

        public CompanyUser GetCompanyUser(int user_id)
        {
            return context.CompanyUsers.Find(user_id);
        }

        public IEnumerable<CompanyUser> GetCompanyUsers(int company_id)
        {
            return context.CompanyUsers.Where(x => x.company.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCompanyUser(CompanyUser user)
        {
            user.updated = DateTime.Now;

            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}