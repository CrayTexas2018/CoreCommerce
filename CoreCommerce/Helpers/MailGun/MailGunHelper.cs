using System;
using CoreCommerce.Models.MailGun;

namespace CoreCommerce.Helpers.MailGun
{
    public class MailGunHelper
    {
        public MailGunHelper()
        {
        }

        public void sendEmail(MailGunEmail emailModel)
        {
            //RestClient client = new RestClient();
            //client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            //client.Authenticator =
            //    new HttpBasicAuthenticator("api",
            //                                "YOUR_API_KEY");
            //RestRequest request = new RestRequest();
            //request.AddParameter("domain", "YOUR_DOMAIN_NAME", ParameterType.UrlSegment);
            //request.Resource = "{domain}/messages";
            //request.AddParameter("from", emailModel.from_name + " <mailgun@YOUR_DOMAIN_NAME>");
            //foreach (string to_email in emailModel.to_emails)
            //{
            //    request.AddParameter("to", to_email);
            //}
            //foreach (string cc_email in emailModel.cc_emails)
            //{
            //    request.AddParameter("cc", cc_email);
            //}
            //foreach (string bcc_email in emailModel.bcc_email)
            //{
            //    request.AddParameter("bcc", bcc_email);
            //}
            //request.AddParameter("subject", emailModel.subject);
            //request.AddParameter("html", emailModel.html_body);
            //request.Method = Method.POST;
            //client.Execute(request);
        }
    }
}
