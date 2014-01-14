using System.Web.Mvc;

namespace Sandbox.SOA.Portal.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name = RouteConfig.Home)]
        public ActionResult Index()
        {
            return View();
        }
    }
}