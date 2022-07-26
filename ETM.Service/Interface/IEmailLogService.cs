using ETM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Interface
{
    public interface IEmailLogService
    {
        ICollection<EmailLog> GetEmailLogs();
        EmailLog GetEmailLog(int emailLogId);        
        Task<bool> CreateUser(EmailLog emailLog);
       
    }
}
