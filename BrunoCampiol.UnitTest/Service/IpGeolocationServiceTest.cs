using Microsoft.Extensions.Configuration;
using Moq;
using RestSharp;
using Xunit;

namespace BrunoCampiol.UnitTest.Service
{
    public class IpGeolocationServiceTest
    {
        private readonly Mock<IConfiguration> _configuration;
        private static readonly string _hostUrl = "mock-url.campiol";
        private static readonly string _hostScheme = "https";

        public IpGeolocationServiceTest()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(x => x["IpApiService:Host"]).Returns("https://" + _hostUrl);
            _configuration.Setup(x => x["IpApiService:Resource"]).Returns("userManagement/{userName}/permissions/{permission}");
        }

        [Fact]
        public void WhenValidIP_ExpectValidVisitor()
        {
            //// Assemble
            //RestResponse response = new RestResponse();
            //response.StatusCode = System.Net.HttpStatusCode.OK;
            //response.Content = _permissionGrantedResponse;

            //Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            //restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response);

            //OptOutPermissionFilter filter = new OptOutPermissionFilter(_configuration.Object);
            //filter.SetRestClient(restClientMock.Object);

            //AuthorizationFilterContext currentContext = GetAuthorizationFilterContext();
            //currentContext.HttpContext.Request.Headers.Add("Authorization", "Basic dW5pdF90ZXN0OmNhc2Nzc2NzYWNzYWNjc2Nh");

            //// Act
            //filter.OnAuthorization(currentContext);
            //var actionResult = currentContext.Result as JsonResult;

            //// Assert
            //// Do not expect to be HTTP status 200 because the api can later return another result
            //Assert.NotNull(currentContext.HttpContext.User);
            //Assert.NotNull(currentContext.HttpContext.User.Identity);
            //Assert.NotNull(currentContext.HttpContext.User.Identity.Name);
            //Assert.NotNull(currentContext.HttpContext.Request);
            //Assert.NotNull(currentContext.HttpContext.Request.Host.Value);
            //Assert.NotNull(currentContext.HttpContext.Request.Scheme);
            //Assert.Equal(_userName, currentContext.HttpContext.User.Identity.Name);
        }
    }
}
