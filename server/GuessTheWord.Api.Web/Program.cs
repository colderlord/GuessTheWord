using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessTheWord.Api.Web
{
    /// <summary>
    /// Класс-конфигуратор приложения
    /// </summary>
    public class Startup
    {
        public static async Task Main(string[] args)
        {
            SetupResolver(AppContext.BaseDirectory);
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            new ServiceConfigurator().ConfigureServices(builder.Services);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            await app.RunAsync();
        }

        private static void SetupResolver(string path)
        {
            var resolver = new ResolveEventHandler((_, a) =>
            {
                var asmName = new AssemblyName(a.Name);
                var asmPath = Path.Combine(AppContext.BaseDirectory, asmName.Name + ".dll");
                if (File.Exists(asmPath))
                {
                    return Assembly.LoadFrom(asmPath);
                }
                asmPath = Path.Combine(AppContext.BaseDirectory, path, asmName.Name + ".dll");
                return File.Exists(asmPath) ? Assembly.LoadFrom(asmPath) : null;
            });
            AppDomain.CurrentDomain.AssemblyResolve += resolver;
        }
    }
}