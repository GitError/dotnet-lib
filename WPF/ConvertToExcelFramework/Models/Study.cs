using System.Collections.Generic;

namespace ConvertToExcelFramework.Models
{
    public class Study
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Event> Events { get; set; }
    }
}