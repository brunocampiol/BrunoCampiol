using BrunoCampiol.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Service.Service
{
    public class IpApiService
    {
        public IWebClient WebClient { get; private set; }

        public IpApiService(IWebClient webClient)
        {
            if (webClient == null) throw new ArgumentNullException(nameof(webClient));

            WebClient = webClient;
        }
    }
}
