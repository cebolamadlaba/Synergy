using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Email manager
    /// </summary>
    public interface IEmailManager
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<bool> SendEmail(string recipient, string subject, string message);

        /// <summary>
        /// Sends the expiring concession email.
        /// </summary>
        /// <param name="expiringConcession">The expiring concession.</param>
        /// <returns></returns>
        Task<bool> SendExpiringConcessionEmail(ExpiringConcession expiringConcession);

        /// <summary>
        /// Sends the concession added email.
        /// </summary>
        /// <param name="concessionAddedEmail">The concession added email.</param>
        /// <returns></returns>
        Task<bool> SendConcessionAddedEmail(ConcessionAddedEmail concessionAddedEmail);
    }
}
