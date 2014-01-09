﻿using System.Threading.Tasks;
using System.Web.Mvc;

using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Portal.Controllers
{
    [RoutePrefix("people/{person.identifier}/addresses")]
    public class PersonAddressesController : Controller
    {
        readonly ActionHandler _actionHandler;

        public PersonAddressesController(ICommandHandler commandHandler)
        {
            _actionHandler = new ActionHandler(
                this, View, RedirectToAction,
                commandHandler);
        }

        public PersonAddressesController() :
            this(new WebApiClientCommandHandler("http://localhost:60746/")
                     .Get<PersonAddressSearchCriteria, PersonAddressSearchResult>("people/{person.identifier}/addresses"))
        {
        }

        [Route("")]
        public async Task<ActionResult> Index(PersonAddressSearchCriteria model)
        {
            return _actionHandler.With(model).Returns<PersonAddressSearchResult>();
        }
    }
}