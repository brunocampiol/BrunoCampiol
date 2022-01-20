using BrunoCampiol.Application.Interfaces;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.CrossCutting.Common.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace BrunoCampiol.UI.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IVisitorAppService _appService;

        public IndexModel(IVisitorAppService appService)
        {
            _appService = appService;
        }

        public void OnGet()
        {
            var visitor = GetVisitorViewModel();
            _appService.HandleVisitor(visitor);
        }

        private VisitorViewModel GetVisitorViewModel()
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var browserName = StaticLibrary.GetBrowserName(userAgent);
            var osName = StaticLibrary.GetOperationalSystemName(userAgent);
            var headers = Request.Headers.Select(x => x.Key + ":" + x.Value);
            var headerString = string.Join(Environment.NewLine, headers.Select(x => x.ToString()));

            var visitor = new VisitorViewModel()
            {
                Ip = ipAddress,
                ClientHeaders = headerString,
                ClientBrowser = browserName,
                ClientOS = osName,
                ClientUserAgent = userAgent,
            };

            return visitor;
        }
    }
}