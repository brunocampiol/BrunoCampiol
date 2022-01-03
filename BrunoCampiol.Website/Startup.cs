using BrunoCampiol.Common.Logger;
using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Context;
using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrunoCampiol.Website
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; set; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));
            services.AddDbContext<DatabaseContext>();

            // IOptions configuration
            ConfigureIOptions(services);

            // IoC
            ConfigureIoC(services);

            // Register Services
            RegisterServices(services);


            // The AntiForgery Token needs to be added and before services.AddMvc()
            //services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            //// http://codereform.com/blog/post/asp-net-core-2-1-authentication-with-social-logins/
            //services.AddAuthentication(options =>
            //{
            //    // No database user authentication storage
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddFacebook(options =>
            //{
            //    options.AppId = Configuration["Facebook:AppId"];
            //    options.AppSecret = Configuration["Facebook:AppSecret"];
            //})
            //.AddGitHub(options =>
            //{
            //    options.ClientId = Configuration["GitHub:ClientId"];
            //    options.ClientSecret = Configuration["GitHub:ClientSecret"];
            //})
            //.AddTwitter(options =>
            //{
            //    options.ConsumerKey = Configuration["Twitter:ApiKey"];
            //    options.ConsumerSecret = Configuration["Twitter:ApiSecret"];
            //})
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Identity";
            //});


            //services.AddControllersWithViews();
            services.AddRazorPages();

            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");

            //app.UseAuthentication();

            app.UseStaticFiles(GetStaticFileConfiguration());
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                // Which is the same as the template
                //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvc();
        }

        private StaticFileOptions GetStaticFileConfiguration()
        {
            // Allows .exe downloads
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".exe"] = "application/octect-stream";
            provider.Mappings[".vsix"] = "application/vsix";
            return new StaticFileOptions { ContentTypeProvider = provider };
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            // OData
            //services.AddOData();

            // ASP.NET HttpContext dependency
            //services.AddHttpContextAccessor();

            //// Transient
            //services.AddTransient<SMSModelBuilder>();
            //services.AddTransient<ICampaignManagementService, CampaignManagementService>();
            //services.AddTransient<ITemplateManagementService, TemplateManagementService>();
            //services.AddTransient<ILenghtValidations, LenghtValidations>();

            //// Scoped
            //services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
            //services.AddScoped(typeof(IRabbitProducerService<>), typeof(RabbitProducerService<>));
            //services.AddScoped(typeof(IRabbitConnectionSettings<>), typeof(RabbitConnectionSettings<>));
            //services.AddScoped(typeof(IRabbitEventConsumer<>), typeof(RabbitEventConsumer<>));
            //services.AddScoped<IRabbitConsumerService, ObdTextMQConsumerService>();
            //services.AddScoped(typeof(IDataManager), typeof(SqlDataManager));
        }

        private void ConfigureIOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.Configure<IPServiceAPIProvider>(Configuration.GetSection("IPServiceAPIProvider"));
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IIPGeolocationService, IPGeolocationService>();
        }
    }
}
