using System.Web;
using System.Web.Http;

using Castle.Windsor;

namespace Sandbox.SOA.Services.Api
{
    public class WebApiApplication : HttpApplication
    {
        readonly IWindsorContainer _container;

        protected WebApiApplication()
        {
            _container = new WindsorContainer();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            WindsorConfig.Register(_container, GlobalConfiguration.Configuration);
        }

        public override void Dispose()
        {
            _container.Dispose();

            base.Dispose();
        }
    }
}