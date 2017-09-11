using Hangfire;
using Microsoft.AspNetCore.Hosting;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Serilog;
using StandardBank.ConcessionManagement.Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StandardBank.ConcessionManagement.Scheduler
{
    public class SchedulerService : IMicroService
    {
       
        private readonly IWebHost builder;
        public SchedulerService()
        {
           
            builder = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .Build();
        }
        public void Start()
        {
            Log.Information("Service starting");
            builder.Run();
        }

        public void Stop()
        {
            Log.Information("Service stopping");
            builder.Dispose();
        }
    }
}
