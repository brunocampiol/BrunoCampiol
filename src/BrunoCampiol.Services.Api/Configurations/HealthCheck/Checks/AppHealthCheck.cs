using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BrunoCampiol.Services.Api.Configurations.HealthCheck.Checks
{
    public class AppHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
