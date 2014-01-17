using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using FluentValidation.Mvc;

namespace Sandbox.SOA.Portal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);


            WindsorConfig.Register(new WindsorContainer());
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}