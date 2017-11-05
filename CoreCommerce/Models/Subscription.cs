using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Stripe;
using CoreCommerce.Helpers.Stripe;

namespace CoreCommerce.Models
{
    public class Subscription : CommonFields
    {
        [Key]
        public int subscription_id { get; set; }

        public string stripe_subscription_id { get; set; }

        public int user_id { get; set; }

        public string stripe_plan_id { get; set; }

        public string stripe_coupon_id { get; set; }

        [Required(ErrorMessage = "User is required")]
        [ForeignKey("user_id")]
        [Column("user_id")]
        [JsonIgnore]
        public User user { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address_1 { get; set; }

        public string address_2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int zip { get; set; }

        public int box_id { get; set; }

        [ForeignKey("box_id")]
        [JsonIgnore]
        public Box box { get; set; }

        public string box_name { get; set; }

        public long shopify_product_id { get; set; }

        [ForeignKey("shopify_product_id")]
        [JsonIgnore]
        public Product shopify_product { get; set; }

        public string shopify_product_title { get; set; }

        public string shopify_product_body { get; set; }

        public long shopify_variant_id { get; set; }

        [ForeignKey("shopify_variant_id")]
        [JsonIgnore]
        public Variant shopify_variant { get; set; }

        public decimal shopify_variant_price { get; set; }

        public string shopify_variant_sku { get; set; }

        public decimal next_charge_amount { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }   
    }

    public class PostSubscription
    {
        public int user_id { get; set; }

        public string stripe_plan_id { get; set; }

        public string stripe_coupon_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address_1 { get; set; }

        public string address_2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int zip { get; set; }

        public int box_id { get; set; }

        public string box_name { get; set; }

        public long shopify_product_id { get; set; }

        public string shopify_product_title { get; set; }

        public string shopify_product_body { get; set; }

        public long shopify_variant_id { get; set; }

        public decimal shopify_variant_price { get; set; }

        public string shopify_variant_sku { get; set; }

        public decimal next_charge_amount { get; set; }

        public int next_box_id { get; set; }
    }

    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetSubscriptions();
        IEnumerable<Subscription> GetUserSubscriptions(int user_id);
        Subscription GetSubscription(int? subscription_id);
        Subscription CreateSubscription(PostSubscription subscription);
        Subscription GetStripeSubscription(string subscription_id);
        void UpdateSubscription(Subscription subscription);
        void DeleteSubscription(int subscription_id);
        void Save();
    }

    public class SubscriptionRepository : ISubscriptionRepository
    {
        ApplicationContext context;
        CompanyRepository cr;

        public SubscriptionRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public Subscription CreateSubscription(PostSubscription postSubscription)
        {
            UserRepository ur = new UserRepository(context);
            ShopifyProductRepository spr = new ShopifyProductRepository(context);
            ShopifyVariantRepository svr = new ShopifyVariantRepository(context);
            BoxRepository br = new BoxRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            User user = ur.GetUserById(postSubscription.user_id);

            try
            {
                // Create the subscription in stripe
                StripeSubscriptionHelper ssh = new StripeSubscriptionHelper(context);
                StripeSubscription stripeSubscription = ssh.createSubscription(user.stripe_id, postSubscription.stripe_plan_id, postSubscription.stripe_coupon_id);

                Subscription subscription = new Subscription
                {
                    active = true,
                    address_1 = postSubscription.address_1,
                    address_2 = postSubscription.address_2,
                    city = postSubscription.city,
                    first_name = postSubscription.first_name,
                    last_name = postSubscription.last_name,
                    state = postSubscription.state,
                    user_id = postSubscription.user_id,
                    zip = postSubscription.zip,
                    box_id = postSubscription.box_id,
                    box_name = postSubscription.box_name,
                    next_charge_amount = postSubscription.next_charge_amount,
                    shopify_product_body = postSubscription.shopify_product_body,
                    shopify_product_id = postSubscription.shopify_product_id,
                    shopify_product_title = postSubscription.shopify_product_title,
                    shopify_variant_id = postSubscription.shopify_variant_id,
                    shopify_variant_price = postSubscription.shopify_variant_price,
                    shopify_variant_sku = postSubscription.shopify_variant_sku,
                    stripe_plan_id = postSubscription.stripe_plan_id,
                    created = DateTime.Now,
                    updated = DateTime.Now,
                    shopify_product = spr.GetProduct(postSubscription.shopify_product_id),
                    shopify_variant = svr.GetVariant(postSubscription.shopify_variant_id),
                    user = user,
                    box = br.GetBox(postSubscription.box_id),
                    company = cr.GetCompanyFromApiUser(),
                    stripe_subscription_id = stripeSubscription.Id,
                    stripe_coupon_id = stripeSubscription.StripeDiscount.Id
                };

                context.Subscriptions.Add(subscription);
                Save();

                return subscription;

            } catch (StripeException e)
            {
                throw e;
                // handle error
            }
        }

        public void DeleteSubscription(int subscription_id)
        {
            context.Subscriptions.Remove(context.Subscriptions.Find(subscription_id));
        }

        public Subscription GetStripeSubscription(string stripe_subscription_id)
        {
            return context.Subscriptions.Where(x => x.stripe_subscription_id == stripe_subscription_id).FirstOrDefault();
        }

        public Subscription GetSubscription(int? subscription_id)
        {
            return context.Subscriptions.Find(subscription_id);
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return context.Subscriptions.ToList();
        }

        public IEnumerable<Subscription> GetUserSubscriptions(int user_id)
        {
            return context.Subscriptions.Where(x => x.user.user_id == user_id); 
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateSubscription(Subscription subscription)
        {
            subscription.updated = DateTime.Now;

            context.Entry(subscription).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}