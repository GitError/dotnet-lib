using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
        
        public IEnumerable<Model> Models { get; set; }
    }
}