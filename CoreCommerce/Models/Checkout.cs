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

        [ForeignKey("user_id")]
        [JsonIgnore]
        public User user { get; set; }

        public string url { get; set; }

        public bool is_completed { get; set; }

        public DateTime? completed { get; set; }

        public bool is_deleted { get; set; }

        public DateTime? deleted { get; set; }
        
        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostCheckout
    {
        public int user_id { get; set; }

        public string url { get; set; }
    }

    public interface ICheckoutRepository
    {
        IEnumerable<Checkout> GetCheckouts();
        IEnumerable<Checkout> GetUserCheckouts(int user_id);
        Checkout GetCheckout(int checkout_id);
        Checkout CreateCheckout(PostCheckout postCheckout);
        Checkout GetLastUserCheckout(int user_id);
        void DeleteCheckout(int checkout_id);
        void CompleteCheckout(int checkout_id);
        void UpdateCheckout(Checkout checkout);
        void Save();
    }

    public class CheckoutRepository : ICheckoutRepository
    {
        private ApplicationContext context;

        public CheckoutRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void CompleteCheckout(int checkout_id)
        {
            // Get the checkout
            Checkout checkout = GetCheckout(checkout_id);
            checkout.is_completed = true;
            checkout.completed = DateTime.Now;
            UpdateCheckout(checkout);
        }

        public Checkout CreateCheckout(PostCheckout postCheckout)
        {
            UserRepository ur = new UserRepository(context);
            CompanyRepository cr = new CompanyRepository(context);
            Checkout checkout = new Checkout
            {
                user_id = postCheckout.user_id,
                url = postCheckout.url,
                user = ur.GetUserById(postCheckout.user_id),
                company_id = cr.GetCompanyIdFromApiUser(),
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
            checkout.deleted = DateTime.Now;
            checkout.is_deleted = true;
            UpdateCheckout(checkout);
        }

        public Checkout GetCheckout(int checkout_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            Checkout checkout = context.Checkouts.Where(x => x.checkout_id == checkout_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (checkout != null)
            {
                return checkout;
            }
            throw new Exception("Checkout ID " + checkout.checkout_id + " not found.");
        }

        public IEnumerable<Checkout> GetCheckouts()
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Checkouts.Where(x => x.company_id == company_id).ToList();
        }

        public Checkout GetLastUserCheckout(int user_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Checkouts.Where(x => x.user_id == user_id).Where(x => x.company_id == company_id).OrderByDescending(x => x.checkout_id).FirstOrDefault();
        }

        public IEnumerable<Checkout> GetUserCheckouts(int user_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Checkouts.Where(x => x.user_id == user_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCheckout(Checkout checkout)
        {
            Checkout verify = GetCheckout(checkout.checkout_id);

            if (verify != null)
            {
                checkout.updated = DateTime.Now;
                context.Entry(checkout).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
        }
    }
}