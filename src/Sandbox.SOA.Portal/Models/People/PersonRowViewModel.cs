using System;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonRowViewModel
    {
        public Guid Identifier { get; set; }

        public PersonNameViewModel Name { get; set; }
        public PersonMobilePhoneInfo MobilePhone { get; set; }
        public PersonAddressInfo DefaultAddress { get; set; }
    }
}