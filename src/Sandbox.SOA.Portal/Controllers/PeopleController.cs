using System.Web.Mvc;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Portal.Models;

namespace Sandbox.SOA.Portal.Controllers
{
    [RoutePrefix("people")]
    public class PeopleController : Controller
    {
        readonly ActionHandler _actionHandler;

        public PeopleController(ICommandHandler commandHandler)
        {
            _actionHandler = new ActionHandler(
                this, View, RedirectToAction,
                commandHandler);
        }

        public PeopleController() :
            this(new WebApiClientCommandHandler("http://localhost:60746/")
                     .Get<PersonSearchCriteria, PersonGrid>("people"))
        {
        }

        [Route("")]
        public ActionResult Index(PersonSearchCriteria model)
        {
            return _actionHandler.With(model).Returns<PersonGrid>();
        }
    }
}