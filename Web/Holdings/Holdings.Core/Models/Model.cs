using System.Collections.Generic;

namespace Holdings.Core.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Holding> Holdings { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}