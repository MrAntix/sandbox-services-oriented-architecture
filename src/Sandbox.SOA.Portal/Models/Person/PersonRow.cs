using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;

namespace Sandbox.SOA.Portal.Models.Person
{
    public class PersonRow
    {
        public string Identifier { get; set; }
        
        public PersonNameEdit Name { get; set; }
        public PersonMobilePhoneInfo MobilePhone { get; set; }
        public PersonAddressInfo DefaultAddress { get; set; }
    }
}