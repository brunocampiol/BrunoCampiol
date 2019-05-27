using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrunoCampiol.Common.Common;
using BrunoCampiol.Common.Global;
using BrunoCampiol.Repository.Context;
using BrunoCampiol.Repository.Generic;
using BrunoCampiol.Repository.Models;
using BrunoCampiol.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace BrunoCampiol.Website.Pages
{
    [Authorize]
    public class PostsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IList<POSTS> PostList;

        public PostsModel(IConfiguration configuration)
        {
            _configuration = configuration;

            PostList = new List<POSTS>();
        }

        public void OnGet()
        {
            var connectionString = GlobalSettings.Instance.ConnectionString;
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connectionString).Options;
            DatabaseContext context = new DatabaseContext(options);
            Repository<POSTS> repository = new Repository<POSTS>(context);

            int page = 1;
            int pageSize = 50;

            IQueryable<POSTS> postQuery = repository.GetAll().OrderByDescending(post => post.CREATED_ON_UTC).Skip((page - 1) * pageSize).Take(pageSize);
            PostList = postQuery.ToList();

            repository.Dispose();
        }

        public void OnPost()
        {
            // Gets local information from user
            string postComments = Request.Form["postComments"];
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string browserName = StaticLibrary.GetBrowserName(userAgent);
            string osName = StaticLibrary.GetOperationalSystemName(userAgent);

            // Gets IP-based information
            string host = _configuration["IpApiService:Host"];
            string resource = _configuration["IpApiService:Resource"];

            WebClient2 webClient = new WebClient2(host);
            string jsonData = webClient.HttpGet(resource + ipAddress).Result;

            JObject jObject = JObject.Parse(jsonData);

            POSTS post = new POSTS();
            post.USER_POST = postComments;
            post.USER_NAME = User.Identity.Name;
            post.USER_MEDIA = User.Identity.AuthenticationType;
            post.USER_IP = ipAddress;
            post.USER_OS = osName;
            post.USER_BROWSER = browserName;
            post.USER_COUNTRY = (string)jObject["countryCode"];
            post.CREATED_ON_UTC = DateTime.UtcNow;

            var connectionString = GlobalSettings.Instance.ConnectionString;
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connectionString).Options;
            DatabaseContext context = new DatabaseContext(options);
            Repository<POSTS> repository = new Repository<POSTS>(context);

            repository.Add(post);
            repository.Save();
            repository.Dispose();
        }
    }
}