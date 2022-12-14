using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebStatus
{
    /// <summary>
    /// Класс-конфигуратор приложения
    /// </summary>
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
                var host = BuildWebHost(configuration, args);

                LogPackagesVersionInfo();

                Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Program.AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .CaptureStartupErrors(false)
                    .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                    .UseStartup<Startup>()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseSerilog()
                    .Build();

            Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
            {
                var seqServerUrl = configuration["Serilog:SeqServerUrl"];
                var logstashUrl = configuration["Serilog:LogstashgUrl"];
                return new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .Enrich.WithProperty("ApplicationContext", Program.AppName)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                    .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl, 1000)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }

            IConfiguration GetConfiguration()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                return builder.Build();
            }

            string GetVersion(Assembly assembly)
            {
                try
                {
                    return $"{assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version} ({assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split()[0]})";
                }
                catch
                {
                    return string.Empty;
                }
            }

            void LogPackagesVersionInfo()
            {
                var assemblies = new List<Assembly>();

                foreach (var dependencyName in typeof(Program).Assembly.GetReferencedAssemblies())
                {
                    try
                    {
                        // Try to load the referenced assembly...
                        assemblies.Add(Assembly.Load(dependencyName));
                    }
                    catch
                    {
                        // Failed to load assembly. Skip it.
                    }
                }

                var versionList = assemblies.Select(a => $"-{a.GetName().Name} - {GetVersion(a)}").OrderBy(value => value);

                Log.Logger.ForContext("PackageVersions", string.Join("\n", versionList)).Information("Package versions ({ApplicationContext})", Program.AppName);
            }

        }

        private static readonly string _namespace = typeof(Startup).Namespace;
        public static readonly string AppName = _namespace;
    }
}