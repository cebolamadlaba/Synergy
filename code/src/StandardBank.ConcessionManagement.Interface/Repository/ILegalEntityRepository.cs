using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LegalEntity repository interface
    /// </summary>
    public interface ILegalEntityRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        LegalEntity Create(LegalEntity model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LegalEntity ReadById(int id);

        /// <summary>
        /// Reads by the id and the is active flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        LegalEntity ReadByIdIsActive(int id, bool isActive);

        /// <summary>
        /// Reads by the risk group id specified
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<LegalEntity> ReadByRiskGroupIdIsActive(int riskGroupId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LegalEntity> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(LegalEntity model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(LegalEntity model);
    }
}