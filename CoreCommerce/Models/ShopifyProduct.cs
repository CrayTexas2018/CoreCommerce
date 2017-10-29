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
    public class RootShopifyProduct
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

        public bool is_deleted { get; set; }

        public DateTime? database_deleted { get; set; }

        public DateTime database_created { get; set; }

        public DateTime database_updated { get; set; }
    }

    public interface IShopifyProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(long? product_id);
        void RefreshProducts();
        List<Product> GetShopifyProducts();
        void DeleteProduct(long? variant_id);
        void DeleteProducts();
        void PullProducts();
        void Save();
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

        public void DeleteProduct(long? product_id)
        {
            // Get the variant
            Product product = GetProduct(product_id);
            // Set flags to deleted
            product.database_deleted = DateTime.Now;
            product.database_updated = DateTime.Now;
            product.is_deleted = true;

            context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void DeleteProducts()
        {
            // Get all variants from database
            List<Product> database_products = GetProducts();
            // Get all variants from shopify
            List<Product> shopify_products = GetShopifyProducts();

            foreach (Product database_product in database_products)
            {
                bool delete = true;
                foreach (Product shopify_product in shopify_products)
                {
                    if (database_product.Id == shopify_product.Id)
                    {
                        delete = false;
                        break;
                    }
                }
                if (delete)
                {
                    // Delete the product
                    DeleteProduct(database_product.Id);
                }
            }
        }

        public List<Product> GetShopifyProducts()
        {
            ShopifyManager manager = new ShopifyManager(context);

            // Get list of all products in shopify account
            string json = manager.shopifyGetRequest("/admin/products.json");
            RootShopifyProduct all_products = JsonConvert.DeserializeObject<RootShopifyProduct>(json);

            return all_products.products;
        }

        public Product GetProduct(long? product_id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ShopifyProducts.Where(x => x.company_id == company_id).Where(x => x.Id == product_id).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ShopifyProducts.Where(x => x.company_id == company_id).ToList();
        }

        public void RefreshProducts()
        {
            PullProducts();
            DeleteProducts();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void PullProducts()
        {
            List<Product> shopify_products = GetShopifyProducts();
            // For every product, add or update
            foreach (Product product in shopify_products)
            {
                // Add or update existing product
                Product p = GetProduct(product.Id);

                if (p != null)
                {
                    // existing product, just update it
                    DateTime dbcreate = p.database_created;
                    context.Entry(p).CurrentValues.SetValues(product);
                    p.database_updated = DateTime.Now;
                    p.database_created = dbcreate;
                    p.database_deleted = null;
                    p.is_deleted = false;
                    context.Entry(p).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    // new product, add it to database
                    product.database_created = DateTime.Now;
                    product.database_updated = DateTime.Now;
                    context.ShopifyProducts.Add(product);
                }
                Save();
            }
        }
    }
}
