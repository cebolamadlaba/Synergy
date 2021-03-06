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
            new ConditionTypeProductRepository(DbConnection, CacheManager);

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
        /// The RoleSubRole repository
        /// </summary>
        public static IRoleSubRoleRepository RoleSubRoleRepository = new RoleSubRoleRepository(DbConnection);

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
        /// The concession condition view repository
        /// </summary>
        public static IConcessionConditionViewRepository ConcessionConditionViewRepository =
            new ConcessionConditionViewRepository(DbConnection);

        /// <summary>
        /// The TransactionTableNumber repository
        /// </summary>
        public static ITransactionTableNumberRepository TransactionTableNumberRepository =
            new TransactionTableNumberRepository(DbConnection, CacheManager);


        /// <summary>
        /// The SapDataImport repository
        /// </summary>
        public static ISapDataImportRepository SapDataImportRepository = new SapDataImportRepository(DbConnection);

        /// <summary>
        /// The SapDataImportConfiguration repository
        /// </summary>
        public static ISapDataImportConfigurationRepository SapDataImportConfigurationRepository =
            new SapDataImportConfigurationRepository(DbConnection);

        /// <summary>
        /// The ChannelTypeImport repository
        /// </summary>
        public static IChannelTypeImportRepository ChannelTypeImportRepository =
            new ChannelTypeImportRepository(DbConnection, CacheManager);

        /// <summary>
        /// The ProductImport repository
        /// </summary>
        public static IProductImportRepository ProductImportRepository =
            new ProductImportRepository(DbConnection, CacheManager);

        /// <summary>
        /// The TransactionTypeImport repository
        /// </summary>
        public static ITransactionTypeImportRepository TransactionTypeImportRepository =
            new TransactionTypeImportRepository(DbConnection, CacheManager);

        /// <summary>
        /// The misc performance repository
        /// </summary>
        public static IMiscPerformanceRepository MiscPerformanceRepository =
            new MiscPerformanceRepository(DbConnection, CacheManager);

        /// <summary>
        /// The AccountExecutiveAssistant repository
        /// </summary>
        public static IAccountExecutiveAssistantRepository AccountExecutiveAssistantRepository =
            new AccountExecutiveAssistantRepository(DbConnection);



        public static IPrimeRateRepository PrimeRateRepository =
        new PrimeRateRepository(DbConnection);

        /// <summary>
        /// The look up table manager
        /// </summary>
        public static ILookupTableManager LookupTableManager = new LookupTableManager(StatusRepository,
            SubStatusRepository, ReferenceTypeRepository, MarketSegmentRepository,
            ConcessionTypeRepository, ProductRepository, ReviewFeeTypeRepository, PeriodRepository,
            PeriodTypeRepository, ConditionTypeRepository, Mapper, ConditionProductRepository,
            ConditionTypeProductRepository, AccrualTypeRepository, ChannelTypeRepository, TransactionTypeRepository,
            TableNumberRepository, RelationshipRepository, RoleRepository, CentreRepository,
            RiskGroupRepository, TransactionTableNumberRepository, BolUserRepository, ConcessionTradeRepository, ConcessionInvestmentRepository,
            LegalEntityRepository, RoleSubRoleRepository);

        /// <summary>
        /// The region manager
        /// </summary>
        public static IRegionManager RegionManager = new RegionManager(RegionRepository, Mapper);

        /// <summary>
        /// The user manager
        /// </summary>
        public static IUserManager UserManager = new UserManager(CacheManager, UserRepository, UserRoleRepository,
            RoleRepository, CentreRepository, CentreUserRepository, Mapper, RoleSubRoleRepository, AccountExecutiveAssistantRepository,
            RegionManager, null);

        /// <summary>
        /// The concession manager
        /// </summary>
        public static IConcessionManager ConcessionManager =
            new ConcessionManager(ConcessionRepository, LookupTableManager, RiskGroupRepository,
                Mapper, ConcessionConditionRepository, ConcessionCommentRepository,
                ConcessionRelationshipRepository, AuditRepository, UserManager, ConcessionInboxViewRepository,
                ConcessionDetailRepository, ConcessionConditionViewRepository, MiscPerformanceRepository, CentreRepository, PrimeRateRepository, null
                , AENumberUserManager, RoleSubRoleRepository);

        /// <summary>
        /// The rule manager
        /// </summary>
        public static IRuleManager RuleManager = new RuleManager(ConcessionRelationshipRepository, LookupTableManager);

        /// <summary>
        /// The lending manager
        /// </summary>
        public static ILendingManager LendingManager = new LendingManager(ConcessionManager,
            ConcessionLendingRepository, Mapper, FinancialLendingRepository, LookupTableManager,
            LoadedPriceLendingRepository, RuleManager, MiscPerformanceRepository, null,null, null, null, null);

        /// <summary>
        /// The transactional manager
        /// </summary>
        public static ITransactionalManager TransactionalManager =
            new TransactionalManager(ConcessionManager, ConcessionTransactionalRepository, Mapper, LookupTableManager,
                FinancialTransactionalRepository, LoadedPriceTransactionalRepository, RuleManager,
                MiscPerformanceRepository, null, null);

        /// <summary>
        /// The cash manager
        /// </summary>
        public static ICashManager CashManager = new CashManager(ConcessionManager, ConcessionCashRepository, Mapper,
            FinancialCashRepository, LookupTableManager, LoadedPriceCashRepository, RuleManager,
            MiscPerformanceRepository, null, null);

        public static IAENumberUserRepository AENumberUserRepository = new AENumberUserRepository(DbConnection);

        public static IAENumberUserManager AENumberUserManager = new AENumberUserManager(AENumberUserRepository, AccountExecutiveAssistantRepository, UserRepository);
    }
}
