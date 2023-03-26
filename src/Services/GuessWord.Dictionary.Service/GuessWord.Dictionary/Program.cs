using System.Text.Json.Serialization;
using EventBus.Bus;
using GuessWord.Abstractions.Events;
using GuessWord.API;
using GuessWord.Dictionary.DBContext;
using GuessWord.Dictionary.Infrastructure;
using GuessWord.Dictionary.IntegrationEvents.EventHandlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
            
            builder.Services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            new ServiceConfigurator().ConfigureServices(builder.Services, builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();

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

            app.MigrateDbContext<DictionaryContext>((context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var logger = services.GetService<ILogger<DictionaryContextSeed>>();

                new DictionaryContextSeed().SeedAsync(context, env, logger).Wait();
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
}