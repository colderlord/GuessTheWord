using System;
using GuessTheWord.Abstractions;
using GuessTheWord.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace GuessTheWord.Api.Web
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
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = AssemblyHelper.InjectionAssemblies(AppContext.BaseDirectory);
            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo<IExtension>())
                        .AsSelfWithInterfaces()
                        .WithSingletonLifetime()
            );
        }
    }
}