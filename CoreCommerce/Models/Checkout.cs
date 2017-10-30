using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class Checkout
    {
        [Key]
        public int checkout_id { get; set; }

        public int user_id { get; set; }
        
        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }
}