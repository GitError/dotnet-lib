using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Holding> Holdings { get; set; }
    }
}