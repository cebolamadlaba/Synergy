using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface INotificationService
    {
        bool SendEmail(EmailMessage emailMessage);
    }
}
