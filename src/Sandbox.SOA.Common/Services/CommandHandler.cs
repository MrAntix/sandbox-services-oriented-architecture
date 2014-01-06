using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.SOA.Common.Services
{
    public class CommandHandler : ICommandHandler
    {
        public void Handle<T>(T model)
        {
            var service = Resolve<ICommand<T>>();

            service.Execute(model);
        }

        public TOut Handle<TIn, TOut>(TIn model)
        {
            var service = Resolve<ICommand<TIn, TOut>>();

            return service.Execute(model);
        }

        static readonly Type CommandType = typeof (ICommand);

        readonly IDictionary<Type, object> _getServices
            = new Dictionary<Type, object>();

        public CommandHandler Add<T>(Func<T> getService)
            where T : ICommand
        {
            var interfaceType =
                typeof (T).GetInterfaces()
                          .Single(i => i.IsGenericType
                                       && CommandType.IsAssignableFrom(i));

            var lazyType = typeof (Lazy<>).MakeGenericType(interfaceType);

            _getServices.Add(
                interfaceType,
                Activator.CreateInstance(lazyType, getService)
                );

            return this;
        }

        T Resolve<T>()
        {
            var lazy = (Lazy<T>) _getServices[typeof (T)];

            return lazy.Value;
        }
    }
}