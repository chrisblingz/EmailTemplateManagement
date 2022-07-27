using ETM.Entity;
using ETM.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailLogService _emailLogService;
        private MailSettings _mailSettings;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration, IEmailLogService emailLogService)
        {
            _logger = logger;
            _configuration = configuration;
            _emailLogService = emailLogService;
            _mailSettings = configuration.GetSection("SmtpDetails").Get<MailSettings>();
        }
        public async Task<bool> SendEmailAsync(EmailLog emailLog)
        {
            try
            {
                var status = await SendMail(emailLog);
                if (status)
                {
                    emailLog.SendStatus = 1;
                    await _emailLogService.CreateUser(emailLog);
                    return true;
                }
                else
                {
                    emailLog.SendStatus = 2;
                    await _emailLogService.CreateUser(emailLog);
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " : " + ex.StackTrace);
                return false;
            }

        }

        private async Task<bool> SendMail(EmailLog emailLog)
        {
            try
            {
                var cred = GetEmailCredentials();

                MailMessage mMessage = new MailMessage();

                mMessage.To.Add(emailLog.Receiver);
                mMessage.Subject = emailLog.Subject;
                mMessage.From = new MailAddress(cred.FrmMail);
                if (!string.IsNullOrEmpty(emailLog.Cc))
                {
                    string[] CCId = emailLog.Cc.Split(',');
                    foreach (string CCEmail in CCId)
                    {
                        mMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }
                if (!string.IsNullOrEmpty(emailLog.Bcc))
                {
                    string[] BCCId = emailLog.Bcc.Split(',');
                    foreach (string BCCEmail in BCCId)
                    {
                        mMessage.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                    }
                }

                mMessage.Body = emailLog.MessageBody;
                mMessage.Priority = MailPriority.High;
                mMessage.IsBodyHtml = true;

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.host;
                    smtpMail.Port = Convert.ToInt32(cred.Port);
                    smtpMail.EnableSsl = true;
                    smtpMail.Credentials = new NetworkCredential(cred.Username, cred.Password);
                    await smtpMail.SendMailAsync(mMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Email - [Subject: {emailLog.Subject}, Reciever: {emailLog.Receiver}, Send Status : Failed, Error : {ex.Message}]");
                return false;
            }
        }

        private MailSettings GetEmailCredentials()
        {
            try
            {
                var mailSettings = _mailSettings;
                return mailSettings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new MailSettings();
            }
        }
    }
}
