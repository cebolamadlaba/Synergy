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
