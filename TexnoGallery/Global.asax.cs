using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TexnoGallery
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    var context = HttpContext.Current;
        //    var response = context.Response;

        //    // enable CORS  
        //    response.AddHeader("Access-Control-Allow-Origin", "*");

        //    if (context.Request.HttpMethod == "OPTIONS")
        //    {
        //        response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        //        response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
        //        response.End();
        //    }
        //}
    }
  }
