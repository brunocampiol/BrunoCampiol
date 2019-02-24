using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves all exceptions messages from an exception and its inner exceptions
        /// </summary>
        /// <param name="ex">The exception to retrieve messages</param>
        /// <param name="message">Optional information to add before the message</param>
        /// <returns>String with all exceptions messages from exception and its inner exceptions</returns>
        public static string AllExceptionMessages(this Exception ex, string message = "(Exception): ")
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(message + ex.Message);

            if (ex.InnerException != null)
            {
                sb.Append(ex.InnerException.AllExceptionMessages(" (InnerException): "));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Truncates the string to a specified length
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            if (text == null || text.Length <= maxLength) return text;

            var truncatedString = text.Substring(0, maxLength);
            truncatedString = truncatedString.TrimEnd();

            return truncatedString;
        }

        /// <summary>
        /// Same as String.Contains but is case insensitive
        /// </summary>
        /// <param name="text">The string to be cheked</param>
        /// <param name="value">The pattern to be cheked</param>
        /// <returns>A boolean if the text contains the value ignoring case</returns>
        public static bool ContainsIgnoreCase(this string text, string value)
        {
            if (text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gives in english language how much time the DateTime spent from now
        /// </summary>
        /// <param name="dateTime">In UTC!</param>
        /// <returns></returns>
        public static string ToTimeAgo(this DateTime dateTime)
        {
            return StaticLibrary.ToTimeAgo(dateTime, DateTime.UtcNow);
        }
    }
}
