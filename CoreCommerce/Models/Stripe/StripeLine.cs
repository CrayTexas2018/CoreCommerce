using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class StripeLine
    {
        [JsonProperty("object")]
        public string stripe_object { get; set; }

        public List<Stripe_Line_Item> data { get; set; }

        public bool has_more { get; set; }

        public string url { get; set; }
    }
}