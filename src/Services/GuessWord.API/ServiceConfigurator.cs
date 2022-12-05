using GuessWord.API.DBContexts;
using GuessWord.API.Repository;
using GuessWord.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContext<GuessGameContext>(o => o.UseNpgsql(configuration.GetConnectionString("GuessGameDB")));

            services.AddTransient<IGuessGameRepository, GuessGameRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IHistoryItemRepository, HistoryItemRepository>();
            services.AddTransient<ILetterModelRepository, LetterModelRepository>();
            services.AddTransient<IWordModelRepository, WordModelRepository>();
            services.AddTransient<IGuessGameService, GuessGameService>();
        }
    }
}