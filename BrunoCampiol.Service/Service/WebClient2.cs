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
        private readonly string url;

        public WebClient2(string url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            this.url = url;
        }

        public Task<string> DownloadString()
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadStringTaskAsync(new Uri(url));
            }
        }
    }
}
