﻿using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BrunoCampiol.IntegrationTest.Service
{
    public class IpGeolocationServiceTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IOptions<IPServiceAPIProvider> _configuration;
        private const string _hostUrl = "http://ip-api.com";

        public IpGeolocationServiceTest()
        {
            _configuration = Options.Create(new IPServiceAPIProvider() { Host = _hostUrl });
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void WhenGoogleDns_ExpectValidVisitor()
        {
            // Assemble
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            IIPGeolocationService service = new IPGeolocationService(_configuration, httpClientFactory);
            IPAddress address = IPAddress.Parse("8.8.8.8"); // Google dns address

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
    }
}