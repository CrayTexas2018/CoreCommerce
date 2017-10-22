using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class CompanyLogin
    {
        [Key]
        public int company_login_id { get; set; }

        public CompanyUser company_user { get; set; }

        public DateTime login_date { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface ICompanyLoginRepository
    {
        IEnumerable<CompanyLogin> GetCompanyLogins(int company_id);
        IEnumerable<CompanyLogin> GetUserCompanyLogins(int user_id);
        CompanyLogin GetCompanyLogin(int login_id);
        CompanyLogin CreateCompanyLogin(CompanyLogin login);
        void Save();
    }

    public class CompanyLoginRepository : ICompanyLoginRepository
    {
        private ApplicationContext context;

        public CompanyLoginRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public CompanyLogin CreateCompanyLogin(CompanyLogin login)
        {
            login.created = DateTime.Now;
            login.updated = DateTime.Now;

            context.CompanyLogins.Add(login);
            Save();

            return login;
        }

        public CompanyLogin GetCompanyLogin(int login_id)
        {
            return context.CompanyLogins.Find(login_id);
        }

        public IEnumerable<CompanyLogin> GetCompanyLogins(int company_id)
        {
            return context.CompanyLogins.Where(x => x.company_user.company.company_id == company_id).ToList();
        }

        public IEnumerable<CompanyLogin> GetUserCompanyLogins(int user_id)
        {
            return context.CompanyLogins.Where(x => x.company_user.company_user_id == user_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}