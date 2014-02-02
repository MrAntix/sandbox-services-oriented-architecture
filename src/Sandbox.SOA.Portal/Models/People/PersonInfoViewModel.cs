using System;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonInfoViewModel
    {
        public Guid Identifier { get; set; }
        public PersonNameViewModel Name { get; set; }
    }
}