using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class StripePlanModel
    {
        public string id { get; set; }

        [JsonProperty("object")]
        public string stripe_object { get; set; }

        public int amount { get; set; }

        public long created { get; set; }

        public string currency { get; set; }

        public string interval { get; set; }

        public int interval_count { get; set; }

        public bool livemode { get; set; }

        public string name { get; set; }

        public string statement_descriptor { get; set; }
    }
}