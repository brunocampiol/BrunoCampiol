using BrunoCampiol.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCampiol.Service.Service
{
    public class WebClient2 : IWebClient
    {
        private readonly string _host;

        public WebClient2(string host)
        {
            if (String.IsNullOrEmpty(host)) throw new ArgumentNullException(nameof(host));

            this._host = host;
        }

        public Task<string> HttpGet()
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadStringTaskAsync(new Uri(_host));
            }
        }

        public Task<string> HttpGet(string resource)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadStringTaskAsync(new Uri(_host + resource));
            }
        }
    }
}
