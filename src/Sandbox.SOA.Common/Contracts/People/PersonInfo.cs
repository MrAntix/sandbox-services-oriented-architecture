using Sandbox.SOA.Common.Contracts.People.Addresses;

namespace Sandbox.SOA.Common.Contracts.People
{
    public class PersonInfo : PersonIdentifier
    {
        public PersonName Name { get; set; }
        public PersonMobilePhoneInfo MobilePhone { get; set; }
        public PersonAddressInfo DefaultAddress { get; set; }
    }
}