using AutoMapper;
using BrunoCampiol.Application.AutoMapper;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Infra.Data.Models;
using BrunoCampiol.Unit.Test.Base;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Application
{
    public class AutoMapperTest : BaseUnitTest
    {
        public AutoMapperTest()
        {

        }

        [Fact]
        public void WhenValidVisitor_ExpectValidVisitorViewModel()
        {
            // Assemble
            var visitor = new VISITORS()
            {
                CITY = "a",
                COUNTRY = "b",
                IP = "c",
                ISP = "d",
                REGION = "e",
                CLIENT_BROWSER = "f",
                CLIENT_HEADERS = "g",
                CLIENT_OS = "h",
                CLIENT_USER_AGENT = "i",
                CREATED_ON_UTC = DateTime.UtcNow
            };

            var expectedVisitorViewModel = new VisitorViewModel()
            {
                City = visitor.CITY,
                ClientBrowser = visitor.CLIENT_BROWSER,
                ClientHeaders = visitor.CLIENT_HEADERS,
                ClientOS = visitor.CLIENT_OS,
                ClientUserAgent = visitor.CLIENT_USER_AGENT,
                Country = visitor.COUNTRY,
                CreatedUtc = visitor.CREATED_ON_UTC,
                Ip = visitor.IP,
                Isp = visitor.ISP,
                Region= visitor.REGION
            };

            // Act
            var actualVisitorViewModel = _mapper.Map<VisitorViewModel>(visitor);

            // Assert
            Assert.NotNull(actualVisitorViewModel);
            Assert.Equal(expectedVisitorViewModel.City, actualVisitorViewModel.City);
            Assert.Equal(expectedVisitorViewModel.ClientBrowser, actualVisitorViewModel.ClientBrowser);
            Assert.Equal(expectedVisitorViewModel.ClientHeaders, actualVisitorViewModel.ClientHeaders);
            Assert.Equal(expectedVisitorViewModel.ClientOS, actualVisitorViewModel.ClientOS);
            Assert.Equal(expectedVisitorViewModel.Country, actualVisitorViewModel.Country);
            Assert.Equal(expectedVisitorViewModel.Ip,actualVisitorViewModel.Ip);
            Assert.Equal(expectedVisitorViewModel.Region, actualVisitorViewModel.Region);
            Assert.Equal(expectedVisitorViewModel.ClientUserAgent, actualVisitorViewModel.ClientUserAgent);
            Assert.Equal(expectedVisitorViewModel.Isp, actualVisitorViewModel.Isp);
            Assert.Equal(expectedVisitorViewModel.CreatedUtc, actualVisitorViewModel.CreatedUtc);
        }

        [Fact]
        public void WhenValidVisitorViewModel_ExpectValidVisitor()
        {
            // Assemble
            var expectedVisitor = new VISITORS()
            {
                CITY = "a",
                COUNTRY = "b",
                IP = "c",
                ISP = "d",
                REGION = "e",
                CLIENT_BROWSER = "f",
                CLIENT_HEADERS = "g",
                CLIENT_OS = "h",
                CLIENT_USER_AGENT = "i",
                CREATED_ON_UTC = DateTime.UtcNow
            };

            var viewModel = new VisitorViewModel()
            {
                City = expectedVisitor.CITY,
                ClientBrowser = expectedVisitor.CLIENT_BROWSER,
                ClientHeaders = expectedVisitor.CLIENT_HEADERS,
                ClientOS = expectedVisitor.CLIENT_OS,
                ClientUserAgent = expectedVisitor.CLIENT_USER_AGENT,
                Country = expectedVisitor.COUNTRY,
                CreatedUtc = expectedVisitor.CREATED_ON_UTC,
                Ip = expectedVisitor.IP,
                Isp = expectedVisitor.ISP,
                Region = expectedVisitor.REGION
            };

            // Act
            var actualVisitor = _mapper.Map<VISITORS>(viewModel);

            // Assert
            Assert.NotNull(actualVisitor);
            Assert.Equal(expectedVisitor.CITY, actualVisitor.CITY);
            Assert.Equal(expectedVisitor.CLIENT_BROWSER, actualVisitor.CLIENT_BROWSER);
            Assert.Equal(expectedVisitor.CLIENT_HEADERS, actualVisitor.CLIENT_HEADERS);
            Assert.Equal(expectedVisitor.CLIENT_OS, actualVisitor.CLIENT_OS);
            Assert.Equal(expectedVisitor.COUNTRY, actualVisitor.COUNTRY);
            Assert.Equal(expectedVisitor.IP, actualVisitor.IP);
            Assert.Equal(expectedVisitor.REGION, actualVisitor.REGION);
            Assert.Equal(expectedVisitor.CLIENT_USER_AGENT, actualVisitor.CLIENT_USER_AGENT);
            Assert.Equal(expectedVisitor.ISP, actualVisitor.ISP);
            Assert.Equal(expectedVisitor.CREATED_ON_UTC, actualVisitor.CREATED_ON_UTC);
        }
    }
}
