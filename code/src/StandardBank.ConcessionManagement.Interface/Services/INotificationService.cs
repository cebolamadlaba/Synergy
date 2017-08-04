namespace StandardBank.ConcessionManagement.Interface.Services
{
  
    using StandardBank.ConcessionManagement.Model.Common;

    public interface INotificationService
    {
         bool SendEmail(EmailMessage emailMessage);
    }
}