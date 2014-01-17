using System.Web.Mvc;

using Newtonsoft.Json;

namespace Sandbox.SOA.Portal.Filters
{
    public class AjaxValidationFilter : IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest()
                || filterContext.Controller.ViewData.ModelState.IsValid) return;

            var serializationSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

            var serializedModelState = JsonConvert.SerializeObject(
                filterContext.Controller.ViewData.ModelState,
                serializationSettings);

            var result = new ContentResult
                {
                    Content = serializedModelState,
                    ContentType = "application/json"
                };

            filterContext.HttpContext.Response.StatusCode = 400;
            filterContext.Result = result;
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            if (!filterContext.HttpContext.Request.IsAjaxRequest()) return;

            var redirect = filterContext.Result as RedirectToRouteResult;
            if(redirect==null) return;

            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var url = urlHelper.RouteUrl(redirect.RouteName, redirect.RouteValues);
            
            filterContext.HttpContext.Response.Headers.Add("redirect", url);
            filterContext.Result = new EmptyResult();
        }
    }
}