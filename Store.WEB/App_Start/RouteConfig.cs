using System.Web.Mvc;
using System.Web.Routing;

namespace Store.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Goods", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}