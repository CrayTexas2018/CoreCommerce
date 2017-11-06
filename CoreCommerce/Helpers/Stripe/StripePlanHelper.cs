using System;
using CoreCommerce.Models;
using Stripe;

namespace CoreCommerce.Helpers.Stripe
{
    public class StripePlanHelper
    {
        public Company company;
        ApplicationContext context;

        public StripePlanHelper(ApplicationContext context)
        {
            this.context = context;
            CompanyRepository cr = new CompanyRepository(context);
            company = cr.GetCompanyFromApiUser();
        }

        public StripePlan getPlan(string plan_id)
        {
            StripePlanService sps = new StripePlanService(company.stripe_key);

            return sps.Get(plan_id);
        }
    }
}
