using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class Company
    {
        [Key]
        public int company_id { get; set; }

        public string name { get; set; }

        public string website { get; set; }

        public string logo { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IApiCompanyRepository
    {
        Company GetApiCompany(string username);
        Company CreateApicompany(Company company);
        void Save();
    }

    public class ApiCompanyRepository : IApiCompanyRepository
    {
        private ApplicationContext context;

        public ApiCompanyRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Company CreateApicompany(Company company)
        {
            context.ApiCompanies.Add(company);
            Save();

            return company;
        }

        public Company GetApiCompany(string username)
        {
            Company company = context.ApiUsers.Where(u => u.Username == username).Select(u => u.company).FirstOrDefault() ;
            return company;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}