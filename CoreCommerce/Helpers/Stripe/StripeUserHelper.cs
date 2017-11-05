using CoreCommerce.Models;
using CoreCommerce.Models.Stripe;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Helpers.Stripe
{
    public class StripeUserHelper
    {
        public void createStripeUser(User user, CreditCard card)
        {
            var customerService = new StripeCustomerService();

            StripeCustomerCreateOptions options = new StripeCustomerCreateOptions
            {
                Email = user.email,


                SourceCard = new SourceCard
                {
                    AddressLine1 = user.billing_address_1,
                    AddressLine2 = user.billing_address_2,
                    AddressCity = user.billing_city,
                    AddressState = user.billing_state,
                    AddressZip = user.billing_zip,
                    Name = card.name,
                    Number = card.number,
                    Cvc = card.cvc,
                    ExpirationMonth = card.expMonth,
                    ExpirationYear = card.expYear
                }
            };

            StripeCustomer customer = customerService.Create(options);
        }
    }
}