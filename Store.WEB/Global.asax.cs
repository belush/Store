using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Store.BLL;
using Store.WEB.Controllers;
using Store.WEB.Infrastructure;

namespace Store.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }

        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        //protected void Application_Error()
        //{
        //    var exception = Server.GetLastError();
        //    if (exception is HttpException)
        //    {
        //        var httpException = (HttpException)exception;
        //        Response.StatusCode = httpException.GetHttpCode();
        //        string redirectUrl = string.Empty;
        //        switch (Response.StatusCode)
        //        {
        //            case 404:
        //                redirectUrl = "~/Error/NotFound";
        //                break;
        //            default:
        //                redirectUrl = "~Error";
        //                break;
        //        }
        //        Response.RedirectPermanent(redirectUrl);
        //    }
        //}
    }
}