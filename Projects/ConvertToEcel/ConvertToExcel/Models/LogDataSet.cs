using System.Collections.Generic;

namespace ConvertToExcel.Models
{
    public class LogDataSet
    {
        public LogDataSet()
        {
            Rows = new List<LogDataRow>();
        }

        public string FilePath { get; set; }

        public List<LogDataRow> Rows { get; set; }
    }
}