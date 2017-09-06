using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// FinancialCash repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IFinancialCashRepository" />
    public class FinancialCashRepository : IFinancialCashRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialCashRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public FinancialCashRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public FinancialCash Create(FinancialCash model)
        {
            const string sql = @"INSERT [dbo].[tblFinancialCash] ([fkRiskGroupId], [WeightedAverageBranchPrice], [TotalCashCentrCashTurnover], [TotalCashCentrCashVolume], [TotalBranchCashTurnover], [TotalBranchCashVolume], [TotalAutosafeCashTurnover], [TotalAutosafeCashVolume], [WeightedAverageCCPrice], [WeightedAverageAFPrice], [LatestCrsOrMrs]) 
                                VALUES (@RiskGroupId, @WeightedAverageBranchPrice, @TotalCashCentrCashTurnover, @TotalCashCentrCashVolume, @TotalBranchCashTurnover, @TotalBranchCashVolume, @TotalAutosafeCashTurnover, @TotalAutosafeCashVolume, @WeightedAverageCCPrice, @WeightedAverageAFPrice, @LatestCrsOrMrs) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {RiskGroupId = model.RiskGroupId, WeightedAverageBranchPrice = model.WeightedAverageBranchPrice, TotalCashCentrCashTurnover = model.TotalCashCentrCashTurnover, TotalCashCentrCashVolume = model.TotalCashCentrCashVolume, TotalBranchCashTurnover = model.TotalBranchCashTurnover, TotalBranchCashVolume = model.TotalBranchCashVolume, TotalAutosafeCashTurnover = model.TotalAutosafeCashTurnover, TotalAutosafeCashVolume = model.TotalAutosafeCashVolume, WeightedAverageCCPrice = model.WeightedAverageCCPrice, WeightedAverageAFPrice = model.WeightedAverageAFPrice, LatestCrsOrMrs = model.LatestCrsOrMrs}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public FinancialCash ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialCash>(
                    "SELECT [pkFinancialCashId] [Id], [fkRiskGroupId] [RiskGroupId], [WeightedAverageBranchPrice], [TotalCashCentrCashTurnover], [TotalCashCentrCashVolume], [TotalBranchCashTurnover], [TotalBranchCashVolume], [TotalAutosafeCashTurnover], [TotalAutosafeCashVolume], [WeightedAverageCCPrice], [WeightedAverageAFPrice], [LatestCrsOrMrs] FROM [dbo].[tblFinancialCash] WHERE [pkFinancialCashId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinancialCash> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialCash>("SELECT [pkFinancialCashId] [Id], [fkRiskGroupId] [RiskGroupId], [WeightedAverageBranchPrice], [TotalCashCentrCashTurnover], [TotalCashCentrCashVolume], [TotalBranchCashTurnover], [TotalBranchCashVolume], [TotalAutosafeCashTurnover], [TotalAutosafeCashVolume], [WeightedAverageCCPrice], [WeightedAverageAFPrice], [LatestCrsOrMrs] FROM [dbo].[tblFinancialCash]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(FinancialCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblFinancialCash]
                            SET [fkRiskGroupId] = @RiskGroupId, [WeightedAverageBranchPrice] = @WeightedAverageBranchPrice, [TotalCashCentrCashTurnover] = @TotalCashCentrCashTurnover, [TotalCashCentrCashVolume] = @TotalCashCentrCashVolume, [TotalBranchCashTurnover] = @TotalBranchCashTurnover, [TotalBranchCashVolume] = @TotalBranchCashVolume, [TotalAutosafeCashTurnover] = @TotalAutosafeCashTurnover, [TotalAutosafeCashVolume] = @TotalAutosafeCashVolume, [WeightedAverageCCPrice] = @WeightedAverageCCPrice, [WeightedAverageAFPrice] = @WeightedAverageAFPrice, [LatestCrsOrMrs] = @LatestCrsOrMrs
                            WHERE [pkFinancialCashId] = @Id",
                    new {Id = model.Id, RiskGroupId = model.RiskGroupId, WeightedAverageBranchPrice = model.WeightedAverageBranchPrice, TotalCashCentrCashTurnover = model.TotalCashCentrCashTurnover, TotalCashCentrCashVolume = model.TotalCashCentrCashVolume, TotalBranchCashTurnover = model.TotalBranchCashTurnover, TotalBranchCashVolume = model.TotalBranchCashVolume, TotalAutosafeCashTurnover = model.TotalAutosafeCashTurnover, TotalAutosafeCashVolume = model.TotalAutosafeCashVolume, WeightedAverageCCPrice = model.WeightedAverageCCPrice, WeightedAverageAFPrice = model.WeightedAverageAFPrice, LatestCrsOrMrs = model.LatestCrsOrMrs});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(FinancialCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblFinancialCash] WHERE [pkFinancialCashId] = @Id",
                    new {model.Id});
            }
        }
    }
}
