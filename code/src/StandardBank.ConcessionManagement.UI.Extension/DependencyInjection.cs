using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using StandardBank.ConcessionManagement.BusinessLogic;
using StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Repository;
using StructureMap;

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
        public static Container ConfigureServices(IServiceCollection services, ConfigurationData configurationData)
        {
            var container = new Container();

            // Add common services
            services.AddSingleton<IConfigurationData>(configurationData);
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IMarshaller, XmlMarshaller>();
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IFileUtiltity, SystemFileUtility>();
            services.AddScoped<IPdfUtility, WkWrapPdfUtility>();
            services.AddScoped<IRazorRenderer, FluentRazorRenderer>();

            // Add jobs
            services.AddScoped<IDailyScheduledJob, DueForExpiryNotification>();
            services.AddScoped<IDailyScheduledJob, RenewOngoingConditions>();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(ConcessionManager));
                    _.AssemblyContainingType(typeof(AccrualTypeRepository));
                    _.WithDefaultConventions();
                });

                config.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPreProcessorBehavior<,>));
                config.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPostProcessorBehavior<,>));

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container;
        }


    }
}
