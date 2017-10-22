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

    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompany(int company_id);
        Company CreateCompany(Company company);
        void UpdateCompany(CompanyLogin company);
        void DeleteCompany(int company_id);
        void Save();
    }

    public class CompanyRepository : ICompanyRepository
    {
        private ApplicationContext context;

        public CompanyRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Company CreateCompany(Company company)
        {
            company.created = DateTime.Now;
            company.updated = DateTime.Now;

            context.Companies.Add(company);
            Save();

            return company;
        }

        public void DeleteCompany(int company_id)
        {
            context.Companies.Remove(context.Companies.Find(company_id));
        }

        public IEnumerable<Company> GetCompanies()
        {
            return context.Companies.ToList();
        }

        public Company GetCompany(int company_id)
        {
            return context.Companies.Find(company_id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCompany(CompanyLogin company)
        {
            company.updated = DateTime.Now;

            context.Entry(company).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}