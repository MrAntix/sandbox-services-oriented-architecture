using System.Collections.Generic;
using System.Web.Mvc;

namespace Sandbox.SOA.Portal.Helpers
{
    public static class ViewDataDictionaryExtensions
    {
        public static ViewDataDictionary<T> Extract<T>(
            this ViewDataDictionary<T> viewData,
            params string[] keys)
        {
            var newViewData = new ViewDataDictionary<T>();
            foreach (var key in keys)
            {
                if (viewData.ContainsKey(key))
                    newViewData.Add(key, viewData[key]);
            }

            return newViewData;
        }

        public static ViewDataDictionary<T> Include<T>(
            this ViewDataDictionary<T> viewData,
            string key, object value)
        {
            viewData.Add(key, value);
            return viewData;
        }

        public static string Class<T>(
            this ViewDataDictionary<T> viewData)
        {
            return viewData["class"] as string;
        }

        public static IEnumerable<SelectListItem> GetList<T>(
            this ViewDataDictionary<T> viewData)
        {
            return (IEnumerable<SelectListItem>)
                   viewData[viewData.ModelMetadata.PropertyName];
        }

        public static string GetListOption<T>(
            this ViewDataDictionary<T> viewData)
        {
            return string.Empty;
        }
    }
}