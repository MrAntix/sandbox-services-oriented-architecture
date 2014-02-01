using System;
using System.Web.Mvc;

using Sandbox.SOA.Portal.Helpers;

namespace Sandbox.SOA.Portal.Models.Person
{
    public class PersonDeleteViewModel : FormViewModel<PersonDeleteViewModel>
    {
        public string Identifier { get; set; }
        public PersonNameEditViewModel Name { get; set; }

        public override FormSubmitTypes Submit
        {
            get { return FormSubmitTypes.Delete; }
        }

        protected override Func<UrlHelper, PersonDeleteViewModel, string> Close
        {
            get { return (url, m) => url.PersonEdit(m.Identifier); }
        }

    }
}