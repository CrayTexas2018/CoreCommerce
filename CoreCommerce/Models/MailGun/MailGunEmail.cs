using System;
using System.Collections.Generic;

namespace CoreCommerce.Models.MailGun
{
    public class MailGunEmail
    {
        public List<String> to_emails { get; set; }

        public List<String> cc_emails { get; set; }

        public List<String> bcc_email { get; set; }

        public string from_name { get; set; }

        public string subject { get; set; }

        public string html_body { get; set; }
    }
}
