using BrunoCampiol.CrossCutting.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace BrunoCampiol.Integration.Test.Domain
{
    public class VisitorServiceTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IOptions<IPServiceAPIProvider> _configuration;
        private const string _hostUrl = "http://ip-api.com";

        public VisitorServiceTest()
        {
            _configuration = Options.Create(new IPServiceAPIProvider() { Host = _hostUrl });
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void WhenGoogleDns_ExpectValidVisitor()
        {
            // TODO: fix integration tests

            //// Assemble
            //var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            //IIPGeolocationService service = new IPGeolocationService(_configuration, httpClientFactory);
            //IPAddress address = IPAddress.Parse("8.8.8.8"); // Google dns address

            //// Act
            //VISITORS visitor = service.GetVisitorInformation(address.ToString());

            //// Assert
            //// Do not expect to be HTTP status 200 because the api can later return another result
            //Assert.NotNull(visitor);
            //Assert.NotNull(visitor.CITY);
            //Assert.NotNull(visitor.COUNTRY);
            //Assert.NotNull(visitor.IP);
            //Assert.NotNull(visitor.ISP);
            //Assert.NotNull(visitor.REGION);
        }
    }
}