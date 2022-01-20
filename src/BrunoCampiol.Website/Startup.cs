using BrunoCampiol.UI.Web.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrunoCampiol.UI.Web
{
    public class Startup
    {
        private IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
            _environment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC settings
            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});

            // Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            services.AddRazorPages();
                    //.AddRazorRuntimeCompilation(); // remove for PROD

            // Setting DBContexts
            services.AddDatabaseConfiguration(_configuration);

            // IOptions configuration
            services.AddOptionsConfiguration(_configuration);

            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration();

            // AutoMapper Settings
            services.AddAutoMapperConfiguration();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseExceptionHandler("/error/500");
                app.UseStatusCodePagesWithRedirects("/error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(GetStaticFileConfiguration());
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                // Which is the same as the template
                //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
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
