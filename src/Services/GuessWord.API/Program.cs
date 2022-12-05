using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EventBus.Bus;
using EventBus.RabbitMQ.Extensions;
using GuessWord.Abstractions.Events;
using GuessWord.API.IntegrationEvents.EventHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessWord.API
{
    /// <summary>
    /// Класс-конфигуратор приложения
    /// </summary>
    public class Startup
    {
        public static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            new ServiceConfigurator().ConfigureServices(builder.Services, builder.Configuration);
            ConfigureEventBusDependencies(builder.Services, builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            return app.RunAsync();
        }

        private static void ConfigureEventBusDependencies(IServiceCollection services, ConfigurationManager configuration)
        {
            var rabbitMQSection = configuration.GetSection("RabbitMQ");
            services.AddRabbitMQEventBus
            (
                connectionUrl: rabbitMQSection["ConnectionUrl"],
                brokerName: "GuessWord.EventBusBroker",
                queueName: "GuessWord.API.EventBusQueue",
                timeoutBeforeReconnecting: 15
            );

            services.AddTransient<GetWordEventHandler>();
        }

        private static void ConfigureEventBusHandlers(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            // Here you add the event handlers for each intergration event.
            eventBus.Subscribe<SendWordEvent, GetWordEventHandler>();
        }
    }
}