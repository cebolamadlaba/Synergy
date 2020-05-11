using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using MailKit.Net.Smtp;
using MimeKit;
using StandardBank.ConcessionManagement.Interface.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    internal class MailKitEmailSender : ISender
    {
        private IConfigurationData Config { get; }

        public MailKitEmailSender(IConfigurationData config)
        {
            Config = config;
        }

        public SendResponse Send(Email email, CancellationToken? token = default)
        {
            var response = new SendResponse();
            if (token?.IsCancellationRequested ?? false)
            {
                response.ErrorMessages.Add("Message was cancelled by cancellation token.");
                return response;
            }
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(Config.SmtpServer, Config.SmtpPort, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrEmpty(Config.SmtpServerUserName))
                    client.Authenticate(Config.SmtpServerUserName, Config.SmtpServerPassword);

                var message = CreateMailMessage(email);
                client.Send(message);
                client.Disconnect(true);
                return response;
            }
        }

        public async Task<SendResponse> SendAsync(Email email, CancellationToken? token = default)
        {
            var response = new SendResponse();
            if (token?.IsCancellationRequested ?? false)
            {
                response.ErrorMessages.Add("Message was cancelled by cancellation token.");
                return response;
            }
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(Config.SmtpServer, Config.SmtpPort, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrEmpty(Config.SmtpServerUserName))
                    client.Authenticate(Config.SmtpServerUserName, Config.SmtpServerPassword);

                var message = CreateMailMessage(email);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return response;
            }
        }

        private MimeMessage CreateMailMessage(Email email)
        {
            var data = email.Data;
            var message = new MimeMessage
            {
                Subject = data.Subject,
                Body = new TextPart("html") { Text = data.Body }
            };
            message.From.Add(new MailboxAddress(data.FromAddress.Name, data.FromAddress.EmailAddress));

            data.ToAddresses.ForEach(x =>
            {
                message.To.Add(new MailboxAddress(x.Name, x.EmailAddress));
            });

            data.CcAddresses.ForEach(x =>
            {
                message.Cc.Add(new MailboxAddress(x.Name, x.EmailAddress));
            });

            data.BccAddresses.ForEach(x =>
            {
                message.Bcc.Add(new MailboxAddress(x.Name, x.EmailAddress));
            });

            data.ReplyToAddresses.ForEach(x =>
            {
                message.ReplyTo.Add(new MailboxAddress(x.Name, x.EmailAddress));
            });

            switch (data.Priority)
            {
                case Priority.Low:
                    message.Priority = MessagePriority.NonUrgent;
                    break;

                case Priority.Normal:
                    message.Priority = MessagePriority.Normal;
                    break;

                case Priority.High:
                    message.Priority = MessagePriority.Urgent;
                    break;
            }

            return message;
        }
    }
}