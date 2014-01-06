using Castle.Windsor;

using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Services.Api
{
    public class WindsorCommandHandler : ICommandHandler
    {
        readonly IWindsorContainer _container;

        public WindsorCommandHandler(IWindsorContainer container)
        {
            _container = container;
        }

        public void Handle<T>(T model)
        {
            var service = _container.Resolve<ICommand<T>>();

            service.Execute(model);
        }

        public TOut Handle<TIn, TOut>(TIn model)
        {
            var service = _container.Resolve<ICommand<TIn, TOut>>();

            return service.Execute(model);
        }
    }
}