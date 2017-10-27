using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class ShopifyProduct
    {
        [Key]
        public int id { get; set; }

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

        public List<ShopifyVariant> variants { get; set;}
    }

    public class ShopifyVariant 
    {
        [Key]
        public int id { get; set; }

        public int product_id { get; set; }

        [ForeignKey("product_id")]
        [JsonIgnore]
        public ShopifyProduct shopify_product { get; set; }

        public string title { get; set; }

        // String is correct data type
        public string price { get; set; }

        public string sku { get; set; }

        public int position { get; set; }

        public string inventory_policy { get; set; }

        public string compare_at_price { get; set; }

        public string fulfillment_service { get; set; }

        public string inventory_management { get; set; }

        public string option1 { get; set; }

        public string option2 { get; set; }

        public string option3 { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public bool taxable { get; set; }

        public string barcode { get; set; }

        public int grams { get; set; }

        public int image_id { get; set; }

        public int inventory_quantity { get; set; }

        public int weight { get; set; }

        public int weight_unit { get; set; }

        public int old_inventory_quantity { get; set; }

        public bool requires_shipping { get; set; }
    }

    public class ShopifyImage
    {
        [Key]
        public int id { get; set; }

        public int product_id { get; set; }

        [ForeignKey("product_id")]
        [JsonIgnore]
        public ShopifyProduct shopify_product { get; set; }

        public int position { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string src { get; set; }

        public List<int> variant_ids { get; set; }
    }

    public class ShopifyProductManager
    {
        ApplicationContext context;

        public ShopifyProductManager(ApplicationContext context)
        {
            this.context = context;
        }

        public void updateProducts()
        {
            // Get all products from shopify
            StreamReader sr = new StreamReader("JsonFiles/shopifyproduct.json");
            string json = sr.ReadToEnd();
            dynamic dynJson = JsonConvert.DeserializeObject(json);
            foreach (ShopifyProduct product in dynJson)
            {
                if (context.ShopifyProducts.Find(product.id) != null)
                {
                    // Product already exists, just update
                    context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    // Product does not exist, add it
                    context.ShopifyProducts.Add(product);
                    context.SaveChanges();
                }
            }
        }
    }
}
