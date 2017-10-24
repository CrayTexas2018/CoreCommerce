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
    public class BoxCommentVoteController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IBoxCommentVoteRepository commentVotes;

        public BoxCommentVoteController()
        {
            commentVotes = new BoxCommentVoteRepository(context);
        }

        // GET: api/BoxCommentVote/Comment/5
        [SwaggerOperation("GetByID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/BoxCommentVote/Comment/{comment_id}")]
        public IEnumerable<BoxCommentVote> GetBoxCommentVotes(int comment_id)
        {
            return commentVotes.GetCommentVotes(comment_id);
        }

        // GET: api/BoxCommentVote/5
        public BoxCommentVote Get(int vote_id)
        {
            return commentVotes.GetVote(vote_id);
        }

        // POST: api/BoxCommentVote
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public BoxCommentVote Post([FromBody]PostBoxCommentVote postVote)
        {
            return commentVotes.CreateBoxCommentVote(postVote);
        }

        // PUT: api/BoxCommentVote
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]BoxCommentVote vote)
        {
            commentVotes.UpdateBoxCommentVote(vote);
        }

        // DELETE: api/BoxCommentVote/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            commentVotes.DeleteBoxCommentVote(id);
        }
    }
}
