using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Linq;
using System.Data.Entity.Migrations;

namespace CoreCommerce.Models
{
    public class RootObject
    {
        public List<Product> products { get; set; }
    }

    [Table("ShopifyProducts")]
    public class Product : CommonFields
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("product_id")]
        public long? Id { get; set; }

        public string title { get; set; }

        public string body_html { get; set; }

        public string vendor { get; set; }

        public string product_type { get; set; }

        public DateTime created_at { get; set; }

        public string handle { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime published_at { get; set; }

        public string template_suffix { get; set; }

        public string published_scope { get; set; }

        public string tags { get; set; }

        public List<Image> images { get; set; }

        public List<Variant> variants { get; set;}
    }

    public interface IShopifyProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int product_id);
    }

    public class ShopifyProductRepository : IShopifyProductRepository
    {
        private ApplicationContext context;

        private CompanyRepository cr;

        public ShopifyProductRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public Product GetProduct(int product_id)
        {
            return context.ShopifyProducts.Where(x => x.company_id == cr.GetCompanyIdFromApiUser()).Where(x => x.Id == product_id).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return context.ShopifyProducts.Where(x => x.company_id == cr.GetCompanyIdFromApiUser()).ToList();
        }
    }
}
