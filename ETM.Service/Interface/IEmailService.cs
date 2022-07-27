using ETM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailLog emailLog);
    }
}
