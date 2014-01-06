namespace Sandbox.SOA.Common.Contracts.People.Addresses
{
    public class PersonAddress : PersonAddressIdentifier
    {
        public string Name { get; set; }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}