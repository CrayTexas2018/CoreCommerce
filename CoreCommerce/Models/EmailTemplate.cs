using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class EmailTemplate : CommonFields
    {
        [Key]
        public int email_id { get; set; }

        public string email_name { get; set; }

        public string email_template { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostEmailTemplate
    {
        public string email_name { get; set; }

        public string email_template { get; set; }
    }

    public interface IEmailTemplateRepository
    {
        IEnumerable<EmailTemplate> GetEmailTemplates();
        EmailTemplate GetTemplate(int template_id);
        EmailTemplate GetTemplateByName(string template_name);
        EmailTemplate CreateEmailTemplate(PostEmailTemplate postEmailTemplate);
        void UpdateTemplate(EmailTemplate emailTemplate);
        void Save();
    }

    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        ApplicationContext context;
        CompanyRepository cr;

        public EmailTemplateRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public EmailTemplate CreateEmailTemplate(PostEmailTemplate postEmailTemplate)
        {
            EmailTemplate template = new EmailTemplate
            {
                email_name = postEmailTemplate.email_name,
                email_template = postEmailTemplate.email_template,
                active = true,
                company_id = cr.GetCompanyIdFromApiUser(),
                company = cr.GetCompanyFromApiUser(),
                created = DateTime.Now,
                updated = DateTime.Now
            };

            context.EmailTemplates.Add(template);
            Save();

            return template;
        }

        public IEnumerable<EmailTemplate> GetEmailTemplates()
        {
            // Get the template by name
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.EmailTemplates.Where(x => x.company_id == company_id).ToList();
        }

        public EmailTemplate GetTemplate(int template_id)
        {
            // Get the template by name
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.EmailTemplates.Where(x => x.email_id == template_id).Where(x => x.company_id == company_id).FirstOrDefault();
        }

        public EmailTemplate GetTemplateByName(string template_name)
        {
            // Get the template by name
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.EmailTemplates.Where(x => x.email_template == template_name).Where(x => x.company_id == company_id).FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateTemplate(EmailTemplate emailTemplate)
        {
            context.Entry(emailTemplate).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
