using Sandbox.SOA.Common.Services.Models;

namespace Sandbox.SOA.Common.Contracts.People.Addresses
{
    public class PersonAddressSearchCriteria : SearchCriteria
    {
        public PersonIdentifier Person { get; set; }
    }
}