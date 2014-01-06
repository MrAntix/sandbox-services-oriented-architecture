using System;
using System.Collections.Generic;

namespace Sandbox.SOA.Services.Data.Models
{
    public class PersonData : EntityData
    {
        public Guid Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PersonAddressData> Addresses { get; set; }
    }
}