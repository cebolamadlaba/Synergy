using Microsoft.Extensions.DependencyInjection;
using StandardBank.ConcessionManagement.BusinessLogic;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.UI.Extension
{
    /// <summary>
    /// Dependency injection setup for the application
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configuration the services used by the application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationData"></param>
        public static void ConfigureServices(IServiceCollection services, ConfigurationData configurationData)
        {
            // Add common services
            services.AddSingleton<IConfigurationData>(configurationData);
            services.AddScoped<ICacheManager, MemoryCacheManager>();

            // Add repository services.
            AddRepositoryServices(services);

            // Add business logic services
            AddBusinessLogicServices(services);
        }

        /// <summary>
        /// Add the business logic services
        /// </summary>
        /// <param name="services"></param>
        private static void AddBusinessLogicServices(IServiceCollection services)
        {
            services.AddScoped<IConcessionManager, ConcessionManager>();
            services.AddScoped<ILookupTableManager, LookupTableManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IPricingManager, PricingManager>();
        }

        /// <summary>
        /// Adds the repository services
        /// </summary>
        /// <param name="services"></param>
        private static void AddRepositoryServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizingUserRepository, AuthorizingUserRepository>();
            services.AddScoped<ISMTRawDataRepository, SMTRawDataRepository>();
            services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();

            services.AddScoped<IAdValoremRepository, AdValoremRepository>();
            services.AddScoped<IApprovalTypeRepository, ApprovalTypeRepository>();
            services.AddScoped<IBaseRateRepository, BaseRateRepository>();
            services.AddScoped<IChannelTypeRepository, ChannelTypeRepository>();
            services.AddScoped<IConcessionTypeRepository, ConcessionTypeRepository>();
            services.AddScoped<IConditionProductRepository, ConditionProductRepository>();
            services.AddScoped<IConditionTypeRepository, ConditionTypeRepository>();
            services.AddScoped<IMarketSegmentRepository, MarketSegmentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IReviewFeeTypeRepository, ReviewFeeTypeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ISubStatusRepository, SubStatusRepository>();
            services.AddScoped<ITransactionGroupRepository, TransactionGroupRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<IReferenceTypeRepository, ReferenceTypeRepository>();
            services.AddScoped<IBolUserRepository, BolUserRepository>();
            services.AddScoped<IBusinesOnlineTransactionTypeRepository, BusinesOnlineTransactionTypeRepository>();
            services.AddScoped<ICentreRepository, CentreRepository>();
            services.AddScoped<ICentreBusinessManagerRepository, CentreBusinessManagerRepository>();
            services.AddScoped<ICentreUserRepository, CentreUserRepository>();
            services.AddScoped<IChannelTypeBaseRateRepository, ChannelTypeBaseRateRepository>();
            services.AddScoped<IConcessionRepository, ConcessionRepository>();
            services.AddScoped<IConcessionAccountRepository, ConcessionAccountRepository>();
            services.AddScoped<IConcessionApprovalRepository, ConcessionApprovalRepository>();
            services.AddScoped<IConcessionBolRepository, ConcessionBolRepository>();
            services.AddScoped<IConcessionCashRepository, ConcessionCashRepository>();
            services.AddScoped<IConcessionCommentRepository, ConcessionCommentRepository>();
            services.AddScoped<IConcessionConditionRepository, ConcessionConditionRepository>();
            services.AddScoped<IConcessionInvestmentRepository, ConcessionInvestmentRepository>();
            services.AddScoped<IConcessionLendingRepository, ConcessionLendingRepository>();
            services.AddScoped<IConcessionMasRepository, ConcessionMasRepository>();
            services.AddScoped<IConcessionRemovalRequestRepository, ConcessionRemovalRequestRepository>();
            services.AddScoped<IConcessionTradeRepository, ConcessionTradeRepository>();
            services.AddScoped<IConcessionTransactionalRepository, ConcessionTransactionalRepository>();
            services.AddScoped<IConditionTypeProductRepository, ConditionTypeProductRepository>();
            services.AddScoped<ILegalEntityRepository, LegalEntityRepository>();
            services.AddScoped<ILegalEntityAccountRepository, LegalEntityAccountRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRiskGroupRepository, RiskGroupRepository>();
            services.AddScoped<IScenarioManagerToolDealRepository, ScenarioManagerToolDealRepository>();
            services.AddScoped<IUserRegionRepository, UserRegionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
