using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Html
{
    public class Table
    {
        public TableHeader TableHeader { get; set; }

        public IList<TableRow> TableRows { get; set; }

        public string CssClass { get; set; }

        public Table(TableHeader header, IList<TableRow> rows)
        {
            TableHeader = header;
            TableRows = rows;
        }

        public string ToHtml()
        {
            string html = String.Empty;

            if (TableHeader == null) return html;
            if (TableRows == null) return html;

            // start table
            if (String.IsNullOrEmpty(CssClass)) html += "<table>";
            else html += "<table class=\"" + CssClass + "\">";

            // add header
            html += TableHeader.ToHtml();


            foreach (TableRow row in TableRows)
            {
                html += row.ToHtml();
            }

            html += "</table>";

            return html;
        }
    }
}
