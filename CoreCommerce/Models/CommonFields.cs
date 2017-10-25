using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class CommonFields
    {
        public ApplicationContext context;

        public CommonFields()
        {
            ApplicationContext context = this.context;
            CompanyRepository cr = new CompanyRepository(context);
            company = cr.GetCompanyFromApiUser();
        }

        public int company_id { get; set; }

        [ForeignKey("company_id")]
        [JsonIgnore]
        public Company company { get; set; }
    }
}
