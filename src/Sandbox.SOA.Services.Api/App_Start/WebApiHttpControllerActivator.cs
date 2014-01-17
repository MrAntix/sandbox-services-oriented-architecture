using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Sandbox.SOA.Services.Api
{
    public class WebApiHttpControllerActivator : IHttpControllerActivator
    {
        readonly Func<Type, IHttpController> _create;
        readonly Action<IHttpController> _release;

        public WebApiHttpControllerActivator(
            Func<Type, IHttpController> create,
            Action<IHttpController> release)
        {
            _create = create;
            _release = release;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller = _create(controllerType);

            request.RegisterForDispose(
                new Disposable(
                    () => _release(controller)));

            return controller;
        }

        class Disposable : IDisposable
        {
            readonly Action _release;

            public Disposable(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}