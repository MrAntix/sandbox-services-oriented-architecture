using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services.Models;
using Sandbox.SOA.Portal.Models.People;

namespace Sandbox.SOA.Portal.Models.PersonAddresses
{
    public class PersonAddressesIndexViewModel : SearchResult<PersonAddressInfo>
    {
        public PersonInfoViewModel Person { get; set; }
    }
}