using CoreCommerce.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CoreCommerce.Controllers
{
    [BasicAuthentication]
    public class UsersController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private IUserRepository users;

        public UsersController()
        {
            users = new UserRepository(context);
        }

        // GET: api/Users
        [SwaggerOperation("GetAll")]
        public IEnumerable<User> Get()
        {
            return users.GetUsers();
        }

        // GET: api/Users/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            User user = users.GetUserById(id);
            if (user != null)
            {
                return Request.CreateResponse<User>(HttpStatusCode.OK, user);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET: api/Users/Email/email@email.com
        [Route("api/users/Email/{email}")]
        [SwaggerOperation("GetByEmail")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetUserByEmail(string email)
        {
            User user = users.GetUserByEmail(email);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST: api/Users
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromBody]PostUser postUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);               
            }
            User newUser = users.CreateUser(postUser);
            return CreatedAtRoute("DefaultApi", new { id = newUser.user_id }, newUser);
        }

        // PUT: api/Users/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put([FromBody]User user)
        {
            try
            {
                users.UpdateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            } catch (DbEntityValidationException e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.EntityValidationErrors);
            }
            
        }

        // DELETE: api/Users/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            users.DeleteUser(id);
        }
    }
}
