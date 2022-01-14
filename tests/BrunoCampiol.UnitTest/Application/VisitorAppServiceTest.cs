using BrunoCampiol.Application.Services;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Unit.Test.Base;
using Moq;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Application
{
    public class VisitorAppServiceTest : BaseUnitTest
    {
        private readonly Mock<IVisitorService> _visitorService;

        public VisitorAppServiceTest()
        {
            _visitorService = new Mock<IVisitorService>();
        }

        [Fact]
        public void WhenNullVisitor_ExpectArgumentNullException()
        {
            // Assamble
            var service = new VisitorAppService(_mapper, _visitorService.Object);

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => service.HandleVisitor(null));
        }
    }
}
