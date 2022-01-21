using BrunoCampiol.CrossCutting.Common.Common;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Common
{
    public class ExceptionExtensionsTest
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
    }
}