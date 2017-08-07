using StandardBank.ConcessionManagement.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IEmailManager
    {
        Task<bool> SendEmail(string recipient , string subject , string message);
        Task<bool> SendTemplatedEmail(string recipient, string subject, string message , string templateName , object model);

    }
}
