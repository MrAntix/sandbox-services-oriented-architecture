using Sandbox.SOA.Common.Contracts.People;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonNameViewModel : PersonName
    {
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