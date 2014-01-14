using System.Web.Mvc;
using System.Web.Routing;

namespace Sandbox.SOA.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
        }

        public const string Home = "Home";
        public const string People = "People";
        public const string PersonEdit = "PersonEdit";
    }
}
