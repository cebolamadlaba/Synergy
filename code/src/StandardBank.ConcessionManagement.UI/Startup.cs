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
using System;
using System.Collections.Generic;
using System.IO;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic;
using FluentValidation.AspNetCore;
using Hangfire;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;

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

        /// <summary>
        /// Gets the hosting environment
        /// </summary>
        IHostingEnvironment Environment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add the local project services
            services.AddScoped<ISiteHelper, SiteHelper>();

            // Add framework services.
            services.AddMemoryCache();
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Add automapper
            services.AddAutoMapper();

            // Add MediatR
            services.AddMediatR(typeof(ConcessionManager));

            // Add Hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionString"]));

            // Add the custom services we've created
            var container = DependencyInjection.ConfigureServices(services, GenerateConfigurationData(Environment));

            return container.GetInstance<IServiceProvider>();


            
        }

        /// <summary>
        /// Generates a populated configuration data object using the configuration values from the appSettings.json
        /// </summary>
        /// <returns></returns>
        private ConfigurationData GenerateConfigurationData(IHostingEnvironment env)
        {
            var config = new ConfigurationData
            {
                EmailTemplatePath = Path.Combine(env.ContentRootPath, "EmailTemplates"),
                LetterTemplatePath = Path.Combine(env.ContentRootPath, "LetterTemplates")
            };

            Configuration.Bind(config);
            return config;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            loggerFactory.AddSerilog();

            //use the developer exception page
            app.UseDeveloperExceptionPage();

            //this allows the anuglar routing to work
            app.UseStatusCodePagesWithReExecute("/");

            app.UseDefaultFiles();
            app.UseMvc();
            app.UseStaticFiles();


            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] {new HangfireDashboardAuthorizationFilter()}
            });

            if(Configuration["LoadHangfireJobs"] == "true")
            {
                ScheduleJobs(app);
            }
          
        }

        /// <summary>
        /// Schedules the jobs.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void ScheduleJobs(IApplicationBuilder builder)
        {
            foreach (var torunJob in builder.ApplicationServices.GetRequiredService<IEnumerable<IDailyScheduledJob>>())
            {
                //the UTC hour needs to be passed to the cron as it works in UTC time
                var hourToRun = torunJob.HourToRun - (DateTime.Now.Hour - DateTime.UtcNow.Hour);

                if (torunJob.type == "Recurring")
                {
                    //will schedule the job to run every X amount of hours.
                    //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"),Cron.DayInterval(torunJob.HourToRun));

                    RecurringJob.AddOrUpdate(torunJob.Name, () => torunJob.Run(), Cron.MinuteInterval(60));
                }
                else
                {
                    RecurringJob.AddOrUpdate(torunJob.Name, () => torunJob.Run(), Cron.Daily(hourToRun, torunJob.MinuteToRun));
                }
            }
        }
    }
}
