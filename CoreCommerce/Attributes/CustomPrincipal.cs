using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CoreCommerce.Attributes
{
    interface ICustomPrincipal : IPrincipal
    {
        int Company_Id { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string company_name)
        {
            this.Identity = new GenericIdentity(company_name);
        }

        public int Company_Id { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}