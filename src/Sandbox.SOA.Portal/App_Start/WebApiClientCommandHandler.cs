using System;
using System.Collections.Generic;
using System.Net.Http;

using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Portal
{
    public class WebApiClientCommandHandler : ICommandHandler
    {
        readonly Uri _baseAddress;

        readonly IDictionary<Type, string> _routes
            = new Dictionary<Type, string>();

        public WebApiClientCommandHandler(string baseAddressString)
        {
            _baseAddress = new Uri(baseAddressString);
        }

        public void Handle<T>(T model)
        {
            var client = new HttpClient {BaseAddress = _baseAddress};
            var route = _routes[typeof (T)];

            var _ = client.GetAsync(route).Result;
        }

        public TOut Handle<TIn, TOut>(TIn model)
        {
            var client = new HttpClient {BaseAddress = _baseAddress};
            var route = _routes[typeof (Tuple<TIn, TOut>)];

            var response = client.GetAsync(route).Result;

            var result = response.Content.ReadAsAsync<TOut>().Result;

            return result;
        }

        public WebApiClientCommandHandler Register<TIn>(string urlTemplate)
        {
            _routes.Add(typeof (TIn), urlTemplate);
            return this;
        }

        public WebApiClientCommandHandler Register<TIn, TOut>(string urlTemplate)
        {
            return Register<Tuple<TIn, TOut>>(urlTemplate);
        }
    }
}