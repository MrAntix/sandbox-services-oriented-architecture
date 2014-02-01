using System;
using System.Web.Mvc;

namespace Sandbox.SOA.Portal.Models
{
    public interface IFormViewModel
    {
        FormSubmitTypes Submit { get; }

        Func<UrlHelper, object, string> Close { get; }
        Func<UrlHelper, object, string> Delete { get; }
    }
}