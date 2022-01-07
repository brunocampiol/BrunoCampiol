using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BrunoCampiol.IntegrationTest.Service
{
    public class IpGeolocationServiceTest
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<IPServiceAPIProvider> _configuration;
        private const string _hostUrl = "http://ip-api.com";

        public IpGeolocationServiceTest()
        {
            _configuration = Options.Create(new IPServiceAPIProvider() { Host = _hostUrl });
        }

        [Fact]
        public void WhenValidIP_ExpectValidVisitor()
        {
            // Assemble
            IIPGeolocationService service = new IPGeolocationService(_configuration);
            IPAddress address = GetRandomIP();

            // Act
            VISITORS visitor = service.GetVisitorInformation(address.ToString());

            // Assert
            // Do not expect to be HTTP status 200 because the api can later return another result
            Assert.NotNull(visitor);
            Assert.NotNull(visitor.CITY);
            Assert.NotNull(visitor.COUNTRY);
            Assert.NotNull(visitor.IP);
            Assert.NotNull(visitor.ISP);
            Assert.NotNull(visitor.REGION);
        }

        private IPAddress GetRandomIP()
        {
            var data = new byte[4];
            new Random().NextBytes(data);
            IPAddress ip = new IPAddress(data);

            return ip;
        }
    }
}
