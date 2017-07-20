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
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Concession> ReadAll();

        /// <summary>
        /// Reads all the records for the requestor id and the status id supplied
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRequestorIdAndStatusId(int requestorId, int statusId);

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