using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GuessTheWord.API.Gateway
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
            services.AddCustomHealthCheck(configuration);
        }
    }

    internal static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            // hcBuilder.AddUrlGroup(new Uri("guessword"), name: "guessword-check", tags: new string[] { "guesswordapi" })
            //     .AddUrlGroup(new Uri("tryguessword"), name: "tryguessword-check", tags: new string[] { "tryguesswordapi" })
            //     .AddUrlGroup(new Uri("identity"), name: "identity-check", tags: new string[] { "identityapi" });
            // hcBuilder.AddUrlGroup(new Uri(configuration["GuessWordHC"]), name: "guessword-check", tags: new string[] { "guesswordapi" })
            //     .AddUrlGroup(new Uri(configuration["TryGuessWordHC"]), name: "tryguessword-check", tags: new string[] { "tryguesswordapi" })
            //     .AddUrlGroup(new Uri(configuration["IdentityHC"]), name: "identity-check", tags: new string[] { "identityapi" });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("urls:identity");
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "webagg";
                });

            return services;
        }
    }
}