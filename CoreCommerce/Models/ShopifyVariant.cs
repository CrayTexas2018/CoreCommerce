using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class RootShopifyVariant
    {
        public List<Variant> variants { get; set; }
    }

    [Table("ShopifyVariants")]
    public class Variant : CommonFields
    {
        //public int shopify_variant_id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("variant_id")]
        [Index(IsUnique = true)]
        public long? Id { get; set; }

        public long? product_id { get; set; }

        [JsonIgnore]
        [ForeignKey("product_id")]
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

        public bool is_deleted { get; set; }

        public DateTime? database_deleted { get; set; }

        public DateTime database_created { get; set; }

        public DateTime database_updated { get; set; }
    }

    public interface IShopifyVariantRepository
    {
        List<Variant> GetVariants();
        List<Variant> GetShopifyVariants();
        Variant GetVariant(long? product_id);
        void RefreshVariants();
        void DeleteVariant(long? variant_id);
        void DeleteVariants();
        void PullVariants();
        void Save();
    }

    public class ShopifyVariantRepository : IShopifyVariantRepository
    {
        private ApplicationContext context;

        private CompanyRepository cr;

        public ShopifyVariantRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public void DeleteVariant(long? variant_id)
        {
            // Get the variant
            Variant variant = GetVariant(variant_id);
            // Set flags to deleted
            variant.database_deleted = DateTime.Now;
            variant.database_updated = DateTime.Now;
            variant.is_deleted = true;

            context.Entry(variant).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void DeleteVariants()
        {
            // Get all variants from database
            List<Variant> database_variants = GetVariants();
            // Get all variants from shopify
            List<Variant> shopify_variants = GetShopifyVariants();

            foreach (Variant database_variant in database_variants)
            {
                bool delete = true;
                foreach (Variant shopify_variant in shopify_variants)
                {
                    if (database_variant.Id == shopify_variant.Id)
                    {
                        delete = false;
                        break;
                    }
                }
                if (delete)
                {
                    // Delete the product
                    DeleteVariant(database_variant.Id);
                }
            }
        }

        public List<Variant> GetShopifyVariants()
        {
            ShopifyManager manager = new ShopifyManager(context);

            // Get list of all products in shopify account
            string json = manager.shopifyGetRequest("/admin/variants.json");
            RootShopifyVariant all_variants = JsonConvert.DeserializeObject<RootShopifyVariant>(json);

            return all_variants.variants;
        }

        public Variant GetVariant(long? variant_id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ShopifyVariants.Where(x => x.company_id == company_id).Where(x => x.Id == variant_id).FirstOrDefault();
        }

        public List<Variant> GetVariants()
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ShopifyVariants.Where(x => x.company_id == company_id).ToList();
        }

        public void PullVariants()
        {
            // Get shopify variants
            List<Variant> shopify_variants = GetShopifyVariants();
            // For every variant, add or update
            foreach (Variant variant in shopify_variants)
            {
                // Add or update existing variant
                Variant v = GetVariant(variant.Id);

                if (v != null)
                {
                    // existing variant, just update it
                    DateTime dbcreate = v.database_created;
                    context.Entry(v).CurrentValues.SetValues(variant);
                    v.database_updated = DateTime.Now;
                    v.database_created = dbcreate;
                    v.database_deleted = null;
                    v.is_deleted = false;
                    context.Entry(v).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    // new variant, add it to database
                    variant.database_created = DateTime.Now;
                    variant.database_updated = DateTime.Now;
                    context.ShopifyVariants.Add(variant);
                }
                Save();
            }
        }

        public void RefreshVariants()
        {
            PullVariants();
            DeleteVariants();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}