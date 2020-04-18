namespace ConvertToExcelFramework.Common
{
    public static class AppSettings
    {
        // refactor this once finalized -- class prop attributes + loop to get desc of each
        public static string LogFileHeader = @"INDX,STUDY,DATA MODEL,JOB ID,LOAD STEP,STATUS,START TS,END TS,RUN TIME,LOCK TIME,TOTAL TIME,REFRESHED,INSERTED,UPDATED,DELETED,TOTAL I+U+D,TOTAL RECORDS,I+U+D/SEC,TOTAL/SEC,LOADED/SEC,";

        public static string SummaryWorkSheetName = "Summary";
        public static string LogDataWorksheetName = "Details";

        public static string SummaryLinePrefix = "*";
        public static string SummaryEventDelimeter = @"*\*";

        public static string SummaryStart = @"*** SUMMARY ***";
        public static string SummaryEnd = @"*** SUMMARY END ***";

        public static char DataDelimiter = ',';

        public static string IgnoreMessage = @"PL/SQL procedure successfully completed.";
    }
}