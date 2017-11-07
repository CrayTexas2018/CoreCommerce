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
        public EmailTemplate CreateEmailTemplate(PostEmailTemplate postEmailTemplate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmailTemplate> GetEmailTemplates()
        {
            throw new NotImplementedException();
        }

        public EmailTemplate GetTemplate(int template_id)
        {
            throw new NotImplementedException();
        }

        public EmailTemplate GetTemplateByName(string template_name)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateTemplate(EmailTemplate emailTemplate)
        {
            throw new NotImplementedException();
        }
    }
}
