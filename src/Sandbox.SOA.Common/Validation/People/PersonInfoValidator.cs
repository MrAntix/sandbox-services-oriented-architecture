using FluentValidation;

using Sandbox.SOA.Common.Contracts.People;

namespace Sandbox.SOA.Common.Validation.People
{
    public class PersonInfoValidator : AbstractValidator<PersonInfo>
    {
        public PersonInfoValidator()
        {
            RuleFor(m => m.Name).NotNull();
            RuleFor(m => m.Name.First).NotEmpty();
            RuleFor(m => m.Name.Last).NotEmpty();
        }
    }
}