using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace Sandbox.SOA.Portal.Helpers
{
    public class ResourceManagerEx:ResourceManager
    {
        public ResourceManagerEx(string name, Assembly assembly):
            base(name, assembly)
        {
            
        }

        public IHtmlString GetHtmlString(string name, System.Globalization.CultureInfo culture)
        {
            var value = string.Format("<resource>{0}</resource>", base.GetString(name, culture));

            return MvcHtmlString.Create(value);
        }
    }
}