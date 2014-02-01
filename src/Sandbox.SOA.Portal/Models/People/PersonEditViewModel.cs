using System;
using System.Web.Mvc;

using Sandbox.SOA.Portal.Helpers;

namespace Sandbox.SOA.Portal.Models.People
{
    public class PersonEditViewModel : FormViewModel<PersonEditViewModel>
    {
        public Guid Identifier { get; set; }
        public PersonNameEditViewModel Name { get; set; }
        public PersonMobilePhoneEditViewModel MobilePhone { get; set; }

        public override FormSubmitTypes Submit
        {
            get { return FormSubmitTypes.Save; }
        }

        protected override Func<UrlHelper, PersonEditViewModel, string> Close
        {
            get { return (url, m) => url.People(); }
        }

        protected override Func<UrlHelper, PersonEditViewModel, string> Delete
        {
            get { return (url, m) => url.PersonDelete(m.Identifier); }
        }
    }
}