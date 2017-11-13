using Hangfire;
using MediatR;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Mocked dependencies
    /// </summary>
    public static class MockedDependencies
    {
        /// <summary>
        /// The mock AccrualType repository
        /// </summary>
        public static Mock<IAccrualTypeRepository> MockAccrualTypeRepository = new Mock<IAccrualTypeRepository>();

        /// <summary>
        /// The mock ExceptionLog repository
        /// </summary>
        public static Mock<IExceptionLogRepository> MockExceptionLogRepository = new Mock<IExceptionLogRepository>();

        /// <summary>
        /// The mock AdValorem repository
        /// </summary>
        public static Mock<IAdValoremRepository> MockAdValoremRepository = new Mock<IAdValoremRepository>();

        /// <summary>
        /// The mock ApprovalType repository
        /// </summary>
        public static Mock<IApprovalTypeRepository> MockApprovalTypeRepository = new Mock<IApprovalTypeRepository>();

        /// <summary>
        /// The mock BaseRate repository
        /// </summary>
        public static Mock<IBaseRateRepository> MockBaseRateRepository = new Mock<IBaseRateRepository>();

        /// <summary>
        /// The mock ChannelType repository
        /// </summary>
        public static Mock<IChannelTypeRepository> MockChannelTypeRepository = new Mock<IChannelTypeRepository>();

        /// <summary>
        /// The mock ConcessionType repository
        /// </summary>
        public static Mock<IConcessionTypeRepository> MockConcessionTypeRepository =
            new Mock<IConcessionTypeRepository>();

        /// <summary>
        /// The mock ConditionProduct repository
        /// </summary>
        public static Mock<IConditionProductRepository> MockConditionProductRepository =
            new Mock<IConditionProductRepository>();

        /// <summary>
        /// The mock ConditionType repository
        /// </summary>
        public static Mock<IConditionTypeRepository> MockConditionTypeRepository = new Mock<IConditionTypeRepository>();

        /// <summary>
        /// The mock MarketSegment repository
        /// </summary>
        public static Mock<IMarketSegmentRepository> MockMarketSegmentRepository = new Mock<IMarketSegmentRepository>();

        /// <summary>
        /// The mock Product repository
        /// </summary>
        public static Mock<IProductRepository> MockProductRepository = new Mock<IProductRepository>();

        /// <summary>
        /// The mock Province repository
        /// </summary>
        public static Mock<IProvinceRepository> MockProvinceRepository = new Mock<IProvinceRepository>();

        /// <summary>
        /// The mock ReviewFeeType repository
        /// </summary>
        public static Mock<IReviewFeeTypeRepository> MockReviewFeeTypeRepository = new Mock<IReviewFeeTypeRepository>();

        /// <summary>
        /// The mock Role repository
        /// </summary>
        public static Mock<IRoleRepository> MockRoleRepository = new Mock<IRoleRepository>();

        /// <summary>
        /// The mock Status repository
        /// </summary>
        public static Mock<IStatusRepository> MockStatusRepository = new Mock<IStatusRepository>();

        /// <summary>
        /// The mock SubStatus repository
        /// </summary>
        public static Mock<ISubStatusRepository> MockSubStatusRepository = new Mock<ISubStatusRepository>();

        /// <summary>
        /// The mock TransactionGroup repository
        /// </summary>
        public static Mock<ITransactionGroupRepository> MockTransactionGroupRepository =
            new Mock<ITransactionGroupRepository>();

        /// <summary>
        /// The mock TransactionType repository
        /// </summary>
        public static Mock<ITransactionTypeRepository> MockTransactionTypeRepository =
            new Mock<ITransactionTypeRepository>();

        /// <summary>
        /// The mock ReferenceType repository
        /// </summary>
        public static Mock<IReferenceTypeRepository> MockTypeRepository = new Mock<IReferenceTypeRepository>();

        /// <summary>
        /// The mock BolUser repository
        /// </summary>
        public static Mock<IBolUserRepository> MockBolUserRepository = new Mock<IBolUserRepository>();

        /// <summary>
        /// The mock BusinesOnlineTransactionType repository
        /// </summary>
        public static Mock<IBusinesOnlineTransactionTypeRepository> MockBusinesOnlineTransactionTypeRepository =
            new Mock<IBusinesOnlineTransactionTypeRepository>();

        /// <summary>
        /// The mock Centre repository
        /// </summary>
        public static Mock<ICentreRepository> MockCentreRepository = new Mock<ICentreRepository>();

        /// <summary>
        /// The mock CentreBusinessManager repository
        /// </summary>
        public static Mock<ICentreBusinessManagerRepository> MockCentreBusinessManagerRepository =
            new Mock<ICentreBusinessManagerRepository>();

        /// <summary>
        /// The mock CentreUser repository
        /// </summary>
        public static Mock<ICentreUserRepository> MockCentreUserRepository = new Mock<ICentreUserRepository>();

        /// <summary>
        /// The mock ChannelTypeBaseRate repository
        /// </summary>
        public static Mock<IChannelTypeBaseRateRepository> MockChannelTypeBaseRateRepository =
            new Mock<IChannelTypeBaseRateRepository>();

        /// <summary>
        /// The mock Concession repository
        /// </summary>
        public static Mock<IConcessionRepository> MockConcessionRepository = new Mock<IConcessionRepository>();

        /// <summary>
        /// The mock ConcessionAccount repository
        /// </summary>
        public static Mock<IConcessionAccountRepository> MockConcessionAccountRepository =
            new Mock<IConcessionAccountRepository>();

        /// <summary>
        /// The mock ConcessionApproval repository
        /// </summary>
        public static Mock<IConcessionApprovalRepository> MockConcessionApprovalRepository =
            new Mock<IConcessionApprovalRepository>();

        /// <summary>
        /// The mock ConcessionBol repository
        /// </summary>
        public static Mock<IConcessionBolRepository> MockConcessionBolRepository = new Mock<IConcessionBolRepository>();

        /// <summary>
        /// The mock ConcessionCash repository
        /// </summary>
        public static Mock<IConcessionCashRepository> MockConcessionCashRepository =
            new Mock<IConcessionCashRepository>();

        /// <summary>
        /// The mock ConcessionComment repository
        /// </summary>
        public static Mock<IConcessionCommentRepository> MockConcessionCommentRepository =
            new Mock<IConcessionCommentRepository>();

        /// <summary>
        /// The mock ConcessionCondition repository
        /// </summary>
        public static Mock<IConcessionConditionRepository> MockConcessionConditionRepository =
            new Mock<IConcessionConditionRepository>();

        /// <summary>
        /// The mock ConcessionInvestment repository
        /// </summary>
        public static Mock<IConcessionInvestmentRepository> MockConcessionInvestmentRepository =
            new Mock<IConcessionInvestmentRepository>();

        /// <summary>
        /// The mock ConcessionLending repository
        /// </summary>
        public static Mock<IConcessionLendingRepository> MockConcessionLendingRepository =
            new Mock<IConcessionLendingRepository>();

        /// <summary>
        /// The mock ConcessionMas repository
        /// </summary>
        public static Mock<IConcessionMasRepository> MockConcessionMasRepository = new Mock<IConcessionMasRepository>();

        /// <summary>
        /// The mock ConcessionTrade repository
        /// </summary>
        public static Mock<IConcessionTradeRepository> MockConcessionTradeRepository =
            new Mock<IConcessionTradeRepository>();

        /// <summary>
        /// The mock ConcessionTransactional repository
        /// </summary>
        public static Mock<IConcessionTransactionalRepository> MockConcessionTransactionalRepository =
            new Mock<IConcessionTransactionalRepository>();

        /// <summary>
        /// The mock ConditionTypeProduct repository
        /// </summary>
        public static Mock<IConditionTypeProductRepository> MockConditionTypeProductRepository =
            new Mock<IConditionTypeProductRepository>();

        /// <summary>
        /// The mock LegalEntity repository
        /// </summary>
        public static Mock<ILegalEntityRepository> MockLegalEntityRepository = new Mock<ILegalEntityRepository>();

        /// <summary>
        /// The mock LegalEntityAccount repository
        /// </summary>
        public static Mock<ILegalEntityAccountRepository> MockLegalEntityAccountRepository =
            new Mock<ILegalEntityAccountRepository>();

        /// <summary>
        /// The mock RiskGroup repository
        /// </summary>
        public static Mock<IRiskGroupRepository> MockRiskGroupRepository = new Mock<IRiskGroupRepository>();

        /// <summary>
        /// The mock ScenarioManagerToolDeal repository
        /// </summary>
        public static Mock<IScenarioManagerToolDealRepository> MockScenarioManagerToolDealRepository =
            new Mock<IScenarioManagerToolDealRepository>();

        /// <summary>
        /// The mock User repository
        /// </summary>
        public static Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();

        /// <summary>
        /// The mock UserRole repository
        /// </summary>
        public static Mock<IUserRoleRepository> MockUserRoleRepository = new Mock<IUserRoleRepository>();

        /// <summary>
        /// The mock SiteHelper
        /// </summary>
        public static Mock<ISiteHelper> MockSiteHelper = new Mock<ISiteHelper>();

        /// <summary>
        /// The mock LookupTableManager
        /// </summary>
        public static Mock<ILookupTableManager> MockLookupTableManager = new Mock<ILookupTableManager>();

        /// <summary>
        /// The mock ConcessionManager
        /// </summary>
        public static Mock<IConcessionManager> MockConcessionManager = new Mock<IConcessionManager>();

        /// <summary>
        /// The mock CacheManager
        /// </summary>
        public static Mock<ICacheManager> MockCacheManager = new Mock<ICacheManager>();

        /// <summary>
        /// The mock Region repository
        /// </summary>
        public static Mock<IRegionRepository> MockRegionRepository = new Mock<IRegionRepository>();

        /// <summary>
        /// The mock UserRegion repository
        /// </summary>
        public static Mock<IUserRegionRepository> MockUserRegionRepository = new Mock<IUserRegionRepository>();

        /// <summary>
        /// The mock LendingManager
        /// </summary>
        public static Mock<ILendingManager> MockLendingManager = new Mock<ILendingManager>();

        /// <summary>
        /// The mock Period repository
        /// </summary>
        public static Mock<IPeriodRepository> MockPeriodRepository = new Mock<IPeriodRepository>();

        /// <summary>
        /// The mock PeriodType repository
        /// </summary>
        public static Mock<IPeriodTypeRepository> MockPeriodTypeRepository = new Mock<IPeriodTypeRepository>();

        /// <summary>
        /// The mock Mediatr
        /// </summary>
        public static Mock<IMediator> MockMediator = new Mock<IMediator>();

        public static Mock<IApprovalRoutingManager> MockApprovalRoutingManager = new Mock<IApprovalRoutingManager>();

        public static Mock<IApprovalWorkflowRepository> MockApprovalWorkflowRepository =
            new Mock<IApprovalWorkflowRepository>();

        public static Mock<ILetterGeneratorManager> MockLetterGeneratorManager = new Mock<ILetterGeneratorManager>();

        /// <summary>
        /// The mock cash manager
        /// </summary>
        public static Mock<ICashManager> MockCashManager = new Mock<ICashManager>();

        /// <summary>
        /// The mock transactional manager
        /// </summary>
        public static Mock<ITransactionalManager> MockTransactionalManager = new Mock<ITransactionalManager>();

        /// <summary>
        /// The mock TableNumber repository
        /// </summary>
        public static Mock<ITableNumberRepository> MockTableNumberRepository = new Mock<ITableNumberRepository>();

        /// <summary>
        /// The mock Relationship repository
        /// </summary>
        public static Mock<IRelationshipRepository> MockRelationshipRepository = new Mock<IRelationshipRepository>();

        /// <summary>
        /// The mock ConcessionRelationship repository
        /// </summary>
        public static Mock<IConcessionRelationshipRepository> MockConcessionRelationshipRepository =
            new Mock<IConcessionRelationshipRepository>();

        /// <summary>
        /// The mock audit repository
        /// </summary>
        public static Mock<IAuditRepository> MockAuditRepository = new Mock<IAuditRepository>();

        /// <summary>
        /// The mock ProductLending repository
        /// </summary>
        public static Mock<IProductLendingRepository> MockProductLendingRepository =
            new Mock<IProductLendingRepository>();

        /// <summary>
        /// The mock FinancialLending repository
        /// </summary>
        public static Mock<IFinancialLendingRepository> MockFinancialLendingRepository =
            new Mock<IFinancialLendingRepository>();

        /// <summary>
        /// The mock user manager
        /// </summary>
        public static Mock<IUserManager> MockUserManager = new Mock<IUserManager>();

        /// <summary>
        /// The mock FinancialCash repository
        /// </summary>
        public static Mock<IFinancialCashRepository> MockFinancialCashRepository = new Mock<IFinancialCashRepository>();

        /// <summary>
        /// The mock ProductCash repository
        /// </summary>
        public static Mock<IProductCashRepository> MockProductCashRepository = new Mock<IProductCashRepository>();

        /// <summary>
        /// The mock file utiltity
        /// </summary>
        public static Mock<IFileUtiltity> MockFileUtiltity = new Mock<IFileUtiltity>();

        /// <summary>
        /// The mock PDF utility
        /// </summary>
        public static Mock<IPdfUtility> MockPdfUtility = new Mock<IPdfUtility>();

        /// <summary>
        /// The mock razor renderer
        /// </summary>
        public static Mock<IRazorRenderer> MockRazorRenderer = new Mock<IRazorRenderer>();

        /// <summary>
        /// The mock FinancialTransactional repository
        /// </summary>
        public static Mock<IFinancialTransactionalRepository> MockFinancialTransactionalRepository =
            new Mock<IFinancialTransactionalRepository>();

        /// <summary>
        /// The mock ProductTransactional repository
        /// </summary>
        public static Mock<IProductTransactionalRepository> MockProductTransactionalRepository =
            new Mock<IProductTransactionalRepository>();

        /// <summary>
        /// The mock LoadedPriceCash repository
        /// </summary>
        public static Mock<ILoadedPriceCashRepository> MockLoadedPriceCashRepository =
            new Mock<ILoadedPriceCashRepository>();

        /// <summary>
        /// The mock LoadedPriceLending repository
        /// </summary>
        public static Mock<ILoadedPriceLendingRepository> MockLoadedPriceLendingRepository =
            new Mock<ILoadedPriceLendingRepository>();

        /// <summary>
        /// The mock LoadedPriceTransactional repository
        /// </summary>
        public static Mock<ILoadedPriceTransactionalRepository> MockLoadedPriceTransactionalRepository =
            new Mock<ILoadedPriceTransactionalRepository>();

        /// <summary>
        /// The mock ConcessionDetail repository
        /// </summary>
        public static Mock<IConcessionDetailRepository> MockConcessionDetailRepository =
            new Mock<IConcessionDetailRepository>();

        /// <summary>
        /// The mock FinancialInvestment repository
        /// </summary>
        public static Mock<IFinancialInvestmentRepository> MockFinancialInvestmentRepository =
            new Mock<IFinancialInvestmentRepository>();

        /// <summary>
        /// The mock ProductInvestment repository
        /// </summary>
        public static Mock<IProductInvestmentRepository> MockProductInvestmentRepository =
            new Mock<IProductInvestmentRepository>();

        /// <summary>
        /// The mock concession inbox view repository
        /// </summary>
        public static Mock<IConcessionInboxViewRepository> MockConcessionInboxViewRepository =
            new Mock<IConcessionInboxViewRepository>();

        /// <summary>
        /// The mock concession condition view repository
        /// </summary>
        public static Mock<IConcessionConditionViewRepository> MockConcessionConditionViewRepository =
            new Mock<IConcessionConditionViewRepository>();

        /// <summary>
        /// The mock daily scheduled job
        /// </summary>
        public static Mock<IDailyScheduledJob> MockDailyScheduledJob = new Mock<IDailyScheduledJob>();

        /// <summary>
        /// The mock TransactionTableNumber repository
        /// </summary>
        public static Mock<ITransactionTableNumberRepository> MockTransactionTableNumberRepository =
            new Mock<ITransactionTableNumberRepository>();

        /// <summary>
        /// The mock rule manager
        /// </summary>
        public static Mock<IRuleManager> MockRuleManager = new Mock<IRuleManager>();

        /// <summary>
        /// The mock SapDataImport repository
        /// </summary>
        public static Mock<ISapDataImportRepository> MockSapDataImportRepository = new Mock<ISapDataImportRepository>();

        /// <summary>
        /// The mock SapDataImportConfiguration repository
        /// </summary>
        public static Mock<ISapDataImportConfigurationRepository> MockSapDataImportConfigurationRepository =
            new Mock<ISapDataImportConfigurationRepository>();

        /// <summary>
        /// The mock email manager
        /// </summary>
        public static Mock<IEmailManager> MockEmailManager = new Mock<IEmailManager>();

        /// <summary>
        /// The mock background job client
        /// </summary>
        public static Mock<IBackgroundJobClient> MockBackgroundJobClient = new Mock<IBackgroundJobClient>();

        /// <summary>
        /// The mock ChannelTypeImport repository
        /// </summary>
        public static Mock<IChannelTypeImportRepository> MockChannelTypeImportRepository =
            new Mock<IChannelTypeImportRepository>();

        /// <summary>
        /// The mock ProductImport repository
        /// </summary>
        public static Mock<IProductImportRepository> MockProductImportRepository = new Mock<IProductImportRepository>();

        /// <summary>
        /// The mock TransactionTypeImport repository
        /// </summary>
        public static Mock<ITransactionTypeImportRepository> MockTransactionTypeImportRepository =
            new Mock<ITransactionTypeImportRepository>();

        /// <summary>
        /// The mock misc performance repository
        /// </summary>
        public static Mock<IMiscPerformanceRepository> MockMiscPerformanceRepository =
            new Mock<IMiscPerformanceRepository>();
    }
}