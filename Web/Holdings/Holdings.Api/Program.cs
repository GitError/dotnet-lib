using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Holdings.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
              .ConfigureLogging(logging =>
              {
                  if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                  {
                      // clear default logging providers
                      logging.ClearProviders();

                      // add built-in providers manually, as needed 
                      logging.AddConsole();
                      logging.AddDebug();
                      logging.AddEventLog();
                      logging.AddEventSourceLogger();
                  }
              });
    }
}