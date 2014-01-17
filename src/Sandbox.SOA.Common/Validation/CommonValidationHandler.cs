using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;

namespace Sandbox.SOA.Common.Validation
{
    public class CommonValidationHandler : IValidationHandler
    {
        readonly Func<Type, IValidator> _getValidator;

        public CommonValidationHandler(Func<Type, IValidator> getValidator)
        {
            _getValidator = getValidator;
        }

        public IEnumerable<ValidationFailure> Validate<TIn>(TIn model)
        {
            var validator = _getValidator(typeof (IValidator<TIn>));
            if (validator == null)
                return new ValidationFailure[] {};

            var result = validator.Validate(model);

            return result.Errors
                         .Select(e => new ValidationFailure(
                                          e.PropertyName,
                                          e.ErrorMessage,
                                          e.AttemptedValue));
        }
    }
}