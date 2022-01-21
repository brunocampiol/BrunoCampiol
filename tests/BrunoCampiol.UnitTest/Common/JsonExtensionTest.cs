using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.CrossCutting.Common.Common;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Common
{
    public class JsonExtensionTest
    {
        [Fact]
        public void WhenInlineObject_ExpectEqualJson()
        {
            // Assemble
            var currentDatetime = DateTime.UtcNow.ToString();
            var sampleObject = new { id = 1, data = "json data", timestamp = currentDatetime };
            string expectedJson = $"{{\"id\":1,\"data\":\"json data\",\"timestamp\":\"{currentDatetime}\"}}";

            // Act
            string actualJson = sampleObject.ToJson();

            // Assert
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void WhenSerializingAndDeserializing_ExpectEqualObjects()
        {
            // Assemble
            var expectedObject = new VisitorViewModel()
            {
                City = "unit",
                Country = "test",
                CreatedUtc = DateTime.UtcNow                
            };

            var jsonString = expectedObject.ToJson();

            // Act
            var actualObject = jsonString.ToObject<VisitorViewModel>();

            // Assert
            Assert.Equal(expectedObject.City, actualObject.City);
            Assert.Equal(expectedObject.Country, actualObject.Country);
            Assert.Equal(expectedObject.CreatedUtc, actualObject.CreatedUtc);
        }
    }
}
