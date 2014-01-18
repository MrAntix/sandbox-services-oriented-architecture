using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;

using Sandbox.SOA.Services.Data;
using Sandbox.SOA.Services.Migrations;

namespace Sandbox.SOA.Services.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            // filters
            config.Filters.Clear();
            config.Filters.Add(new ValidationExceptionFilter());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
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

        class ValidationExceptionFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(HttpActionContext context)
            {
                if (!context.ModelState.IsValid)
                    context.Response = context.Request
                                              .CreateErrorResponse(
                                                  HttpStatusCode.BadRequest, context.ModelState);
            }
        }
    }
}