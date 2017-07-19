using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Instantiated dependencies
    /// </summary>
    public static class InstantiatedDependencies
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        public static IConfigurationData ConfigurationData = new ConfigurationData(Configuration.ConnectionString);

        /// <summary>
        /// The cache manager
        /// </summary>
        public static ICacheManager CacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));

        /// <summary>
        /// Concession count repository
        /// </summary>
        public static IConcessionCountRepository ConcessionCountRepository = new ConcessionCountRepository();

        /// <summary>
        /// Authorizing user repository
        /// </summary>
        public static IAuthorizingUserRepository AuthorizingUserRepository = new AuthorizingUserRepository(ConfigurationData);

        /// <summary>
        /// The SMTRawData repository
        /// </summary>
        public static ISMTRawDataRepository SMTRawDataRepository = new SMTRawDataRepository(ConfigurationData);

        /// <summary>
        /// The AdValorem repository
        /// </summary>
        public static IAdValoremRepository AdValoremRepository = new AdValoremRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ApprovalType repository
        /// </summary>
        public static IApprovalTypeRepository ApprovalTypeRepository = new ApprovalTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The BaseRate repository
        /// </summary>
        public static IBaseRateRepository BaseRateRepository = new BaseRateRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ChannelType repository
        /// </summary>
        public static IChannelTypeRepository ChannelTypeRepository = new ChannelTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ConcessionType repository
        /// </summary>
        public static IConcessionTypeRepository ConcessionTypeRepository = new ConcessionTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ConditionProduct repository
        /// </summary>
        public static IConditionProductRepository ConditionProductRepository = new ConditionProductRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ConditionType repository
        /// </summary>
        public static IConditionTypeRepository ConditionTypeRepository = new ConditionTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The MarketSegment repository
        /// </summary>
        public static IMarketSegmentRepository MarketSegmentRepository = new MarketSegmentRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The Product repository
        /// </summary>
        public static IProductRepository ProductRepository = new ProductRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The Province repository
        /// </summary>
        public static IProvinceRepository ProvinceRepository = new ProvinceRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ReviewFeeType repository
        /// </summary>
        public static IReviewFeeTypeRepository ReviewFeeTypeRepository = new ReviewFeeTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The Role repository
        /// </summary>
        public static IRoleRepository RoleRepository = new RoleRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The Status repository
        /// </summary>
        public static IStatusRepository StatusRepository = new StatusRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The SubStatus repository
        /// </summary>
        public static ISubStatusRepository SubStatusRepository = new SubStatusRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The TransactionGroup repository
        /// </summary>
        public static ITransactionGroupRepository TransactionGroupRepository = new TransactionGroupRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The TransactionType repository
        /// </summary>
        public static ITransactionTypeRepository TransactionTypeRepository = new TransactionTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The ReferenceTypeRepository repository
        /// </summary>
        public static IReferenceTypeRepository ReferenceTypeRepository = new ReferenceTypeRepository(ConfigurationData, CacheManager);

        /// <summary>
        /// The BolUser repository
        /// </summary>
        public static IBolUserRepository BolUserRepository = new BolUserRepository(ConfigurationData);

        /// <summary>
        /// The BusinesOnlineTransactionType repository
        /// </summary>
        public static IBusinesOnlineTransactionTypeRepository BusinesOnlineTransactionTypeRepository = new BusinesOnlineTransactionTypeRepository(ConfigurationData);

        /// <summary>
        /// The Centre repository
        /// </summary>
        public static ICentreRepository CentreRepository = new CentreRepository(ConfigurationData);

        /// <summary>
        /// The CentreBusinessManager repository
        /// </summary>
        public static ICentreBusinessManagerRepository CentreBusinessManagerRepository = new CentreBusinessManagerRepository(ConfigurationData);

        /// <summary>
        /// The CentreUser repository
        /// </summary>
        public static ICentreUserRepository CentreUserRepository = new CentreUserRepository(ConfigurationData);

        /// <summary>
        /// The ChannelTypeBaseRate repository
        /// </summary>
        public static IChannelTypeBaseRateRepository ChannelTypeBaseRateRepository = new ChannelTypeBaseRateRepository(ConfigurationData);

        /// <summary>
        /// The Concession repository
        /// </summary>
        public static IConcessionRepository ConcessionRepository = new ConcessionRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionAccount repository
        /// </summary>
        public static IConcessionAccountRepository ConcessionAccountRepository = new ConcessionAccountRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionApproval repository
        /// </summary>
        public static IConcessionApprovalRepository ConcessionApprovalRepository = new ConcessionApprovalRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionBol repository
        /// </summary>
        public static IConcessionBolRepository ConcessionBolRepository = new ConcessionBolRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionCash repository
        /// </summary>
        public static IConcessionCashRepository ConcessionCashRepository = new ConcessionCashRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionComment repository
        /// </summary>
        public static IConcessionCommentRepository ConcessionCommentRepository = new ConcessionCommentRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionCondition repository
        /// </summary>
        public static IConcessionConditionRepository ConcessionConditionRepository = new ConcessionConditionRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionInvestment repository
        /// </summary>
        public static IConcessionInvestmentRepository ConcessionInvestmentRepository = new ConcessionInvestmentRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionLending repository
        /// </summary>
        public static IConcessionLendingRepository ConcessionLendingRepository = new ConcessionLendingRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionMas repository
        /// </summary>
        public static IConcessionMasRepository ConcessionMasRepository = new ConcessionMasRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionRemovalRequest repository
        /// </summary>
        public static IConcessionRemovalRequestRepository ConcessionRemovalRequestRepository = new ConcessionRemovalRequestRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionTrade repository
        /// </summary>
        public static IConcessionTradeRepository ConcessionTradeRepository = new ConcessionTradeRepository(ConfigurationData);

        /// <summary>
        /// The ConcessionTransactional repository
        /// </summary>
        public static IConcessionTransactionalRepository ConcessionTransactionalRepository = new ConcessionTransactionalRepository(ConfigurationData);

        /// <summary>
        /// The ConditionTypeProduct repository
        /// </summary>
        public static IConditionTypeProductRepository ConditionTypeProductRepository = new ConditionTypeProductRepository(ConfigurationData);

        /// <summary>
        /// The LegalEntity repository
        /// </summary>
        public static ILegalEntityRepository LegalEntityRepository = new LegalEntityRepository(ConfigurationData);

        /// <summary>
        /// The LegalEntityAccount repository
        /// </summary>
        public static ILegalEntityAccountRepository LegalEntityAccountRepository = new LegalEntityAccountRepository(ConfigurationData);

        /// <summary>
        /// The RiskGroup repository
        /// </summary>
        public static IRiskGroupRepository RiskGroupRepository = new RiskGroupRepository(ConfigurationData);

        /// <summary>
        /// The ScenarioManagerToolDeal repository
        /// </summary>
        public static IScenarioManagerToolDealRepository ScenarioManagerToolDealRepository = new ScenarioManagerToolDealRepository(ConfigurationData);

        /// <summary>
        /// The User repository
        /// </summary>
        public static IUserRepository UserRepository = new UserRepository(ConfigurationData);

        /// <summary>
        /// The UserRole repository
        /// </summary>
        public static IUserRoleRepository UserRoleRepository = new UserRoleRepository(ConfigurationData);
    }
}
