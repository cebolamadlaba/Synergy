using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// FinancialLending repository interface
    /// </summary>
    public interface IFinancialLendingRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        FinancialLending Create(FinancialLending model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        FinancialLending ReadById(int id);

        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        IEnumerable<FinancialLending> ReadByRiskGroupId(int riskGroupId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<FinancialLending> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(FinancialLending model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(FinancialLending model);
    }
}