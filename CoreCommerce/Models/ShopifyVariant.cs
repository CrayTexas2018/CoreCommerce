using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    [Table("ShopifyVariants")]
    public class Variant : CommonFields
    {
        //public int shopify_variant_id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("variant_id")]
        [Index(IsUnique = true)]
        public long? Id { get; set; }

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
}