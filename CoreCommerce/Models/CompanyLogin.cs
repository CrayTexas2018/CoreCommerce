using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class CompanyLogin : CommonFields
    {
        [Key]
        public int company_login_id { get; set; }

        public int company_user_id { get; set; }

        [ForeignKey("company_user_id")]
        [JsonIgnore]
        [Column("company_user_id")]
        public CompanyUser company_user { get; set; }

        public DateTime login_date { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostCompanyLogin
    {
        public int company_user_id { get; set; }
    }

    public interface ICompanyLoginRepository
    {
        IEnumerable<CompanyLogin> GetCompanyLogins(int company_id);
        IEnumerable<CompanyLogin> GetUserCompanyLogins(int user_id);
        CompanyLogin GetCompanyLogin(int login_id);
        CompanyLogin CreateCompanyLogin(PostCompanyLogin login);
        void Save();
    }

    public class CompanyLoginRepository : ICompanyLoginRepository
    {
        private ApplicationContext context;

        public CompanyLoginRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public CompanyLogin CreateCompanyLogin(PostCompanyLogin postLogin)
        {
            CompanyUserRepository cur = new CompanyUserRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            CompanyLogin login = new CompanyLogin()
            {
                active = true,
                company_id = cr.GetCompanyIdFromApiUser(),
                company_user_id = postLogin.company_user_id,
                login_date = DateTime.Now,
                created = DateTime.Now,
                updated = DateTime.Now
                
            };
            context.CompanyLogins.Add(login);
            Save();

            return login;
        }

        public CompanyLogin GetCompanyLogin(int login_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            CompanyLogin cl = context.CompanyLogins.Where(x => x.company_login_id == login_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (cl != null)
            {
                return cl;
            }
            throw new Exception("Company login with ID " + cl.company_login_id + " not found.");
        }

        public IEnumerable<CompanyLogin> GetCompanyLogins(int company_id)
        {
            // get current user company
            CompanyRepository cr = new CompanyRepository(context);
            if (company_id == cr.GetCompanyIdFromApiUser())
            {
                return context.CompanyLogins.Where(x => x.company_user.company.company_id == company_id).ToList();
            }
            throw new Exception("Company ID " + company_id + "is not valid.");
        }

        public IEnumerable<CompanyLogin> GetUserCompanyLogins(int user_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.CompanyLogins.Where(x => x.company_user.company_user_id == user_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}