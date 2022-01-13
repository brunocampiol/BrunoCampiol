using BrunoCampiol.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BrunoCampiol.Website.Configurations
{
    public static class OptionsConfig
    {
        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<AppSettings>(configuration);
            services.Configure<IPServiceAPIProvider>(configuration.GetSection("IPServiceAPIProvider"));
        }
    }
}