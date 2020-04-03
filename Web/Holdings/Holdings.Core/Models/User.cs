using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        
        public string Location { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public IEnumerable<Portfolio> Portfolios { get; set; }
    }
}