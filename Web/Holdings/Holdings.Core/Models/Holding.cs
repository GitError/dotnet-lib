using Holdings.Core.Common;
using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Holding
    {
        public int Id { get; set; }

        public HoldingType HoldingType { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal BuyPrice { get; set; }

        public IEnumerable<Model> Models { get; set; }
    }
}