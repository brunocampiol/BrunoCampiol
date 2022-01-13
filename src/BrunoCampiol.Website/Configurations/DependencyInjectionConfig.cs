using BrunoCampiol.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BrunoCampiol.Website.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // TODO fix to add this inside RegisterServices
            services.AddHttpClient();

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
