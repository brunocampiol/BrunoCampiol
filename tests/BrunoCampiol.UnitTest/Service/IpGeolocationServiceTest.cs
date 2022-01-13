using BrunoCampiol.Common.Common;
using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Service;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BrunoCampiol.UnitTest.Service
{
    public class IpGeolocationServiceTest
    {
        // change to options create
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly IOptions<IPServiceAPIProvider> _configuration;
        private static readonly string _hostUrl = "https://mock-url.campiol";

        public IpGeolocationServiceTest()
        {
            _configuration = Options.Create(new IPServiceAPIProvider() { Host = _hostUrl });
            _httpClientFactory = new Mock<IHttpClientFactory>();
        }

        [Fact]
        public void WhenValidIP_ExpectValidVisitor()
        {
            // Assemble

            VISITORS expectedVisitor = new VISITORS()
            {
                CITY = "string",
                COUNTRY = "string",
                IP = "string",
                ISP = "string",
                REGION = "string"
            };

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.Content = new StringContent(expectedVisitor.ToJson());
            httpResponse.StatusCode = HttpStatusCode.OK;

            var httpHandler = new Mock<DelegatingHandler>();
            httpHandler.Protected()
                                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                .ReturnsAsync(httpResponse)
                                .Verifiable();
            httpHandler.As<IDisposable>().Setup(s => s.Dispose());

            var httpClient = new HttpClient(httpHandler.Object);

            _httpClientFactory.Setup(cf => cf.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new IPGeolocationService(_configuration, _httpClientFactory.Object);

            // Act
            VISITORS actualVisitor = service.GetVisitorInformation("127.0.0.1");

            // Assert
            // Do not expect to be HTTP status 200 because the api can later return another result
            Assert.NotNull(actualVisitor);
            Assert.NotNull(actualVisitor.CITY);
            Assert.NotNull(actualVisitor.COUNTRY);
            Assert.NotNull(actualVisitor.IP);
            Assert.NotNull(actualVisitor.ISP);
            Assert.NotNull(actualVisitor.REGION);
        }
    }
}
