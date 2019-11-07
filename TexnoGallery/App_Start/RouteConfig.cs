using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TexnoGallery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //        name: "Products",
            //        url: "{id}/kategoriya/{*title}",
            //        defaults: new { controller = "Product", action = "product" },
            //       namespaces: new string[] { "TexnoGallery.Controllers" }
            //        );
            //routes.MapRoute(
            //        name: "ProductDetail",
            //        url: "{id}/{*title}",
            //        defaults: new { controller = "Product", action = "Details" },
            //       namespaces: new string[] { "TexnoGallery.Controllers" }
            //        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] {"TexnoGallery.Controllers"}
            );
        }
    }
}
