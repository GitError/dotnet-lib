using System.Collections.Generic;

namespace Holdings.Api.Resources
{
    public class PortfolioRes
    {
        public int PortfolioId { get; set; }

        public string Name { get; set; }

        public UserRes User { get; set; }

        public int UserId { get; set; }

        public IEnumerable<ModelRes> Models { get; set; }
    }
}