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
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
using static StandardBank.ConcessionManagement.Model.BusinessLogic.Constants;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

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

        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId, string concessiontype)
        {
            return this.GetClientAccounts(riskGroupNumber, userId, concessiontype, 0);
        }

        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId, string concessiontype, int? legalEntityCustomerNumber = null)
        {
            try
            {
                IEnumerable<ClientAccount> Function()
                {

                    using (var db = _dbConnectionFactory.Connection())
                    {
                        string sql = "";

                        switch (concessiontype)
                        {
                            case Model.BusinessLogic.Constants.ConcessionType.Lending:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    rg.[pkRiskGroupId] [RiskGroupId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName] ,
                                                    prod.Description 'AccountType' 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductLending on lea.pkLegalEntityAccountId = tblProductLending.fkLegalEntityAccountId
                                                join rtblProduct prod on tblProductLending.fkProductId = prod.pkProductId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber                          
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName] ,
                                                    prod.Description 'AccountType' 
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductLending on lea.pkLegalEntityAccountId = tblProductLending.fkLegalEntityAccountId
                                                join rtblProduct prod on tblProductLending.fkProductId = prod.pkProductId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.Cash:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    rg.[pkRiskGroupId] [RiskGroupId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName] 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductCash on lea.pkLegalEntityAccountId = tblProductCash.fkLegalEntityAccountId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName] 
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductCash on lea.pkLegalEntityAccountId = tblProductCash.fkLegalEntityAccountId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.Transactional:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                rg.[pkRiskGroupId] [RiskGroupId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductTransactional on lea.pkLegalEntityAccountId = tblProductTransactional.fkLegalEntityAccountId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId][LegalEntityId],
                                                lea.[pkLegalEntityAccountId][LegalEntityAccountId],
                                                lea.[AccountNumber],
                                                le.[CustomerName]
                                            FROM[dbo].[tblLegalEntity] le
                                                JOIN[dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductTransactional on lea.pkLegalEntityAccountId = tblProductTransactional.fkLegalEntityAccountId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.BusinessOnline:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                rg.[pkRiskGroupId] [RiskGroupId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM	[dbo].[tblRiskGroup] rg
                                                JOIN	[dbo].[tblLegalEntity] le	on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN	[dbo].[tblLegalEntityAccount] lea	on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                JOIN	[dbo].[tblLegalEntityBOLUser] lebu	On lebu.fkLegalEntityAccountId = lea.pkLegalEntityAccountId
                                                Join	[dbo].[tblProductBOL] pb	On pb.fkLegalEntityBOLUserId = lebu.pkLegalEntityBOLUserId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                lea.[AccountNumber],
                                                le.[CustomerName]
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN	[dbo].[tblLegalEntityAccount] lea	on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                JOIN	[dbo].[tblLegalEntityBOLUser] lebu	On lebu.fkLegalEntityAccountId = lea.pkLegalEntityAccountId
                                                Join	[dbo].[tblProductBOL] pb	On pb.fkLegalEntityBOLUserId = lebu.pkLegalEntityBOLUserId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.Trade:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                rg.[pkRiskGroupId] [RiskGroupId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductTrade on lea.pkLegalEntityAccountId = tblProductTrade.fkLegalEntityAccountId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductTrade on lea.pkLegalEntityAccountId = tblProductTrade.fkLegalEntityAccountId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.Investment:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                rg.[pkRiskGroupId] [RiskGroupId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] ,
                                                prod.Description 'AccountType' 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductInvestment on lea.pkLegalEntityAccountId = tblProductInvestment.fkLegalEntityAccountId
                                                join rtblProduct prod on tblProductInvestment.fkProductId = prod.pkProductId
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] ,
                                                prod.Description 'AccountType' 
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                join tblProductInvestment on lea.pkLegalEntityAccountId = tblProductInvestment.fkLegalEntityAccountId
                                                join rtblProduct prod on tblProductInvestment.fkProductId = prod.pkProductId
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            case Model.BusinessLogic.Constants.ConcessionType.Glms:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    rg.[pkRiskGroupId] [RiskGroupId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName] 
                                                FROM [dbo].[tblRiskGroup] rg
                                                    JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
	                                                Join [dbo].[tblProductGlms] pg On pg.fkRiskGroupId = rg.pkRiskGroupId
                                                WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                    AND rg.[IsActive] = 1
                                                    AND le.[IsActive] = 1
                                                    AND lea.[IsActive] = 1
                                                    AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                    lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                    lea.[AccountNumber],
                                                    le.[CustomerName]
                                                FROM [dbo].[tblLegalEntity] le
                                                    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                                    Join [dbo].[tblProductGlms] pg On pg.fkLegalEntityId = le.pkLegalEntityId
                                                WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                    AND le.[IsActive] = 1
                                                    --AND lea.[IsActive] = 1
                                                    AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                            default:
                                {
                                    if (riskGroupNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                rg.[pkRiskGroupId] [RiskGroupId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM [dbo].[tblRiskGroup] rg
                                                JOIN [dbo].[tblLegalEntity] le on le.[fkRiskGroupId] = rg.[pkRiskGroupId]
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                            WHERE rg.[RiskGroupNumber] = @riskGroupNumber
                                                AND rg.[IsActive] = 1
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    else if (legalEntityCustomerNumber.HasValue && legalEntityCustomerNumber > 0)
                                    {
                                        sql =
                                            @"SELECT distinct le.[pkLegalEntityId] [LegalEntityId],
                                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                                lea.[AccountNumber],
                                                le.[CustomerName] 
                                            FROM [dbo].[tblLegalEntity] le
                                                JOIN [dbo].[tblLegalEntityAccount] lea on lea.[fkLegalEntityId] = le.[pkLegalEntityId]
                                            WHERE le.[CustomerNumber] = @legalEntityCustomerNumber
                                                AND le.[IsActive] = 1
                                                AND lea.[IsActive] = 1
                                                AND (@userId Is Null Or le.[fkUserId] = @userId)";
                                    }
                                    break;
                                }
                        }

                        IEnumerable<ClientAccount> clientAccounts = null;

                        if (riskGroupNumber > 0)
                            clientAccounts = db.Query<ClientAccount>(sql, new { riskGroupNumber, userId }, commandTimeout: Int32.MaxValue);
                        else
                            clientAccounts = db.Query<ClientAccount>(sql, new { legalEntityCustomerNumber, userId }, commandTimeout: Int32.MaxValue);

                        if (clientAccounts != null && clientAccounts.Any())
                            return clientAccounts;

                    }

                    return null;
                }

                if (_cacheManager.Exists(CacheKey.Repository.TransactionTableNumberRepository.ReadAll))
                {
                    return _cacheManager.ReturnFromCache(Function, 30,
                   CacheKey.Repository.MiscPerformanceRepository.GetClientAccounts,
                   new CacheKeyParameter(nameof(riskGroupNumber), riskGroupNumber),
                   new CacheKeyParameter(nameof(userId), userId),
                   new CacheKeyParameter(nameof(concessiontype), concessiontype));

                }
                else
                {
                    _cacheManager.Remove(CacheKey.Repository.MiscPerformanceRepository.GetClientAccounts,
                   new CacheKeyParameter(nameof(riskGroupNumber), riskGroupNumber),
                   new CacheKeyParameter(nameof(userId), userId),
                   new CacheKeyParameter(nameof(concessiontype), concessiontype));

                    return _cacheManager.ReturnFromCache(Function, 30,
                   CacheKey.Repository.MiscPerformanceRepository.GetClientAccounts,
                   new CacheKeyParameter(nameof(riskGroupNumber), riskGroupNumber),
                   new CacheKeyParameter(nameof(userId), userId),
                   new CacheKeyParameter(nameof(concessiontype), concessiontype));

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //this is a depricated function to be removed as part of the next production deployment.
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId)
        {
            IEnumerable<ClientAccount> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    if (userId.HasValue)
                    {
                        var clientAccounts = db.Query<ClientAccount>(
                            @"SELECT le.[pkLegalEntityId] [LegalEntityId],
                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                rg.[pkRiskGroupId] [RiskGroupId],
                                lea.[AccountNumber],
                                le.[CustomerName] 
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
                            @"SELECT le.[pkLegalEntityId] [LegalEntityId],
                                lea.[pkLegalEntityAccountId] [LegalEntityAccountId],
                                rg.[pkRiskGroupId] [RiskGroupId],
                                lea.[AccountNumber],
                                le.[CustomerName] 
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
                        @"SELECT pl.[pkProductLendingId] [LendingProductId],
                            p.[Description] [Product],
                            le.[CustomerName],
                            lea.[AccountNumber],
                            pl.[Limit],
                            pl.[AverageBalance],
                            pl.[LoadedMap],
                            @riskGroupName [RiskGroupName] 
                        FROM [dbo].[tblProductLending] pl
                            JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pl.[fkProductId]
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pl.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pl.[fkLegalEntityAccountId]
                        WHERE pl.[fkRiskGroupId] = @riskGroupId
                            AND p.showpricing = 1",
                        new { riskGroupId, riskGroupName },
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

        public IEnumerable<LendingProduct> GetLendingProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<LendingProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var lendingProducts = db.Query<LendingProduct>(
                        @"SELECT pl.[pkProductLendingId] [LendingProductId],
                                p.[Description] [Product],
                                le.[CustomerName],
                                lea.[AccountNumber],
                                pl.[Limit],
                                pl.[AverageBalance],
                                pl.[LoadedMap],
                                le.[CustomerName] [LegalEntityName]
                        FROM [dbo].[tblProductLending] pl
                            JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pl.[fkProductId]
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pl.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pl.[fkLegalEntityAccountId]
                        WHERE le.[pkLegalEntityId] = @legalEntityId
                            AND p.showpricing = 1",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (lendingProducts != null && lendingProducts.Any())
                        return lendingProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetLendingProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
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
                        @"SELECT pc.[pkProductCashId] [CashProductId],
                            @riskGroupName [RiskGroupName],
                            le.[CustomerName],
                            lea.[AccountNumber],
                            tn.[TariffTable],
                            pc.[Channel],
                            pc.[BpId],
                            pc.[Volume],
                            pc.[Value],
                            pc.[LoadedPrice] 
                        FROM [dbo].[tblProductCash] pc
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pc.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pc.[fkLegalEntityAccountId]
                            JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = pc.[fkTableNumberId]
                        WHERE pc.[fkRiskGroupId] = @riskGroupId",
                        new { riskGroupId, riskGroupName },
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

        public IEnumerable<CashProduct> GetCashProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<CashProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var cashProducts = db.Query<CashProduct>(
                        @"SELECT pc.[pkProductCashId] [CashProductId],
                            @legalEntityName [RiskGroupName],
                            le.[CustomerName],
                            lea.[AccountNumber],
                            tn.[TariffTable],
                            pc.[Channel],
                            pc.[BpId],
                            pc.[Volume],
                            pc.[Value],
                            pc.[LoadedPrice] 
                        FROM [dbo].[tblProductCash] pc
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pc.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pc.[fkLegalEntityAccountId]
                            JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = pc.[fkTableNumberId]
                        WHERE pc.[fkLegalEntityId] = @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (cashProducts != null && cashProducts.Any())
                        return cashProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetCashProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
        }

        public IEnumerable<BolProduct> GetBolProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<BolProduct> Function()
            {
                try
                {
                    using (var db = _dbConnectionFactory.Connection())
                    {
                        var bolProducts = db.Query<BolProduct>(
                            @"Select bol.[pkProductBOLId] 
                                [BolProductId],
                                @riskGroupName [RiskGroupName],
						        le.[CustomerName] [LegalEntity], 
                                lu.BOLUserId, 
                                bol.[LoadedRate], 
                                ch.ChargeCode, 
                                ch.[Description] [ChargeCodeDesc], 
                                ty.Description [BolProductType]
					        FROM [dbo].[tblProductBOL] bol
						        JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = bol.[fkLegalEntityId]
						        JOIN [dbo].tblLegalEntityBOLUser lu on bol.fkLegalEntityBOLUserId = lu.pkLegalEntityBOLUserId
						        JOIN [dbo].rtblBOLChargeCode ch on bol.fkChargeCodeId = ch.pkChargeCodeId
                                left join rtblBOLChargeCode cc on bol.fkChargeCodeId = cc.pkChargeCodeId
						        left join rtblBOLChargeCodeType ty on cc.fkChargeCodeTypeId = ty.pkChargeCodeTypeId
						    where bol.fkRiskGroupId =  @riskGroupId",
                            new { riskGroupId, riskGroupName },
                            commandTimeout: Int32.MaxValue);

                        if (bolProducts != null && bolProducts.Any())
                            return bolProducts;
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }



                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetBolProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        public IEnumerable<BolProduct> GetBolProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<BolProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var bolProducts = db.Query<BolProduct>(
                        @"Select bol.[pkProductBOLId] [BolProductId],
                            le.[CustomerName] [LegalEntity],
                            lu.BOLUserId,
                            bol.[LoadedRate],
                            ch.ChargeCode,
                            ch.[Description] [ChargeCodeDesc],
                            ty.Description [BolProductType]
					    FROM [dbo].[tblProductBOL] bol
						    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = bol.[fkLegalEntityId]
						    JOIN [dbo].tblLegalEntityBOLUser lu on bol.fkLegalEntityBOLUserId = lu.pkLegalEntityBOLUserId
						    JOIN [dbo].rtblBOLChargeCode ch on bol.fkChargeCodeId = ch.pkChargeCodeId
                            left join rtblBOLChargeCode cc on bol.fkChargeCodeId = cc.pkChargeCodeId
						    left join rtblBOLChargeCodeType ty on cc.fkChargeCodeTypeId = ty.pkChargeCodeTypeId
						where bol.fkLegalEntityId =  @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (bolProducts != null && bolProducts.Any())
                        return bolProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetBolProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
        }

        public IEnumerable<TradeProduct> GetTradeProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<TradeProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var tradeProduct = db.Query<TradeProduct>(
                        @"Select trade.pkProductTradeId [TradeProductId],
                            @riskGroupName [RiskGroupName],lea.[AccountNumber],
                            le.[CustomerName] [LegalEntity],
                            ty.Description [TradeProductType],
                            prod.description [TradeProductName],
                            LoadedRate
						from [tblProductTrade] trade
						    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = trade.[fkLegalEntityId]
						    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = trade.[fkLegalEntityAccountId]
						    JOIN [dbo].rtblTradeProduct prod on trade.fkTradeProductId = prod.pkTradeProductId
						    JOIN [dbo].rtblTradeProductType ty on prod.fkTradeProductTypeId = ty.pkTradeProductTypeId					
						where trade.fkRiskGroupId = @riskGroupId",
                        new { riskGroupId, riskGroupName },
                        commandTimeout: Int32.MaxValue);

                    if (tradeProduct != null && tradeProduct.Any())
                        return tradeProduct;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetTradeProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        public IEnumerable<TradeProduct> GetTradeProductsBySAPBPID(int legalEntityId, string legalEntityName)
        {
            IEnumerable<TradeProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var tradeProduct = db.Query<TradeProduct>(
                        @"Select trade.pkProductTradeId [TradeProductId],
                            lea.[AccountNumber],
                            le.[CustomerName] [LegalEntity],
                            ty.Description [TradeProductType],
                            prod.description [TradeProductName],
                            LoadedRate
						from [tblProductTrade] trade
						    JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = trade.[fkLegalEntityId]
						    JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = trade.[fkLegalEntityAccountId]
						    JOIN [dbo].rtblTradeProduct prod on trade.fkTradeProductId = prod.pkTradeProductId
						    JOIN [dbo].rtblTradeProductType ty on prod.fkTradeProductTypeId = ty.pkTradeProductTypeId					
						where trade.fkLegalEntityId =  @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (tradeProduct != null && tradeProduct.Any())
                        return tradeProduct;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetTradeProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
        }

        public IEnumerable<InvestmentProduct> GetInvestmentProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<InvestmentProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var investmentProduct = db.Query<InvestmentProduct>(
                        @"SELECT pinv.pkProductInvestmentId [InvestmentProductId],
                            p.[Description] [InvestmentProductName],
                            le.[CustomerName] [legalEntity],
                            lea.[AccountNumber],
                            pinv.[AverageBalance],
                            pinv.LoadedCustomerRate [LoadedRate],
                            @riskGroupName [RiskGroupName] 
                        FROM [dbo].tblProductInvestment pinv
                            JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pinv.[fkProductId]
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pinv.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pinv.[fkLegalEntityAccountId]
                        WHERE pinv.[fkRiskGroupId] = @riskGroupId",
                        new { riskGroupId, riskGroupName },
                        commandTimeout: Int32.MaxValue);

                    if (investmentProduct != null && investmentProduct.Any())
                        return investmentProduct;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetInvestmentProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        public IEnumerable<InvestmentProduct> GetInvestmentProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<InvestmentProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var investmentProduct = db.Query<InvestmentProduct>(
                        @"SELECT pinv.pkProductInvestmentId [InvestmentProductId],
                            p.[Description] [InvestmentProductName],
                            le.[CustomerName] [legalEntity],
                            lea.[AccountNumber],
                            pinv.[AverageBalance],
                            pinv.LoadedCustomerRate [LoadedRate]
                        FROM [dbo].tblProductInvestment pinv
                            JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pinv.[fkProductId]
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pinv.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pinv.[fkLegalEntityAccountId]
                        WHERE le.[pkLegalEntityId] = @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (investmentProduct != null && investmentProduct.Any())
                        return investmentProduct;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetInvestmentProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
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
                        @"SELECT pt.[pkProductTransactionalId] [TransactionalProductId],
                            @riskGroupName [RiskGroupName],
                            le.[CustomerName],
                            lea.[AccountNumber],
                            ttn.[TariffTable],
                            tt.[Description] [TransactionType],
                            pt.[Volume],
                            pt.[Value],
                            pt.[LoadedPrice] 
                        FROM [dbo].[tblProductTransactional] pt
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pt.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pt.[fkLegalEntityAccountId]
                            JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = pt.[fkTransactionTypeId]
                            JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = pt.[fkTransactionTableNumberId]
                        WHERE pt.[fkRiskGroupId] = @riskGroupId",
                        new { riskGroupId, riskGroupName },
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

        public IEnumerable<TransactionalProduct> GetTransactionalProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<TransactionalProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var transactionalProducts = db.Query<TransactionalProduct>(
                        @"SELECT pt.[pkProductTransactionalId] [TransactionalProductId],
                            le.[CustomerName],
                            lea.[AccountNumber],
                            ttn.[TariffTable],
                            tt.[Description] [TransactionType],
                            pt.[Volume],
                            pt.[Value],
                            pt.[LoadedPrice] 
                        FROM [dbo].[tblProductTransactional] pt
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pt.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = pt.[fkLegalEntityAccountId]
                            JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = pt.[fkTransactionTypeId]
                            JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = pt.[fkTransactionTableNumberId]
                        WHERE le.[pkLegalEntityId] = @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (transactionalProducts != null && transactionalProducts.Any())
                        return transactionalProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetTransactionalProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
        }


        public IEnumerable<GlmsProduct> GetGlmsProducts(int riskGroupId, string riskGroupName)
        {
            IEnumerable<GlmsProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var glmsProducts = db.Query<GlmsProduct>(
                        @"SELECT Distinct pglms.pkProductGlmsId [GlmsProductId],
		                    pglms.GroupType,
	                        le.[CustomerName] [legalEntity],
	                        pricing.Description pricingDescription,
	                        pglms.Spread,
		                    pglms.RateType,
                            @riskGroupName [RiskGroupName],
	                        lea.[AccountNumber]
                        FROM [dbo].tblProductGlms pglms
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = pglms.[fkLegalEntityId]
                            JOIN [dbo].tblInterestPricingCategory pricing on pricing.pkInterestPricingCategoryId= pglms.fkInterestPricingCategoryId
                            JOIN [dbo].[tblLegalEntityAccount] lea On lea.fkLegalEntityId = le.pkLegalEntityId
                        WHERE pglms.[fkRiskGroupId] = @riskGroupId",
                        new { riskGroupId, riskGroupName },
                        commandTimeout: Int32.MaxValue);

                    if (glmsProducts != null && glmsProducts.Any())
                        return glmsProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
               CacheKey.Repository.MiscPerformanceRepository.GetGlmsProducts,
                new CacheKeyParameter(nameof(riskGroupId), riskGroupId),
                new CacheKeyParameter(nameof(riskGroupName), riskGroupName));
        }

        public IEnumerable<GlmsProduct> GetGlmsProductsByLegalEntity(int legalEntityId, string legalEntityName)
        {
            IEnumerable<GlmsProduct> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    var glmsProducts = db.Query<GlmsProduct>(
                        @"SELECT Distinct pglms.pkProductGlmsId [GlmsProductId],
	                        pglms.GroupType,
	                        le.[CustomerName] [legalEntity],
	                        pricing.Description pricingDescription,
	                        pglms.Spread,
	                        pglms.RateType,
	                        lea.AccountNumber [AccountNumber]
                        FROM [dbo].tblProductGlms pglms
                            JOIN [dbo].[tblLegalEntity] le ON le.[pkLegalEntityId] = pglms.[fkLegalEntityId]
                            JOIN [dbo].tblInterestPricingCategory pricing ON pricing.pkInterestPricingCategoryId= pglms.fkInterestPricingCategoryId
	                        JOIN [dbo].[tblLegalEntityAccount] lea On lea.fkLegalEntityId = le.pkLegalEntityId
                        WHERE le.[pkLegalEntityId] = @legalEntityId",
                        new { legalEntityId, legalEntityName },
                        commandTimeout: Int32.MaxValue);

                    if (glmsProducts != null && glmsProducts.Any())
                        return glmsProducts;
                }

                return null;
            }

            return _cacheManager.ReturnFromCache(Function, 300,
                CacheKey.Repository.MiscPerformanceRepository.GetGlmsProducts,
                new CacheKeyParameter(nameof(legalEntityId), legalEntityId),
                new CacheKeyParameter(nameof(legalEntityName), legalEntityName));
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
                return db.Query<CashConcessionDetail>(
                    @"SELECT
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
                    WHERE cd.[fkConcessionId] = @concessionId  
                        and cd.Archived is null",
                    new { concessionId });
            }
        }

        /// <summary>
        /// Gets the lending concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<LendingConcessionDetail> GetLendingConcessionDetails(int concessionId)
        {
            try
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<LendingConcessionDetail>(
                        @"SELECT cd.[pkConcessionDetailId] [ConcessionDetailId], 
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
                            [UffFee],
                            [Frequency],
                            [ServiceFee],
                            [MRS_ERI] [MrsEri]
                        FROM [dbo].[tblConcessionDetail] cd
                            JOIN [dbo].[tblConcessionLending] cl on cl.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
                            JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = cd.[fkLegalEntityId]
                            JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
                            JOIN [dbo].[rtblProduct] pt on pt.[pkProductId] = cl.[fkProductTypeId]
                            LEFT JOIN [dbo].[rtblReviewFeeType] rft on rft.[pkReviewFeeTypeId] = cl.[fkReviewFeeTypeId]
                        WHERE cd.[fkConcessionId] = @concessionId 
                            and cd.Archived is null",
                        new { concessionId });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<BolConcessionDetail> GetBolConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BolConcessionDetail>(
                    @"Select cd.[pkConcessionDetailId] [ConcessionDetailId], 
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
                        bl.fkChargeCodeId,
                        bl.fkChargeCodeTypeId
                    FROM [dbo].[tblConcessionDetail] cd
                        join [dbo].[tblConcessionBol] bl on cd.pkConcessionDetailId = bl.fkConcessionDetailId
                        JOIN [dbo].[tblLegalEntityBOLUser] bo on bl.fkLegalEntityBOLUserId = bo.pkLegalEntityBOLUserId
                        left join tblLegalEntityAccount la on bo.fkLegalEntityAccountId = la.pkLegalEntityAccountId
                        left JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = la.fkLegalEntityId
                        left JOIN [dbo].rtblBOLChargeCode co on bl.fkChargeCodeId = co.pkChargeCodeId
                        left JOIN rtblBOLChargeCodeType ct on co.fkChargeCodeTypeId = ct.pkChargeCodeTypeId
                    where cd.fkConcessionId = @concessionId  
                        and cd.Archived is null",
                    new { concessionId });
            }
        }

        public IEnumerable<TradeConcessionDetail> GetTradeConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<TradeConcessionDetail>(
                    @"Select cd.[pkConcessionDetailId] [ConcessionDetailId], 
                        cd.[fkConcessionId] [ConcessionId], 
                        cd.[fkLegalEntityId] [LegalEntityId],
                        cd.[fkLegalEntityAccountId] [LegalEntityAccountId],  
                        le.[CustomerName] [LegalEntity],                    
                        [ExpiryDate], 
                        [DateApproved], 
                        [IsMismatched], 
                        [PriceExported], 
                        [PriceExportedDate],
                        AdValorem ,                   
                        Currency,
                        EstablishmentFee,
					    tr.pkConcessionTradeId [TradeConcessionDetailId],
                        lea.AccountNumber,
					    LoadedRate,
					    ApprovedRate,				
                        tp.Description TradeProduct,
					    ty.Description TradeProductType,							
                        tr.fkLegalEntityAccountId,
                        tr.fkTradeProductId,
                        tr.Communication, 
					    tr.EstablishmentFee,
					    tr.FlatFee,
					    IsNull(gb.GBBNumber,tr.GBBNumber) [GBBNumber],	
                        tr.[Max],
                        tr.[Min],
					    tr.[Term],
                        tr.fkLegalEntityGBBNumber,
					    tr.fkLegalEntityAccountId,
                        tr.[Rate]
                    FROM [dbo].[tblConcessionDetail] cd
                        left join [dbo].[tblConcessionTrade] tr on cd.pkConcessionDetailId = tr.fkConcessionDetailId
                        left JOIN [dbo].tblLegalEntityAccount lea on tr.fkLegalEntityAccountId = lea.pkLegalEntityAccountId                  
                        left JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = lea.fkLegalEntityId
					    left join tblLegalEntityGBBNumber gb on tr.fkLegalEntityGBBNumber = gb.pkLegalEntityGBBNumber
				        left JOIN rtblTradeProduct tp on tr.fkTradeProductId = tp.pkTradeProductId
                        left JOIN rtblTradeProductType ty on tp.fkTradeProductTypeId = ty.pkTradeProductTypeId
                    where cd.fkConcessionId = @concessionId  
                        and cd.Archived is null",
                    new { concessionId });
            }
        }


        public IEnumerable<InvestmentConcessionDetail> GetInvestmentConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<InvestmentConcessionDetail>(
                    @"select cd.[pkConcessionDetailId] [ConcessionDetailId], 
                        cd.[fkConcessionId] [ConcessionId], 
                        cd.[fkLegalEntityId] [LegalEntityId],
                        cd.[fkLegalEntityAccountId] [LegalEntityAccountId],  
                        le.[CustomerName] [LegalEntity],                    
                        [ExpiryDate], 
                        [DateApproved], 
                        [IsMismatched], 
                        [PriceExported], 
                        [PriceExportedDate],                  
					    pinv.pkConcessionInvestmentId [InvestmentConcessionDetailId],
                        ac.AccountNumber,
					    LoadedRate,
					    ApprovedRate,				
                        p.Description InvestmentProduct,										
                        pinv.fkLegalEntityAccountId,
                        pinv.fkProductId [productTypeId],                  
					    pinv.Balance,				
					    pinv.[Term],                
					    pinv.fkLegalEntityAccountId
                    from [dbo].[tblConcessionDetail] cd
                        left join [dbo].[tblConcessionInvestment] pinv on cd.pkConcessionDetailId = pinv.fkConcessionDetailId
                        left JOIN [dbo].tblLegalEntityAccount lea on pinv.fkLegalEntityAccountId = lea.pkLegalEntityAccountId  
                        left JOIN [dbo].[tblLegalEntity] le on le.[pkLegalEntityId] = lea.fkLegalEntityId
                        left join tblLegalEntityAccount ac on pinv.fkLegalEntityAccountId = ac.pkLegalEntityAccountId				
                        left JOIN [dbo].[rtblProduct] p on p.[pkProductId] = pinv.fkProductId
                    where cd.fkConcessionId = @concessionId  
                        and cd.Archived is null",
                    new { concessionId });
            }
        }


        public IEnumerable<GlmsConcessionDetail> GetGlmsConcessionDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                var query = db.Query<GlmsConcessionDetail>(
                    @"SELECT	Distinct cd.[pkConcessionDetailId] [ConcessionDetailId], 
		                        cd.[fkConcessionId] [ConcessionId], 
		                        le.[pkLegalEntityId] [LegalEntityId], 
		                        le.[CustomerName] [LegalEntity],                    
		                        cd.[ExpiryDate], 
		                        [DateApproved], 
		                        [IsMismatched], 
		                        [PriceExported], 
		                        [PriceExportedDate],                  
		                        cg.pkConcessionGlmsId [GlmsConcessionDetailId],
		                        pg.GroupType GlmsProduct,										
		                        gg.GroupNumber,
		                        cg.fkSlabTypeId SlabTypeId,
		                        cg.fkInterestPricingCategoryId interestPricingCategoryId,				
		                        cg.fkGroupId GlmsGroupId,
		                        cg.fkInterestTypeId InterestTypeId,
		                        ic.Description InterestPricingCategory
                    From		tblConcession c
                    Inner Join	tblConcessionDetail cd			On	cd.fkConcessionId		=	c.pkConcessionId
                    Inner Join	tblConcessionGlms cg			On	cg.fkConcessionDetailId	=	cd.pkConcessionDetailId
                    Inner join	tblProductGlms pg				on	pg.fkGroupId			=	cg.fkGroupId
											                    And	pg.fkRiskGroupId		=	c.fkRiskGroupId
                    Inner Join	tblLegalEntity le				On	le.pkLegalEntityId		=	pg.fkLegalEntityId
                    Inner join	tblGlmsGroup gg					on	gg.pkGlmsGroupId		=	cg.fkGroupId
                    Inner join	tblInterestPricingCategory ic	on	ic.pkInterestPricingCategoryId = cg.fkInterestPricingCategoryId
                    WHERE cd.fkConcessionId = @concessionId  
                    and cd.Archived is null",
                    new { concessionId });

                return query;
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
                return db.Query<TransactionalConcessionDetail>(
                    @"SELECT cd.[pkConcessionDetailId] [ConcessionDetailId], 
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
                    WHERE cd.[fkConcessionId] = @concessionId 
                        and cd.Archived is null",
                    new { concessionId });
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
                return db.Query<BusinessCentreManagementModel>(
                    @"SELECT c.[pkCentreId] [CentreId], 
	                    c.[CentreName], 
	                    c.[IsActive], 
	                    bcmtable.[BCMId] [BusinessCentreManagerId], 
	                    bcmtable.[BCM] [BusinessCentreManager], 
	                    r.[pkRegionId] [RegionId], 
	                    r.[Description] [Region], 
	                    CASE 
		                    WHEN requestortable.[AECount] IS NULL THEN 0 
		                    ELSE requestortable.[AECount] 
	                    END [RequestorCount] 
                    FROM [dbo].[tblCentre] c
	                    LEFT JOIN (
		                    SELECT u.[FirstName] + ' ' + u.[Surname] [BCM], u.[pkUserId] [BCMId], cu.[fkCentreId] 
                            FROM [dbo].[tblCentreUser] cu
		                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = cu.[fkUserId]
		                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
		                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId] and r.[RoleName] = 'BCM'
	                    ) bcmtable ON bcmtable.[fkCentreId] = c.[pkCentreId]
	                    JOIN [dbo].[rtblRegion] r ON r.[pkRegionId] = c.[fkRegionId]
	                    LEFT JOIN (
		                    SELECT count(*) [AECount], cu.[fkCentreId] 
                            FROM [dbo].[tblCentreUser] cu
		                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = cu.[fkUserId]
		                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
		                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId] and r.[RoleName] = 'Requestor'
		                    GROUP BY cu.[fkCentreId]
	                    ) requestortable ON requestortable.[fkCentreId] = c.[pkCentreId]");
            }
        }

        public BusinessCentreManagementModel GetBusinessCentreManager(int pkCentreId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BusinessCentreManagementModel>(
                    @"SELECT c.[pkCentreId] [CentreId], 
	                    c.[CentreName], 
	                    c.[IsActive], 
	                    bcmtable.[BCMId] [BusinessCentreManagerId], 
	                    bcmtable.[BCM] [BusinessCentreManager], 
	                    r.[pkRegionId] [RegionId],
	                    r.[Description] [Region]
                    FROM [dbo].[tblCentre] c
	                    LEFT JOIN (
		                    SELECT u.[FirstName] + ' ' + u.[Surname] [BCM], u.[pkUserId] [BCMId], cu.[fkCentreId] 
		                    FROM [dbo].[tblCentreUser] cu
		                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = cu.[fkUserId]
		                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
		                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId] and r.[RoleName] = 'BCM'
	                    ) bcmtable ON bcmtable.[fkCentreId] = c.[pkCentreId]
	                    JOIN [dbo].[rtblRegion] r ON r.[pkRegionId] = c.[fkRegionId]
                    where pkCentreId = @pkCentreId",
                    new { pkCentreId }).FirstOrDefault();
            }
        }
    }
}