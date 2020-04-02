using System.Collections.Generic;

namespace ConvertToExcelFramework.Models
{
    public class Log
    {
        public Log()
        {
            Records = new List<LogRecord>();
        }

        public string FilePath { get; set; }

        public List<LogRecord> Records { get; set; }
    }
}