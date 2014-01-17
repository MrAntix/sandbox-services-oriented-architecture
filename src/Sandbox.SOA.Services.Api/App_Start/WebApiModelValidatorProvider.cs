using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

using Sandbox.SOA.Common.Validation;

namespace Sandbox.SOA.Services.Api
{
    public class WebApiModelValidatorProvider : ModelValidatorProvider
    {
        readonly IValidationHandler _handler;

        public WebApiModelValidatorProvider(IValidationHandler handler)
        {
            _handler = handler;
        }

        public override IEnumerable<ModelValidator> GetValidators(
            ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
        {
            yield return new WebApiModelValidator(_handler, validatorProviders);
        }

        class WebApiModelValidator : ModelValidator
        {
            readonly IValidationHandler _handler;

            public WebApiModelValidator(IValidationHandler handler,
                                        IEnumerable<ModelValidatorProvider> validatorProviders)
                : base(validatorProviders)
            {
                _handler = handler;
            }

            public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
            {
                var method = typeof (IValidationHandler).GetMethod("Validate")
                                                        .MakeGenericMethod(metadata.ModelType);

                var result = (IEnumerable<ValidationFailure>) method.Invoke(_handler, new[] {metadata.Model});

                return result.Select(vf => new ModelValidationResult
                    {
                        MemberName = vf.PropertyName,
                        Message = vf.Message
                    });
            }
        }
    }
}