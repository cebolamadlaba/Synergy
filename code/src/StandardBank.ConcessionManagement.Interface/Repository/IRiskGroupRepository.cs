using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// RiskGroup repository interface
    /// </summary>
    public interface IRiskGroupRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        RiskGroup Create(RiskGroup model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        RiskGroup ReadById(int id);

        /// <summary>
        /// Reads by the id and is active flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        RiskGroup ReadByIdIsActive(int id, bool isActive);

        /// <summary>
        /// Reads by the risk group number specified and the is active flag
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        RiskGroup ReadByRiskGroupNumberIsActive(int riskGroupNumber, bool isActive); 

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RiskGroup> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(RiskGroup model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(RiskGroup model);
    }
}