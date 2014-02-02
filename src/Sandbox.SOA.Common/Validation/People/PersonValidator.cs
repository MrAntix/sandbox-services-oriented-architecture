using FluentValidation;

using Sandbox.SOA.Common.Contracts.People;

namespace Sandbox.SOA.Common.Validation.People
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(m => m.Name).NotNull();
            RuleFor(m => m.Name.First).NotEmpty();
            RuleFor(m => m.Name.Last).NotEmpty();
        }
    }
}