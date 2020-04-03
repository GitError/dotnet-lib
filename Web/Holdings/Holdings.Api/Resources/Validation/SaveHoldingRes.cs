using FluentValidation;

namespace Holdings.Api.Resources.Validation
{
    public class SaveHoldingRes : AbstractValidator<SaveHoldingRes>
    {
        public int AssetClass { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal BuyPrice { get; set; }

        public int ModelId { get; set; }
    }
}