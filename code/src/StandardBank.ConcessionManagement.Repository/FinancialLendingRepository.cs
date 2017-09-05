using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// FinancialLending repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IFinancialLendingRepository" />
    public class FinancialLendingRepository : IFinancialLendingRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialLendingRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public FinancialLendingRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public FinancialLending Create(FinancialLending model)
        {
            const string sql = @"INSERT [dbo].[tblFinancialLending] ([fkRiskGroupId], [TotalExposure], [WeightedAverageMap], [WeightedCrsOrMrs], [LatestCrsOrMrs]) 
                                VALUES (@RiskGroupId, @TotalExposure, @WeightedAverageMap, @WeightedCrsOrMrs, @LatestCrsOrMrs) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        TotalExposure = model.TotalExposure,
                        WeightedAverageMap = model.WeightedAverageMap,
                        WeightedCrsOrMrs = model.WeightedCrsOrMrs,
                        LatestCrsOrMrs = model.LatestCrsOrMrs
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public FinancialLending ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialLending>(
                    "SELECT [pkFinancialLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalExposure], [WeightedAverageMap], [WeightedCrsOrMrs], [LatestCrsOrMrs] FROM [dbo].[tblFinancialLending] WHERE [pkFinancialLendingId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the risk group id specified
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        public IEnumerable<FinancialLending> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialLending>(
                    @"SELECT [pkFinancialLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalExposure], [WeightedAverageMap], [WeightedCrsOrMrs], [LatestCrsOrMrs] 
                    FROM [dbo].[tblFinancialLending] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new {riskGroupId});
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinancialLending> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialLending>("SELECT [pkFinancialLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalExposure], [WeightedAverageMap], [WeightedCrsOrMrs], [LatestCrsOrMrs] FROM [dbo].[tblFinancialLending]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(FinancialLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblFinancialLending]
                            SET [fkRiskGroupId] = @RiskGroupId, [TotalExposure] = @TotalExposure, [WeightedAverageMap] = @WeightedAverageMap, [WeightedCrsOrMrs] = @WeightedCrsOrMrs, [LatestCrsOrMrs] = @LatestCrsOrMrs
                            WHERE [pkFinancialLendingId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        TotalExposure = model.TotalExposure,
                        WeightedAverageMap = model.WeightedAverageMap,
                        WeightedCrsOrMrs = model.WeightedCrsOrMrs,
                        LatestCrsOrMrs = model.LatestCrsOrMrs
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(FinancialLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblFinancialLending] WHERE [pkFinancialLendingId] = @Id",
                    new {model.Id});
            }
        }
    }
}
