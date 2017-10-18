using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Concession repository interface
    /// </summary>
    public interface IConcessionRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Concession Create(Concession model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Concession ReadById(int id);

        /// <summary>
        /// Reads the by concession reference is active.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByConcessionRefIsActive(string concessionReferenceNumber, bool isActive);

        /// <summary>
        /// Reads the by risk group identifier concession type identifier is active.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRiskGroupIdConcessionTypeIdIsActive(int riskGroupId, int concessionTypeId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Concession> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(Concession model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(Concession model);
    }
}