using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Services
{


    public static class EmailFactory
    {
        public static EmailMessage GetEmail(NotificationTypes notificationType, Concession concession)
        {
            var email = new EmailMessage();

            string emailTemplate = string.Empty;

            switch (notificationType)
            {
                case NotificationTypes.Declined:


                    emailTemplate = string.Format(Resources.Declined,
                                                      concession.Requestor.FullName,
                                                       concession.ReferenceNumber,
                                                      concession.User.FullName
                                                      );

                    email.Subject = "Declined Concession";
                    email.Body = emailTemplate;
                    email.ToEmailAddress = concession.Requestor.EmailAddress;
                    email.CCEmailAddress = concession.User.EmailAddress;
                    email.Body = emailTemplate;

                    break;

                case NotificationTypes.Approved:


                    emailTemplate = string.Format(Resources.Approved,
                                                      concession.Requestor.FullName,
                                                      concession.ReferenceNumber,
                                                      concession.User.FullName
                                                      );

                    email.Subject = "Approved Concession";
                    email.Body = emailTemplate;
                    email.ToEmailAddress = concession.Requestor.EmailAddress;
                    email.CCEmailAddress = concession.User.EmailAddress;
                    email.Body = emailTemplate;

                    break;



                case NotificationTypes.ApprovedWithChanges:


                    emailTemplate = string.Format(Resources.ApprovedWithChanges,
                                                      concession.Requestor.FullName,
                                                       concession.ReferenceNumber,
                                                      concession.User.FullName
                                                      );

                    email.Subject = "Approved With Changes Concession";
                    email.Body = emailTemplate;
                    email.ToEmailAddress = concession.Requestor.EmailAddress;
                    email.CCEmailAddress = concession.User.EmailAddress;
                    email.Body = emailTemplate;

                    break;

                case NotificationTypes.NewConcession:


                    emailTemplate = string.Format(Resources.NewConcession,
                                                      concession.BusinessCentreManager.FullName,
                                                       concession.ReferenceNumber                                                     
                                                      );

                    email.Subject = "New Concession";
                    email.Body = emailTemplate;
                    email.ToEmailAddress = concession.BusinessCentreManager.EmailAddress;
                    email.CCEmailAddress = concession.Requestor.EmailAddress;
                    email.Body = emailTemplate;

                    break;


                case NotificationTypes.Foward:


                    emailTemplate = string.Format(Resources.ApprovedWithChanges,
                                                      concession.PricingManager.FullName,
                                                       concession.ReferenceNumber,
                                                      concession.User.FullName
                                                      );

                    email.Subject = "Concession fowarded to you";
                    email.Body = emailTemplate;
                    email.ToEmailAddress = concession.Requestor.EmailAddress;
                    email.CCEmailAddress = concession.User.EmailAddress;
                    
                    email.Body = emailTemplate;

                    break;
            }

            return email;
        }
    }
}
