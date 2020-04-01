using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Model> Models { get; set; }
    }
}