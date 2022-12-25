using System.Data.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Проверка работоспособности БД PG
    /// </summary>
    internal class PostgeSQLHealthCheck : IHealthCheck, IDisposable
    {
        private readonly bool ownsConnection;
        private NpgsqlConnection connection;
        private bool disposed;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connection">Подключение к БД</param>
        public PostgeSQLHealthCheck(string connectionString)
        {
            this.connection = new NpgsqlConnection(connectionString);
            ownsConnection = true;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connection">Подключение к БД</param>
        public PostgeSQLHealthCheck(NpgsqlConnection connection)
        {
            this.connection = connection;
            ownsConnection = true;
        }

        /// <inheritdoc />
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                using var model = EnsureConnection();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new HealthCheckResult(context.Registration.FailureStatus, exception: ex));
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private DbConnection EnsureConnection()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(PostgeSQLHealthCheck));
            }

            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            connection.GetSchema();

            return connection;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose connection only if RabbitMQHealthCheck owns it
                if (!disposed && connection != null && ownsConnection)
                {
                    connection.Dispose();
                    connection = null;
                }
                disposed = true;
            }
        }
    }
}