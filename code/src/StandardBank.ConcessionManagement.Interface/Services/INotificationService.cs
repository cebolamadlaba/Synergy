namespace StandardBank.ConcessionManagement.Interface.Services
{
    using StandardBank.ConcessionManagement.Model;
    
    public interface INotificationService
    {
         bool SendEmail(EmailMessage emailMessage);
    }
}