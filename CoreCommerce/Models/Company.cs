using CoreCommerce.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;

namespace CoreCommerce.Models
{
    public class Company
    {
        [Key]
        public int company_id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(255)]
        public string name { get; set; }

        public string website { get; set; }

        public string logo { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface ICompanyRepository
    {
        Company GetCompanyFromApiUser();
        Company CreateApicompany(Company company);
        Company GetCompanyByName(string company_name);
        Company GetCompany(int company_id);
        int GetCompanyIdFromApiUser();

        void Save();
    }

    public class CompanyRepository : ICompanyRepository
    {
        private ApplicationContext context;

        public CompanyRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Company CreateApicompany(Company company)
        {
            context.Companies.Add(company);
            Save();

            return company;
        }

        public Company GetCompany(int company_id)
        {
            return context.Companies.Find(company_id);
        }

        public Company GetCompanyByName(string company_name)
        {
            return context.Companies.Where(x => x.name == company_name).FirstOrDefault();
        }

        public Company GetCompanyFromApiUser()
        {
            // Get the user from the request
            string username = HttpContext.Current.User.Identity.Name;
            Company company = context.ApiUsers.Where(u => u.Username == username).Select(u => u.company).FirstOrDefault() ;
            return company;
        }

        public int GetCompanyIdFromApiUser()
        {
            // Get the user from the request
            string username = HttpContext.Current.User.Identity.Name;
            Company company = context.ApiUsers.Where(u => u.Username == username).Select(u => u.company).FirstOrDefault();
            return company.company_id;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}