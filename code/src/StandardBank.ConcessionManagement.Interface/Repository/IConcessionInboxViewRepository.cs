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
        /// Reads the by centre ids status identifier sub status identifier is active.
        /// </summary>
        /// <param name="centreIds">The centre ids.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByCentreIdsStatusIdSubStatusIdIsActive(IEnumerable<int> centreIds,
            int statusId, int subStatusId, bool isActive);

        /// <summary>
        /// Reads the by region identifier status identifier sub status identifier is active.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByRegionIdStatusIdSubStatusIdIsActive(int regionId, int statusId,
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

        IEnumerable<ConcessionInboxView> ReadbyPCMPending(int? region, int? businesscentre, DateTime? datefilter,IEnumerable<int> statusIds);

        IEnumerable<ConcessionInboxView> Search(int? region, int? businesscentre, DateTime? datefilter, IEnumerable<int> statusIds);


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

        /// <summary>
        /// Actioned by PCM and HO user identifier is active.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ConcessionsActionedByPcmAndHo(bool isActive);

        /// <summary>
        /// Reads for due for expiry notification.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadForDueForExpiryNotification();

        /// <summary>
        /// Reads for data export.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadForDataExport();

        /// <summary>
        /// Reads the by legal entity identifier requestor identifier status ids is active.
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView>
            ReadByLegalEntityIdRequestorIdStatusIdsIsActive(int legalEntityId, int requestorId, IEnumerable<int> statusIds, bool isActive);

        /// <summary>
        /// Reads the by concession detail ids.
        /// </summary>
        /// <param name="concessionDetailIds">The concession detail ids.</param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> ReadByConcessionDetailIds(IEnumerable<int> concessionDetailIds);

        IEnumerable<ConcessionInboxView> ReadByConcessionIds(IEnumerable<int> concessionDetailIds);

        IEnumerable<ConcessionInboxView> ReadDueFor72HourEscaltion(IEnumerable<int> statusIdlist);

        /// <summary>
        /// Get the approved concession list by RequestorId aka CurrentAeUserId
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusIds"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<ConcessionInboxView> GetapporvedView(int requestorId, IEnumerable<int> statusIds, bool isActive);

        IEnumerable<ConcessionMismatchEscalationView> GetMisMatchedConcession();
    }
}
