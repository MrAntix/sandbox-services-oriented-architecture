using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentValidation;
using FluentValidation.Mvc;
using Sandbox.SOA.Common.Validation.People;
using Sandbox.SOA.Portal.Validation;

namespace Sandbox.SOA.Portal
{
    public static class WindsorConfig
    {
        public static void Register(IWindsorContainer container)
        {
            RegisterValidation(container);
        }

        static void RegisterValidation(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyContaining(typeof (PersonInfoValidator))
                       .InSameNamespaceAs<PersonInfoValidator>()
                       .WithServiceAllInterfaces()
                );

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(
                    new FluentValidationValidatorFactory(
                        t => container.Kernel.HasComponent(t)
                                 ? (IValidator) container.Resolve(t)
                                 : null)
                    )
                );
        }
    }
}