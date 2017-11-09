using CoreCommerce.Models;
using CoreCommerce.Models.Stripe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stripe;
using CoreCommerce.Helpers.Stripe;
using CoreCommerce.Helpers.MailGun;
using CoreCommerce.Models.MailGun;

namespace CoreCommerce.Controllers
{
    public class StripeController : ApiController
    {
        ApplicationContext context = new ApplicationContext();

        // GET: api/Stripe
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Stripe/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stripe
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Stripe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stripe/5
        public void Delete(int id)
        {
        }

        /*
         * Webhooks
         */
        [HttpPost]
        [Route("api/stripe/webhook")]
        public IHttpActionResult StripeWebook([FromBody] StripeEvent stripeEvent)
        {
            // get the event to verify it is real
            //var eventService = new StripeEventService();
            //stripeEvent = eventService.Get(stripeEvent.Id);

            if (stripeEvent == null)
            {
                // Event is not a real event
                return BadRequest();
            }
            else if (stripeEvent.Type == "invoice.upcoming")
            {
                // Email customer that they are about to get billed again
                // Get the email template
                EmailTemplateRepository etr = new EmailTemplateRepository(context);
                EmailTemplate emailTemplate = etr.GetTemplateByName("invoice.upcoming");

                MailGunEmail mge = new MailGunEmail
                {
                    // Construt email  
                };

                // Send mailgun email
                MailGunHelper mgh = new MailGunHelper();
                mgh.sendEmail(mge);

                return Ok();
            }
            else if (stripeEvent.Type == "invoice.created")
            {
                // Email customer that they are about to get charged
                return Ok();
            }
            else if (stripeEvent.Type == "charge.succeeded")
            {
                // Do stuff
                return Ok();
            }
            else if (stripeEvent.Type == "invoice.upcoming")
            {
                // Email customer that they are about to get charged
                return Ok();
            }
            else if (stripeEvent.Type == "invoice.payment_succeeded")
            {
                // update subscription
                StripeSubscriptionHelper ssh = new StripeSubscriptionHelper(context);
                ssh.SubscriptionInvoicePaid(stripeEvent.Id);

                return Ok();
            }
            else if (stripeEvent.Type == "invoice.payment_failed")
            {
                // Email that payment failed
                return Ok();
            }
            else if (stripeEvent.Type == "customer.subscription.deleted")
            {
                // Subscription cancelled
                return Ok();
            }
            return Ok();
        }
    }
}
