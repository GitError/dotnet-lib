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
        [Description("Study")]
        public string StudyName { get; set; }

        [Description("Level")]
        public string EventLevel { get; set; }


        [Description("Message")]
        public string EventMessage { get; set; }
    }
}