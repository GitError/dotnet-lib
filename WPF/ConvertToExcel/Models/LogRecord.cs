using System.ComponentModel;

namespace ConvertToExcel.Models
{
    public class LogRecord
    {
        [Description("INDX")]
        public int? Index { get; set; }

        [Description("STUDY")]
        public string Study { get; set; }

        [Description("DATA_MODEL")]
        public string DataModel { get; set; }

        [Description("JOB_ID")]
        public int? JobId { get; set; }

        [Description("LOAD_STEP")]
        public string LoadStep { get; set; }

        [Description("STATUS")]
        public string Status { get; set; }

        [Description("START_TS")]
        public string StartTime { get; set; }

        [Description("END_TS")]
        public string EndTime { get; set; }

        [Description("RUN_TIME")]
        public string RunTime { get; set; }

        [Description("LOCK_TIME")]
        public string LockTime { get; set; }

        [Description("TOTAL_TIME")]
        public string TotalTime { get; set; }

        [Description("REFRESHED")]
        public int? Refreshed { get; set; }

        [Description("INSERTED")]
        public int? Inserted { get; set; }

        [Description("UPDATED")]
        public int? Updated { get; set; }

        [Description("DELETED")]
        public int? Deleted { get; set; }

        [Description("TOTAL_I_U_D")]
        public int? TotalInsertsUpdatedDeletes { get; set; }

        [Description("TOTAL_RECORDS")]
        public int? TotalRecords { get; set; }

        [Description("I_U_D_SEC")]
        public int? I_U_D_SEC { get; set; }

        [Description("TOTAL_SEC")]
        public int? TotalSeconds { get; set; }

        [Description("LOADED_SEC")]
        public int? LoadedSeconds { get; set; }
    }
}