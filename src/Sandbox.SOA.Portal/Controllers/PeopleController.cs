using System.Linq;
using System.Web.Mvc;

using Antix.Data.Static;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Portal.Properties;

namespace Sandbox.SOA.Portal.Controllers
{
    [RoutePrefix("people")]
    public class PeopleController : Controller
    {
        readonly ActionHandler _actionHandler;

        public PeopleController(ICommandHandler commandHandler)
        {
            _actionHandler = new ActionHandler(
                this, AjaxView, RedirectToAction,
                commandHandler);
        }

        public PeopleController() :
            this(new ClientCommandHandler(Settings.Default.ServicesApiUrl)
                     .Get<PersonSearchCriteria, PersonSearchResult>("people")
                     .Get<PersonIdentifier, Person>("people/{identifier}")
                     .Get<PersonIdentifier, PersonInfo>("people/{identifier}")
                     .Post<PersonInfo, PersonIdentifier>("people")
                     .Put<Person>("people/{identifier}")
                     .Delete<PersonIdentifier>("people/{identifier}")
            )
        {
        }

        ActionResult AjaxView(object model)
        {
            return Request.IsAjaxRequest()
                       ? View(null, "_LayoutModal", model)
                       : View(model);
        }

        [Route("", Name = RouteConfig.People)]
        public ActionResult Index(PersonSearchCriteria model)
        {
            return _actionHandler.With(model).Returns<PersonSearchResult>();
        }

        [Route("create", Name = RouteConfig.PersonCreate)]
        public ActionResult Create()
        {
            return AjaxView(new PersonInfo());
        }

        [HttpPost]
        [Route("create", Name = RouteConfig.PersonCreatePost)]
        public ActionResult Create(PersonInfo model)
        {
            return _actionHandler.With(model).Returns<PersonIdentifier>()
                                 .Done("Edit", p => p);
        }

        [Route("{identifier}/edit", Name = RouteConfig.PersonEdit)]
        public ActionResult Edit(PersonIdentifier model)
        {
            ViewData["CountryCode"] = Phone.CountryConfigurations
                                           .Select(c => new SelectListItem
                                               {
                                                   Text = GetFormattedDialingPrefix(c),
                                                   Value = c.CountryCode
                                               });

            return _actionHandler.With(model).Returns<Person>();
        }

        [HttpPost]
        [Route("{identifier}/edit", Name = RouteConfig.PersonEditPost)]
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

        [Route("{identifier}/delete", Name = RouteConfig.PersonDelete)]
        public ActionResult Delete(PersonIdentifier model)
        {
            return _actionHandler.With(model).Returns<PersonInfo>();
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("{identifier}/delete", Name = RouteConfig.PersonDeletePost)]
        public ActionResult DeletePost(PersonIdentifier model)
        {
            return _actionHandler.With(model)
                                 .Done("Index");
        }
    }
}