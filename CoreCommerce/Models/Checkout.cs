using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class Checkout : CommonFields
    {
        [Key]
        public int checkout_id { get; set; }

        public int user_id { get; set; }

        public string url { get; set; }

        public bool is_completed { get; set; }

        public DateTime completed { get; set; }

        public bool deleted { get; set; }
        
        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface ICheckoutRepository
    {
        IEnumerable<Checkout> GetCheckouts();
        IEnumerable<Checkout> GetUserCheckouts(int user_id);
        Checkout GetCheckout(int checkout_id);
        Checkout CreateCheckout(int user_id);
        Checkout GetLastUserCheckout(int user_id);
        void DeleteCheckout(int checkout_id);
        void CompleteCheckout(int checkout_id);
        void UpdateCheckout(Checkout checkout);
        void Save();
    }

    public class CheckoutRepository : ICheckoutRepository
    {
        private ApplicationContext context;
        private CompanyRepository cr;

        public CheckoutRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public void CompleteCheckout(int checkout_id)
        {
            // Get the checkout
            Checkout checkout = GetCheckout(checkout_id);
            checkout.is_completed = true;
            checkout.completed = DateTime.Now;
            UpdateCheckout(checkout);
        }

        public Checkout CreateCheckout(int user_id)
        {
            Checkout checkout = new Checkout
            {
                user_id = user_id,
                created = DateTime.Now,
                updated = DateTime.Now
            };

            context.Checkouts.Add(checkout);
            Save();
            return checkout;
        }

        public void DeleteCheckout(int checkout_id)
        {
            // Get the checkout
            Checkout checkout = GetCheckout(checkout_id);
            context.Checkouts.Remove(checkout);
            Save();
        }

        public Checkout GetCheckout(int checkout_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Checkout> GetCheckouts()
        {
            throw new NotImplementedException();
        }

        public Checkout GetLastUserCheckout(int user_id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Checkouts.Where(x => x.user_id == user_id).Where(x => x.company_id == company_id).OrderBy(x => x.checkout_id).FirstOrDefault();
        }

        public IEnumerable<Checkout> GetUserCheckouts(int user_id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Checkouts.Where(x => x.user_id == user_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCheckout(Checkout checkout)
        {
            context.Entry(checkout).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}