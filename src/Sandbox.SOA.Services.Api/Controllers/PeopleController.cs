using System.Web.Http;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Services.Api.Controllers
{
    [RoutePrefix("people")]
    public class PeopleController : ApiController
    {
        readonly ICommandHandler _commandHandler;

        public PeopleController(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [Route("")]
        public PersonSearchResult Get([FromUri] PersonSearchCriteria model)
        {
            return _commandHandler.Handle<PersonSearchCriteria, PersonSearchResult>(model);
        }

        [Route("{identifier}")]
        public Person Get([FromUri] PersonIdentifier model)
        {
            return _commandHandler.Handle<PersonIdentifier, Person>(model);
        }

        [Route("")]
        public PersonIdentifier Post(PersonInfo model)
        {
            return _commandHandler.Handle<PersonInfo, PersonIdentifier>(model);
        }

        [Route("{identifier}")]
        public void Put(Person model)
        {
            _commandHandler.Handle(model);
        }

        [Route("{identifier}")]
        public void Delete([FromUri] PersonDelete model)
        {
            _commandHandler.Handle(model);
        }
    }
}