using Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data.Extensions
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
