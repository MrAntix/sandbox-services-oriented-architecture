using Sandbox.SOA.Common.Contracts.People.Addresses;

namespace Sandbox.SOA.Portal.Models
{
    public class PersonRow
    {
        public string Identifier { get; set; }
        
        public PersonNameView Name { get; set; }
        public PersonAddressInfo DefaultAddress { get; set; }
    }
}