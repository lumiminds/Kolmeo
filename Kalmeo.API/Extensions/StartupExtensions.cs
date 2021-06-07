using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Kalmeo.API.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\Kalmeo.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kalmeo.API",
                    Version = "v1"
                });
            });
        }
    }
}
