using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;
using CoreCommerce.Models;
using CoreCommerce.Models.Stripe;
using Newtonsoft.Json;

namespace CoreCommerce.Helpers.Stripe
{
    public class StripeSubscriptionHelper
    {
        ApplicationContext context;

        public StripeSubscriptionHelper(ApplicationContext context)
        {
            this.context = context;
        }

        public StripeSubscription createSubscription(string stripe_user_id, string stripe_plan_id, string stripe_coupon_id)
        {
            var subscriptionOptions = new StripeSubscriptionCreateOptions()
            {
                PlanId = stripe_plan_id,
                CouponId = stripe_coupon_id,
            };

            var subscriptionService = new StripeSubscriptionService();
            StripeSubscription stripeSubscription = subscriptionService.Create(stripe_user_id, subscriptionOptions);

            return stripeSubscription;
        }

        public StripeSubscription setInitialTrialPeriod(string subscription_id)
        {
            var subscriptionService = new StripeSubscriptionService();
            StripeSubscription stripeSubscription = subscriptionService.Get(subscription_id);
            // set to first day of the month
            DateTime dt = DateTime.Now;
            if (dt.Day > 15)
            {
                // Set the next shipment to be the first of the next month
                stripeSubscription.TrialEnd = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);

                var subscriptionOptions = new StripeSubscriptionUpdateOptions()
                {
                    Prorate = false
                };

                stripeSubscription = subscriptionService.Update(subscription_id, subscriptionOptions);
            }

            return stripeSubscription;
        }

        public void SubscriptionInvoicePaid(string event_id)
        {
            // Get the event
            var eventService = new StripeEventService();
            StripeEvent stripeEvent = eventService.Get(event_id);
            StripeInvoiceModel invoice = JsonConvert.DeserializeObject<StripeInvoiceModel>(stripeEvent.Data.Object.ToString());

            // Get the subscription from database
            SubscriptionRepository sr = new SubscriptionRepository(context);
            Subscription dbSubscription = sr.GetStripeSubscription(invoice.id);
        }
    }
}