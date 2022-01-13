using BrunoCampiol.Common.Logger;
using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Context;
using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrunoCampiol.Website
{
    public class Startup
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IConfiguration _configuration;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            // IOptions configuration
            ConfigureIOptions(services);

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
        public void Configure(IApplicationBuilder app)
        {
            //if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            //else app.UseExceptionHandler("/Error");

            app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/Error");

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

        private void ConfigureIOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration);
            services.Configure<IPServiceAPIProvider>(_configuration.GetSection("IPServiceAPIProvider"));
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<ILogger, Logger>();
            services.AddScoped<IIPGeolocationService, IPGeolocationService>();
        }
    }
}
