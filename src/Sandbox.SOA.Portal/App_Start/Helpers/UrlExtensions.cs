using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sandbox.SOA.Portal.Helpers
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

        public static string PersonCreate(
            this UrlHelper urlHelper
            )
        {
            return urlHelper.RouteUrl(RouteConfig.PersonCreate);
        }

        public static string PersonEdit(
            this UrlHelper urlHelper,
            Guid identifier
            )
        {
            return urlHelper.RouteUrl(RouteConfig.PersonEdit,
                                      new {identifier});
        }

        public static string PersonDelete(
            this UrlHelper urlHelper,
            Guid identifier
            )
        {
            return urlHelper.RouteUrl(RouteConfig.PersonDelete,
                                      new {identifier});
        }

        public static string PersonAddresses(
            this UrlHelper urlHelper,
            Guid personIdentifier
            )
        {
            return urlHelper.RouteUrl(RouteConfig.PersonAddresses,
                                      new RouteValueDictionary
                                          {
                                              {"person.identifier", personIdentifier}
                                          }
                );
        }
    }
}