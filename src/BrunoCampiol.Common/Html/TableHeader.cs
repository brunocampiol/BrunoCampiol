using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Html
{
    public class TableHeader
    {
        public IList<string> Headers { get; set; }

        public TableHeader(IList<string> headers)
        {
            Headers = headers;
        }

        public string ToHtml()
        {
            string html = String.Empty;

            if (Headers == null) return html;
            if (Headers.Count == 0) return html;

            html += "<tr>";

            foreach (string cell in Headers)
            {
                html += "<th>" + cell + "</th>";
            }

            html += "</tr>";

            return html;
        }
    }
}
