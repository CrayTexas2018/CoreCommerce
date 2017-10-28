using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    [Table("ShopifyImages")]
    public class Image : CommonFields
    {

        //public int shopify_image_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column("image_id", Order = 0)]
        public long? Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column(Order = 1)]
        public long variant_id { get; set; }

        public int position { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string src { get; set; }

        public List<Int64> variant_ids { get; set; }
    }
}