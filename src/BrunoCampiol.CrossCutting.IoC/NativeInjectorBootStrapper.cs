using BrunoCampiol.Common.Logger;
using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
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