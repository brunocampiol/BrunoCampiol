using BrunoCampiol.Service.Interface;
using BrunoCampiol.Service.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BrunoCampiol.UnitTest.Service
{
    public class TestWebClient2
    {

        [Fact]
        public void TestWebsiteService_ExpectJsonString_WhenDownloadStringMethod()
        {
            // Assemble
            string expectedJson = "{\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",\"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"query\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}";
            Mock<IWebClient> fakeRequest = new Mock<IWebClient>();

            fakeRequest
                .Setup(request => request.DownloadString())
                .Returns(Task.Run(() => "{\"as\":\"AS4230 CLARO S.A.\",\"city\":\"Eldorado do Sul\",\"country\":\"Brazil\",\"countryCode\":\"BR\",\"isp\":\"Claro S.A\",\"lat\":-30.0003,\"lon\":-51.3119,\"org\":\"Dell Computadores DO Brasil Ltda\",\"query\":\"200.182.161.10\",\"region\":\"RS\",\"regionName\":\"Rio Grande do Sul\",\"status\":\"success\",\"timezone\":\"America/Sao_Paulo\",\"zip\":\"92990-000\"}"));

            WebsiteService service = new WebsiteService(fakeRequest.Object);

            // Act
            string actualJson = service.WebClient.DownloadString().Result;

            // Assert
            Assert.Equal(expectedJson, actualJson);
        }
    }
}
