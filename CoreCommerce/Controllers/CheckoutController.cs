using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoreCommerce.Models;

namespace CoreCommerce.Controllers
{
    [BasicAuthentication]
    public class CheckoutController : ApiController
    {
        /*
         IEnumerable<Checkout> GetCheckouts();
        IEnumerable<Checkout> GetUserCheckouts(int user_id);
        Checkout GetCheckout(int checkout_id);
        Checkout CreateCheckout(PostCheckout postCheckout);
        Checkout GetLastUserCheckout(int user_id);
        void DeleteCheckout(int checkout_id);
        void CompleteCheckout(int checkout_id);
        void UpdateCheckout(Checkout checkout);
        */

        private ApplicationContext context = new ApplicationContext();
        private ICheckoutRepository checkouts;

        public CheckoutController()
        {
            checkouts = new CheckoutRepository(context);
        }

        // GET: /api/checkout
        public IEnumerable<Checkout> Get()
        {
            return checkouts.GetCheckouts();
        }

        [Route("api/checkout/user/{user_id}")]
        public IEnumerable<Checkout> GetUserCheckouts(int user_id)
        {
            return checkouts.GetUserCheckouts(user_id);
        }

        [Route("api/checkout/{checkout_id}")]
        public Checkout GetCheckout(int checkout_id)
        {
            return checkouts.GetCheckout(checkout_id);
        }

        public Checkout Post(PostCheckout postCheckout)
        {
            return checkouts.CreateCheckout(postCheckout);
        }

        [Route("api/checkout/user/{user_id}/last")]
        public Checkout GetLastUserCheckout(int user_id)
        {
            return checkouts.GetLastUserCheckout(user_id);
        }

        public void Delete(int checkout_id)
        {
            checkouts.DeleteCheckout(checkout_id);
        }

        [HttpPost]
        [Route("api/checkout/complete/{checkout_id}")]
        public void CompleteCheckout(int checkout_id)
        {
            checkouts.CompleteCheckout(checkout_id);
        }

        public void Put(Checkout checkout)
        {
            checkouts.UpdateCheckout(checkout);
        }
    }
}
