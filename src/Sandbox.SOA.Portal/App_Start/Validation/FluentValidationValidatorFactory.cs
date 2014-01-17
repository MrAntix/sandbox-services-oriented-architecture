using System;
using FluentValidation;

namespace Sandbox.SOA.Portal.Validation
{
    public class FluentValidationValidatorFactory : ValidatorFactoryBase
    {
        readonly Func<Type, IValidator> _getValidator;

        public FluentValidationValidatorFactory(Func<Type, IValidator> getValidator)
        {
            _getValidator = getValidator;
        }

        public override IValidator CreateInstance(Type type)
        {
            return _getValidator(type);
        }
    }
}