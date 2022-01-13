using BrunoCampiol.CrossCutting.Common.Logger;
using BrunoCampiol.Domain.Interface;
using BrunoCampiol.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BrunoCampiol.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IIPGeolocationService, IPGeolocationService>();
        }
    }
}