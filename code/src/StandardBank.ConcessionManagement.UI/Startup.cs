using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.UI.Extension;
using StandardBank.ConcessionManagement.UI.Helpers.Implementation;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using System.IO;

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
            Environment = env;

            //setup the application logger
            ApplicationLogging.SetupLogger(Configuration["LogLevel"], Configuration["LogFileFolder"]);
        }

        /// <summary>
        /// Get the configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        IHostingEnvironment Environment { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMemoryCache();
            services.AddMvc();

            // Add automapper
            services.AddAutoMapper();

            // Add the custom services we've created
            DependencyInjection.ConfigureServices(services, GenerateConfigurationData(Environment));

            // Add the local project services
            services.AddScoped<ISiteHelper, SiteHelper>();
        }

        /// <summary>
        /// Generates a populated configuration data object using the configuration values from the appSettings.json
        /// </summary>
        /// <returns></returns>
        private ConfigurationData GenerateConfigurationData(IHostingEnvironment env)
        {
            var config = new ConfigurationData();
            config.TemplatePath = Path.Combine(env.ContentRootPath,"EmailTemplates");
            Configuration.Bind(config);
            return config;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            //this allows the anuglar routing to work
            app.UseStatusCodePagesWithReExecute("/");

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}