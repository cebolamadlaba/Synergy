using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Concession manager interface
    /// </summary>
    public interface IConcessionManager
    {
        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Concession> GetPendingConcessionsForUser(User user);

        /// <summary>
        /// Get the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetDueForExpiryConcessionsForUser(User user);

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetExpiredConcessionsForUser(User user);

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetMismatchedConcessionsForUser(User user);

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetDeclinedConcessionsForUser(User user);

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserConcessions GetUserConcessions(User user);

        /// <summary>
        /// Gets the concessions for the legal entity id and the concession type
        /// </summary>
        /// <param name="legalEntityId"></param>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetConcessionsForLegalEntityIdAndConcessionType(int legalEntityId,
            string concessionType);

        /// <summary>
        /// Gets the concession conditions
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        IEnumerable<ConcessionCondition> GetConcessionConditions(int concessionId);
    }
}
