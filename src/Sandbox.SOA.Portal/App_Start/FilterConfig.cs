using System.Web.Mvc;

using Sandbox.SOA.Portal.Filters;

namespace Sandbox.SOA.Portal
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AjaxValidationFilter());
        }
    }
}