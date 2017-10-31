using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoreCommerce.Controllers
{
    public class CheckoutController : ApiController
    {
        // GET: api/Checkout
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Checkout/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Checkout
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Checkout/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Checkout/5
        public void Delete(int id)
        {
        }
    }
}
