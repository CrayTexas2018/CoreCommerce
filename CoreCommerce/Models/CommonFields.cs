using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using CoreCommerce.Attributes;
using System.Threading;
using System.Web;

namespace CoreCommerce.Models
{
    public class CommonFields
    {
        public int company_id { get; set; }

        [ForeignKey("company_id")]
        [JsonIgnore]
        public Company company { get; set; }

        public CommonFields()
        {
            CustomPrincipal p = (CustomPrincipal)HttpContext.Current.User;
            company_id = p.Company_Id;
        }
    }
}
