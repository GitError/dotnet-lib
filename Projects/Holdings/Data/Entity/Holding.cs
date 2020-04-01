using System;

namespace Data.Entity
{
    public class Holding
    {
        public int Id { get; set; }

        public Enum HoldingType { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal BuyPrice { get; set; } 
    }
}