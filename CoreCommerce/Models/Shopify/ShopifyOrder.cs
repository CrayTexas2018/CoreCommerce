using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Shopify
{
    public class Order
    {
        public string email { get; set; }

        public string fulfillment_status { get; set; }

        public bool send_receipt { get; set; }

        public List<Line_Items> line_items { get; set; }
    }

    public class Line_Items
    {
        public long variant_id { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }
    }
}