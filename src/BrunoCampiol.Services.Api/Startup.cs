﻿using BrunoCampiol.Domain.Core.Interfaces;
using BrunoCampiol.Domain.Core.Notifications;
using BrunoCampiol.Services.Api.Middleware;

namespace BrunoCampiol.Services.Api
{
    public class Startup
    {
        //private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            //_configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            // NET core http client factory
            services.AddHttpClient();

            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

            // Controllers
            services.AddControllers();
            services.AddRouting();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Swagger
            //services.AddSwaggerSetup();

            // Healthcheck
            //services.AddHealthCheckSetup();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            // Routing 
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Healthcheck
            //app.UseAppHealthChecks();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseSwaggerApp();

            // Use wwwroot folder
            app.UseStaticFiles();
        }
    }
}