using BrunoCampiol.Application.Interfaces;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.CrossCutting.Common.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrunoCampiol.UI.Web.Pages.Projects
{
    public class VisitorsModel : PageModel
    {
        public string visitorListString { get; private set; }

        public string pieDataScript { get; private set; }

        private readonly IVisitorAppService _appService;

        public VisitorsModel(IVisitorAppService appService)
        {
            _appService = appService;
        }

        public void OnGet()
        {
            var visitorList = GetVisitorList(1, 50);
            visitorListString = GetVisitorListAsHtml(visitorList);
        }

        public IActionResult OnGetRow(int id)
        {
            var visitorList = GetVisitorList(id, 50);

            string rows = String.Empty;

            if (visitorList != null && visitorList.Count > 0)
            {
                rows = GetVisitorListAsHtml(visitorList);
                rows += "<div class=\"next\" style=\"height:30px;\"></div>";
            }

            return new JsonResult(rows);
        }

        private string GetVisitorListAsHtml(List<VisitorViewModel> visitorList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (VisitorViewModel visitor in visitorList)
            {
                sb.Append("<div class=\"row visitor-text\">");
                sb.Append("<div class=\"col-3 ellipsis text-center responsive-table-text-visitors\">");
                sb.Append(visitor.Ip);
                sb.Append("</div>");
                sb.Append("<div class=\"col-1 ellipsis text-center responsive-table-text-visitors\">");
                sb.Append(" <span class=\"flag-icon flag-icon-" + visitor.Country.ToLower() + "\"></span>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-2 ellipsis  text-center responsive-table-text-visitors\">");
                sb.Append(visitor.CreatedUtc.ToTimeAgo());
                sb.Append("</div>");
                sb.Append("<div class=\"col-1 ellipsis text-center responsive-table-text-visitors\">");
                sb.Append(GetOSIcon(visitor.ClientOS));
                sb.Append("  ");
                sb.Append(GetBrowserIcon(visitor.ClientBrowser));
                sb.Append("</div>");
                sb.Append("<div class=\"col-5 ellipsis responsive-table-text-visitors\">");
                sb.Append(visitor.Region + " - " + visitor.City);
                sb.Append("</div>");
                sb.Append("</div>");
            }

            return sb.ToString();
        }

        private List<VisitorViewModel> GetVisitorList(int page, int pageSize)
        {
            //var htmlString = String.Empty;
            //Repository<VISITORS> repository = new Repository<VISITORS>(_appService);
            //IQueryable<VISITORS> visitorListQuery = repository.GetAllNoTrack().OrderByDescending(visitor => visitor.CREATED_ON_UTC).Skip((page - 1) * pageSize).Take(pageSize);
            //List<VISITORS> listVisitors = visitorListQuery.ToList();
            //return listVisitors;

            var visitors = _appService.GetPagedVisitors(page, pageSize);
            return visitors.ToList();
        }

        //private Color GetRandomColor()
        //{
        //    Random rnd = new Random();

        //    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

        //    return randomColor;
        //}

        //private string GetRandomColorScript(string colorHardness)
        //{
        //    Color color = GetRandomColor();

        //    string scriptColor = $"'rgba({color.R.ToString()},{color.G.ToString()},{color.B.ToString()},{colorHardness})'";

        //    return scriptColor;
        //}

        //private List<CountryChartData> GetDatabasePieData()
        //{
        //    Repository<VISITORS> repository = new Repository<VISITORS>(_databaseContext);

        //    IQueryable<CountryChartData> visitorListQuery = repository.GetAllNoTrack()
        //                                                        .GroupBy(group => group.COUNTRY)
        //                                                        .Select(item => new CountryChartData { Country = item.First().COUNTRY,
        //                                                                                                Count = item.Count() });

        //    List<CountryChartData> countriesData = visitorListQuery.ToList();

        //    return countriesData;
        //}

        //private void GetPieData()
        //{
        //    List<CountryChartData> countriesData = GetDatabasePieData();

        //    string script = String.Empty;

        //    script += "<script>";

        //    script += " countrySum = [";

        //    for (int i = 0; i < countriesData.Count; i++)
        //    {
        //        if (i == countriesData.Count - 1) script += countriesData[i].Count + "]; ";
        //        else script += countriesData[i].Count + ", ";
        //    }

        //    script += " countryLabels = [";

        //    for (int i = 0; i < countriesData.Count; i++)
        //    {
        //        if (i == countriesData.Count - 1) script += "'" + countriesData[i].Country + "']; ";
        //        else script += "'" + countriesData[i].Country + "', ";
        //    }

        //    script += " countryColors = [";

        //    for (int i = 0; i < countriesData.Count; i++)
        //    {
        //        if (i == countriesData.Count - 1) script += GetRandomColorScript("1") + "]; ";
        //        else script += GetRandomColorScript("1") + ", ";
        //    }


        //    script += "</script>";

        //    pieDataScript = script;
        //}

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