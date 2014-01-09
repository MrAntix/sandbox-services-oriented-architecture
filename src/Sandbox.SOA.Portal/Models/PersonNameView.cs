namespace Sandbox.SOA.Portal.Models
{
    public class PersonNameView
    {
        public string First { get; set; }
        public string Last { get; set; }

        public string Full
        {
            get { return string.Format("{0} {1}", First, Last); }
        }
    }
}