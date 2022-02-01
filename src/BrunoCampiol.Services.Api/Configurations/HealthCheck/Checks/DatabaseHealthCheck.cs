using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BrunoCampiol.Services.Api.Configurations.HealthCheck.Checks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private const string _defaultQuery = "SELECT 1";

        public DatabaseHealthCheck()
        {
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var connectionString = "";
            var dataSource = Regex.Match(connectionString, @"Data Source=([A-Za-z0-9_.]+)", RegexOptions.IgnoreCase).Value;

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    var command = connection.CreateCommand();
                    command.CommandText = _defaultQuery;

                    await command.ExecuteNonQueryAsync(cancellationToken);

                    return HealthCheckResult.Healthy(dataSource);
                }
                catch (Exception e)
                {
                    return HealthCheckResult.Unhealthy(dataSource, e);
                }
            }
        }
    }
}
