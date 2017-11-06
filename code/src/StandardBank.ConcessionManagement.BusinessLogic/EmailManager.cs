using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using FluentEmail.Core;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.Common;
using System.IO;
using FluentEmail.Razor;
using RazorLight.Extensions;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Email manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IEmailManager" />
    public class EmailManager : IEmailManager
    {
        /// <summary>
        /// Gets the default email.
        /// </summary>
        /// <value>
        /// The default email.
        /// </value>
        private string DefaultEmail { get; }

        /// <summary>
        /// Gets the email template path.
        /// </summary>
        /// <value>
        /// The email template path.
        /// </value>
        string EmailTemplatePath { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailManager"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public EmailManager(IConfigurationData config)
        {
            Email.DefaultSender = new MailKitEmailSender(config);
            Email.DefaultRenderer = new RazorRenderer();
            DefaultEmail = config.DefaultEmail;
            EmailTemplatePath = config.EmailTemplatePath;
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

        /// <summary>
        /// Sends the templated email.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private async Task<bool> SendTemplatedEmail(string recipient, string subject, string message,
            string templateName, object model)
        {
            var email = Email
                .From(DefaultEmail)
                .To(recipient)
                .Subject(subject)
                .UsingTemplateFromFile(Path.Combine(EmailTemplatePath, templateName + ".cshtml"), model.ToExpando());

            var response = await email.SendAsync();

            return response.Successful;
        }

        /// <summary>
        /// Sends the expiring concession email.
        /// </summary>
        /// <param name="expiringConcession">The expiring concession.</param>
        /// <returns></returns>
        public async Task<bool> SendExpiringConcessionEmail(ExpiringConcession expiringConcession)
        {
            return await SendTemplatedEmail(expiringConcession.RequestorEmail,
                "CMS Notification: Expiring Concession(s)", string.Empty, "ExpiringConcession",
                new
                {
                    RequestorName = expiringConcession.RequestorName,
                    ExpiringConcessionDetails = expiringConcession.ExpiringConcessionDetails
                });
        }

        /// <summary>
        /// Sends the concession added email.
        /// </summary>
        /// <param name="concessionAddedEmail">The concession added email.</param>
        /// <returns></returns>
        public async Task<bool> SendConcessionAddedEmail(ConcessionAddedEmail concessionAddedEmail)
        {
            return await SendTemplatedEmail(concessionAddedEmail.EmailAddress, "Pricing Tool: New Concession", null,
                Constants.EmailTemplates.NewConcession,
                new {Name = concessionAddedEmail.FirstName, ConcessionId = concessionAddedEmail.ConsessionId});
        }

        /// <summary>
        /// Sends the sap data import issues email.
        /// </summary>
        /// <param name="sapDataImportIssuesEmail">The sap data import issues email.</param>
        /// <returns></returns>
        public async Task<bool> SendSapDataImportIssuesEmail(SapDataImportIssuesEmail sapDataImportIssuesEmail)
        {
            return await SendTemplatedEmail(sapDataImportIssuesEmail.SupportEmailAddress, "CMS SAP Data Import Issues",
                null, "SapDataImportIssues", new
                {
                    sapDataImportIssuesEmail.ServerName,
                    sapDataImportIssuesEmail.DatabaseServer,
                    sapDataImportIssuesEmail.DatabaseName,
                    sapDataImportIssuesEmail.ImportFolder,
                    sapDataImportIssuesEmail.SapDataImportIssues
                });
        }
    }
}
