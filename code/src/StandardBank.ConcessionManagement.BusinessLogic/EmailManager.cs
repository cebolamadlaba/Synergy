using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using FluentEmail.Core;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.Common;
using System.IO;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class EmailManager: IEmailManager
    {
      private string DefaultEmail { get; }
        string TempaltePath { get; }
        public EmailManager(IConfigurationData config)
        {
            Email.DefaultSender = new MailKitEmailSender(config);
            DefaultEmail = config.DefaultEmail;
            TempaltePath = config.TemplatePath;
        }
      
       
        /// <summary>
        /// Sends the email
        /// </summary>
        /// <param name="recipient">Adds a reciepient to the email, Splits name and address on ';'</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message"> The body of the email</param>
        /// <returns></returns>
        public async Task<bool> SendEmail(string recipient, string subject, string message)
        {
            var email = Email
                .From(DefaultEmail)
                .To(recipient)
                .Subject(subject)
                .Body(message);
            var response = await email.SendAsync();
            return response.Successful;
        }

        public async Task<bool> SendTemplatedEmail(string recipient, string subject, string message, string templateName, object model)
        {
            var email = Email
                .From(DefaultEmail)
                .To(recipient)
                .Subject(subject)
                .UsingTemplateFromFile(Path.Combine(TempaltePath,templateName+".cshtml"), model);
            var response = await email.SendAsync();
            return response.Successful;
        }
    }
}
