using Antix;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Portal
{
    public class WebApiClientCommandHandler : ICommandHandler
    {
        readonly Uri _baseAddress;

        readonly IDictionary<Type, Func<HttpClient, object, Task<HttpResponseMessage>>> _routes
            = new Dictionary<Type, Func<HttpClient, object, Task<HttpResponseMessage>>>();

        public WebApiClientCommandHandler(string baseAddressString)
        {
            _baseAddress = new Uri(baseAddressString);
        }

        public void Handle<T>(T model)
        {
            var client = new HttpClient {BaseAddress = _baseAddress};
            var call = _routes[typeof (T)];

            var _ = call(client, model).Result;
        }

        public TOut Handle<TIn, TOut>(TIn model)
        {
            var client = new HttpClient {BaseAddress = _baseAddress};
            var call = _routes[typeof (Tuple<TIn, TOut>)];

            var response = call(client, model).Result;

            var result = response.Content.ReadAsAsync<TOut>().Result;

            return result;
        }

        public WebApiClientCommandHandler Get<TIn>(string urlTemplate)
        {
            _routes.Add(typeof (TIn),
                        (c, m) => c.GetAsync(urlTemplate));
            return this;
        }

        public WebApiClientCommandHandler Get<TIn, TOut>(string urlTemplate)
        {
            return Get<Tuple<TIn, TOut>>(urlTemplate);
        }

        public static string ToQueryString(IEnumerable<KeyValuePair<string, string>> values)
        {
            return string.Join(
                "&", values.Select(kv =>
                                   string.Concat(
                                       Uri.EscapeDataString(kv.Key),
                                       "=",
                                       Uri.EscapeDataString(kv.Value))));
        }

        public static string MergeUrl(string template, IDictionary<string, string> values)
        {
            foreach (var kv in values.ToArray())
            {
                var placeholder = string.Concat("{", kv.Key, "}");
                if (!template.Contains(placeholder, StringComparison.OrdinalIgnoreCase))
                    continue;

                template = template.Replace(
                    placeholder,
                    Uri.EscapeDataString(kv.Value),
                    StringComparison.OrdinalIgnoreCase
                    );

                values.Remove(kv);
            }

            return template;
        }

        public static IDictionary<string, string> ToDictionary(object obj)
        {
            var json = JObject.FromObject(obj);
            return ToDictionary(json, string.Empty)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        static IEnumerable<KeyValuePair<string, string>> ToDictionary(JObject obj, string prefix)
        {
            foreach (var property in obj.Properties())
            {
                if (property.Value == null) continue;

                var name = string.Concat(prefix, property.Name);

                var valueArray = property.Value as JArray;
                if (valueArray != null)
                    throw new NotSupportedException();

                var valueObject = property.Value as JObject;
                if (valueObject != null)
                {
                    foreach (var kv in ToDictionary(
                        valueObject, string.Concat(name, ".")))
                    {
                        yield return kv;
                    }
                    continue;
                }

                yield return new KeyValuePair<string, string>(
                    name, property.Value.ToString());
            }
        }
    }
}