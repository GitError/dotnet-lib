using System.ComponentModel;

namespace ConvertToExcelFramework.Models
{
    public class LogRecord
    {
        public LogRecord()
        {

        }

        [Description("INDEX")]
        public int? INDEX { get; set; }

        [Description("STUDY")]
        public string STUDY { get; set; }

        [Description("DATA_MODEL")]
        public string DATA_MODEL { get; set; }

        [Description("JOB_ID")]
        public int? JOB_ID { get; set; }

        [Description("LOAD_STEP")]
        public string LOAD_STEP { get; set; }

        [Description("STATUS")]
        public string STATUS { get; set; }

        [Description("START_TS")]
        public string START_TS { get; set; }

        [Description("END_TS")]
        public string END_TS { get; set; }

        [Description("RUN_TIME")]
        public string RUN_TIME { get; set; }

        [Description("LOCK_TIME")]
        public string LOCK_TIME { get; set; }

        [Description("TOTAL_TIME")]
        public string TOTAL_TIME { get; set; }

        [Description("REFRESHED")]
        public int? REFRESHED { get; set; }

        [Description("INSERTED")]
        public int? INSERTED { get; set; }

        [Description("UPDATED")]
        public int? UPDATED { get; set; }

        [Description("DELETED")]
        public int? DELETED { get; set; }

        [Description("TOTAL_I_U_D")]
        public int? TOTAL_I_U_D { get; set; }

        [Description("TOTAL_RECORDS")]
        public int? TOTAL_RECORDS { get; set; }

        [Description("I_U_D_SEC")]
        public int? I_U_D_SEC { get; set; }

        [Description("TOTAL_SEC")]
        public int? TOTAL_SEC { get; set; }

        [Description("LOADED_SEC")]
        public int? LOADED_SEC { get; set; }
    }
}