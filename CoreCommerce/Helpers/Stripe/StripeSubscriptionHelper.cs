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
        Company company;

        public StripeSubscriptionHelper(ApplicationContext context)
        {
            this.context = context;
            CompanyRepository cr = new CompanyRepository(context);
            company = cr.GetCompanyFromApiUser();
        }

        public StripeSubscription getSubscription(string subscription_id)
        {
            StripeSubscriptionService sss = new StripeSubscriptionService(company.stripe_key);
            return sss.Get(subscription_id);
        }

        public StripeSubscription createSubscription(string stripe_user_id, string stripe_plan_id = null, string stripe_coupon_id = null)
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
            StripeSubscription stripeSubscription = getSubscription(subscription_id);
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

                var subscriptionService = new StripeSubscriptionService(company.stripe_key);
                stripeSubscription = subscriptionService.Update(subscription_id, subscriptionOptions);
            }

            return stripeSubscription;
        }

        public void SubscriptionInvoicePaid(string event_id)
        {
            // Get the event
            StripeEventHelper seh = new StripeEventHelper(company.stripe_key);
            StripeEvent stripeEvent = seh.getStripeEvent(event_id);
            StripeInvoiceModel invoice = JsonConvert.DeserializeObject<StripeInvoiceModel>(stripeEvent.Data.Object.ToString());

            // Get the subscription from database
            SubscriptionRepository sr = new SubscriptionRepository(context);
            Subscription dbSubscription = sr.GetStripeSubscription(invoice.id);

            // Create an order
            OrderRepository or = new OrderRepository(context);
            PostOrder order = new PostOrder
            {
                address_1 = dbSubscription.address_1,
                address_2 = dbSubscription.address_2,
                city = dbSubscription.city,
                first_name = dbSubscription.first_name,
                last_name = dbSubscription.last_name,
                state = dbSubscription.state,
                zip = dbSubscription.zip,
                subsciption_id = dbSubscription.subscription_id,
                user_id = dbSubscription.user_id,
                billing_address_1 = dbSubscription.billing_address_1,
                billing_address_2 = dbSubscription.billing_address_2,
                billing_city = dbSubscription.billing_city,
                billing_state = dbSubscription.billing_state,
                billing_zip = dbSubscription.billing_zip,
                checkout_id = dbSubscription.checkout_id,
            };
            // Create order in db and send shopify info
            or.CreateOrder(order);

            // Get plan from invoice
            StripePlan plan = new StripePlan();
            foreach (Stripe_Line_Item line_item in invoice.lines.data)
            {
                if (line_item.stripe_object == "plan")
                {
                    StripePlanHelper sph = new StripePlanHelper(context);
                    plan = sph.getPlan(line_item.id);
                }
            }            

            //Update the subscription
            BoxRepository br = new BoxRepository(context);
            dbSubscription.box_id = br.GetBox(dbSubscription.next_box_id).box_id;
            dbSubscription.box = br.GetBox(dbSubscription.next_box_id);
            // The billing amount associated with the plan
            dbSubscription.next_charge_amount = (plan.Amount / 100);
            // Update in database
            sr.UpdateSubscription(dbSubscription);
        }
    }
}