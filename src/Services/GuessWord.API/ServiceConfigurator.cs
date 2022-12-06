using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using EventBus.RabbitMQ.Extensions;
using GuessWord.API.DBContext;
using GuessWord.API.Infrastructure.Filters;
using GuessWord.API.IntegrationEvents.EventHandlers;
using GuessWord.API.Repository;
using GuessWord.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace GuessWord.API
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
            services.AddCustomSwagger(configuration);
            services.AddCustomAuthentication(configuration);
            services.AddCustomHealthCheck(configuration);

            services.ConfigureEventBusDependencies(configuration);

            services.AddDbContext<GuessGameContext>(o => o.UseNpgsql(configuration.GetConnectionString("GuessGameDb")));

            services.AddTransient<IGuessGameRepository, GuessGameRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IHistoryItemRepository, HistoryItemRepository>();
            services.AddTransient<ILetterModelRepository, LetterModelRepository>();
            services.AddTransient<IWordModelRepository, WordModelRepository>();
            services.AddTransient<IGuessGameService, GuessGameService>();
        }
    }

    internal static class CustomExtensionMethods
    {
        private const string RabbitMQSectionName = "RabbitMQ";
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSection = configuration.GetSection(RabbitMQSectionName);
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder
                .AddRabbitMQ(
                    $"amqp://{rabbitMQSection["ConnectionUrl"]}",
                    name: "payment-rabbitmqbus-check",
                    tags: new string[] { "rabbitmqbus" });

            return services;
        }

        public static IServiceCollection ConfigureEventBusDependencies(this IServiceCollection services, ConfigurationManager configuration)
        {
            var rabbitMQSection = configuration.GetSection(RabbitMQSectionName);
            services.AddRabbitMQEventBus
            (
                connectionUrl: rabbitMQSection["ConnectionUrl"],
                exchangeName: "GuessWord.EventBusBroker",
                queueName: "GuessWord.API.EventBusQueue",
                timeoutBeforeReconnecting: 15
            );

            services.AddTransient<GetWordEventHandler>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "guessword", "GuessWord API" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "guessword";
            });

            return services;
        }
    }
}