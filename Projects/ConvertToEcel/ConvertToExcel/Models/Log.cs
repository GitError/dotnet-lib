using System.Collections.Generic;

namespace ConvertToExcel.Models
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