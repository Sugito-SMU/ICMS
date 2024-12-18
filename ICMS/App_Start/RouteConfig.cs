using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ICMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("oauth/token");

            routes.MapRoute(
                 name: "Unauthorized",
                 url: "Home/Unauthorized",
                 defaults: new { controller = "Home", action = "Unauthorized" }
             );

            routes.MapRoute(
                 name: "Error",
                 url: "Home/Error",
                 defaults: new { controller = "Home", action = "Error" }
             );

            routes.MapRoute(
                name: "Angular Via Index",
                url: "{*clientRoute}",
                defaults: new { controller = "Home", action = "Index" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
