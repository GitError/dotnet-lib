using FluentValidation;

namespace Holdings.Api.Resources.Validation
{
    public class SavePortfolioValidator : AbstractValidator<SavePortfolioRes>
    {
        public SavePortfolioValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(m => m.Name)
             .NotEmpty()
             .WithMessage("Name cannot be empty");
        }
    }
}
