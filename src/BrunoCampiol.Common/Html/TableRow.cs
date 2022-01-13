using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Html
{
    public class TableRow
    {
        public IList<string> Cells { get; set; }

        public TableRow(IList<string> cells)
        {
            Cells = cells;
        }

        public string ToHtml()
        {
            string html = String.Empty;

            if (Cells == null) return html;
            if (Cells.Count == 0) return html;

            html += "<tr>";

            foreach(string cell in Cells)
            {
                html += "<td>" + cell + "</td>";
            }

            html += "</tr>";

            return html;
        }
    }
}
