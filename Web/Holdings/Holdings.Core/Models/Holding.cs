namespace Holdings.Core.Models
{
    public class Holding
    {
        public int HoldingId { get; set; }

        public int HoldingType { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal BuyPrice { get; set; }

        public Model Model { get; set; }

        public int ModelId { get; set; }

    }
}