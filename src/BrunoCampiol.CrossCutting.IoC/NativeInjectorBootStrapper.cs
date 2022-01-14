using BrunoCampiol.Application.Services;
using BrunoCampiol.CrossCutting.Common.Logger;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Domain.Services;
using BrunoCampiol.Infra.Data.Interfaces;
using BrunoCampiol.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BrunoCampiol.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();

            services.AddScoped<IVisitorRepository, VisitorRepository>();

            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<VisitorAppService, VisitorAppService>();
        }
    }
}