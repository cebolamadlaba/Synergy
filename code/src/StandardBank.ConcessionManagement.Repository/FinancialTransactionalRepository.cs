using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// FinancialTransactional repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IFinancialTransactionalRepository" />
    public class FinancialTransactionalRepository : IFinancialTransactionalRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinancialTransactionalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public FinancialTransactionalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public FinancialTransactional Create(FinancialTransactional model)
        {
            const string sql =
                @"INSERT [dbo].[tblFinancialTransactional] ([fkRiskGroupId], [TotalNumberOfAccounts], [AverageAccountManagementFee], [AverageMinimumMonthlyFee], [TotalChequeIssuingVolumes], [TotalChequeDepositVolumes], [TotalChequeEncashmentVolumes], [TotalChequeEncashmentValues], [TotalCashWithdrawalVolumes], [TotalCashWithdrawalValues], [AverageChequeIssuingValue], [AverageChequeIssuingPrice], [AverageChequeDepositValue], [AverageChequeDepositPrice], [AverageChequeEncashmentPrice], [AverageCashWithdrawalPrice], [LatestCrsOrMrs]) 
                VALUES (@RiskGroupId, @TotalNumberOfAccounts, @AverageAccountManagementFee, @AverageMinimumMonthlyFee, @TotalChequeIssuingVolumes, @TotalChequeDepositVolumes, @TotalChequeEncashmentVolumes, @TotalChequeEncashmentValues, @TotalCashWithdrawalVolumes, @TotalCashWithdrawalValues, @AverageChequeIssuingValue, @AverageChequeIssuingPrice, @AverageChequeDepositValue, @AverageChequeDepositPrice, @AverageChequeEncashmentPrice, @AverageCashWithdrawalPrice, @LatestCrsOrMrs) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        TotalNumberOfAccounts = model.TotalNumberOfAccounts,
                        AverageAccountManagementFee = model.AverageAccountManagementFee,
                        AverageMinimumMonthlyFee = model.AverageMinimumMonthlyFee,
                        TotalChequeIssuingVolumes = model.TotalChequeIssuingVolumes,
                        TotalChequeDepositVolumes = model.TotalChequeDepositVolumes,
                        TotalChequeEncashmentVolumes = model.TotalChequeEncashmentVolumes,
                        TotalChequeEncashmentValues = model.TotalChequeEncashmentValues,
                        TotalCashWithdrawalVolumes = model.TotalCashWithdrawalVolumes,
                        TotalCashWithdrawalValues = model.TotalCashWithdrawalValues,
                        AverageChequeIssuingValue = model.AverageChequeIssuingValue,
                        AverageChequeIssuingPrice = model.AverageChequeIssuingPrice,
                        AverageChequeDepositValue = model.AverageChequeDepositValue,
                        AverageChequeDepositPrice = model.AverageChequeDepositPrice,
                        AverageChequeEncashmentPrice = model.AverageChequeEncashmentPrice,
                        AverageCashWithdrawalPrice = model.AverageCashWithdrawalPrice,
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
        public FinancialTransactional ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialTransactional>(
                    @"SELECT [pkFinancialTransactionalId] [Id],
		                    [fkRiskGroupId] [RiskGroupId],
		                    [TotalNumberOfAccounts],
		                    [AverageAccountManagementFee],
		                    [AverageMinimumMonthlyFee],
		                    [TotalChequeIssuingVolumes],
		                    [TotalChequeDepositVolumes],
		                    [TotalChequeEncashmentVolumes],
		                    [TotalChequeEncashmentValues],
		                    [TotalCashWithdrawalVolumes],
		                    [TotalCashWithdrawalValues],
		                    [AverageChequeIssuingValue],
		                    [AverageChequeIssuingPrice],
		                    [AverageChequeDepositValue],
		                    [AverageChequeDepositPrice],
		                    [AverageChequeEncashmentPrice],
		                    [AverageCashWithdrawalPrice],
		                    [LatestCrsOrMrs] 
                    FROM [dbo].[tblFinancialTransactional] 
                    WHERE [pkFinancialTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        public IEnumerable<FinancialTransactional> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialTransactional>(
                    @"SELECT [pkFinancialTransactionalId] [Id],
		                    [fkRiskGroupId] [RiskGroupId],
		                    [TotalNumberOfAccounts],
		                    [AverageAccountManagementFee],
		                    [AverageMinimumMonthlyFee],
		                    [TotalChequeIssuingVolumes],
		                    [TotalChequeDepositVolumes],
		                    [TotalChequeEncashmentVolumes],
		                    [TotalChequeEncashmentValues],
		                    [TotalCashWithdrawalVolumes],
		                    [TotalCashWithdrawalValues],
		                    [AverageChequeIssuingValue],
		                    [AverageChequeIssuingPrice],
		                    [AverageChequeDepositValue],
		                    [AverageChequeDepositPrice],
		                    [AverageChequeEncashmentPrice],
		                    [AverageCashWithdrawalPrice],
		                    [LatestCrsOrMrs] 
                    FROM [dbo].[tblFinancialTransactional] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinancialTransactional> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialTransactional>(
                    @"SELECT [pkFinancialTransactionalId] [Id],
		                    [fkRiskGroupId] [RiskGroupId],
		                    [TotalNumberOfAccounts],
		                    [AverageAccountManagementFee],
		                    [AverageMinimumMonthlyFee],
		                    [TotalChequeIssuingVolumes],
		                    [TotalChequeDepositVolumes],
		                    [TotalChequeEncashmentVolumes],
		                    [TotalChequeEncashmentValues],
		                    [TotalCashWithdrawalVolumes],
		                    [TotalCashWithdrawalValues],
		                    [AverageChequeIssuingValue],
		                    [AverageChequeIssuingPrice],
		                    [AverageChequeDepositValue],
		                    [AverageChequeDepositPrice],
		                    [AverageChequeEncashmentPrice],
		                    [AverageCashWithdrawalPrice],
		                    [LatestCrsOrMrs] 
                    FROM [dbo].[tblFinancialTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(FinancialTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblFinancialTransactional]
                    SET [fkRiskGroupId] = @RiskGroupId,
                        [TotalNumberOfAccounts] = @TotalNumberOfAccounts,
                        [AverageAccountManagementFee] = @AverageAccountManagementFee,
                        [AverageMinimumMonthlyFee] = @AverageMinimumMonthlyFee,
                        [TotalChequeIssuingVolumes] = @TotalChequeIssuingVolumes,
                        [TotalChequeDepositVolumes] = @TotalChequeDepositVolumes,
                        [TotalChequeEncashmentVolumes] = @TotalChequeEncashmentVolumes,
                        [TotalChequeEncashmentValues] = @TotalChequeEncashmentValues,
                        [TotalCashWithdrawalVolumes] = @TotalCashWithdrawalVolumes,
                        [TotalCashWithdrawalValues] = @TotalCashWithdrawalValues,
                        [AverageChequeIssuingValue] = @AverageChequeIssuingValue,
                        [AverageChequeIssuingPrice] = @AverageChequeIssuingPrice,
                        [AverageChequeDepositValue] = @AverageChequeDepositValue,
                        [AverageChequeDepositPrice] = @AverageChequeDepositPrice,
                        [AverageChequeEncashmentPrice] = @AverageChequeEncashmentPrice,
                        [AverageCashWithdrawalPrice] = @AverageCashWithdrawalPrice,
                        [LatestCrsOrMrs] = @LatestCrsOrMrs
                    WHERE [pkFinancialTransactionalId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        TotalNumberOfAccounts = model.TotalNumberOfAccounts,
                        AverageAccountManagementFee = model.AverageAccountManagementFee,
                        AverageMinimumMonthlyFee = model.AverageMinimumMonthlyFee,
                        TotalChequeIssuingVolumes = model.TotalChequeIssuingVolumes,
                        TotalChequeDepositVolumes = model.TotalChequeDepositVolumes,
                        TotalChequeEncashmentVolumes = model.TotalChequeEncashmentVolumes,
                        TotalChequeEncashmentValues = model.TotalChequeEncashmentValues,
                        TotalCashWithdrawalVolumes = model.TotalCashWithdrawalVolumes,
                        TotalCashWithdrawalValues = model.TotalCashWithdrawalValues,
                        AverageChequeIssuingValue = model.AverageChequeIssuingValue,
                        AverageChequeIssuingPrice = model.AverageChequeIssuingPrice,
                        AverageChequeDepositValue = model.AverageChequeDepositValue,
                        AverageChequeDepositPrice = model.AverageChequeDepositPrice,
                        AverageChequeEncashmentPrice = model.AverageChequeEncashmentPrice,
                        AverageCashWithdrawalPrice = model.AverageCashWithdrawalPrice,
                        LatestCrsOrMrs = model.LatestCrsOrMrs
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(FinancialTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblFinancialTransactional] 
                            WHERE [pkFinancialTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}