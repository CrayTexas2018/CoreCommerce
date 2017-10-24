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
    public class BoxCommentController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IBoxCommentRepository boxComments;

        public BoxCommentController()
        {
            boxComments = new BoxCommentRepository(context);
        }

        // GET: api/BoxComment/5
        [SwaggerOperation("GetByID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/BoxComment/Box/{box_id}")]
        public IEnumerable<BoxComment> GetBoxComments(int box_id)
        {
            return boxComments.GetBoxComments(box_id);
        }

        public BoxComment Get(int comment_id)
        {
            return boxComments.GetBoxCommentById(comment_id);
        }

        // POST: api/Box
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public BoxComment Post([FromBody]PostBoxComment comment)
        {
            return boxComments.CreateBoxComment(comment);
        }

        // PUT: api/Box
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]BoxComment comment)
        {
            boxComments.UpdateBoxComment(comment);
        }

        // DELETE: api/Box/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            boxComments.DeleteBoxComment(id);
        }
    }
}
