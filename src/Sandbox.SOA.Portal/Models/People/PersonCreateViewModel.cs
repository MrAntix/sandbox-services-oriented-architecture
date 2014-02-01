using System;
using System.Web.Mvc;

using Sandbox.SOA.Portal.Helpers;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonCreateViewModel : FormViewModel<PersonCreateViewModel>
    {
        public PersonNameEditViewModel Name { get; set; }

        public override FormSubmitTypes Submit
        {
            get { return FormSubmitTypes.Create; }
        }

        protected override Func<UrlHelper, PersonCreateViewModel, string> Close
        {
            get { return (url, m) => url.People(); }
        }
    }
}