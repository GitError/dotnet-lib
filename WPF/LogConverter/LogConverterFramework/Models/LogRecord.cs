namespace LogConverterFramework.Models
{
    public class LogRecord
    {
        public int? Index { get; set; }

        public string Study { get; set; }

        public string DataModel { get; set; }

        public string LC { get; set; }

        public int? JobId { get; set; }

        public string LoadStep { get; set; }

        public string Status { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string RunTime { get; set; }

        public string LockTime { get; set; }

        public string TotalTime { get; set; }

        public int? Refreshed { get; set; }

        public int? Inserted { get; set; }

        public int? Updated { get; set; }

        public int? Deleted { get; set; }

        public int? TotalInsertsUpdatedDeletes { get; set; }

        public int? TotalRecords { get; set; }

        public int? I_U_D_SEC { get; set; }

        public int? TotalSeconds { get; set; }

        public int? LoadedSeconds { get; set; }
    }
}