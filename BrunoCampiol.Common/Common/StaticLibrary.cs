using System;
using System.Collections.Generic;
using System.Text;
using UAParser;

namespace BrunoCampiol.Common.Common
{
    /// <summary>
    /// Purpose is to have common static code that will be shared across project
    /// </summary>
    public static class StaticLibrary
    {
        /// <summary>
        /// Gives in english language how much time the DateTime spent from now
        /// </summary>
        /// <param name="from">The date you want to check</param>
        /// <param name="to">You shall use DateTime.Now here</param>
        /// <returns></returns>
        public static string ToTimeAgo(DateTime from, DateTime to)
        {
            string result = string.Empty;
            var timeSpan = to.Subtract(from);


            if (timeSpan.TotalMilliseconds < 0)
            {
                result = "didn't happen yet";
            }
            else if (timeSpan < TimeSpan.FromMilliseconds(1000))
            {
                result = "right now";
            }
            else if (timeSpan < TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan < TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ? String.Format("{0} minutes ago", timeSpan.Minutes) : "a minute ago";
            }
            else if (timeSpan < TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ? String.Format("{0} hours ago", timeSpan.Hours) : "an hour ago";
            }
            else if (timeSpan < TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? String.Format("{0} days ago", timeSpan.Days) : "yesterday";
            }
            else if (timeSpan < TimeSpan.FromDays(365))
            {
                int pastDays = (int)(timeSpan.Days / 30);
                if (pastDays == 1) result = "a month ago";
                else result = String.Format("{0} months ago", pastDays);
            }
            else
            {
                int pastYears = (int)(timeSpan.Days / 365);
                if (pastYears == 1) result = "a year ago";
                else result = String.Format("{0} years ago", pastYears);
            }

            return result;
        }

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
