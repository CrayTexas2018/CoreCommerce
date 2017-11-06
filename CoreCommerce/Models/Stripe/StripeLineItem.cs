using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class Stripe_Line_Item
    {
        public string id { get; set; }

        public string stripe_object { get; set; }

        public int amount { get; set; }

        public string currency { get; set; }

        public string description { get; set; }

        public bool discountable { get; set; }

        public bool livemode { get; set; }

        public StripePlanModel plan { get; set; }

        public bool proration { get; set; }

        public int quantity { get; set; }

        public string subscription { get; set; }

        public string subscription_item { get; set; }

        public string type { get; set; }
    }
}