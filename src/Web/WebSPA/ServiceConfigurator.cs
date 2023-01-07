using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebSPA.Server.Model;

namespace WebSPA
{
    /// <summary>
    /// Конфигуратор сервисов в <see cref="IServiceCollection"/>
    /// </summary>
    internal sealed class ServiceConfigurator
    {
        /// <summary>
        /// Конфигурировать IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация</param>
        public void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<AppSettings>(configuration);

            services.AddCustomHealthCheck(configuration);
        }
    }

    internal static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy())
                .AddUrlGroup(new Uri(configuration["IdentityUrlHC"]), name: "identity-check",
                    tags: new string[] { "identityapi" })
                .AddUrlGroup(new Uri(configuration["WebAggregatorUrlHC"]), name: "webaggregator-check",
                    tags: new string[] { "webaggregator" });

            return services;
        }
    }
}