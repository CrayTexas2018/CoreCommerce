using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class StripeCoupon
    {
        public string id { get; set; }

        [JsonProperty("object")]
        public string stripe_object { get; set; }

        public int amount_off { get; set; }

        public long created { get; set; }

        public string currency { get; set; }

        public string duration { get; set; }

        public int duration_in_months { get; set; }

        public bool livemode { get; set; }

        public int max_redemptions { get; set; }

        public int percent_off { get; set; }

        public long? redeem_by { get; set; }

        public int times_redeemed { get; set; }

        public bool valid { get; set; }
    }
}