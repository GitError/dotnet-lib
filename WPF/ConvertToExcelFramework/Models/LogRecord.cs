﻿using System;

namespace ConvertToExcelFramework.Models
{
    public class LogRecord
    {
        public LogRecord()
        {

        }

        public int? JOB_ID { get; set; }

        public string LOAD_STEP { get; set; }

        public string STATUS { get; set; }

        public DateTime? START_TS { get; set; }

        public DateTime? END_TS { get; set; }

        public string RUN_TIME { get; set; }

        public string LOCK_TIME { get; set; }

        public string TOTAL_TIME { get; set; }

        public int? REFRESHED { get; set; }

        public int? INSERTED { get; set; }

        public int? UPDATED { get; set; }

        public int? DELETED { get; set; }

        public int? TOTAL_I_U_D { get; set; }

        public int? TOTAL_RECORDS { get; set; }

        public int? I_U_D_SEC { get; set; }

        public int? TOTAL_SEC { get; set; }

        public int? LOADED_SEC { get; set; }
    }
}