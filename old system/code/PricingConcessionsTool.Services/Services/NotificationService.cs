using PricingConcessionsTool.Core.Business.Classes;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly LogContext _log;

        public NotificationService()
        {
            _log = new LogContext();
        }
        public bool SendEmail(EmailMessage emailMessage)
        {
            if (!Settings.Default.SendEmails)
                return true;

            SendAsyncMail(emailMessage);

            return true;
        }

        private async Task SendAsyncMail(EmailMessage emailMessage)
        {
            try
            {
                await Task.Run(() =>
                {

                    SmtpClient smtpMailClient = new SmtpClient();
                    MailMessage msg = new MailMessage();


                    msg.To.Add(new MailAddress(emailMessage.ToEmailAddress));

                    if (emailMessage.CCEmailAddress != null)
                    {
                        msg.CC.Add(emailMessage.CCEmailAddress);

                    }

                    msg.IsBodyHtml = true;
                    msg.From = new MailAddress(Settings.Default.FromEmail, "Pricing concessions tool");
                    msg.Subject = emailMessage.Subject;
                    msg.Body = emailMessage.Body;

                    smtpMailClient.Host = Settings.Default.EmailHost;

                    smtpMailClient.Credentials = new NetworkCredential(Settings.Default.EmailUserName, Settings.Default.EmailUserPassword);

                    //Send the  Email
                    smtpMailClient.Send(msg);
                });
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }

        }
    }
}
