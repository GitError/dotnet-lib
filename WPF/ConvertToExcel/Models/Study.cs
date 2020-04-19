using System.Collections.Generic;
using System.ComponentModel;

namespace ConvertToExcelCore.Models
{

    public class Study
    {
        public Study()
        {
            Events = new List<Event>();
        }

        [Description("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        [Description("Data Model Name")]
        public string DataModelName { get; set; }

        public List<Event> Events { get; set; }
    }
}
