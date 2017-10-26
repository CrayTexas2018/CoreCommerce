using CoreCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CoreCommerce
{
    public class ApiIdentity : IIdentity
    {
        public ApiUser User
        {
            get; private set;
        }
        public ApiIdentity(ApiUser User)
        {
            if (User == null) throw new ArgumentNullException("user");
            this.User = User;
        }

        public string Name
        {
            get
            {
                return this.User.Username;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Basic";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
    }
}