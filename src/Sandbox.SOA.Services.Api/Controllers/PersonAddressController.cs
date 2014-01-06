using System.Web.Http;

using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Services.Api.Controllers
{
    [RoutePrefix("people/{person.identifier}/addresses")]
    public class PersonAddressController : ApiController
    {
        readonly ICommandHandler _commandHandler;

        public PersonAddressController(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [Route("")]
        public PersonAddressSearchResult Get([FromUri] PersonAddressSearchCriteria model)
        {
            return _commandHandler.Handle<PersonAddressSearchCriteria, PersonAddressSearchResult>(model);
        }

        [Route("{identifier}")]
        public PersonAddress Get([FromUri] PersonAddressIdentifier model)
        {
            return _commandHandler.Handle<PersonAddressIdentifier, PersonAddress>(model);
        }

        [Route("")]
        public PersonAddressIdentifier Post(PersonAddressInfo model)
        {
            return _commandHandler.Handle<PersonAddressInfo, PersonAddressIdentifier>(model);
        }

        [Route("{identifier}")]
        public void Put(PersonAddress model)
        {
            _commandHandler.Handle(model);
        }

        [Route("{identifier}")]
        public void Delete([FromUri] PersonAddressDelete model)
        {
            _commandHandler.Handle(model);
        }
    }
}