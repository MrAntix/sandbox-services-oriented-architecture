using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sandbox.SOA.Portal
{
    public static class BreadcrumbHelper
    {
        public static Builder Breadcrumb(this HtmlHelper helper)
        {
            return new Builder(helper);
        }

        public class Builder
        {
            readonly HtmlHelper _helper;
            readonly IList<Crumb> _crumbs;

            public Builder(HtmlHelper helper)
            {
                _helper = helper;
                _crumbs = new List<Crumb>();
            }

            public Builder Add(
                string text,
                string url)
            {
                _crumbs.Add(new Crumb(text, url));
                return this;
            }

            public IHtmlString Render()
            {
                return _helper.Partial("Breadcrumbs", _crumbs);
            }
        }

        public class Crumb
        {
            readonly string _text;
            readonly string _url;

            public Crumb(
                string text,
                string url)
            {
                _text = text;
                _url = url;
            }

            public string Text
            {
                get { return _text; }
            }

            public string Url
            {
                get { return _url; }
            }
        }
    }
}