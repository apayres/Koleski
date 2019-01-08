using System.Web.Mvc;
using System.Web.Routing;

namespace Koleski.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Default", url: "Index.html", defaults: null);
        }
    }
}
