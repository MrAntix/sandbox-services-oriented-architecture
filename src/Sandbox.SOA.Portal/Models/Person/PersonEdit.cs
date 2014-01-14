namespace Sandbox.SOA.Portal.Models.Person
{
    public class PersonEdit
    {
        public string Identifier { get; set; }
        public PersonNameEdit Name { get; set; }
        public PersonMobilePhoneEdit MobilePhone { get; set; }
    }
}