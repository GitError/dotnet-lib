using System.Collections.Generic;

namespace Holdings.Api.Resources
{
    public class UserRes
    {
        public int UserId { get; set; }
        
        public string Location { get; set; }

        public string Username { get; set; }
        
        public IEnumerable<PortfolioRes> Portfolios { get; set; }
    }
}