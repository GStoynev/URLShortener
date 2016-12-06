using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace URLShortener.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Redirect",
                url: "{slugOrId}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{slugOrId}",
                defaults: new { controller = "Home", action = "Index", slugOrId = UrlParameter.Optional }
            );
        }
    }
}
