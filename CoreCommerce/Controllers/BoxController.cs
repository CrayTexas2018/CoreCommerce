using CoreCommerce.Models;
using CoreCommerce.Models.Stripe;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
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
            // Get json file
            //using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/JsonFiles/StripeInvoice.json")))
            //{
            //    string json = r.ReadToEnd();
            //    StripeInvoice invoice = JsonConvert.DeserializeObject<StripeInvoice>(json);
            //}

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
        public Box Post([FromBody]PostBox box)
        {
            return boxes.CreateBox(box);
        }

        // PUT: api/Box
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]Box box)
        {
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
