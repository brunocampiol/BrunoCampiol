using System;
using System.Text;

namespace BrunoCampiol.CrossCutting.Common.Common
{
    public static class ExceptionExtensions
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
    }
}
