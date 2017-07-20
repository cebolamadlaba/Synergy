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
        /// Reads all the records for the requestor id, status id, sub status id and is active supplied
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="subStatusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRequestorIdStatusIdSubStatusIdIsActive(int requestorId, int statusId,
            int subStatusId, bool isActive);

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