using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;

namespace Sandbox.SOA.Portal.Models
{
    public class PersonRow
    {
        PersonName _name;

        public string Identifier { get; set; }

        public PersonName Name
        {
            set { _name = value; }
        }

        public string FullName
        {
            get { return string.Format("{0}, {1}", _name.Last, _name.First); }
        }

        public PersonAddressInfo DefaultAddress { get; set; }
    }
}