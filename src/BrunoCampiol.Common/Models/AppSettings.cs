using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Models
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public IPServiceAPIProvider IPServiceAPIProvider { get; set; }
        public GithubProvider GitHubProvider { get; set; }
        public TwitterProvider TwitterProvider { get; set; }
        public FacebookProvider FacebookProvider { get; set; }
    }
}