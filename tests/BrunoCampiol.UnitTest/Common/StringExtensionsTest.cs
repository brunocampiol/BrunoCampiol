using BrunoCampiol.CrossCutting.Common.Common;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Common
{
    public class StringExtensionsTest
    {
        [Fact]
        public void TestTruncate_ExpectTruncatedString_WhenValidString()
        {
            // Assemble
            string message = "1234567890";
            string expectedMessage = "12345";

            // Act
            string actualMessage = message.Truncate(5);

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void TestTruncate_ExpectEqualString_WhenValidString()
        {
            // Assemble
            string message = "123";
            string expectedMessage = "123";

            // Act
            string actualMessage = message.Truncate(10);

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void TestTruncate_ExpectNull_WhenStringIsNull()
        {
            // Assemble
            string expectedMessage = null;

            // Act
            string actualMessage = expectedMessage.Truncate(5);

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void TestToTimeAgo_ExpectNoErrors_WhenCalled()
        {
            // Assemble
            DateTime dateTime = DateTime.UtcNow.AddHours(-3);

            // Act
            string returnedString = dateTime.ToTimeAgo();

            // Assert
            Assert.False(String.IsNullOrEmpty(returnedString));
            Assert.False(String.IsNullOrWhiteSpace(returnedString));
            Assert.True(returnedString.Length > 0);
            Assert.True(returnedString.Contains('3'));
        }
    }
}
