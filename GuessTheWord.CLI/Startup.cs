using System;

namespace GuessTheWord.CLI
{
    /// <summary>
    /// Запуск CLI приложения
    /// </summary>
    public class Startup
    {
        private readonly string[] args;
        private readonly Action<IServiceCollection> configureServices;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        /// <param name="configureServices">Конфигуратор сервисов</param>
        public Startup(string[] args, Action<IServiceCollection> configureServices)
        {
            this.args = args;
            this.configureServices = configureServices;
        }
    }
}