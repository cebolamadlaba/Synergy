using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// TransactionTableNumber repository interface
    /// </summary>
    public interface ITransactionTableNumberRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionTableNumber Create(TransactionTableNumber model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TransactionTableNumber ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TransactionTableNumber> ReadAll();

        TransactionType Create(TransactionType model);

        TransactionTableNumber CreateupdateTransactionTableNumber(TransactionTableNumber transactionTableNumber);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
       // void Update(TransactionTableNumber model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(TransactionTableNumber model);
    }
}