using Kalmeo.Services;
using Kalmeo.Services.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Kalmeo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Migrate on startup
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Migrate DB changes
                    scope.Configure();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while executing database migrations.");

                    // Rethrow the exception so the program halts. A failed DB should NOT be used.
                    throw;
                }
            }

            // Start Service
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
