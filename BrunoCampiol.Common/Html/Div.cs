using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Common.Html
{
    public class Div
    {
        public string Id { get; set; }

        public string CssClass { get; set; }

        public string Content { get; set; }

        public string ToHtml()
        {
            // Adds div css and ids
            string html = "<div";
            if (!String.IsNullOrEmpty(Id)) html += " id=\"" + Id + "\"";
            if (!String.IsNullOrEmpty(CssClass)) html += " class=\"" + CssClass + "\"";
            html += ">";

            // Add div content
            if (!String.IsNullOrEmpty(Content)) html += Content;

            // Closes div tag
            html += "</div>";

            return html;
        }
    }
}
