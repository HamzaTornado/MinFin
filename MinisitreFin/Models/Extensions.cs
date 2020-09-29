using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.Extensions
{
    public static class HtmlHelperExtension
    {
        public static IHtmlString DisplayMessage<T>(this HtmlHelper<T> htmlHelper, string content)
        {
            return htmlHelper.Raw($@"
                <div class=""note"">
                  <p>
                     {content ?? "&nbsp;"}
                  </p>
                </div>
            ");
        }
    }
}