using System.Linq;
using System.Web.Mvc;

namespace Sandbox.SOA.Portal
{
    public static class UrlExtensions
    {
        public static string Home(
            this UrlHelper urlHelper
            )
        {
            return urlHelper.RouteUrl(RouteConfig.Home);
        }

        public static string People(
            this UrlHelper urlHelper
            )
        {
            return urlHelper.RouteUrl(RouteConfig.People);
        }

        public static string PersonEdit(
            this UrlHelper urlHelper,
            string identifier
            )
        {
            return urlHelper.RouteUrl(RouteConfig.PersonEdit, new { identifier });
        }

        //public static string People(
        //    this UrlHelper urlHelper,
        //    string actionName = null,
        //    string personIdentifier = null)
        //{
        //    if (personIdentifier == null
        //        && actionName != null)
        //        personIdentifier = urlHelper.CurrentPersonIdentifier();

        //    return urlHelper.RouteUrl(RouteConfig.Root,
        //                              new
        //                                  {
        //                                      controller = "People",
        //                                      action = actionName,
        //                                      identifier = personIdentifier
        //                                  });
        //}
    }
}