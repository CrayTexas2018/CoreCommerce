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
    public class BoxController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IBoxRepository boxes;

        public BoxController()
        {
            boxes = new BoxRepository(context);
        }

        // GET: api/Box
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IEnumerable<Box> Get()
        {
            return boxes.GetBoxes();
        }

        // GET: api/Box/5
        [SwaggerOperation("GetByID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public Box Get(int id)
        {
            return boxes.GetBox(id);
        }

        // POST: api/Box
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public Box Post([FromBody]Box box)
        {
            return boxes.CreateBox(box);
        }

        // PUT: api/Box
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]Box box)
        {
            box.updated = DateTime.Now;

            boxes.UpdateBox(box);
        }

        // DELETE: api/Box/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            boxes.DeleteBox(id);
        }
    }
}
