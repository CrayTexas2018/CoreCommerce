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
    public class BoxRatingController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IBoxRatingRepository boxRatings;

        public BoxRatingController()
        {
            boxRatings = new BoxRatingRepository(context);
        }

        // GET: api/BoxItem/Box/5
        [SwaggerOperation("GetByBoxID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/BoxRating/Box/{box_id}")]
        public IEnumerable<BoxRating> GetBoxRatings(int box_id)
        {
            return boxRatings.GetBoxRatings(box_id);
        }

        // GET: api/BoxItem/5
        public BoxRating Get(int rating_id)
        {
            return boxRatings.GetBoxRating(rating_id);
        }

        // POST: api/Box
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public BoxRating Post([FromBody]BoxRating rating)
        {
            rating.created = DateTime.Now;
            rating.updated = DateTime.Now;

            return boxRatings.CreateBoxRating(rating);
        }

        // PUT: api/Box
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]BoxRating rating)
        {
            rating.updated = DateTime.Now;

            boxRatings.UpdateBoxRating(rating);
        }

        // DELETE: api/Box/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            boxRatings.DeleteBoxRating(id);
        }
    }
}
