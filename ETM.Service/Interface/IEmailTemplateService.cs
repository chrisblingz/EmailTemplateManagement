using ETM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Interface
{
    public interface IEmailTemplateService
    {
        ICollection<EmailTemplate> GetEmailTemplates();
        EmailTemplate GetEmailTemplate(int templateId);
        bool EmailTemplateExists(string code);
        bool EmailTemplateExists(int templateId);
        Task<bool> CreateEmailTemplate(EmailTemplate emailTemplate);
        Task<bool> UpdateEmailTemplate(EmailTemplate emailTemplate);
        Task<bool> DeleteEmailTemplate(EmailTemplate emailTemplate);
    }
}
