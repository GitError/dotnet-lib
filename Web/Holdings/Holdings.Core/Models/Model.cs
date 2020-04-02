using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Portfolio Portfolio { get; set; }

        public int PortfolioId { get; set; }

        public IEnumerable<Holding> Holdings { get; set; }
    }
}