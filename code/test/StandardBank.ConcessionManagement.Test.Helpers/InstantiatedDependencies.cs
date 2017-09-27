using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.BusinessLogic;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Repository;
using StandardBank.ConcessionManagement.UI.Extension;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Instantiated dependencies
    /// </summary>
    public static class InstantiatedDependencies
    {
        /// <summary>
        /// Mapper configuration provider
        /// </summary>
        private static readonly IConfigurationProvider MapperConfigurationProvider =
            new MapperConfiguration(_ => _.AddProfile<MappingProfile>());

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper = new Mapper(MapperConfigurationProvider);

        /// <summary>
        /// The configuration data
        /// </summary>
        public static IConfigurationData ConfigurationData =
            new ConfigurationData
            {
                ConnectionString = Configuration.ConnectionString,
                DatabaseType =
                    Configuration.DatabaseType == "SqlServer" ? DatabaseType.SqlServer : DatabaseType.SqlLite,
                LetterTemplatePath = @"C:\Temp"
            };

        /// <summary>
        /// The database connection
        /// </summary>
        private static readonly IDbConnectionFactory DbConnection = new DbConnectionFactory(ConfigurationData);

        /// <summary>
        /// The cache manager
        /// </summary>
        public static ICacheManager CacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));

        /// <summary>
        /// The site helper
        /// </summary>
        public static ISiteHelper SiteHelper = new FakeSiteHelper();

        /// <summary>
        /// The ConcessionDetail repository
        /// </summary>
        public static IConcessionDetailRepository ConcessionDetailRepository =
            new ConcessionDetailRepository(DbConnection);

        /// <summary>
        /// The AccrualType repository
        /// </summary>
        public static IAccrualTypeRepository AccrualTypeRepository =
            new AccrualTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ExceptionLog repository
        /// </summary>
        public static IExceptionLogRepository ExceptionLogRepository = new ExceptionLogRepository(DbConnection);

        /// <summary>
        /// The AdValorem repository
        /// </summary>
        public static IAdValoremRepository AdValoremRepository = new AdValoremRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ApprovalType repository
        /// </summary>
        public static IApprovalTypeRepository ApprovalTypeRepository =
            new ApprovalTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The BaseRate repository
        /// </summary>
        public static IBaseRateRepository BaseRateRepository = new BaseRateRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ChannelType repository
        /// </summary>
        public static IChannelTypeRepository ChannelTypeRepository =
            new ChannelTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ConcessionType repository
        /// </summary>
        public static IConcessionTypeRepository ConcessionTypeRepository =
            new ConcessionTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ConditionProduct repository
        /// </summary>
        public static IConditionProductRepository ConditionProductRepository =
            new ConditionProductRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ConditionType repository
        /// </summary>
        public static IConditionTypeRepository ConditionTypeRepository =
            new ConditionTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The MarketSegment repository
        /// </summary>
        public static IMarketSegmentRepository MarketSegmentRepository =
            new MarketSegmentRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Product repository
        /// </summary>
        public static IProductRepository ProductRepository = new ProductRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Province repository
        /// </summary>
        public static IProvinceRepository ProvinceRepository = new ProvinceRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ReviewFeeType repository
        /// </summary>
        public static IReviewFeeTypeRepository ReviewFeeTypeRepository =
            new ReviewFeeTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Role repository
        /// </summary>
        public static IRoleRepository RoleRepository = new RoleRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Status repository
        /// </summary>
        public static IStatusRepository StatusRepository = new StatusRepository(DbConnection, CacheManager);

        /// <summary>
        /// The SubStatus repository
        /// </summary>
        public static ISubStatusRepository SubStatusRepository = new SubStatusRepository(DbConnection, CacheManager);

        /// <summary>
        /// The TransactionGroup repository
        /// </summary>
        public static ITransactionGroupRepository TransactionGroupRepository =
            new TransactionGroupRepository(DbConnection, CacheManager);

        /// <summary>
        /// The TransactionType repository
        /// </summary>
        public static ITransactionTypeRepository TransactionTypeRepository =
            new TransactionTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ReferenceTypeRepository repository
        /// </summary>
        public static IReferenceTypeRepository ReferenceTypeRepository =
            new ReferenceTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The BolUser repository
        /// </summary>
        public static IBolUserRepository BolUserRepository = new BolUserRepository(DbConnection);

        /// <summary>
        /// The BusinesOnlineTransactionType repository
        /// </summary>
        public static IBusinesOnlineTransactionTypeRepository BusinesOnlineTransactionTypeRepository =
            new BusinesOnlineTransactionTypeRepository(DbConnection);

        /// <summary>
        /// The Centre repository
        /// </summary>
        public static ICentreRepository CentreRepository = new CentreRepository(DbConnection);

        /// <summary>
        /// The CentreBusinessManager repository
        /// </summary>
        public static ICentreBusinessManagerRepository CentreBusinessManagerRepository =
            new CentreBusinessManagerRepository(DbConnection);

        /// <summary>
        /// The CentreUser repository
        /// </summary>
        public static ICentreUserRepository CentreUserRepository = new CentreUserRepository(DbConnection);

        /// <summary>
        /// The ChannelTypeBaseRate repository
        /// </summary>
        public static IChannelTypeBaseRateRepository ChannelTypeBaseRateRepository =
            new ChannelTypeBaseRateRepository(DbConnection);

        /// <summary>
        /// The Concession repository
        /// </summary>
        public static IConcessionRepository ConcessionRepository = new ConcessionRepository(DbConnection);

        /// <summary>
        /// The ConcessionAccount repository
        /// </summary>
        public static IConcessionAccountRepository ConcessionAccountRepository =
            new ConcessionAccountRepository(DbConnection);

        /// <summary>
        /// The ConcessionApproval repository
        /// </summary>
        public static IConcessionApprovalRepository ConcessionApprovalRepository =
            new ConcessionApprovalRepository(DbConnection);

        /// <summary>
        /// The ConcessionBol repository
        /// </summary>
        public static IConcessionBolRepository ConcessionBolRepository =
            new ConcessionBolRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionCash repository
        /// </summary>
        public static IConcessionCashRepository ConcessionCashRepository =
            new ConcessionCashRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionComment repository
        /// </summary>
        public static IConcessionCommentRepository ConcessionCommentRepository =
            new ConcessionCommentRepository(DbConnection);

        /// <summary>
        /// The ConcessionCondition repository
        /// </summary>
        public static IConcessionConditionRepository ConcessionConditionRepository =
            new ConcessionConditionRepository(DbConnection);

        /// <summary>
        /// The ConcessionInvestment repository
        /// </summary>
        public static IConcessionInvestmentRepository ConcessionInvestmentRepository =
            new ConcessionInvestmentRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionLending repository
        /// </summary>
        public static IConcessionLendingRepository ConcessionLendingRepository =
            new ConcessionLendingRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionMas repository
        /// </summary>
        public static IConcessionMasRepository ConcessionMasRepository =
            new ConcessionMasRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionTrade repository
        /// </summary>
        public static IConcessionTradeRepository ConcessionTradeRepository =
            new ConcessionTradeRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConcessionTransactional repository
        /// </summary>
        public static IConcessionTransactionalRepository ConcessionTransactionalRepository =
            new ConcessionTransactionalRepository(DbConnection, ConcessionDetailRepository);

        /// <summary>
        /// The ConditionTypeProduct repository
        /// </summary>
        public static IConditionTypeProductRepository ConditionTypeProductRepository =
            new ConditionTypeProductRepository(DbConnection);

        /// <summary>
        /// The LegalEntity repository
        /// </summary>
        public static ILegalEntityRepository LegalEntityRepository = new LegalEntityRepository(DbConnection);

        /// <summary>
        /// The LegalEntityAccount repository
        /// </summary>
        public static ILegalEntityAccountRepository LegalEntityAccountRepository =
            new LegalEntityAccountRepository(DbConnection);

        /// <summary>
        /// The RiskGroup repository
        /// </summary>
        public static IRiskGroupRepository RiskGroupRepository = new RiskGroupRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ScenarioManagerToolDeal repository
        /// </summary>
        public static IScenarioManagerToolDealRepository ScenarioManagerToolDealRepository =
            new ScenarioManagerToolDealRepository(DbConnection);

        /// <summary>
        /// The User repository
        /// </summary>
        public static IUserRepository UserRepository = new UserRepository(DbConnection);

        /// <summary>
        /// The UserRole repository
        /// </summary>
        public static IUserRoleRepository UserRoleRepository = new UserRoleRepository(DbConnection);

        /// <summary>
        /// The Period repository
        /// </summary>
        public static IPeriodRepository PeriodRepository = new PeriodRepository(DbConnection, CacheManager);

        /// <summary>
        /// The PeriodType repository
        /// </summary>
        public static IPeriodTypeRepository PeriodTypeRepository = new PeriodTypeRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Region repository
        /// </summary>
        public static IRegionRepository RegionRepository = new RegionRepository(DbConnection, CacheManager);

        /// <summary>
        /// The UserRegion repository
        /// </summary>
        public static IUserRegionRepository UserRegionRepository = new UserRegionRepository(DbConnection);

        /// <summary>
        /// The TableNumber repository
        /// </summary>
        public static ITableNumberRepository TableNumberRepository =
            new TableNumberRepository(DbConnection, CacheManager);

        /// <summary>
        /// The Relationship repository
        /// </summary>
        public static IRelationshipRepository RelationshipRepository =
            new RelationshipRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ConcessionRelationship repository
        /// </summary>
        public static IConcessionRelationshipRepository ConcessionRelationshipRepository =
            new ConcessionRelationshipRepository(DbConnection);

        /// <summary>
        /// The audit repository
        /// </summary>
        public static IAuditRepository AuditRepository = new AuditRepository(DbConnection, new XmlMarshaller());

        /// <summary>
        /// The ProductLending repository
        /// </summary>
        public static IProductLendingRepository ProductLendingRepository = new ProductLendingRepository(DbConnection);

        /// <summary>
        /// The FinancialLending repository
        /// </summary>
        public static IFinancialLendingRepository FinancialLendingRepository =
            new FinancialLendingRepository(DbConnection);

        /// <summary>
        /// The FinancialCash repository
        /// </summary>
        public static IFinancialCashRepository FinancialCashRepository = new FinancialCashRepository(DbConnection);

        /// <summary>
        /// The ProductCash repository
        /// </summary>
        public static IProductCashRepository ProductCashRepository = new ProductCashRepository(DbConnection);

        /// <summary>
        /// The FinancialTransactional repository
        /// </summary>
        public static IFinancialTransactionalRepository FinancialTransactionalRepository =
            new FinancialTransactionalRepository(DbConnection);

        /// <summary>
        /// The ProductTransactional repository
        /// </summary>
        public static IProductTransactionalRepository ProductTransactionalRepository =
            new ProductTransactionalRepository(DbConnection);

        /// <summary>
        /// The LoadedPriceCash repository
        /// </summary>
        public static ILoadedPriceCashRepository LoadedPriceCashRepository =
            new LoadedPriceCashRepository(DbConnection);

        /// <summary>
        /// The LoadedPriceLending repository
        /// </summary>
        public static ILoadedPriceLendingRepository LoadedPriceLendingRepository =
            new LoadedPriceLendingRepository(DbConnection);

        /// <summary>
        /// The LoadedPriceTransactional repository
        /// </summary>
        public static ILoadedPriceTransactionalRepository LoadedPriceTransactionalRepository =
            new LoadedPriceTransactionalRepository(DbConnection);

        /// <summary>
        /// The approval workflow repository
        /// </summary>
        public static IApprovalWorkflowRepository ApprovalWorkflowRepository =
            new ApprovalWorkflowRepository(DbConnection, CacheManager);

        /// <summary>
        /// The FinancialInvestment repository
        /// </summary>
        public static IFinancialInvestmentRepository FinancialInvestmentRepository =
            new FinancialInvestmentRepository(DbConnection);

        /// <summary>
        /// The ProductInvestment repository
        /// </summary>
        public static IProductInvestmentRepository ProductInvestmentRepository =
            new ProductInvestmentRepository(DbConnection);

        /// <summary>
        /// The concession inbox view repository
        /// </summary>
        public static IConcessionInboxViewRepository ConcessionInboxViewRepository =
            new ConcessionInboxViewRepository(DbConnection);

        /// <summary>
        /// The look up table manager
        /// </summary>
        public static ILookupTableManager LookupTableManager = new LookupTableManager(StatusRepository,
            SubStatusRepository, ReferenceTypeRepository, MarketSegmentRepository, ProvinceRepository,
            ConcessionTypeRepository, ProductRepository, ReviewFeeTypeRepository, PeriodRepository,
            PeriodTypeRepository, ConditionTypeRepository, Mapper, ConditionProductRepository,
            ConditionTypeProductRepository, AccrualTypeRepository, ChannelTypeRepository, TransactionTypeRepository,
            TableNumberRepository, RelationshipRepository, RoleRepository, CentreRepository, RegionRepository);

        /// <summary>
        /// The rule manager
        /// </summary>
        public static IRuleManager RuleManager = new RuleManager(LookupTableManager, ConcessionRelationshipRepository,
            ConcessionRepository, ConcessionLendingRepository);

        /// <summary>
        /// The user manager
        /// </summary>
        public static IUserManager UserManager = new UserManager(CacheManager, LookupTableManager, UserRepository,
            UserRoleRepository,
            RoleRepository, UserRegionRepository, RegionRepository, CentreRepository, CentreUserRepository, Mapper);

        /// <summary>
        /// The concession manager
        /// </summary>
        public static IConcessionManager ConcessionManager =
            new ConcessionManager(ConcessionRepository, LookupTableManager, LegalEntityRepository, RiskGroupRepository,
                Mapper, ConcessionConditionRepository, LegalEntityAccountRepository, ConcessionCommentRepository,
                ConcessionLendingRepository, MarketSegmentRepository, ConcessionCashRepository,
                ConcessionTransactionalRepository, ConcessionRelationshipRepository, AuditRepository, UserManager,
                RuleManager, ConcessionInboxViewRepository);

        /// <summary>
        /// The pricing manager
        /// </summary>
        public static IPricingManager PricingManager = new PricingManager(RiskGroupRepository, Mapper);

        /// <summary>
        /// The lending manager
        /// </summary>
        public static ILendingManager LendingManager = new LendingManager(PricingManager, ConcessionManager,
            LegalEntityRepository, ConcessionLendingRepository, Mapper, LegalEntityAccountRepository,
            ProductLendingRepository, FinancialLendingRepository, LookupTableManager, LoadedPriceLendingRepository);

        /// <summary>
        /// The transactional manager
        /// </summary>
        public static ITransactionalManager TransactionalManager =
            new TransactionalManager(PricingManager, ConcessionManager, ConcessionTransactionalRepository,
                LegalEntityRepository, LegalEntityAccountRepository, Mapper, LookupTableManager,
                FinancialTransactionalRepository, ProductTransactionalRepository, LoadedPriceTransactionalRepository);

        /// <summary>
        /// The cash manager
        /// </summary>
        public static ICashManager CashManager = new CashManager(PricingManager, ConcessionManager,
            ConcessionCashRepository, LegalEntityRepository, Mapper, LegalEntityAccountRepository,
            FinancialCashRepository, ProductCashRepository, LookupTableManager, LoadedPriceCashRepository);
    }
}
