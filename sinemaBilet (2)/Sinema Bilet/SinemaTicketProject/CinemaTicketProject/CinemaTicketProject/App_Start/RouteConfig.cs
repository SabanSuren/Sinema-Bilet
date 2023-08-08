using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CinemaTicketProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //        name: "Home",
            //    url: "{Home}/{Film}/{FilmId}",
            //    defaults: new { controller = "Home", action = "Film" }
            // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "AnaSayfa", id = UrlParameter.Optional }
            );
        }
    }
}
