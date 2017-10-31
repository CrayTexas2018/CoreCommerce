using CoreCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoreCommerce.Controllers
{
    [BasicAuthentication]
    public class SubscriptionController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private ISubscriptionRepository subscriptions;

        public SubscriptionController()
        {
            subscriptions = new SubscriptionRepository(context);
        }

        // GET: api/Subscription
        public IEnumerable<Subscription> Get()
        {
            return subscriptions.GetSubscriptions();
        }

        // GET: api/Subscription/5
        public Subscription Get(int subscription_id)
        {
            return subscriptions.GetSubscription(subscription_id);
        }

        // POST: api/Subscription
        public Subscription Post([FromBody]PostSubscription postSubscription)
        {
            return subscriptions.CreateSubscription(postSubscription);
        }

        // PUT: api/Subscription/5
        public void Put(int id, [FromBody]Subscription subscription)
        {
            subscriptions.UpdateSubscription(subscription);
        }

        // DELETE: api/Subscription/5
        public void Delete(int subscription_id)
        {
            subscriptions.DeleteSubscription(subscription_id);
        }
    }
}
