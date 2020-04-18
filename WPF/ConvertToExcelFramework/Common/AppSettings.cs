namespace ConvertToExcelFramework.Common
{
    public static class AppSettings
    {
        public static string SummaryWorksheetName = "Summary";
        public static string LogDataWorksheetName = "Details";

        public static string SummaryStart = @"*** SUMMARY ***";
        public static string SummaryEnd = @"*** SUMMARY END ***";
        public static string SummaryEventDelimeter = @"*\*";

        public static char DataDelimiter = ',';

        public static string IgnoreMessage = @"PL/SQL procedure successfully completed.";
    }
}