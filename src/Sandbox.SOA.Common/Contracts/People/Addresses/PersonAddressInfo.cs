namespace Sandbox.SOA.Common.Contracts.People.Addresses
{
    public class PersonAddressInfo : PersonAddressIdentifier
    {
        public string Name { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
    }
}