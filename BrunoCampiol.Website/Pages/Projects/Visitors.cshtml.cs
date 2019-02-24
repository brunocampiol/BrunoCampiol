﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrunoCampiol.Common.Common;
using BrunoCampiol.Common.Global;
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
                rows += "<div class=\"col-2 ellipsis text-center\">";
                rows += visitor.IP;
                rows += "</div>";
                rows += "<div class=\"col-2 ellipsis  text-center\">";
                rows += visitor.CREATED_ON_UTC.ToTimeAgo();
                rows += "</div>";
                rows += "<div class=\"col-2 ellipsis text-center\">";
                rows += visitor.COUNTRY;
                rows += " <span class=\"flag-icon flag-icon-" + visitor.COUNTRY.ToLower() + "\"></span>";
                rows += "</div>";
                rows += "<div class=\"col-6 ellipsis\">";
                rows += visitor.REGION + " - " + visitor.CITY;
                rows += "</div>";
                rows += "</div>";
            }

            return rows;
        }

        private List<VISITORS> GetVisitorList(int page, int pageSize)
        {
            var htmlString = String.Empty;
            var connectionString = GlobalSettings.Instance.ConnectionString;

            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connectionString).Options;

            DatabaseContext context = new DatabaseContext(options);
            Repository<VISITORS> repository = new Repository<VISITORS>(context);

            IQueryable<VISITORS> visitorListQuery = repository.GetAll().OrderByDescending(visitor => visitor.CREATED_ON_UTC).Skip((page - 1) * pageSize).Take(pageSize);
            List<VISITORS> listVisitors = visitorListQuery.ToList();

            return listVisitors;
        }
    }
}