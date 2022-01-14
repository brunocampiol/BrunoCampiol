using BrunoCampiol.CrossCutting.Common.Common;
using BrunoCampiol.CrossCutting.Common.Models;
using BrunoCampiol.Domain.Services;
using BrunoCampiol.Infra.Data.Interfaces;
using BrunoCampiol.Infra.Data.Models;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BrunoCampiol.Unit.Test.Service
{
    public class VisitorServiceTest
    {        
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<IVisitorRepository> _visitorRepository;
        private readonly IOptions<IPServiceAPIProvider> _configuration;
        private static readonly string _hostUrl = "https://mock-url.campiol";

        public VisitorServiceTest()
        {
            _configuration = Options.Create(new IPServiceAPIProvider() { Host = _hostUrl });
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _visitorRepository = new Mock<IVisitorRepository>();
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

            var service = new VisitorService(_configuration, _httpClientFactory.Object, _visitorRepository.Object);

            // Act
            service.HandleVisitor(expectedVisitor);

            // Assert
            // Not useful test - simply does not throws exception
        }
    }
}
