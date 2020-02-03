using System.Collections.Generic;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Concession condition view repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionConditionViewRepository" />
    public class ConcessionConditionViewRepository : IConcessionConditionViewRepository
    {
        /// <summary>
        /// The database connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionConditionViewRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        public ConcessionConditionViewRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Reads the by period identifier period type identifier.
        /// </summary>
        /// <param name="periodId">The period identifier.</param>
        /// <param name="periodTypeId">The period type identifier.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionConditionView> ReadByPeriodIdPeriodTypeId(int periodId, int periodTypeId, int requestorId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
               

                return db.Query<ConcessionConditionView>(
                    @"SELECT [ConcessionConditionId], 
                             [ConcessionId], 
                             [RequestorId], 
                             [ReferenceNumber], 
                             [ConcessionTypeId], 
                             [ConcessionType], 
                             [RiskGroupId], 
                             [RiskGroupNumber], 
                             [RiskGroupName],
                             [ConditionTypeId], 
                             [ConditionType], 
                             [ConditionProductId], 
                             [ConditionProduct], 
                             [PeriodTypeId], 
                             [PeriodType], 
                             [PeriodId], 
                             [Period], 
                             [InterestRate], 
                             [Volume], 
                             [Value], 
                             [ConditionMet], 
                             cd.[DateApproved], 
                             cv.[ExpiryDate], 
                             [IsActive],
                             isnull(cast([ActualVolume] as varchar),'Unavailable') 'ActualVolume', 
                             isnull(cast([ActualValue] as varchar),'Unavailable') 'ActualValue', 
                             isnull(cast([ActualTurnover] as varchar),'Unavailable') 'ActualTurnover'
                    FROM [dbo].[ConcessionConditionView] cv
					     join tblConcessionDetail cd on cv.ConcessionId = cd.fkConcessionId
                    WHERE [ConditionMet] IS NULL
                          AND [PeriodId] = @periodId
                          AND [PeriodTypeId] = @periodTypeId
                          AND [CurrentAEUserId] = @RequestorId and cv.ExpiryDate <= @ExpiryDate",
                    new { periodId, periodTypeId, requestorId, ExpiryDate = System.DateTime.Today.AddMonths(1) });
            }
        }

        /// <summary>
        /// Reads the concession counts.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionCount> ReadConcessionCounts(int requestorId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCount>(
                    @"SELECT [PeriodType], 
                             COUNT(*) [RecordCount] 
                    FROM [dbo].[ConcessionConditionView] cv
                  	     join tblConcessionDetail cd on cv.ConcessionId = cd.fkConcessionId
                    WHERE [ConditionMet] IS NULL
                          AND [CurrentAEUserId] = @RequestorId  and cv.ExpiryDate <= @ExpiryDate
                    GROUP BY [PeriodType]", 
                    new { requestorId, ExpiryDate = System.DateTime.Today.AddMonths(1) });
            }
        }

        /// <summary>
        /// Reads for renewing ongoing conditions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionConditionView> ReadForRenewingOngoingConditions()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionConditionView>(
                    @"SELECT [ConcessionConditionId], 
                             [ConcessionId], 
                             [RequestorId], 
                             [ReferenceNumber], 
                             [ConcessionTypeId], 
                             [ConcessionType], 
                             [RiskGroupId], 
                             [RiskGroupNumber], 
                             [RiskGroupName], 
                             [ConditionTypeId], 
                             [ConditionType], 
                             [ConditionProductId], 
                             [ConditionProduct], 
                             [PeriodTypeId], 
                             [PeriodType], 
                             [PeriodId], 
                             [Period], 
                             [InterestRate], 
                             [Volume], 
                             [Value], 
                             [ConditionMet], 
                             [DateApproved], 
                             [ExpiryDate], 
                             [IsActive]
                    FROM [dbo].[ConcessionConditionView]
                    WHERE [PeriodTypeId] = 2
                          AND [IsActive] = 1
                          AND [ExpiryDate] < GETDATE()");
            }
        }
    }
}
