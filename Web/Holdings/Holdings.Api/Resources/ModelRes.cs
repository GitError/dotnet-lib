using System.Collections.Generic;

namespace Holdings.Api.Resources
{
    public class ModelRes
    {
        public int ModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PortfolioRes Portfolio { get; set; }

        public int PortfolioId { get; set; }

        public IEnumerable<HoldingRes> Holdings { get; set; }
    }
}