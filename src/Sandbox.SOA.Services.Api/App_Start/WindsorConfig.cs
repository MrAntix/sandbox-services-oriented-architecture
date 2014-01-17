using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Validation;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using FluentValidation;

using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Common.Validation;
using Sandbox.SOA.Common.Validation.People;
using Sandbox.SOA.Services.Data;
using Sandbox.SOA.Services.People;

namespace Sandbox.SOA.Services.Api
{
    public static class WindsorConfig
    {
        public static void Register(
            IWindsorContainer container, HttpConfiguration configuration)
        {
            container.Register(
                Classes.FromAssembly(typeof (PersonGetService).Assembly)
                       .Where(t => (typeof (ICommand).IsAssignableFrom(t)))
                       .WithServiceAllInterfaces()
                );

            container.Register(
                Component.For<ICommandHandler>()
                         .Instance(new WindsorCommandHandler(container))
                );

            container.Register(
                Component.For<DataContext>()
                         .ImplementedBy<DataContext>()
                );

            container.Register(
                Classes.FromThisAssembly()
                       .BasedOn<ApiController>()
                       .LifestylePerWebRequest()
                );

            configuration.Services.Replace(
                typeof (IHttpControllerActivator),
                new WebApiHttpControllerActivator(
                    t => (IHttpController) container.Resolve(t),
                    container.Release)
                );

            container.Register(
                Classes.FromAssemblyContaining(typeof(PersonInfoValidator))
                       .InSameNamespaceAs<PersonInfoValidator>()
                       .WithServiceAllInterfaces()
                );

            configuration.Services.Replace(
                typeof (ModelValidatorProvider),
                new WebApiModelValidatorProvider(
                    new CommonValidationHandler(
                        t => container.Kernel.HasComponent(t)
                                 ? (IValidator) container.Resolve(t)
                                 : null))
                );
        }
    }
}