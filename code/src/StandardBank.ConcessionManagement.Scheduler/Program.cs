using Microsoft.AspNetCore.Hosting;
using PeterKottas.DotNetCore.WindowsService;
using Serilog;
using System.IO;

namespace StandardBank.ConcessionManagement.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .Build();
            builder.Run();
        }
    }
}
