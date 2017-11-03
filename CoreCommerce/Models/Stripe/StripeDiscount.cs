using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class StripeDiscount
    {
        [JsonProperty("discount")]
        public string stripe_object { get; set; }

        public StripeCoupon coupon { get; set; }

        public string customer { get; set; }

        public long? end { get; set; }

        public long start { get; set; }

        public string subscription { get; set; }
    }
}