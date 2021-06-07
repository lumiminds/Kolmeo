using Kalmeo.Repositories;
using Kalmeo.Repositories.Implementation;
using Kalmeo.Repositories.Interfaces;
using Kalmeo.Services.Implementation;
using Kalmeo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kalmeo.Services.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            // Goes through the assembly and looks for all types that extend AutoMapper.Profile to figure out mappings
            services.AddAutoMapper(typeof(StartupExtensions));
        }

        public static void RegisterServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductService, ProductService>();

            //For Database/Repositories
            services.AddDbContext<KalmeoContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        public static void Configure(this IServiceScope serviceScope)
        {
            // Application DB context
            var context = serviceScope.ServiceProvider.GetService<KalmeoContext>();
            context.Database.Migrate();
        }
    }
}
