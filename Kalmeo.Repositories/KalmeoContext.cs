using Kalmeo.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Kalmeo.Repositories
{
    public class KalmeoContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public KalmeoContext(DbContextOptions<KalmeoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(KalmeoContext).Assembly.FullName)
                );

                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Inheritance from BaseContext
            base.OnModelCreating(modelBuilder);

            // Configure your entities and add them here to apply
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
