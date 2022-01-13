using BrunoCampiol.Common.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BrunoCampiol.UnitTest.Common
{
    public class ExtensionsTest
    {
        [Fact]
        public void TestAllExceptionMessages()
        {
            // Assemble
            Exception innerInnerException = new Exception("Inner Inner Exception Message");
            Exception innerException = new Exception("Inner Exception Message", innerInnerException);
            Exception exception = new Exception("Exception Message", innerException);
            string expectedMessage = "(Exception): Exception Message (InnerException): Inner Exception Message (InnerException): Inner Inner Exception Message";

            // Act
            string actualMessage = exception.AllExceptionMessages();

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

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
            DateTime dateTime = DateTime.Now;
           
            // Act
            string returnedString = dateTime.ToTimeAgo();

            // Assert
            Assert.False(String.IsNullOrEmpty(returnedString));
            Assert.False(String.IsNullOrWhiteSpace(returnedString));
            Assert.True(returnedString.Length > 0);
        }
    }
}
