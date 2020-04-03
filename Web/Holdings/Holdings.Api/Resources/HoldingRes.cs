namespace Holdings.Api.Resources
{
    public class HoldingRes
    {
        public int HoldingId { get; set; }

        public int AccetClass { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal BuyPrice { get; set; }

        public ModelRes Model { get; set; }

        public int ModelId { get; set; }
    }
}