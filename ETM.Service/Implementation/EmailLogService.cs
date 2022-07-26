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
    public class EmailLogService : IEmailLogService
    {
        private readonly ApplicationDbContext _db;

        public EmailLogService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateUser(EmailLog emailLog)
        {
            await _db.EmailLogs.AddAsync(emailLog);
            return await Save();
        }

        public EmailLog GetEmailLog(int emailLogId)
        {
            return _db.EmailLogs.FirstOrDefault(x => x.Id == emailLogId);
        }

        public ICollection<EmailLog> GetEmailLogs()
        {
           return _db.EmailLogs.OrderBy(x => x.Id).ToList();
        }

        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
