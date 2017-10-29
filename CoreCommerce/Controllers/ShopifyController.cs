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
    public class ShopifyController : ApiController
    {
        ApplicationContext context = new ApplicationContext();

        ShopifyProductRepository spr;
        ShopifyVariantRepository svr;
        ShopifyImageRepository sir;

        public ShopifyController()
        {
            spr = new ShopifyProductRepository(context);
            svr = new ShopifyVariantRepository(context);
            sir = new ShopifyImageRepository(context);
        }

        // GET: api/Shopify
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Shopify/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Shopify
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Shopify/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Shopify/5
        public void Delete(int id)
        {
        }

        // GET: api/Shopify/Refresh
        [Route("api/shopify/refresh")]
        [HttpGet]
        public void Refresh()
        {
            // Refresh all products, variants, and images
            spr.RefreshProducts();
            svr.RefreshVariants();
            sir.RefreshImages();
        }
    }
}
