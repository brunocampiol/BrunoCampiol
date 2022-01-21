using System;
using UAParser;

namespace BrunoCampiol.CrossCutting.Common.Common
{
    /// <summary>
    /// Purpose is to have common static code that will be shared across project
    /// </summary>
    public static class StaticLibrary
    {
        public static string GetBrowserName(string userAgentHeader)
        {
            if (String.IsNullOrEmpty(userAgentHeader)) throw new ArgumentException("Cannot be null or empty", nameof(userAgentHeader));

            Parser uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgentHeader);

            string browserName = null;

            if (c != null && c.UserAgent != null)
            {
                browserName = c.UserAgent.Family;

                if (!String.IsNullOrEmpty(c.UserAgent.Major)) browserName += " " + c.UserAgent.Major;
            }
            
            return browserName;
        }

        public static string GetOperationalSystemName(string userAgentHeader)
        {
            if (String.IsNullOrEmpty(userAgentHeader)) throw new ArgumentException("Cannot be null or empty", nameof(userAgentHeader));

            Parser uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgentHeader);

            string osName = null;

            if (c != null && c.OS != null)
            {
                osName = c.OS.Family;

                if (!String.IsNullOrEmpty(c.OS.Major)) osName += " " + c.OS.Major;
            }

            return osName;
        }
    }
}
