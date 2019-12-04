using BrunoCampiol.Common.Common;
using BrunoCampiol.Common.Global;
using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Context;
using BrunoCampiol.Repository.Generic;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;

namespace BrunoCampiol.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppSettings _appSettings;
        private readonly DatabaseContext _databaseContext;
        //private readonly ILogger _logger;

        public IndexModel(IOptions<AppSettings> appSettings, DatabaseContext databaseContext)
        {
            _appSettings = appSettings.Value;
            _databaseContext = databaseContext;
        }

        public void OnGet()
        {
            // Ip and UserAgent shall not be queried in thread
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string browserName = StaticLibrary.GetBrowserName(userAgent);
            string osName = StaticLibrary.GetOperationalSystemName(userAgent);

            var headers = Request.Headers.Select(x => x.Key + ":" + x.Value);
            string headerString = String.Join(Environment.NewLine, headers.Select(x => x.ToString()));

            // We dont want visitors to wait for database readings
            Thread thread = new Thread(() => SaveVisitor(userAgent, ipAddress, browserName, osName, headerString));
            thread.Start();
        }

        private void SaveVisitor(string userAgent, string ipAddress, string browserName, string osName, string headers)
        {
            // Gets current request IP address
            if (ipAddress == "localhost" || ipAddress == "::1") return;

            // Checks if we already have it locally ; default is true
            bool isIpStored = true;
            
            Repository<VISITORS> repository = new Repository<VISITORS>(_databaseContext);

            isIpStored = repository.Get(v => v.IP == ipAddress).Any();

            // If not stored then query IP information
            if (!isIpStored)
            {
                try
                {
                    IpGeolocationService ipService = new IpGeolocationService(_appSettings.IPServiceAPIProvider);
                    VISITORS visitor = ipService.GetVisitorInformation(ipAddress);

                    visitor.CLIENT_HEADERS = headers;
                    visitor.CLIENT_BROWSER = browserName;
                    visitor.CLIENT_OS = osName;
                    visitor.CLIENT_USER_AGENT = userAgent;

                    // information from current request
                    repository.Add(visitor);
                    repository.Save();
                }
                catch (Exception ex)
                {
                    // TODO: fix this
                    //_logger.LogCritical(ex, ex.AllExceptionMessages(), null);

                    using (_databaseContext)
                    {
                        Repository<LOGS> repo = new Repository<LOGS>(_databaseContext);

                        LOGS log = new LOGS();
                        log.LEVEL = LogEntryLevel.ERROR.ToString();
                        log.MESSAGE = "IP " + ipAddress + Environment.NewLine + ex.AllExceptionMessages();
                        log.STACK_TRACE = ex.StackTrace;
                        log.FULL_EXCEPTION = ex.ToString();
                        log.CREATED_ON_UTC = DateTime.UtcNow;

                        repo.Add(log);
                        repo.Save();
                    }
                }
            }

            repository.Dispose();
        }

    }
}