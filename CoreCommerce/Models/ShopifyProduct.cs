using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Text.RegularExpressions;

namespace CoreCommerce.Models
{
    public class RootObject
    {
        public Product product { get; set; }
    }

    [Table("ShopifyProducts")]
    public class Product : CommonFields
    {
        [Key]
        public int shopify_product_id { get; set; }

        [Column("product_id")]
        [Index(IsUnique = true)]
        public int Id { get; set; }

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

    [Table("ShopifyVariants")]
    public class Variant : CommonFields
    {
        [Key]
        public int shopify_variant_id { get; set; }

        [Column("variant_id")]
        [Index(IsUnique = true)]
        public int Id { get; set; }

        public int shopify_product_id { get; set; }

        [ForeignKey("shopify_product_id")]
        [JsonIgnore]
        public Product shopify_product { get; set; }

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

        public int inventory_quantity { get; set; }

        public string weight { get; set; }

        public string weight_unit { get; set; }

        public int old_inventory_quantity { get; set; }

        public bool requires_shipping { get; set; }
    }

    [Table("ShopifyImages")]
    public class Image : CommonFields
    {
        [Key]
        public int shopify_image_id { get; set; }

        [Column("image_id")]
        [Index(IsUnique = true)]
        public int Id { get; set; }

        public int position { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string src { get; set; }

        public List<int> variant_ids { get; set; }
    }

    public class Variant_Ids
    {
        public int variant_id { get; set; }

        [ForeignKey("variant_id")]
        [JsonIgnore]
        public Variant variant { get; set; }
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
            StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/JsonFiles/shopifyproduct.json"));
            string json = sr.ReadToEnd();
            json = json.Replace(@"\", "");
            RootObject p = JsonConvert.DeserializeObject<RootObject>(json);

                if (context.ShopifyProducts.Find(p.product.shopify_product_id) != null)
                {
                    // Product already exists, just update
                    context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    // Product does not exist, add it
                    context.ShopifyProducts.Add(p.product);
                    context.SaveChanges();
                }
            
        }
    }
}
