namespace ConvertToExcelFramework.Common
{
    public static class AppConfig
    {
        public static class Parsing
        {
            public static char LogDataDelimiter = ',';
            public static char NewStudyDelimiter = '#';
            public static string SummaryEventDelimeter = @"*\*";

            public static string SummarySheetStartLine = @"*** SUMMARY ***";
            public static string SummarySheetEndLine = @"*** SUMMARY END ***";
        }

        public static class Validation
        {
            public static string IgnoreMessage = @"PL/SQL procedure successfully completed.";
        }

        public static class Labels
        {
            public static string Date = "Date";
            public static string Summary = "Summary";
            public static string Studies = "Studies";

            public static string SummaryWorksheetName = "Summary";
            public static string LogDataWorksheetName = "Details";
        }
    }
}