using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace BrunoCampiol.IntegrationTest.Service
{
    public class IpGeolocationServiceTest
    {
        private readonly Mock<IConfiguration> _configuration;
        private static readonly string _hostUrl = "http://ip-api.com";
        private static readonly string _hostResource = "/json/";

        public IpGeolocationServiceTest()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(x => x["IpApiService:Host"]).Returns(_hostUrl);
            _configuration.Setup(x => x["IpApiService:Resource"]).Returns(_hostResource);
        }

        [Fact]
        public void WhenValidIP_ExpectValidVisitor()
        {
            // Assemble
            IpGeolocationService service = new IpGeolocationService(_configuration.Object);
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
