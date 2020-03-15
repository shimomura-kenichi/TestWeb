using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Attributes;

namespace TestWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PositionAuthorizeAttribute());
        }
    }
}
