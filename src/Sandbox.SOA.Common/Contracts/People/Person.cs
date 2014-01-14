namespace Sandbox.SOA.Common.Contracts.People
{
    public class Person : PersonIdentifier
    {
        public PersonName Name { get; set; }
        public PersonMobilePhone MobilePhone { get; set; }
    }
}