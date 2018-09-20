using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// FinancialInvestment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IFinancialInvestmentRepository" />
    public class FinancialInvestmentRepository : IFinancialInvestmentRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialInvestmentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public FinancialInvestmentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public FinancialInvestment Create(FinancialInvestment model)
        {
            const string sql =
                @"INSERT [dbo].[tblFinancialInvestment] ([fkRiskGroupId], [TotalLiabilityBalances], [WeightedAverageMTP], [WeightedAverageNetMargin], [LatestCrsOrMrs]) 
                                VALUES (@RiskGroupId, @TotalLiabilityBalances, @WeightedAverageMTP, @WeightedAverageNetMargin, @LatestCrsOrMrs) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        TotalLiabilityBalances = model.TotalLiabilityBalances,
                        WeightedAverageMTP = model.WeightedAverageMTP,
                        WeightedAverageNetMargin = model.WeightedAverageNetMargin,
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
        public FinancialInvestment ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialInvestment>(
                    "SELECT [pkFinancialInvestmentId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalLiabilityBalances], [WeightedAverageMTP], [WeightedAverageNetMargin], [LatestCrsOrMrs] FROM [dbo].[tblFinancialInvestment] WHERE [pkFinancialInvestmentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinancialInvestment> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialInvestment>(
                    "SELECT [pkFinancialInvestmentId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalLiabilityBalances], [WeightedAverageMTP], [WeightedAverageNetMargin], [LatestCrsOrMrs] FROM [dbo].[tblFinancialInvestment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(FinancialInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblFinancialInvestment]
                            SET [fkRiskGroupId] = @RiskGroupId, [TotalLiabilityBalances] = @TotalLiabilityBalances, [WeightedAverageMTP] = @WeightedAverageMTP, [WeightedAverageNetMargin] = @WeightedAverageNetMargin, [LatestCrsOrMrs] = @LatestCrsOrMrs
                            WHERE [pkFinancialInvestmentId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        TotalLiabilityBalances = model.TotalLiabilityBalances,
                        WeightedAverageMTP = model.WeightedAverageMTP,
                        WeightedAverageNetMargin = model.WeightedAverageNetMargin,
                        LatestCrsOrMrs = model.LatestCrsOrMrs
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(FinancialInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblFinancialInvestment] WHERE [pkFinancialInvestmentId] = @Id",
                    new {model.Id});
            }
        }

        public IEnumerable<FinancialInvestment> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialInvestment>(
                    @"SELECT [pkFinancialInvestmentId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalLiabilityBalances], [WeightedAverageMTP] ,[WeightedAverageNetMargin]
                    FROM [dbo].[tblFinancialInvestment] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }
    }
}