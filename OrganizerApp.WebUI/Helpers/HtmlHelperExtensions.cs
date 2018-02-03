using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizerApp.WebUI.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString GetPriorityCssClassName(this HtmlHelper htmlHelper , string priority)
        {
            if (String.Equals(priority , "low" , StringComparison.Ordinal)) { return new HtmlString("lowPriority"); }
            if (String.Equals(priority , "medium" , StringComparison.Ordinal)) { return new HtmlString("mediumPriority"); }
            if (String.Equals(priority , "high" , StringComparison.Ordinal)) { return new HtmlString("highPriority"); }

            return new HtmlString("");
        }
    }
}