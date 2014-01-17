namespace Sandbox.SOA.Portal.Models.Person
{
    public class PersonEditViewModel
    {
        public string Identifier { get; set; }
        public PersonNameEditViewModel Name { get; set; }
        public PersonMobilePhoneEditViewModel MobilePhone { get; set; }
    }
}