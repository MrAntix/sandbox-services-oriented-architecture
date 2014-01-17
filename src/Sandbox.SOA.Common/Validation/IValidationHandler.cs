using System.Collections.Generic;

namespace Sandbox.SOA.Common.Validation
{
    public interface IValidationHandler
    {
        IEnumerable<ValidationFailure> Validate<TIn>(TIn model);
    }
}