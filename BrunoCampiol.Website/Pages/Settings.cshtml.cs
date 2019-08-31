using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BrunoCampiol.Website.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public SettingsModel(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void OnGet()
        {
            ViewData["SettingsList"] =
            new List<(int Order, string Key, string Value)>
            {
                (1, "Environment", _environment.EnvironmentName),
                (2, "IP Geolocation Host", _configuration["IpApiService:Host"])
            };

        }
    }
}