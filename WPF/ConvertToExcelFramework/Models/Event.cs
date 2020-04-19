using System.ComponentModel;

namespace ConvertToExcelFramework.Models
{
    public class Event
    {
        public string Level { get; set; }

        public string Message { get; set; }
    }

    public class EventVm
    {
        [Description("Id")]
        public int Id { get; set; }

        public string Study { get; set; }

        [Description("Data Model")]
        public string DataModel { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }
    }
}