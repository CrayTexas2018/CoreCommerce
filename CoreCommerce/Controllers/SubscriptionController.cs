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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Subscription/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Subscription
        public Subscription Post([FromBody]PostSubscription postSubscription)
        {
            return subscriptions.CreateSubscription(postSubscription);
        }

        // PUT: api/Subscription/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Subscription/5
        public void Delete(int id)
        {
        }
    }
}
