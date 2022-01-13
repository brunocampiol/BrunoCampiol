using BrunoCampiol.CrossCutting.Common.Common;
using BrunoCampiol.CrossCutting.Common.Logger;
using BrunoCampiol.CrossCutting.Common.Models;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Infra.Data.Context;
using BrunoCampiol.Infra.Data.Generic;
using BrunoCampiol.Infra.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace BrunoCampiol.UI.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _databaseContext;
        private readonly IIPGeolocationService _iPGeolocationService;

        public IndexModel(ILogger logger,
                          DatabaseContext databaseContext, 
                          IIPGeolocationService iPGeolocationService)
        {
            _logger = logger;
            _databaseContext = databaseContext;
            _iPGeolocationService = iPGeolocationService;
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

            SaveVisitor(userAgent, ipAddress, browserName, osName, headerString);
        }

        private void SaveVisitor(string userAgent, string ipAddress, string browserName, string osName, string headers)
        {
            try
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
                        VISITORS visitor = _iPGeolocationService.GetVisitorInformation(ipAddress);

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
            }
            catch (Exception e)
            {
                // TODO add global exception handler
                _logger.LogException(e);
            }
        }
    }
}