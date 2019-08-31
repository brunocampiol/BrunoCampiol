using BrunoCampiol.Common.Global;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrunoCampiol.Website
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();

            GlobalSettings.Instance.ConnectionString = Configuration["ConnectionString"];
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
            .AddFacebook(options =>
            {
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
            .AddCookie(options =>
            {
                options.LoginPath = "/Identity";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");

            app.UseAuthentication();

            app.UseStaticFiles(GetStaticFileConfiguration());

            app.UseMvc();
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
