using CoreCommerce.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoreCommerce.Controllers
{
    [BasicAuthentication]
    public class ItemController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IItemRepository items;

        public ItemController()
        {
            items = new ItemRepository(context);
        }

        // GET: api/CompanyUser/Company/{company_id}
        [SwaggerOperation("GetByBoxID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/Item/Company/{company_id}")]
        public IEnumerable<Item> GetCompanyItems(int company_id)
        {
            return items.GetCompanyItems(company_id);
        }

        // GET: api/Item/5
        public Item Get(int item_id)
        {
            return items.GetItem(item_id);
        }

        // POST: api/Company_User
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public Item Post([FromBody]Item item)
        {
            return items.CreateItem(item);
        }

        // PUT: api/Company_User
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]Item item)
        {
            items.UpdateItem(item);
        }

        // DELETE: api/Company_User/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            items.DeleteItem(id);
        }
    }
}
