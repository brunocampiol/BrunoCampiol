using BrunoCampiol.CrossCutting.Common.Common;
using BrunoCampiol.Services.Api.Configurations.HealthCheck.Checks;
using BrunoCampiol.Services.Api.Configurations.HealthCheck.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BrunoCampiol.Services.Api.Configurations.HealthCheck
{
    public static class HealthCheckSetup
    {
        private const string _healthResource = "/health";
        private const string _healthChecksResource = "/healthchecks";

        public static void AddHealthCheckSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecks()
                    .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck), tags: new[] { _healthChecksResource })
                    .AddCheck<AppHealthCheck>(nameof(AppHealthCheck), tags: new[] { _healthResource });
        }

        public static void UseAppHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks(_healthResource, GetHealthOptions());
            app.UseHealthChecks(_healthChecksResource, GetHealthChecksOptions());
        }

        private static HealthCheckOptions GetHealthOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(_healthResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static HealthCheckOptions GetHealthChecksOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(_healthChecksResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static Func<HttpContext, HealthReport, Task> GetResponseWriter()
        {
            return async (c, r) =>
            {
                c.Response.ContentType = "application/json";

                var results = r.Entries.Select(pair =>
                {
                    return KeyValuePair.Create(pair.Key, new ResponseResults
                    {
                        Status = pair.Value.Status.ToString(),
                        Description = pair.Value.Description,
                        Duration = pair.Value.Duration.TotalSeconds.ToString() + "s",
                        ExceptionMessage = pair.Value.Exception != null ? pair.Value.Exception.Message : null,
                        Data = pair.Value.Data
                    });
                }).ToDictionary(p => p.Key, p => p.Value);

                var result = new ResponseHealthCheck
                {
                    Status = r.Status.ToString(),
                    TotalDuration = r.TotalDuration.TotalSeconds.ToString() + "s",
                    Results = results
                };

                await c.Response.WriteAsync(result.ToJson());
                //await c.Response.WriteAsync(result.ToJsonIndented());
            };
        }
    }
}
