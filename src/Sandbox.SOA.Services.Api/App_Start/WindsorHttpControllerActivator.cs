using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

using Castle.Windsor;

namespace Sandbox.SOA.Services.Api
{
    public class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        readonly IWindsorContainer _container;

        public WindsorHttpControllerActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController) _container.Resolve(controllerType);

            request.RegisterForDispose(
                new Disposable(
                    () => _container.Release(controller)));

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