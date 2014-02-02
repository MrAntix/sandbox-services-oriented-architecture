using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Sandbox.SOA.Portal.Models;

namespace Sandbox.SOA.Portal.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString FormSave<T>(
            this HtmlHelper<T> helper,
            string close,
            string delete)
        {
            return helper.Partial(
                "FormFooter",
                new FormFooterViewModel(
                    FormSubmitTypes.Save,
                    close,
                    delete));
        }

        public static IHtmlString FormDelete<T>(
            this HtmlHelper<T> helper,
            string close)
        {
            return helper.Partial(
                "FormFooter",
                new FormFooterViewModel(
                    FormSubmitTypes.Delete,
                    close,
                    null));
        }

        public static IHtmlString FormCreate<T>(
            this HtmlHelper<T> helper,
            string close)
        {
            return helper.Partial(
                "FormFooter",
                new FormFooterViewModel(
                    FormSubmitTypes.Create,
                    close,
                    null));
        }
    }
}