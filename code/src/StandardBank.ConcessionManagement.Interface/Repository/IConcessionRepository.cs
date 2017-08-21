using System;
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
        /// Reads the approved concessions for the user
        /// </summary>
        /// <param name="requestorId"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadApprovedConcessions(int requestorId);

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
        /// Reads by the requestor id, status id and is active flag
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRequestorIdStatusIdIsActive(int requestorId, int statusId, bool isActive);

        /// <summary>
        /// Reads by the requestor id, between the start and end expiry date and is active 
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="startExpiryDate"></param>
        /// <param name="endExpiryDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(int requestorId,
            DateTime startExpiryDate, DateTime endExpiryDate, bool isActive);

        /// <summary>
        /// Reads by the risk group id and is active
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="concessionTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByRiskGroupIdConcessionTypeIdIsActive(int riskGroupId, int concessionTypeId, bool isActive);

        /// <summary>
        /// Reads by the concession reference and the is active flag
        /// </summary>
        /// <param name="concessionRef"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByConcessionRefIsActive(string concessionRef, bool isActive);

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

        IEnumerable<Concession> GetActionedByBCMUser(int userId);
        IEnumerable<Concession> GetActionedByPCMUser(int userId);
        IEnumerable<Concession> GetActionedByHOUser(int userId);
        IEnumerable<Concession> GetConcessions(IEnumerable<int> concessionIds);

        /// <summary>
        /// Reads by the centre id, status is, sub status id and the is active flag
        /// </summary>
        /// <param name="centreId"></param>
        /// <param name="statusId"></param>
        /// <param name="subStatusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Concession> ReadByCentreIdStatusIdSubStatusIdIsActive(int centreId, int statusId, int subStatusId,
            bool isActive);
    }
}