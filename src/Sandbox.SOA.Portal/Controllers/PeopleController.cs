using System.Linq;
using System.Web.Mvc;
using Antix.Data.Static;
using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Portal.Models;
using Sandbox.SOA.Portal.Models.Person;

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
                     .Get<PersonSearchCriteria, PersonGrid>("people")
                     .Get<PersonIdentifier, PersonEdit>("people/{identifier}")
                     .Put<Person>("people/{identifier}")
            )
        {
        }

        [Route("", Name = RouteConfig.People)]
        public ActionResult Index(PersonSearchCriteria model)
        {
            return _actionHandler.With(model).Returns<PersonGrid>();
        }

        [Route("edit/{identifier}", Name = RouteConfig.PersonEdit)]
        public ActionResult Edit(PersonIdentifier model)
        {
            ViewData["CountryCode"] = Phone.CountryConfigurations
                .Select(c => new SelectListItem
                {
                    Text = GetFormattedDialingPrefix(c),
                    Value = c.CountryCode
                });

            return _actionHandler.With(model).Returns<PersonEdit>();
        }

        [HttpPost]
        [Route("edit/{identifier}", Name = RouteConfig.PersonEditPost)]
        public ActionResult Edit(Person model)
        {
            return _actionHandler.With(model)
                                 .Done(() => Edit((PersonIdentifier) model));
        }

        static string GetFormattedDialingPrefix(PhoneCountryConfiguration config)
        {
            return string.Concat("+", config.CountryDialing, " ",
                                 string.IsNullOrWhiteSpace(config.NationalDirectDialing)
                                     ? ""
                                     : string.Concat("(", config.NationalDirectDialing, ") "));
        }
    }
}