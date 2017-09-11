using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StandardBank.ConcessionManagement.BusinessLogic;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace StandardBank.ConcessionManagement.Scheduler
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;
        private IContainer ApplicationContainer;
        private IHostingEnvironment environment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            environment = env;

            Configuration = builder.Build();
        }
        private IConfigurationData GenerateConfigurationData()
        {
            var config = new ConfigurationData { TemplatePath = Path.Combine(environment.ContentRootPath, "EmailTemplates") };
            Configuration.Bind(config);
            return config;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
          
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddSingleton(GenerateConfigurationData());
            services.AddTransient<IDbConnection>(_ => new SqlConnection(_.GetRequiredService<IConfigurationData>().ConnectionString));
            services.AddLogging();
            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("Hangfire")));
            var builder = new ContainerBuilder();
            //This is magic to get all implementations of IJob 
            builder.RegisterAssemblyTypes(typeof(ConsessionNotificationJob).GetTypeInfo().Assembly).AsImplementedInterfaces();
           
            // When you do service population, it will include your controller
            // types automatically.
            builder.Populate(services);

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            app.UseHangfireDashboard(options: new DashboardOptions { Authorization = new[] { new DashboardAuthorizationFilter() } });
            app.UseHangfireServer();
            ScheduleJobs(app);
            app.Run(async (context) =>
            {
               
              await context.Response.WriteAsync("Job Scheduler running");
                
            });
        }
        private void ScheduleJobs(IApplicationBuilder builder)
        {
            //Get all jobs from the container
            var jobs = builder.ApplicationServices.GetRequiredService<IEnumerable<IJob>>();
            foreach (var job in jobs)
            {
                switch (job.JobType)
                {
                    case JobType.OnceOff:
                        BackgroundJob.Schedule(()=> job.Run(),job.OnceOffRunAt);
                        break;
                    default:
                        RecurringJob.AddOrUpdate(()=> job.Run(),job.Cron);
                        break;
                }
            }
        }
    }
}
