using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BrunoCampiol.Common.Common;
using BrunoCampiol.Common.Global;
using BrunoCampiol.Common.Models;
using BrunoCampiol.Repository.Context;
using BrunoCampiol.Repository.Generic;
using BrunoCampiol.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrunoCampiol.Website.Pages.Projects
{
    public class VisitorsModel : PageModel
    {
        public string visitorListString { get; private set; }

        public string pieDataScript { get; private set; }


        private readonly DatabaseContext _databaseContext;

        public VisitorsModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void OnGet()
        {
            List<VISITORS> visitorList = GetVisitorList(1, 50);

            visitorListString = GetVisitorListAsHtml(visitorList);
        }

        public IActionResult OnGetRow(int id)
        {
            List<VISITORS> visitorList = GetVisitorList(id, 50);

            string rows = String.Empty;

            if (visitorList != null && visitorList.Count > 0)
            {
                rows = GetVisitorListAsHtml(visitorList);
                rows += "<div class=\"next\" style=\"height:30px;\"></div>";
            }

            return new JsonResult(rows);
        }

        private string GetVisitorListAsHtml(List<VISITORS> visitorList)
        {
            string rows = String.Empty;

            foreach (VISITORS visitor in visitorList)
            {
                rows += "<div class=\"row visitor-text\">";
                rows += "<div class=\"col-3 ellipsis text-center responsive-table-text-visitors\">";
                rows += visitor.IP;
                rows += "</div>";
                rows += "<div class=\"col-1 ellipsis text-center responsive-table-text-visitors\">";
                rows += " <span class=\"flag-icon flag-icon-" + visitor.COUNTRY.ToLower() + "\"></span>";
                rows += "</div>";
                rows += "<div class=\"col-2 ellipsis  text-center responsive-table-text-visitors\">";
                rows += visitor.CREATED_ON_UTC.ToTimeAgo();
                rows += "</div>";
                rows += "<div class=\"col-1 ellipsis text-center responsive-table-text-visitors\">";
                rows += GetOSIcon(visitor.CLIENT_OS);
                rows += "  ";
                rows += GetBrowserIcon(visitor.CLIENT_BROWSER);
                rows += "</div>";
                rows += "<div class=\"col-5 ellipsis responsive-table-text-visitors\">";
                rows += visitor.REGION + " - " + visitor.CITY;
                rows += "</div>";
                rows += "</div>";
            }

            return rows;
        }

        private List<VISITORS> GetVisitorList(int page, int pageSize)
        {
            var htmlString = String.Empty;

            Repository<VISITORS> repository = new Repository<VISITORS>(_databaseContext);

            IQueryable<VISITORS> visitorListQuery = repository.GetAll().OrderByDescending(visitor => visitor.CREATED_ON_UTC).Skip((page - 1) * pageSize).Take(pageSize);
            List<VISITORS> listVisitors = visitorListQuery.ToList();

            return listVisitors;
        }

        private Color GetRandomColor()
        {
            Random rnd = new Random();

            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            return randomColor;
        }

        private string GetRandomColorScript(string colorHardness)
        {
            Color color = GetRandomColor();

            string scriptColor = $"'rgba({color.R.ToString()},{color.G.ToString()},{color.B.ToString()},{colorHardness})'";

            return scriptColor;
        }

        private List<CountryChartData> GetDatabasePieData()
        {
            Repository<VISITORS> repository = new Repository<VISITORS>(_databaseContext);

            IQueryable<CountryChartData> visitorListQuery = repository.GetAll()
                                                                .GroupBy(group => group.COUNTRY)
                                                                .Select(item => new CountryChartData { Country = item.First().COUNTRY,
                                                                                                        Count = item.Count() });

            List<CountryChartData> countriesData = visitorListQuery.ToList();

            return countriesData;
        }

        private void GetPieData()
        {
            List<CountryChartData> countriesData = GetDatabasePieData();

            string script = String.Empty;

            script += "<script>";

            script += " countrySum = [";

            for (int i = 0; i < countriesData.Count; i++)
            {
                if (i == countriesData.Count - 1) script += countriesData[i].Count + "]; ";
                else script += countriesData[i].Count + ", ";
            }

            script += " countryLabels = [";

            for (int i = 0; i < countriesData.Count; i++)
            {
                if (i == countriesData.Count - 1) script += "'" + countriesData[i].Country + "']; ";
                else script += "'" + countriesData[i].Country + "', ";
            }

            script += " countryColors = [";

            for (int i = 0; i < countriesData.Count; i++)
            {
                if (i == countriesData.Count - 1) script += GetRandomColorScript("1") + "]; ";
                else script += GetRandomColorScript("1") + ", ";
            }


            script += "</script>";

            pieDataScript = script;
        }

        private string GetOSIcon(string clientOS)
        {
            string windows = "<i class=\"fab fa-windows\"></i>";
            string linux = "<i class=\"fab fa-linux\"></i>";
            string apple = "<i class=\"fab fa-apple\"></i>";
            string android = "<i class=\"fab fa-android\"></i>";
            string unknown = "<i class=\"fas fa-question\" data-toggle=\"tooltip\" data-placement=\"left\" title=\"" + clientOS + "\"></i>";

            if (clientOS == null || clientOS.Length == 0)
            {
                return unknown;
            }
            if (clientOS.ContainsIgnoreCase("windows"))
            {
                return windows;
            }
            if (clientOS.ContainsIgnoreCase("linux") ||
                clientOS.ContainsIgnoreCase("red hat") ||
                clientOS.ContainsIgnoreCase("ubuntu") ||
                clientOS.ContainsIgnoreCase("fedora"))
            {
                return linux;
            }
            if (clientOS.ContainsIgnoreCase("ios") ||
                clientOS.ContainsIgnoreCase("mac"))
            {
                return apple;
            }
            if (clientOS.ContainsIgnoreCase("android"))
            {
                return android;
            }

            return unknown;
        }

        private string GetBrowserIcon(string browser)
        {
            string internetExplorer = "<i class=\"fab fa-internet-explorer\"></i>";
            string chrome = "<i class=\"fab fa-chrome\"></i>";
            string safari = "<i class=\"fab fa-safari\"></i>";
            string firefox = "<i class=\"fab fa-firefox\"></i>";
            string edge = "<i class=\"fab fa-edge\"></i>";
            string opera = "<i class=\"fab fa-opera\"></i>";
            string unknown = "<i class=\"fas fa-question\" data-toggle=\"tooltip\" data-placement=\"right\" title=\"" + browser + "\"></i>";

            if (browser == null || browser.Length == 0)
            {
                return unknown;
            }
            if (browser.ContainsIgnoreCase("ie") ||
                browser.ContainsIgnoreCase("explorer"))
            {
                return internetExplorer;
            }
            if (browser.ContainsIgnoreCase("chrome") ||
                browser.ContainsIgnoreCase("chromium"))
            {
                return chrome;
            }
            if (browser.ContainsIgnoreCase("safari"))
            {
                return safari;
            }
            if (browser.ContainsIgnoreCase("firefox") ||
                browser.ContainsIgnoreCase("mozilla"))
            {
                return firefox;
            }
            if (browser.ContainsIgnoreCase("edge"))
            {
                return edge;
            }
            if (browser.ContainsIgnoreCase("opera"))
            {
                return opera;
            }

            return unknown;

        }

    }
}