using FluentValidation;

namespace Holdings.Api.Resources.Validation
{
    public class SaveHoldingValidator : AbstractValidator<SaveHoldingRes>
    {
        public SaveHoldingValidator()
        {
            RuleFor(a => a.AssetClass)
                .NotEmpty()
                .WithMessage("Asset Class cannot be empty");

            RuleFor(a => a.Symbol)
               .NotEmpty()
               .WithMessage("Symbol cannot be empty");

            RuleFor(a => a.Symbol)
             .MaximumLength(10);

            RuleFor(a => a.Quantity)
               .NotEmpty()
               .WithMessage("Quantity cannot be empty");

            RuleFor(a => a.BuyPrice)
               .NotEmpty()
               .WithMessage("'Buy Price' cannot be empty");

            RuleFor(m => m.ModelId)
              .NotEmpty();
        }
    }
}