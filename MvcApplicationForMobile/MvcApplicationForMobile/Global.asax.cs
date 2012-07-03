using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using System.Data.Entity;
using MvcApplicationForMobile.Models;
using MvcApplicationForMobile.DAL;

namespace MvcApplicationForMobile
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "User", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                
            );



        }

        protected void Application_Start()
        {
            Database.SetInitializer<UserContext>(new UserInitializer());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<RazorViewEngine>().First());
            ViewEngines.Engines.Add(new MobileCapableRazorViewEngine());
            ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<WebFormViewEngine>().First());
            ViewEngines.Engines.Add(new MobileCapableWebFormViewEngine());
        }
    }
}