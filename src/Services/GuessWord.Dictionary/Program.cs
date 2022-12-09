using System.Threading.Tasks;
using EventBus.Bus;
using EventBus.RabbitMQ.Extensions;
using GuessWord.Abstractions.Events;
using GuessWord.Dictionary.IntegrationEvents.EventHandlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace GuessWord.Dictionary
{
    /// <summary>
    /// Класс-конфигуратор приложения
    /// </summary>
    public class Startup
    {
        public static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCustomHealthCheck(builder.Configuration);
            builder.Services.ConfigureEventBusDependencies(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            ConfigureEventBusHandlers(app);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            return app.RunAsync();
        }

        private static void ConfigureEventBusHandlers(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            // Here you add the event handlers for each intergration event.
            eventBus.Subscribe<GetWordEvent, GetWordEventHandler>();
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
                    rabbitMQSection["ConnectionUrl"],
                    name: "guessworddictionary-rabbitmqbus-check",
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
                queueName: "GuessWord.Dictionary.EventBusQueue",
                timeoutBeforeReconnecting: 15
            );

            services.AddTransient<GetWordEventHandler>();

            return services;
        }
    }
}