using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LegalEntityAccount repository interface
    /// </summary>
    public interface ILegalEntityAccountRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        LegalEntityAccount Create(LegalEntityAccount model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LegalEntityAccount ReadById(int id);

        /// <summary>
        /// Reads by the legal entity id specified
        /// </summary>
        /// <param name="legalEntityId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<LegalEntityAccount> ReadByLegalEntityIdIsActive(int legalEntityId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LegalEntityAccount> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(LegalEntityAccount model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(LegalEntityAccount model);
    }
}