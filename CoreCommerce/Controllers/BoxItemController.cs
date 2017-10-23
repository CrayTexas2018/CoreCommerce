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
    public class BoxItemController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IBoxItemRepository boxItems;

        public BoxItemController()
        {
            boxItems = new BoxItemRepository(context);
        }

        // GET: api/BoxItem/Box/5
        [SwaggerOperation("GetByBoxID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/BoxItem/Box/{box_id}")]
        public IEnumerable<BoxItem> GetBoxItems(int box_id)
        {
            return boxItems.GetBoxItems(box_id);
        }

        // GET: api/BoxItem/5
        public BoxItem Get(int item_id)
        {
            return boxItems.GetBoxItem(item_id);
        }

        // POST: api/Box
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public BoxItem Post([FromBody]BoxItem item)
        {
            return boxItems.CreateBoxItem(item);
        }

        // PUT: api/Box
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]BoxItem item)
        {
            item.updated = DateTime.Now;

            boxItems.UpdateBoxItem(item);
        }

        // DELETE: api/Box/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            boxItems.DeleteBoxItem(id);
        }
    }
}
