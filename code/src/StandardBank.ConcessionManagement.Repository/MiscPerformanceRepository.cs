using System;
using System.Collections.Generic;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Miscellaneous repository methods to help with performance 
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IMiscPerformanceRepository" />
    public class MiscPerformanceRepository : IMiscPerformanceRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscPerformanceRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        public MiscPerformanceRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ClientAccount>(
                    @"SELECT le.[pkLegalEntityId] [LegalEntityId], lea.[pkLegalEntityAccountId] [LegalEntityAccountId], rg.[pkRiskGroupId] [RiskGroupId], lea.[AccountNumber], le.[CustomerName] 
                    FROM [dbo].[tblRiskGroup] rg
                    JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                    WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                    AND rg.[IsActive] = 1
                    AND le.[IsActive] = 1
                    AND lea.[IsActive] = 1",
                    new {riskGroupNumber}, commandTimeout: Int32.MaxValue);
            }
        }

        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LendingProduct>(
                    @"SELECT pl.[pkProductLendingId] [LendingProductId], p.[Description] [Product], le.[CustomerName], lea.[AccountNumber], pl.[Limit], pl.[AverageBalance], pl.[LoadedMap], @riskGroupName [RiskGroupName] FROM [dbo].[tblProductLending] pl
                    JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pl.[fkProductId]
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pl.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pl.[fkLegalEntityAccountId]
                    WHERE pl.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                    commandTimeout: Int32.MaxValue);
            }
        }

        /// <summary>
        /// Gets the cash products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<CashProduct> GetCashProducts(int riskGroupId, string riskGroupName)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CashProduct>(
                    @"SELECT pc.[pkProductCashId] [CashProductId], @riskGroupName [RiskGroupName], le.[CustomerName], lea.[AccountNumber], tn.[TariffTable], pc.[Channel], pc.[BpId], pc.[Volume], pc.[Value], pc.[LoadedPrice] FROM [dbo].[tblProductCash] pc
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pc.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pc.[fkLegalEntityAccountId]
                    JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = pc.[fkTableNumberId]
                    WHERE pc.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                    commandTimeout: Int32.MaxValue);
            }
        }

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalProduct> GetTransactionalProducts(int riskGroupId, string riskGroupName)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<TransactionalProduct>(
                    @"SELECT pt.[pkProductTransactionalId] [TransactionalProductId], @riskGroupName [RiskGroupName], le.[CustomerName], lea.[AccountNumber], ttn.[TariffTable], tt.[Description] [TransactionType], pt.[Volume], pt.[Value], pt.[LoadedPrice] FROM [dbo].[tblProductTransactional] pt
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pt.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pt.[fkLegalEntityAccountId]
                    JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = pt.[fkTransactionTypeId]
                    JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = pt.[fkTransactionTableNumberId]
                    WHERE pt.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                    commandTimeout: Int32.MaxValue);
            }
        }
    }
}