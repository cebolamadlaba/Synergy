using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.UI
{
  /// <summary>
  /// Start up
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Start up for the hosting environment
    /// </summary>
    /// <param name="env"></param>
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    /// <summary>
    /// Get the configuration
    /// </summary>
    public IConfigurationRoot Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddMemoryCache();
      services.AddMvc();

      // Add common services
      services.AddSingleton<IConfigurationData>(GenerateConfigurationData());
      services.AddScoped<ICacheManager, MemoryCacheManager>();

      // Add repository services.
      AddRepositoryServices(services);
    }

    /// <summary>
    /// Adds the repository services
    /// </summary>
    /// <param name="services"></param>
    private static void AddRepositoryServices(IServiceCollection services)
    {
      services.AddScoped<IConcessionCountRepository, ConcessionCountRepository>();

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
      services.AddScoped<IRiskGroupRepository, RiskGroupRepository>();
      services.AddScoped<IScenarioManagerToolDealRepository, ScenarioManagerToolDealRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }

    /// <summary>
    /// Generates a populated configuration data object using the configuration values from the appSettings.json
    /// </summary>
    /// <returns></returns>
    private ConfigurationData GenerateConfigurationData()
    {
      var connectionString = Configuration["ConnectionString"];
      return new ConfigurationData(connectionString);
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      //this allows the anuglar routing to work
      app.UseStatusCodePagesWithReExecute("/");

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
