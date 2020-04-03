using FluentValidation;

namespace Holdings.Api.Resources.Validation
{
    public class SaveModelValidator : AbstractValidator<SaveModelRes>
    {
        public SaveModelValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(a => a.PortfolioId)
               .NotEmpty();
        }
    }
}