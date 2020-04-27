namespace ConvertToExcelCore.ViewModels
{
    public class LogRecordVm
    {
        public int? INDX { get; set; }

        public string STUDY { get; set; }

        public string DATA_MODEL { get; set; }

        public string LC { get; set; }

        public int? JOB_ID { get; set; }

        public string LOAD_STEP { get; set; }

        public string STATUS { get; set; }

        public string START_TS { get; set; }

        public string END_TS { get; set; }

        public string RUN_TIME { get; set; }

        public string LOCK_TIME { get; set; }

        public string TOTAL_TIME { get; set; }

        public int? REFRSH { get; set; }

        public int? INS { get; set; }

        public int? UPD { get; set; }

        public int? DEL { get; set; }

        public int? TOTAL_IUD { get; set; }

        public int? TOTAL_REC { get; set; }

        public int? IUD_SEC { get; set; }

        public int? TOTAL_SEC { get; set; }

        public int? LOADED_SEC { get; set; }
    }
}