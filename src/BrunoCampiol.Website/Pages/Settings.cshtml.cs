using BrunoCampiol.CrossCutting.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

namespace BrunoCampiol.UI.Web.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _hostEnv;

        public SettingsModel(IWebHostEnvironment hostingEnvironment, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _hostEnv = hostingEnvironment;
        }

        public void OnGet()
        {
            string dataBase = _appSettings.ConnectionStrings.DefaultConnection;
            string framework = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            string platformVersion = $"{RuntimeInformation.OSDescription} [ {RuntimeInformation.OSArchitecture} ]";

            if (!String.IsNullOrWhiteSpace(dataBase))
            {
                Match serverMatch = Regex.Match(dataBase,
                                                @"Data Source=([A-Za-z0-9_.]+)",
                                                RegexOptions.IgnoreCase);

                dataBase = String.IsNullOrWhiteSpace(serverMatch.Value) ? "Invalid database" : serverMatch.Value;
            }


            ViewData["SettingsList"] =
            new List<(string Key, string Value)>
            {
                ("EnvironmentName", _hostEnv.EnvironmentName),
                ("TargetFrameWork", framework),
                ("PlatformVersion", platformVersion),
                ("Database", dataBase),
                ("IP API Host", _appSettings.IPServiceAPIProvider.Host)
            };
        }
    }
}