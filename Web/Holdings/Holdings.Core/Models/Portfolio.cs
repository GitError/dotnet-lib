using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Model> Models { get; set; }
    }
}