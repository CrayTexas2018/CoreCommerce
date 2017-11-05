using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class CreditCard
    {
        public string name { get; set; }
        public string number { get; set; }
        public string cvc { get; set; }
        public int expMonth { get; set; }
        public int expYear { get; set; }
    }
}