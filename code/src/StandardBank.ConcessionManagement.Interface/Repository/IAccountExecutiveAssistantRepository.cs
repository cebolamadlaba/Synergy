using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// AccountExecutiveAssistant repository interface
    /// </summary>
    public interface IAccountExecutiveAssistantRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        AccountExecutiveAssistant Create(AccountExecutiveAssistant model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        AccountExecutiveAssistant ReadById(int id);

        /// <summary>
        /// Reads the by account assistant user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        AccountExecutiveAssistant ReadByAccountAssistantUserId(int userId);

        /// <summary>
        /// Reads the by account executive user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<AccountExecutiveAssistant> ReadByAccountExecutiveUserId(int userId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<AccountExecutiveAssistant> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(AccountExecutiveAssistant model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(AccountExecutiveAssistant model);
    }
}