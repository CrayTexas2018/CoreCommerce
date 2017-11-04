using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models.Stripe
{
    public class StripeInvoice
    {
        public string id { get; set; }

        [JsonProperty("object")]
        public string stripe_object { get; set; }

        public int? amount_due { get; set; }

        public int? application_fee { get; set; }

        public int? attempt_count { get; set; }

        public bool attempted { get; set; }

        public string billing { get; set; }

        public string charge { get; set; }

        public bool closed { get; set; }

        public string currency { get; set; }

        public string customer { get; set; }

        public long? date { get; set; }

        public string description { get; set; }

        public StripeDiscount discount { get; set; }

        public int? ending_balance {get;set;}

        public bool forgiven { get; set; }

        public StripeInvoiceData lines { get; set; }

        public bool livemode { get; set; }

        public long? next_payment_attempt { get; set; }

        public bool paid { get; set; }

        public long period_end { get; set; }

        public long period_start { get; set; }

        public string receipt_number { get; set; }

        public int? starting_balance { get; set; }

        public string statement_descriptor { get; set; }

        public string subscription { get; set; }

        public int subscription_proration_date { get; set; }

        public int? subtotal { get; set; }

        public int? tax { get; set; }

        public decimal? tax_percent { get; set; }

        public int? total { get; set; }

        public long? webhooks_delivered_at { get; set; }
    }

    public class StripeInvoiceData
    {
        public List<Stripe_Line_Item> data { get; set; }
    }
}