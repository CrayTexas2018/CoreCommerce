using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class CompanyUser
    {
        [Key]
        public int company_user_id { get; set; }

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

    public interface ICompanyUserRepository
    {
        IEnumerable<CompanyUser> GetCompanyUsers(int company_id);
        CompanyUser GetCompanyUser(int user_id);
        CompanyUser CreateCompanyUser(CompanyUser user);
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

        public CompanyUser CreateCompanyUser(CompanyUser user)
        {
            CompanyRepository cr = new CompanyRepository(context);

            user.created = DateTime.Now;
            user.updated = DateTime.Now;
            // Assign the company of the user to the company creating the request
            Company c = cr.GetCompanyFromApiUser(HttpContext.Current.User.Identity.Name);
            user.company = c;
            // Hash user password
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

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