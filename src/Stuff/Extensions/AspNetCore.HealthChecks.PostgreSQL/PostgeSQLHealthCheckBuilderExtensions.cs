using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to configure <see cref="PostgeSQLHealthCheck"/>.
    /// </summary>
    public static class PostgeSQLHealthCheckBuilderExtensions
    {
        private const string NAME = "postgresql";

        public static IHealthChecksBuilder AddPostgreSqlCheck(this IHealthChecksBuilder builder,
            string connectionString,
            string? name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string>? tags = default,
            TimeSpan? timeout = default)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var pgConnection = new NpgsqlConnection(connectionString);
            builder.Services
                .AddSingleton(sp => new PostgeSQLHealthCheck(pgConnection));

            return builder.Add(new HealthCheckRegistration(
                name ?? NAME,
                sp => sp.GetRequiredService<PostgeSQLHealthCheck>(),
                failureStatus,
                tags,
                timeout));
        }
    }
}