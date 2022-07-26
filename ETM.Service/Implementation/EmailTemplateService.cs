using ETM.Data;
using ETM.Entity;
using ETM.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Implementation
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly ApplicationDbContext _db;

        public EmailTemplateService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateEmailTemplate(EmailTemplate emailTemplate)
        {
            await _db.EmailTemplates.AddAsync(emailTemplate);
            return await Save();
        }

        public async Task<bool> DeleteEmailTemplate(EmailTemplate emailTemplate)
        {
             _db.EmailTemplates.Remove(emailTemplate);
            return await Save();
        }

        public bool EmailTemplateExists(string code)
        {
            return _db.EmailTemplates.Any(x => x.Code.ToLower().Trim() == code.ToLower().Trim());
        }

        public bool EmailTemplateExists(int emailTemplateId)
        {
            return _db.EmailTemplates.Any(x=> x.Id == emailTemplateId);
        }

        public EmailTemplate GetEmailTemplate(int emailTemplateId)
        {
            return _db.EmailTemplates.FirstOrDefault(x => x.Id == emailTemplateId);
        }

        public ICollection<EmailTemplate> GetEmailTemplates()
        {
            return _db.EmailTemplates.OrderBy(x => x.Id).ToList();
        }

        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            _db.EmailTemplates.Update(emailTemplate);
            return await Save();
        }
    }
}
