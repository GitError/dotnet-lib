using System.Collections.Generic;
using System.ComponentModel;

namespace LogConverterFramework.Models
{
    public class Log
    {
        public Log()
        {
            Records = new List<LogRecord>(); ;

            HasSummary = false;
            HasEvents = false;
            HasDetails = false;

            Summary = new Summary()
            {
                Studies = new List<Study>()
            };
        }

        public Log(string filePath)
        {
            FilePath = filePath;

            HasSummary = false;
            HasEvents = false;
            HasDetails = false;

            Records = new List<LogRecord>();
            Summary = new Summary()
            {
                Studies = new List<Study>()
            };
        }

        [Description("File Path")]
        public string FilePath { get; set; }

        [Description("Has Summary")]
        public bool HasSummary { get; set; }

        [Description("Has Events")]
        public bool HasEvents { get; set; }

        [Description("Has Details")]
        public bool HasDetails { get; set; }

        public Summary Summary { get; set; }

        public List<LogRecord> Records { get; set; }
    }
}