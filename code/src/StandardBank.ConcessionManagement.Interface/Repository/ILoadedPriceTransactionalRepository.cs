using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LoadedPriceTransactional repository interface
    /// </summary>
    public interface ILoadedPriceTransactionalRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        LoadedPriceTransactional Create(LoadedPriceTransactional model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LoadedPriceTransactional ReadById(int id);

        /// <summary>
        /// Reads by the transaction type it and the legal entity account id
        /// </summary>
        /// <param name="transactionTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        LoadedPriceTransactional ReadByTransactionTypeIdLegalEntityAccountId(int transactionTypeId, int legalEntityAccountId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LoadedPriceTransactional> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(LoadedPriceTransactional model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(LoadedPriceTransactional model);
    }
}