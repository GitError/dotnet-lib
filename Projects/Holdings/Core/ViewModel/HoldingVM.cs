using Core.Common;

namespace Core.ViewModel
{
    public class HoldingVM
    {
        public int Id { get; set; }

        public HoldingType HoldingType { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        // Calculated Fields -- Implement get/ set

        public decimal CurrentPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal MarketValue { get; set; }

        public decimal AverageCost { get; set; }

        public decimal UnrealizedGainLoss { get; set; }

        public decimal UnrealizedLossPectg { get; set; }
    }
}