using System;
using System.Collections.Generic;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Concession inbox view repository
    /// </summary>
    public class ConcessionInboxViewRepository : IConcessionInboxViewRepository
    {
        /// <summary>
        /// The database connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionInboxViewRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        public ConcessionInboxViewRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Reads the by requestor identifier status ids is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByRequestorIdStatusIdsIsActive(int requestorId,
            IEnumerable<int> statusIds, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [RequestorId] = @requestorId
                    AND [StatusId] in @statusIds
                    AND [IsActive] = @isActive",
                    new { requestorId, statusIds, isActive });
            }
        }

        /// <summary>
        /// Reads the by centre identifier status identifier sub status identifier is active.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByCentreIdStatusIdSubStatusIdIsActive(int centreId, int statusId,
            int subStatusId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [CentreId] = @centreId
                    AND [StatusId] = @statusId
                    AND [SubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new { centreId, statusId, subStatusId, isActive });
            }
        }

        /// <summary>
        /// Reads the by requestor identifier between start expiry date end expiry date status ids is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="startExpiryDate">The start expiry date.</param>
        /// <param name="endExpiryDate">The end expiry date.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(
            int requestorId, DateTime startExpiryDate, DateTime endExpiryDate, IEnumerable<int> statusIds,
            bool isActive)
        {
            if (startExpiryDate == DateTime.MinValue)
                startExpiryDate = new DateTime(1900, 1, 1);

            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [RequestorId] = @requestorId
                    AND ([ExpiryDate] BETWEEN @startExpiryDate AND @endExpiryDate)
                    AND [StatusId] in @statusIds
                    AND [IsActive] = @isActive",
                    new { requestorId, startExpiryDate, endExpiryDate, statusIds, isActive });
            }
        }

        /// <summary>
        /// Reads the by requestor identifier status ids is mismatched is active.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isMismatched">if set to <c>true</c> [is mismatched].</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByRequestorIdStatusIdsIsMismatchedIsActive(int requestorId,
            IEnumerable<int> statusIds, bool isMismatched, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [RequestorId] = @requestorId
                    AND [StatusId] in @statusIds
                    AND [IsMismatched] = @isMismatched
                    AND [IsActive] = @isActive",
                    new { requestorId, statusIds, isMismatched, isActive });
            }
        }

        /// <summary>
        /// Reads the by BCM user identifier is active.
        /// </summary>
        /// <param name="bcmUserId">The BCM user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByBcmUserIdIsActive(int bcmUserId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [BCMUserId] = @bcmUserId
                    AND [IsActive] = @isActive",
                    new { bcmUserId, isActive });
            }
        }

        /// <summary>
        /// Reads the by PCM user identifier is active.
        /// </summary>
        /// <param name="pcmUserId">The PCM user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByPcmUserIdIsActive(int pcmUserId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [PCMUserId] = @pcmUserId
                    AND [IsActive] = @isActive",
                    new { pcmUserId, isActive });
            }
        }

        /// <summary>
        /// Reads the by ho user identifier is active.
        /// </summary>
        /// <param name="hoUserId">The ho user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByHoUserIdIsActive(int hoUserId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [HOUserId] = @hoUserId
                    AND [IsActive] = @isActive",
                    new { hoUserId, isActive });
            }
        }

        /// <summary>
        /// Reads for due for expiry notification.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadForDueForExpiryNotification()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [StatusId] IN (2, 3)
                    AND [IsActive] = 1
                    AND [IsCurrent] = 1
                    AND ([ExpiryDate] IS NOT NULL AND [ExpiryDate] BETWEEN GETDATE() AND @DateToCheck)",
                    new { DateToCheck = DateTime.Now.AddMonths(3) });
            }
        }

        /// <summary>
        /// Reads for data export.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadForDataExport()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [StatusId] IN (2, 3)
                    AND [IsActive] = 1
                    AND [IsCurrent] = 1
                    AND [PriceExported] = 0
                    AND ([ExpiryDate] IS NULL OR [ExpiryDate] > GETDATE())");
            }
        }

        /// <summary>
        /// Reads the by legal entity identifier requestor identifier status ids is active.
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <param name="statusIds">The status ids.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByLegalEntityIdRequestorIdStatusIdsIsActive(int legalEntityId,
            int requestorId, IEnumerable<int> statusIds, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [LegalEntityId] = @LegalEntityId
                    AND [RequestorId] = @RequestorId
                    AND [StatusId] IN @StatusIds
                    AND [IsActive] = @IsActive
                    AND ([ExpiryDate] IS NULL OR [ExpiryDate] > GETDATE())",
                    new
                    {
                        LegalEntityId = legalEntityId,
                        RequestorId = requestorId,
                        StatusIds = statusIds,
                        IsActive = isActive
                    });
            }
        }

        /// <summary>
        /// Reads the by concession detail ids.
        /// </summary>
        /// <param name="concessionDetailIds">The concession detail ids.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByConcessionDetailIds(IEnumerable<int> concessionDetailIds)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [ProvinceId], [Province], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [ConcessionDetailId] IN @ConcessionDetailIds",
                    new
                    {
                        ConcessionDetailIds = concessionDetailIds
                    });
            }
        }
    }
}
