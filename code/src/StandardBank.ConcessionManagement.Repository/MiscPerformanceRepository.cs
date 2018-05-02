using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;


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
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscPerformanceRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public MiscPerformanceRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId)
        {
            IEnumerable<ClientAccount> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    if (userId.HasValue)
                    {
                        var clientAccounts = db.Query<ClientAccount>(
                            @"SELECT le.[pkLegalEntityId] [LegalEntityId], lea.[pkLegalEntityAccountId] [LegalEntityAccountId], rg.[pkRiskGroupId] [RiskGroupId], lea.[AccountNumber], le.[CustomerName] 
                            FROM [dbo].[tblRiskGroup] rg
                            JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                            AND rg.[IsActive] = 1
                            AND le.[IsActive] = 1
                            AND lea.[IsActive] = 1
                            AND le.[fkUserId] = @userId",
                            new { riskGroupNumber, userId }, commandTimeout: Int32.MaxValue);

                        if (clientAccounts != null && clientAccounts.Any())
                            return clientAccounts;
                    }
                    else
                    {
                        var clientAccounts = db.Query<ClientAccount>(
                            @"SELECT le.[pkLegalEntityId] [LegalEntityId], lea.[pkLegalEntityAccountId] [LegalEntityAccountId], rg.[pkRiskGroupId] [RiskGroupId], lea.[AccountNumber], le.[CustomerName] 
                            FROM [dbo].[tblRiskGroup] rg
                            JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                            AND rg.[IsActive] = 1
                            AND le.[IsActive] = 1
                            AND lea.[IsActive] = 1",
                            new { riskGroupNumber }, commandTimeout: Int32.MaxValue);

                        if (clientAccounts != null && clientAccounts.Any())
                            return clientAccounts;
                    }
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 30,
                CacheKey.Repository.MiscPerformanceRepository.GetClientAccounts,
                new CacheKeyParameter(nameof(riskGroupNumber), riskGroupNumber),
                new CacheKeyParameter(nameof(userId), userId));
        }

        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<LendingProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var lendingProducts = db.Query<LendingProduct>(
                        @"SELECT pl.[pkProductLendingId] [LendingProductId], p.[Description] [Product], le.[CustomerName], lea.[AccountNumber], pl.[Limit], pl.[AverageBalance], pl.[LoadedMap], @riskGroupName [RiskGroupName] FROM [dbo].[tblProductLending] pl
                        JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pl.[fkProductId]
                        JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pl.[fkLegalEntityId]
                        JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pl.[fkLegalEntityAccountId]
                        WHERE pl.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                        commandTimeout: Int32.MaxValue);

                    if (lendingProducts != null && lendingProducts.Any())
                        return lendingProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetLendingProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        /// <summary>
        /// Gets the cash products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<CashProduct> GetCashProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<CashProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var cashProducts = db.Query<CashProduct>(
                        @"SELECT pc.[pkProductCashId] [CashProductId], @riskGroupName [RiskGroupName], le.[CustomerName], lea.[AccountNumber], tn.[TariffTable], pc.[Channel], pc.[BpId], pc.[Volume], pc.[Value], pc.[LoadedPrice] FROM [dbo].[tblProductCash] pc
                        JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pc.[fkLegalEntityId]
                        JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pc.[fkLegalEntityAccountId]
                        JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = pc.[fkTableNumberId]
                        WHERE pc.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                        commandTimeout: Int32.MaxValue);

                    if (cashProducts != null && cashProducts.Any())
                        return cashProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetCashProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        public IEnumerable<BolProduct> GetBolProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<BolProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var bolProducts = db.Query<BolProduct>(
                        @"Select bol.[pkProductBOLId] [BolProductId],@riskGroupName [RiskGroupName],
						le.[CustomerName] [LegalEntity], lu.BOLUserId, bol.[LoadedRate], ch.ChargeCode, ch.[Description] [ChargeCodeDesc], ty.Description [BolProductType]
					    FROM [dbo].[tblProductBOL] bol
						JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = bol.[fkLegalEntityId]
						JOIN [dbo].tblLegalEntityBOLUser lu on bol.fkLegalEntityBOLUserId = lu.pkLegalEntityBOLUserId
						JOIN [dbo].rtblBOLChargeCode ch on bol.fkChargeCodeId = ch.pkChargeCodeId
                        left join rtblBOLChargeCode cc on bol.fkChargeCodeId = cc.pkChargeCodeId
						left join rtblBOLChargeCodeType ty on cc.fkChargeCodeTypeId = ty.pkChargeCodeTypeId
						where bol.fkRiskGroupId =  @riskGroupId", new { riskGroupId, riskGroupName },
                        commandTimeout: Int32.MaxValue);

                    if (bolProducts != null && bolProducts.Any())
                        return bolProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetBolProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalProduct> GetTransactionalProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<TransactionalProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var transactionalProducts = db.Query<TransactionalProduct>(
                        @"SELECT pt.[pkProductTransactionalId] [TransactionalProductId], @riskGroupName [RiskGroupName], le.[CustomerName], lea.[AccountNumber], ttn.[TariffTable], tt.[Description] [TransactionType], pt.[Volume], pt.[Value], pt.[LoadedPrice] FROM [dbo].[tblProductTransactional] pt
                        JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pt.[fkLegalEntityId]
                        JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pt.[fkLegalEntityAccountId]
                        JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = pt.[fkTransactionTypeId]
                        JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = pt.[fkTransactionTableNumberId]
                        WHERE pt.[fkRiskGroupId] = @riskGroupId", new {riskGroupId, riskGroupName},
                        commandTimeout: Int32.MaxValue);

                    if (transactionalProducts != null && transactionalProducts.Any())
                        return transactionalProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetTransactionalProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        /// <summary>
        /// Gets the cash concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<CashConcessionDetail> GetCashConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CashConcessionDetail>(@"SELECT
                    cd.[pkConcessionDetailId] [ConcessionDetailId], 
                    cd.[fkConcessionId] [ConcessionId], 
                    cd.[fkLegalEntityId] [LegalEntityId], 
                    cd.[fkLegalEntityAccountId] [LegalEntityAccountId], 
                    [CustomerName], 
                    [AccountNumber], 
                    [ExpiryDate], 
                    [DateApproved], 
                    [IsMismatched], 
                    [PriceExported], 
                    [PriceExportedDate], 
                    cc.[pkConcessionCashId] [CashConcessionDetailId], 
                    ct.[Description] [Channel], 
                    cc.[fkChannelTypeId] [ChannelTypeId], 
                    null [BpId], 
                    null [Volume], 
                    null [Value], 
                    cc.[BaseRate], 
                    cc.[AdValorem], 
                    cc.[fkAccrualTypeId] [AccrualTypeId], 
                    cc.[fkTableNumberId] [TableNumberId], 
                    cc.[fkApprovedTableNumberId] [ApprovedTableNumberId], 
                    cc.[fkLoadedTableNumberId] [LoadedTableNumberId], 
                    atn.[TariffTable] [ApprovedTableNumber], 
                    ltn.[TariffTable] [LoadedTableNumber],
          
                    isnull(cast(ltn.[BaseRate] as NVARCHAR(10)),'0.00') +' + ' + isnull(cast(ltn.[AdValorem] as NVARCHAR(10)),'0.000') [displayTextLoaded],
                    isnull(cast(atn.[BaseRate] as NVARCHAR(10)),'0.00')  +' + ' + isnull(cast(atn.[AdValorem] as NVARCHAR(10)),'0.000') [displayTextApproved]

                    FROM [dbo].[tblConcessionDetail] cd
                    JOIN [dbo].[tblConcessionCash] cc on cc.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = cd.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
                    JOIN [dbo].[rtblChannelType] ct on ct.[pkChannelTypeId] = cc.[fkChannelTypeId]
                    LEFT JOIN [dbo].[rtblTableNumber] atn on atn.[pkTableNumberId] = cc.[fkApprovedTableNumberId]
                    LEFT JOIN [dbo].[rtblTableNumber] ltn on ltn.[pkTableNumberId] = cc.[fkLoadedTableNumberId]
                    WHERE cd.[fkConcessionId] = @concessionId", new {concessionId});
            }
        }

        /// <summary>
        /// Gets the lending concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<LendingConcessionDetail> GetLendingConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LendingConcessionDetail>(@"SELECT
                    cd.[pkConcessionDetailId] [ConcessionDetailId], 
                    cd.[fkConcessionId] [ConcessionId], 
                    cd.[fkLegalEntityId] [LegalEntityId], 
                    cd.[fkLegalEntityAccountId] [LegalEntityAccountId],  
                    [CustomerName], 
                    [AccountNumber], 
                    [ExpiryDate], 
                    [DateApproved], 
                    [IsMismatched], 
                    [PriceExported], 
                    [PriceExportedDate], 
                    cl.[pkConcessionLendingId] [LendingConcessionDetailId], 
                    pt.[Description] [ProductType], 
                    cl.[fkProductTypeId] [ProductTypeId], 
                    [Limit], 
                    [AverageBalance], 
                    [Term], 
                    cl.[LoadedMarginToPrime] [LoadedMap], 
                    cl.[ApprovedMarginToPrime] [ApprovedMap], 
                    cl.[MarginToPrime] [MarginAgainstPrime], 
                    [InitiationFee], 
                    rft.[Description] [ReviewFeeType], 
                    cl.[fkReviewFeeTypeId] [ReviewFeeTypeId], 
                    [ReviewFee], 
                    [UffFee]
                    FROM [dbo].[tblConcessionDetail] cd
                    JOIN [dbo].[tblConcessionLending] cl on cl.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = cd.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
                    JOIN [dbo].[rtblProduct] pt on pt.[pkProductId] = cl.[fkProductTypeId]
                    LEFT JOIN [dbo].[rtblReviewFeeType] rft on rft.[pkReviewFeeTypeId] = cl.[fkReviewFeeTypeId]
                    WHERE cd.[fkConcessionId] = @concessionId", new {concessionId});
            }
        }

        public IEnumerable<BolConcessionDetail> GetBolConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BolConcessionDetail>(@"Select cd.[pkConcessionDetailId] [ConcessionDetailId], 
                    cd.[fkConcessionId] [ConcessionId], 
                    cd.[fkLegalEntityId] [LegalEntityId], 
                    cd.[fkLegalEntityAccountId] [LegalEntityAccountId],  
                    le.[CustomerName] [LegalEntity],                    
                    [ExpiryDate], 
                    [DateApproved], 
                    [IsMismatched], 
                    [PriceExported], 
                    [PriceExportedDate],
					bl.pkConcessionBolId [BolConcessionDetailId],
					LoadedRate,
					ApprovedRate,
					co.ChargeCode [ChargeCode],
                    ct.Description [ChargeCodeType],
					co.Description [ChargeCodeDesc],
					co.Length [ChargeCodeLength],
					bo.BOLUserId,
                    bl.fkLegalEntityBOLUserId,
                    bl.fkChargeCodeId
                    FROM [dbo].[tblConcessionDetail] cd
                    join [dbo].[tblConcessionBol] bl on cd.pkConcessionDetailId = bl.fkConcessionDetailId
                    JOIN [dbo].[tblLegalEntityBOLUser] bo on bl.fkLegalEntityBOLUserId = bo.pkLegalEntityBOLUserId
                   left join tblLegalEntityAccount la on bo.fkLegalEntityAccountId = la.pkLegalEntityAccountId
                    left JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = la.fkLegalEntityId
                    JOIN [dbo].rtblBOLChargeCode co on bl.fkChargeCodeId = co.pkChargeCodeId
                    JOIN rtblBOLChargeCodeType ct on co.fkChargeCodeTypeId = ct.pkChargeCodeTypeId
                    where cd.fkConcessionId = @concessionId", new { concessionId });
            }
        }

        /// <summary>
        /// Gets the transactional concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalConcessionDetail> GetTransactionalConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<TransactionalConcessionDetail>(@"SELECT
                    cd.[pkConcessionDetailId] [ConcessionDetailId], 
                    cd.[fkConcessionId] [ConcessionId], 
                    cd.[fkLegalEntityId] [LegalEntityId], 
                    cd.[fkLegalEntityAccountId] [LegalEntityAccountId],  
                    [CustomerName], 
                    [AccountNumber], 
                    [ExpiryDate], 
                    [DateApproved], 
                    [IsMismatched], 
                    [PriceExported], 
                    [PriceExportedDate], 
                    ct.[pkConcessionTransactionalId] [TransactionalConcessionDetailId], 
                    tt.[Description] [TransactionType], 
                    ct.[fkTransactionTypeId] [TransactionTypeId], 
                    null [Volume], 
                    null [Value], 
                    ct.[AdValorem], 
                    ct.[Fee], 
                    ct.[fkTransactionTableNumberId] [TransactionTableNumberId], 
                    null [LoadedPrice], 
                    null [ApprovedPrice], 
                    ct.[fkApprovedTransactionTableNumberId] [ApprovedTransactionTableNumberId], 
                    ct.[fkLoadedTransactionTableNumberId] [LoadedTransactionTableNumberId], 
                    atn.[TariffTable] [ApprovedTableNumber], 
                    ltn.[TariffTable] [LoadedTableNumber],                  

                    isnull(cast(ltn.[fee] as NVARCHAR(10)),'0.00') +' + ' + isnull(cast(ltn.[AdValorem] as NVARCHAR(10)),'0.000') [displayTextLoaded],
                    isnull(cast(atn.[fee] as NVARCHAR(10)),'0.00')  +' + ' + isnull(cast(atn.[AdValorem] as NVARCHAR(10)),'0.000') [displayTextApproved]

                    FROM [dbo].[tblConcessionDetail] cd
                    JOIN [dbo].[tblConcessionTransactional] ct on ct.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
                    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = cd.[fkLegalEntityId]
                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
                    JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = ct.[fkTransactionTypeId]
                    LEFT JOIN [dbo].[rtblTransactionTableNumber] atn on atn.[pkTransactionTableNumberId] = ct.[fkApprovedTransactionTableNumberId]
                    LEFT JOIN [dbo].[rtblTransactionTableNumber] ltn on ltn.[pkTransactionTableNumberId] = ct.[fkLoadedTransactionTableNumberId]
                    WHERE cd.[fkConcessionId] = @concessionId", new { concessionId });
            }
        }

        /// <summary>
        /// Gets the business centre management models.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessCentreManagementModel> GetBusinessCentreManagementModels()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BusinessCentreManagementModel>(@"SELECT
                    c.[pkCentreId] [CentreId], c.[CentreName], c.[IsActive], bcmtable.[BCMId] [BusinessCentreManagerId], bcmtable.[BCM] [BusinessCentreManager], r.[pkRegionId] [RegionId], r.[Description] [Region], 
                    CASE WHEN requestortable.[AECount] IS NULL THEN 0 ELSE requestortable.[AECount] END [RequestorCount] 
                    FROM [dbo].[tblCentre] c
                    LEFT JOIN (
                    SELECT u.[FirstName] + ' ' + u.[Surname] [BCM], u.[pkUserId] [BCMId], cu.[fkCentreId] FROM [dbo].[tblCentreUser] cu
                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = cu.[fkUserId]
                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId] and r.[RoleName] = 'BCM') bcmtable ON bcmtable.[fkCentreId] = c.[pkCentreId]
                    JOIN [dbo].[rtblRegion] r ON r.[pkRegionId] = c.[fkRegionId]
                    LEFT JOIN (
                    SELECT count(*) [AECount], cu.[fkCentreId] FROM [dbo].[tblCentreUser] cu
                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = cu.[fkUserId]
                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId] and r.[RoleName] = 'Requestor'
                    GROUP BY cu.[fkCentreId]) requestortable ON requestortable.[fkCentreId] = c.[pkCentreId]");
            }
        }
    }
}