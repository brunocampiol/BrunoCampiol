using BrunoCampiol.Common.Common;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Common
{
    public class StaticLibraryTest
    {
        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1SecondAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 0, 1);
            string exptectedResult = "1 seconds ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When5SecondsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 0, 5);
            string exptectedResult = "5 seconds ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1MinuteAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 1, 0);
            string exptectedResult = "a minute ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When15MinuteAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 15, 0);
            string exptectedResult = "15 minutes ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1HourAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 1, 0, 0);
            string exptectedResult = "an hour ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When10HourAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 10, 0, 0);
            string exptectedResult = "10 hours ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When23HourAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 01, 23, 59, 59);
            string exptectedResult = "23 hours ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1DayAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 02, 0, 0, 0);
            string exptectedResult = "yesterday";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When29DaysAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 01, 30, 0, 0, 0);
            string exptectedResult = "29 days ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1MonthAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 02, 01, 0, 0, 0);
            string exptectedResult = "a month ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1MonthAgo2()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 11, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 12, 01, 0, 0, 0);
            string exptectedResult = "a month ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When5MonthsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2000, 05, 01, 0, 0, 0);
            string exptectedResult = "4 months ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1YearAgoExact()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2001, 01, 01, 0, 0, 0);
            string exptectedResult = "a year ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1YearAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2001, 05, 01, 0, 0, 0);
            string exptectedResult = "a year ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When2YearsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2002, 01, 01, 0, 0, 0);
            string exptectedResult = "2 years ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When20YearsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            DateTime secondDate = new DateTime(2020, 01, 01, 0, 0, 0);
            string exptectedResult = "20 years ago";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_WhenSameDate()
        {
            // Assemble
            DateTime date = new DateTime(2000, 01, 01, 0, 0, 0);
            string exptectedResult = "right now";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(date, date);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When1MilisecondsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 0, 0, 1, DateTimeKind.Local);
            string exptectedResult = "right now";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_When999MilisecondsAgo()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            DateTime secondDate = new DateTime(2000, 01, 01, 0, 0, 0, 999, DateTimeKind.Local);
            string exptectedResult = "right now";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, secondDate);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public void TestToTimeAgo_ExpectTimeAgo_WhenInvalidDate()
        {
            // Assemble
            DateTime firstDate = new DateTime(2000, 01, 01, 0, 0, 0);
            string exptectedResult = "didn't happen yet";

            // Act
            string actualResult = StaticLibrary.ToTimeAgo(firstDate, DateTime.MinValue);

            // Assert
            Assert.Equal(exptectedResult, actualResult);
        }
    }
}
