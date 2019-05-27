using BrunoCampiol.Common.Global;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BrunoCampiol.Website
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
#if DEBUG
            GlobalSettings.Instance.BuildFlavor = "DEBUG";
            Configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                                .Build();
#else
            GlobalSettings.Instance.BuildFlavor = "RELEASE";
            Configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();
#endif

            // TODO: check each value is set so we wont have null exception
            if (String.IsNullOrEmpty(Configuration["AppSettingsFlavor"])) throw new ArgumentException("Cannot be null or empty", "AppSettingsFlavor");
            if (String.IsNullOrEmpty(Configuration["ConnectionString"])) throw new ArgumentException("Cannot be null or empty", "ConnectionString");

            // TODO: set all configurations here
            GlobalSettings.Instance.AppSettingsFlavor = Configuration["AppSettingsFlavor"];
            GlobalSettings.Instance.ConnectionString = Configuration["ConnectionString"];

            // Does check if we have mismatch stuff
            if (GlobalSettings.Instance.BuildFlavor == "DEBUG" && GlobalSettings.Instance.AppSettingsFlavor != "DEVELOPMENT")
            {
                throw new Exception("Cannot use DEBUG code with app settings different then DEVELOPMENT");
            }
            if (GlobalSettings.Instance.BuildFlavor == "RELEASE" && GlobalSettings.Instance.AppSettingsFlavor != "PRODUCTION")
            {
                throw new Exception("Cannot use RELEASE code with app settings different then PRODUCTION");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // The AntiForgery Token needs to be added and before services.AddMvc()
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // http://codereform.com/blog/post/asp-net-core-2-1-authentication-with-social-logins/
            services.AddAuthentication(options =>
            {
                // No database user authentication storage
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddFacebook(options => {
                options.AppId = Configuration["Facebook:AppId"];
                options.AppSecret = Configuration["Facebook:AppSecret"];
            })
            .AddGitHub(options =>
            {
                options.ClientId = Configuration["GitHub:ClientId"];
                options.ClientSecret = Configuration["GitHub:ClientSecret"];
            })
            .AddTwitter(options =>
            {
                options.ConsumerKey = Configuration["Twitter:ApiKey"];
                options.ConsumerSecret = Configuration["Twitter:ApiSecret"];
            })
            .AddCookie(options => {
                options.LoginPath = "/Identity";
            });


            services.AddMvc();

            //services.AddMvc()
            //    .AddRazorPagesOptions(options =>
            //    {
            //        options.Conventions.AuthorizeFolder("/Identity").AllowAnonymousToPage("/Identity/Index");
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");
            
            app.UseAuthentication();

            app.UseStaticFiles(GetStaticFileConfiguration());

            app.UseMvc();

            // Sets the environment name based on build flavor
            // This helps razor pages to get right environment
            if (GlobalSettings.Instance.BuildFlavor == "DEBUG")
            {
                env.EnvironmentName = "Development";
            }
            else
            {
                env.EnvironmentName = "Production";
            }
        }

        private StaticFileOptions GetStaticFileConfiguration()
        {
            // Allows .exe downloads
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".exe"] = "application/octect-stream";
            provider.Mappings[".vsix"] = "application/vsix";
            return new StaticFileOptions { ContentTypeProvider = provider };
        }
    }
}
