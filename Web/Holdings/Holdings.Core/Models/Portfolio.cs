using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; } 
        
        // implement IdentityUser -- GUID

        public IEnumerable<Model> Models { get; set; }
    }
}