using System.Collections.Generic;

namespace ConvertToExcelFramework.Models
{
    public class Log
    {
        public Log()
        {
            Records = new List<LogRecord>();
            Summary = new Summary()
            {
                Studies = new List<Study>()
            };
        }

        public Log(string filePath)
        {
            FilePath = filePath;
            Records = new List<LogRecord>();
            Summary = new Summary()
            {
                Studies = new List<Study>()
            };
        }

        public string FilePath { get; set; }

        public Summary Summary { get; set; }

        public List<LogRecord> Records { get; set; }
    }
}