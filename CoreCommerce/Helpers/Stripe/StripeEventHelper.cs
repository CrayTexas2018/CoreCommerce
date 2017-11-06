using System;
using Stripe;
namespace CoreCommerce.Helpers.Stripe
{
    public class StripeEventHelper
    {
        public StripeEventHelper(string stripe_api_key)
        {
            StripeConfiguration.SetApiKey(stripe_api_key);
        }

        public StripeEvent getStripeEvent(string event_id)
        {
            StripeEventService ses = new StripeEventService();
            return ses.Get(event_id);
        }
    }
}
