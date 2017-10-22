using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ApiCompany
    {
        [Key]
        public int api_company_id { get; set; }

        public string company_name { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IApiCompanyRepository
    {
        ApiCompany GetApiCompany(string username);
        ApiCompany CreateApicompany(ApiCompany company);
        void Save();
    }

    public class ApiCompanyRepository : IApiCompanyRepository
    {
        private ApplicationContext context;

        public ApiCompanyRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public ApiCompany CreateApicompany(ApiCompany company)
        {
            context.ApiCompanies.Add(company);
            Save();

            return company;
        }

        public ApiCompany GetApiCompany(string username)
        {
            ApiCompany company = context.ApiUsers.Where(u => u.Username == username).Select(u => u.company).FirstOrDefault() ;
            return company;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}