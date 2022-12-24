// using System;
// using System.Reflection;
// using Identity.API.DbContext;
// using Identity.API.Model;
// using Identity.API.Services;
// using IdentityServer4.Services;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.eShopOnContainers.Services.Identity.API;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Diagnostics.HealthChecks;
//
// namespace Identity.API
// {
//     /// <summary>
//     /// Конфигуратор сервисов в <see cref="IServiceCollection"/>
//     /// </summary>
//     internal sealed class ServiceConfigurator
//     {
//         /// <summary>
//         /// Конфигурировать IoC
//         /// </summary>
//         /// <param name="services">Коллекция сервисов</param>
//         /// <param name="configuration">Конфигурация</param>
//         public void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
//         {
//             services.AddCustomHealthCheck(configuration);
//
//             var connectionString = configuration.GetConnectionString("IdentityDb");
//             services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString, ops =>
//             {
//                 ops.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
//                 //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
//                 ops.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
//             }));
//
//             services.AddIdentity<ApplicationUser, IdentityRole>()
//                 .AddEntityFrameworkStores<ApplicationDbContext>()
//                 .AddDefaultTokenProviders();
//
//             services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
//             services.AddTransient<IRedirectService, RedirectService>();
//
//             var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
//             // Adds IdentityServer
//             services.AddIdentityServer(x =>
//                 {
//                     x.IssuerUri = "null";
//                     x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
//                 })
//                 //.AddSigningCredential(Certificate.Get())
//                 .AddAspNetIdentity<ApplicationUser>()
//                 .AddConfigurationStore(options =>
//                 {
//                     options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
//                         sqlOptions =>
//                         {
//                             sqlOptions.MigrationsAssembly(migrationsAssembly);
//                             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
//                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
//                         });
//                 })
//                 .AddOperationalStore(options =>
//                 {
//                     options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
//                         sqlOptions =>
//                         {
//                             sqlOptions.MigrationsAssembly(migrationsAssembly);
//                             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
//                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
//                         });
//                 })
//                 .Services.AddTransient<IProfileService, ProfileService>();
//         }
//     }
//
//     internal static class CustomExtensionMethods
//     {
//         public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
//         {
//             var hcBuilder = services.AddHealthChecks();
//
//             hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
//
//             return services;
//         }
//     }
// }