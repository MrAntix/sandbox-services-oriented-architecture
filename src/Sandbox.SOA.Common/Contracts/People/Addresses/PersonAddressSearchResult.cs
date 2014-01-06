using Sandbox.SOA.Common.Services.Models;

namespace Sandbox.SOA.Common.Contracts.People.Addresses
{
    public class PersonAddressSearchResult : SearchResult<PersonAddressInfo>
    {
        public PersonInfo Person { get; set; }
    }
}