using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionCondition repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionConditionRepository" />
    public class ConcessionConditionRepository : IConcessionConditionRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionConditionRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionConditionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionCondition Create(ConcessionCondition model)
        {
            const string sql =
                @"INSERT [dbo].[tblConcessionCondition] ([fkConcessionId], [fkConditionTypeId], [fkConditionProductId], [fkPeriodTypeId], [fkPeriodId], [InterestRate], [Volume], [Value], [ConditionMet], [ExpectedTurnoverValue], [ExpiryDate], [DateApproved], [IsActive]) 
                VALUES (@ConcessionId, @ConditionTypeId, @ConditionProductId, @PeriodTypeId, @PeriodId, @InterestRate, @Volume, @Value, @ConditionMet, @ExpectedTurnoverValue, @ExpiryDate, @DateApproved, @IsActive) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        ConditionTypeId = model.ConditionTypeId,
                        ConditionProductId = model.ConditionProductId,
                        PeriodTypeId = model.PeriodTypeId,
                        PeriodId = model.PeriodId,
                        InterestRate = model.InterestRate,
                        Volume = model.Volume,
                        Value = model.Value,
                        ConditionMet = model.ConditionMet,
                        ExpectedTurnoverValue = model.ExpectedTurnoverValue,
                        ExpiryDate = model.ExpiryDate,
                        DateApproved = model.DateApproved,
                        IsActive = model.IsActive
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionCondition ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCondition>(
                    "SELECT [pkConcessionConditionId] [Id], [fkConcessionId] [ConcessionId], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [fkPeriodTypeId] [PeriodTypeId], [fkPeriodId] [PeriodId], [InterestRate], [Volume], [Value], [ConditionMet], [ExpectedTurnoverValue], [ExpiryDate], [DateApproved], [IsActive] FROM [dbo].[tblConcessionCondition] WHERE [pkConcessionConditionId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the concession id
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCondition>(
                    @"SELECT [pkConcessionConditionId] [Id], [fkConcessionId] [ConcessionId], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [fkPeriodTypeId] [PeriodTypeId], [fkPeriodId] [PeriodId], [InterestRate], [Volume], [Value], [ConditionMet], [ExpectedTurnoverValue], [ExpiryDate], [DateApproved], [IsActive] 
                    FROM [dbo].[tblConcessionCondition] 
                    WHERE [fkConcessionId] = @concessionId",
                    new {concessionId});
            }
        }

        /// <summary>
        /// Reads the by period and approval status.
        /// </summary>
        /// <param name="concessionApprovalStatusId">The concession approval status identifier.</param>
        /// <param name="periodId">The period identifier.</param>
        /// <param name="periodType">Type of the period.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> ReadByPeriodAndApprovalStatus(int concessionApprovalStatusId,
            int periodId, int periodType)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                var query = @"
                    SELECT 
                        rg.RiskGroupName, rg.RiskGroupNumber, c.pkConcessionId 'ConcessionId', c.[ConcessionRef] 'ConcessionReferenceNumber', 
                        ct.Description 'ConditionType', cp.Description 'ProductType', cc.InterestRate, cc.Volume, cc.Value, 
                        cc.[DateApproved], p.[Description] 'Period', cc.[ExpectedTurnoverValue], cc.[ExpiryDate]
                    FROM [dbo].[tblConcessionCondition] cc
                    join dbo.rtblConditionType ct on cc.fkConditionTypeId = ct.pkConditionTypeId
                    join dbo.rtblConditionProduct cp on cp.pkConditionProductId = cc.fkConditionProductId
                    join tblConcession c on c.pkConcessionId = cc.fkConcessionId
                    join tblRiskGroup rg on rg.pkRiskGroupId = c.fkRiskGroupId
                    join [rtblPeriod] p on p.[pkPeriodId] =  cc.fkPeriodId
                    where c.fkStatusId = @statusId and cc.fkPeriodId = @periodId and cc.fkPeriodTypeId = @periodType and cc.[DateApproved] is not null";
                return db.Query<ConcessionCondition>(query,
                    new {statusId = concessionApprovalStatusId, periodId, periodType});
            }
        }

        /// <summary>
        /// Reads the condition counts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConditionCount> ReadConditionCounts()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                var query =
                    @"SELECT CASE WHEN cc.[fkPeriodTypeId] = 1 THEN 'Standard' ELSE 'Ongoing' END [PeriodType], COUNT(*) [RecordCount] FROM [dbo].[tblConcessionCondition] cc
                    JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cc.[fkConcessionId]
                    WHERE c.[fkStatusId] IN (2, 3)
                    AND c.[IsActive] = 1
                    AND cc.[ConditionMet] IS NULL
                    AND cc.[fkPeriodTypeId] IS NOT NULL
                    GROUP BY cc.[fkPeriodTypeId]";

                return db.Query<ConditionCount>(query);
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCondition>(
                    "SELECT [pkConcessionConditionId] [Id], [fkConcessionId] [ConcessionId], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [fkPeriodTypeId] [PeriodTypeId], [fkPeriodId] [PeriodId], [InterestRate], [Volume], [Value], [ConditionMet], [ExpectedTurnoverValue], [ExpiryDate], [DateApproved], [IsActive] FROM [dbo].[tblConcessionCondition]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionCondition model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionCondition]
                            SET [fkConcessionId] = @ConcessionId, [fkConditionTypeId] = @ConditionTypeId, [fkConditionProductId] = @ConditionProductId, [fkPeriodTypeId] = @PeriodTypeId, [fkPeriodId] = @PeriodId, [InterestRate] = @InterestRate, [Volume] = @Volume, [Value] = @Value, [ConditionMet] = @ConditionMet, [ExpectedTurnoverValue] = @ExpectedTurnoverValue, [ExpiryDate] = @ExpiryDate, [DateApproved] = @DateApproved, [IsActive] = @IsActive
                            WHERE [pkConcessionConditionId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        ConditionTypeId = model.ConditionTypeId,
                        ConditionProductId = model.ConditionProductId,
                        PeriodTypeId = model.PeriodTypeId,
                        PeriodId = model.PeriodId,
                        InterestRate = model.InterestRate,
                        Volume = model.Volume,
                        Value = model.Value,
                        ConditionMet = model.ConditionMet,
                        ExpectedTurnoverValue = model.ExpectedTurnoverValue,
                        ExpiryDate = model.ExpiryDate,
                        DateApproved = model.DateApproved,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionCondition model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionCondition] WHERE [pkConcessionConditionId] = @Id",
                    new {model.Id});
            }
        }
    }
}
