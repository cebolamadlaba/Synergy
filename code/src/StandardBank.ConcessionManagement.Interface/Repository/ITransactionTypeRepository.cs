using System.Collections;
using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// TransactionType repository interface
    /// </summary>
    public interface ITransactionTypeRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionType Create(TransactionType model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TransactionType ReadById(int id);

        IEnumerable<TransactionType> ReadAll(bool isActive);

        /// <summary>
        /// Reads the by concession type identifier is active.
        /// </summary>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<TransactionType> ReadByConcessionTypeIdIsActive(int concessionTypeId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TransactionType> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(TransactionType model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(TransactionType model);
    }
}