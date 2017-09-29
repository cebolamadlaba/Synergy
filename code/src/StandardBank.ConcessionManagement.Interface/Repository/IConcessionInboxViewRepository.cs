using System;
using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Concession inbox view repository
    /// </summary>
    public interface IConcessionInboxViewRepository
    {
        /// <summary>
        /// Reads the by requestor identifier status ids is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView>
            ReadByRequestorIdStatusIdsIsActive(int requestorId, IEnumerable<int> statusIds, bool isActive);

        /// <summary>
        /// Reads the by centre identifier status identifier sub status identifier is active.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByCentreIdStatusIdSubStatusIdIsActive(int centreId, int statusId,
            int subStatusId, bool isActive);

        /// <summary>
        /// Reads the by requestor identifier between start expiry date end expiry date status ids is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="startExpiryDate">The start expiry date.</param>
        /// <param name="endExpiryDate">The end expiry date.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(
            int requestorId,
            DateTime startExpiryDate, DateTime endExpiryDate, IEnumerable<int> statusIds, bool isActive);

        /// <summary>
        /// Reads the by requestor identifier status ids is mismatched is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isMismatched">if set to <c>true</c> [is mismatched].</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByRequestorIdStatusIdsIsMismatchedIsActive(int requestorId,
            IEnumerable<int> statusIds, bool isMismatched, bool isActive);

        /// <summary>
        /// Reads the by BCM user identifier is active.
        /// </summary>
        /// <param name="bcmUserId">The BCM user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByBcmUserIdIsActive(int bcmUserId, bool isActive);

        /// <summary>
        /// Reads the by PCM user identifier is active.
        /// </summary>
        /// <param name="pcmUserId">The PCM user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByPcmUserIdIsActive(int pcmUserId, bool isActive);

        /// <summary>
        /// Reads the by ho user identifier is active.
        /// </summary>
        /// <param name="hoUserId">The ho user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByHoUserIdIsActive(int hoUserId, bool isActive);
    }
}
