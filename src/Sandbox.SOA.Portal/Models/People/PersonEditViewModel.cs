using System;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonEditViewModel
    {
        public Guid Identifier { get; set; }
        public PersonNameEditViewModel Name { get; set; }
        public PersonMobilePhoneEditViewModel MobilePhone { get; set; }
    }
}