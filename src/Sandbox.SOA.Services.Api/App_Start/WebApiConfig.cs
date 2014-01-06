using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Sandbox.SOA.Services.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{identifier}",
            //    defaults: new { },
            //    constraints: new { identifier = new GuidConstraint() }
            //    );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiSearch",
            //    routeTemplate: "{controller}/search",
            //    defaults: new {action = "Search"}
            //    );
        }

        class GuidConstraint : IHttpRouteConstraint
        {
            public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
                              IDictionary<string, object> values,
                              HttpRouteDirection routeDirection)
            {
                if (values.ContainsKey(parameterName))
                {
                    var stringValue = values[parameterName] as string;

                    if (!string.IsNullOrEmpty(stringValue))
                    {
                        Guid guidValue;

                        return Guid.TryParse(stringValue, out guidValue) && (guidValue != Guid.Empty);
                    }
                }

                return false;
            }
        }
    }
}