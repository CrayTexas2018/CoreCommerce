using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class Subscription
    {
        [Key]
        public int subscription_id { get; set; }

        [Required(ErrorMessage = "User is required")]
        public User user { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address_1 { get; set; }

        public string address_2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int zip { get; set; }

        [JsonIgnore]
        public Company company { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetSubscriptions();
        IEnumerable<Subscription> GetUserSubscriptions(int user_id);
        Subscription GetSubscription(int subscription_id);
        Subscription CreateSubscription(Subscription subscription);
        void UpdateSubscription(Subscription subscription);
        void DeleteSubscription(int subscription_id);
        void Save();
    }

    public class SubscriptionRepository : ISubscriptionRepository
    {
        ApplicationContext context;

        public SubscriptionRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Subscription CreateSubscription(Subscription subscription)
        {
            CompanyRepository cr = new CompanyRepository(context);
            subscription.created = DateTime.Now;
            subscription.updated = DateTime.Now;
            subscription.company = cr.GetCompanyFromApiUser();

            context.Subscriptions.Add(subscription);
            Save();

            return subscription;
        }

        public void DeleteSubscription(int subscription_id)
        {
            context.Subscriptions.Remove(context.Subscriptions.Find(subscription_id));
        }

        public Subscription GetSubscription(int subscription_id)
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