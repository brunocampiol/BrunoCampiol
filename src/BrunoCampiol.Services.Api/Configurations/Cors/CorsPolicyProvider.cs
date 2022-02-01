using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BrunoCampiol.Services.Api.Configurations.Cors
{
    public class CorsPolicyProvider : ICorsPolicyProvider
    {
        public CorsOptions CorsOptions { get; private set; }

        public CorsPolicyProvider()
        {
            CorsOptions = new CorsOptions();
            CorsOptions.AddPolicy("Default", builder => builder.WithOrigins("*")
                                                                .AllowAnyMethod()
                                                                .AllowAnyHeader());
        }

        public Task<CorsPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            return Task.FromResult(CorsOptions.GetPolicy(policyName ?? CorsOptions.DefaultPolicyName));
        }
    }
}
