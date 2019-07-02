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
                //return db.Query<ConcessionInboxView>(
                //@"SELECT distinct [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], convert(date, [ConcessionDate]) 'ConcessionDate', [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], convert(date, [DatesentForApproval]) 'DatesentForApproval', 
                //    convert(date, [ExpiryDate]) 'ExpiryDate',  [IsMismatched], [IsActive], [IsCurrent], convert(date, [DateApproved]) 'DateApproved'
                //    FROM [dbo].[ConcessionInboxView]
                //    WHERE [RequestorId] = @requestorId
                //    AND [StatusId] in @statusIds
                //    AND [IsActive] = @isActive",
                //new { requestorId, statusIds, isActive });


                return db.Query<ConcessionInboxView>(
                    @"SELECT distinct [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [CustomerNumber], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], max([ConcessionDate]) ConcessionDate, [StatusId], [Status], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], max([DatesentForApproval]) DatesentForApproval, 
                    min([ExpiryDate]) ExpiryDate, max([DateApproved]) DateApproved, [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate], [Location] 'ConcessionLetterURL'
                    FROM [dbo].[ConcessionInboxView]					
					left join tblConcessionLetter on [ConcessionInboxView].ConcessionId = tblConcessionLetter.fkConcessionDetailId 

                    --WHERE [RequestorId] = @requestorId
                    WHERE [CurrentAEUserId] = @requestorId
                    AND [StatusId] in @statusIds
                    AND [IsActive] = @isActive

                    group by [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType],[StatusId], [Status],[SubStatus], [ConcessionRef], [MarketSegmentId], [Segment],
                    [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate], [Location], [CustomerNumber]"

                    ,
                    new { requestorId, statusIds, isActive });
            }
        }

        public IEnumerable<ConcessionInboxView> GetapporvedView(int requestorId,
         IEnumerable<int> statusIds, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT distinct [ConcessionId],LegalEntityId, [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [CustomerName], [ConcessionTypeId], [ConcessionType], max([ConcessionDate]) ConcessionDate, [StatusId], [Status], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], max([DatesentForApproval]) DatesentForApproval, 
                    min([ExpiryDate]) ExpiryDate, max([DateApproved]) DateApproved, [CentreId], [CentreName], [RegionId], [Region],[Location] 'ConcessionLetterURL', CurrentAeUserId
                    FROM [dbo].[ConcessionInboxView]					
					left join tblConcessionLetter on [ConcessionInboxView].ConcessionId = tblConcessionLetter.fkConcessionDetailId  

                    WHERE ([CurrentAEUserId] = @requestorId)
                    AND [StatusId] in @statusIds
                    AND [IsActive] = @isActive

                    group by [ConcessionId],LegalEntityId, [RiskGroupId], [RiskGroupNumber], [RiskGroupName],[CustomerName], [ConcessionTypeId], [ConcessionType],[StatusId], [Status],[SubStatus], [ConcessionRef], [MarketSegmentId], [Segment],
                    [CentreId], [CentreName], [RegionId], [Region], [Location], CurrentAeUserId"

                    ,
                    new { requestorId, statusIds, isActive });
            }
        }


        public IEnumerable<ConcessionInboxView> Search(int? regionId, int? centreId, DateTime? datesentForApproval, IEnumerable<int> statusIds)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                string sql = @"SELECT distinct [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName],[ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], min([DatesentForApproval]) DatesentForApproval , 
                    min([ExpiryDate]) ExpiryDate, min([DateApproved]) DateApproved , [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE                    
                    [IsActive] = 1
                    AND [Archived] is null
                    AND [SubStatusId] in @statusIds";

                sql += (regionId == null || regionId == 0) ? "" : " AND [RegionId] = @regionId";
                sql += (centreId == null || centreId == 0) ? "" : " AND [CentreId] = @centreId";
                sql += (datesentForApproval == null || datesentForApproval.Value.Year == 1) ? "" : " AND datediff(day, [DatesentForApproval],@datesentForApproval ) = 0";

                sql += @" group by [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName],[ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment],
                    [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region]";

                return db.Query<ConcessionInboxView>(sql, new { statusIds, regionId, centreId, datesentForApproval });
            }
        }


        public IEnumerable<ConcessionInboxView> ReadbyPCMPending(int? regionId, int? centreId, DateTime? datesentForApproval, IEnumerable<int> statusIds)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                string sql = @"SELECT distinct [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [CustomerName], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], 
                    [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE                    
                    [IsActive] = 1
                    AND [Archived] is null
                    AND [SubStatusId] in @statusIds";

                sql += (regionId == null || regionId == 0) ? "" : " AND [RegionId] = @regionId";
                sql += (centreId == null || centreId == 0) ? "" : " AND [CentreId] = @centreId";
                sql += (datesentForApproval == null || datesentForApproval.Value.Year == 1) ? "" : " AND datediff(day, [DatesentForApproval],@datesentForApproval ) = 0";

                return db.Query<ConcessionInboxView>(sql, new { statusIds, regionId, centreId, datesentForApproval });
            }
        }

        /// <summary>
        /// Reads the by centre ids status identifier sub status identifier is active.
        /// </summary>
        /// <param name="centreIds">The centre ids.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByCentreIdsStatusIdSubStatusIdIsActive(IEnumerable<int> centreIds, int statusId, int subStatusId,
            bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [CentreId] in @centreIds
                    AND [StatusId] = @statusId
                    AND [SubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new { centreIds, statusId, subStatusId, isActive });
            }
        }

        /// <summary>
        /// Reads the by region identifier status identifier sub status identifier is active.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="subStatusId">The sub status identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<ConcessionInboxView> ReadByRegionIdStatusIdSubStatusIdIsActive(int regionId, int statusId, int subStatusId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [RegionId] = @regionId
                    AND [StatusId] = @statusId
                    AND [SubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new { regionId, statusId, subStatusId, isActive });
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [CurrentAEUserId] = @requestorId
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [CurrentAEUserId] = @requestorId
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [StatusId] IN (2, 3)
                    AND [IsActive] = 1
                    AND [IsCurrent] = 1
                    AND [Archived] is null
                    AND ([ExpiryDate] IS NOT NULL AND [ExpiryDate] BETWEEN GETDATE() AND @DateToCheck)",
                    new { DateToCheck = DateTime.Now.AddMonths(3) });
            }
        }

        public IEnumerable<ConcessionInboxView> ReadDueFor72HourEscaltion(IEnumerable<int> statusIdlist)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                string sql = @"SELECT distinct [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE 
                    [SubStatusId] in @statusIds
                    AND [DatesentForApproval] <= @DateToCheck
                    AND [IsActive] = 1
                    AND [Archived] is null";
                return db.Query<ConcessionInboxView>(sql, new { statusIds = statusIdlist, DateToCheck = DateTime.Now.AddDays(-3) });
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate]
                    FROM [dbo].[ConcessionInboxView]
                    WHERE [LegalEntityId] = @LegalEntityId
                    AND [CurrentAEUserId] = @RequestorId
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
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate],[Location] 'ConcessionLetterURL'
                    FROM [dbo].[ConcessionInboxView]
                    left join tblConcessionLetter on [ConcessionInboxView].ConcessionId = tblConcessionLetter.fkConcessionDetailId 
                    WHERE [ConcessionId] IN @ConcessionDetailIds",
                    new
                    {
                        ConcessionDetailIds = concessionDetailIds
                    });
            }
        }


        public IEnumerable<ConcessionInboxView> ReadByConcessionIds(IEnumerable<int> concessionIds)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInboxView>(
                    @"SELECT [ConcessionId], [RiskGroupId], [RiskGroupNumber], [RiskGroupName], [LegalEntityId], [CustomerName], [LegalEntityAccountId], [AccountNumber], [ConcessionTypeId], [ConcessionType], [ConcessionDate], [StatusId], [Status], [SubStatusId], [SubStatus], [ConcessionRef], [MarketSegmentId], [Segment], [DatesentForApproval], [ConcessionDetailId], [ExpiryDate], [DateApproved], [AAUserId], [RequestorId], [BCMUserId], [PCMUserId], [HOUserId], [CentreId], [CentreName], [RegionId], [Region], [IsMismatched], [IsActive], [IsCurrent], [PriceExported], [PriceExportedDate],[Location] 'ConcessionLetterURL'
                    FROM [dbo].[ConcessionInboxView]
                    left join tblConcessionLetter on [ConcessionInboxView].ConcessionDetailId = tblConcessionLetter.fkConcessionDetailId 
                    WHERE [ConcessionId] IN @ConcessionIds                  
					AND [StatusId] IN  (2,3)
                    AND [IsActive] = 1
                    AND ([ExpiryDate] IS NULL OR [ExpiryDate] > GETDATE())",
                    new
                    {
                        ConcessionIds = concessionIds
                    });
            }
        }
    }
}
