using FluentValidation;

namespace Holdings.Api.Resources.Validation
{
    public class SaveUserValidator : AbstractValidator<SaveUserRes>
    {
        public SaveUserValidator()
        {
            RuleFor(a => a.Username)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(m => m.Username)
              .NotEmpty()
              .WithMessage("Username cannot be empty");
        }
    }
}