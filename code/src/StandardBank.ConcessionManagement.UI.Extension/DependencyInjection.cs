using System;
using System.IO;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using StandardBank.ConcessionManagement.BusinessLogic;
using StandardBank.ConcessionManagement.Common;
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
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(ConcessionManager));
                    _.AssemblyContainingType(typeof(AuthorizingUserRepository));
                    _.WithDefaultConventions();
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>)); // Handlers with no response
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>)); // Handlers with a response
                    _.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<>)); // Async handlers with no response
                    _.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>)); // Async Handlers with a response
                    _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    _.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                    _.ConnectImplementationsToTypesClosing(typeof(IPipelineBehavior<,>));
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestPostProcessor<,>));
                });
                

                config.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                config.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container;
        }


    }
}
