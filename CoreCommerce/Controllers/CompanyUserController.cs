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
    public class CompanyUserController : ApiController
    {
        private ApplicationContext context = new ApplicationContext();
        private ICompanyUserRepository companyUsers;

        public CompanyUserController()
        {
            companyUsers = new CompanyUserRepository(context);
        }

        // GET: api/CompanyUser/Company/{company_id}
        [SwaggerOperation("GetByBoxID")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/CompanyUser/Company/{company_id}")]
        public IEnumerable<CompanyUser> GetCompanyUsers(int company_id)
        {
            return companyUsers.GetCompanyUsers();
        }

        // GET: api/Company_User/5
        public CompanyUser Get(int user_id)
        {
            return companyUsers.GetCompanyUser(user_id);
        }

        // POST: api/Company_User
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public CompanyUser Post([FromBody]PostCompanyUser user)
        {
            return companyUsers.CreateCompanyUser(user);
        }

        // PUT: api/Company_User
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put([FromBody]CompanyUser user)
        {
            companyUsers.UpdateCompanyUser(user);
        }

        // DELETE: api/Company_User/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            companyUsers.DeleteCompanyUser(id);
        }
    }
}
