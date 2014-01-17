namespace Sandbox.SOA.Portal.Models.Person
{
    public class PersonNameEditViewModel
    {
        public string First { get; set; }
        public string Last { get; set; }

        public string Full
        {
            get
            {
                var name = string.Format("{0} {1}", First, Last);

                return string.IsNullOrWhiteSpace(name)
                           ? "(not set)"
                           : name;
            }
        }
    }
}