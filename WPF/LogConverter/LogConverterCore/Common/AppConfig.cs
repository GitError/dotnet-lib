using System.Text.RegularExpressions;

namespace LogConverterCore.Common
{
    public static class AppConfig
    {
        public static class InputFile
        {

        }

        public static class OutputFile
        {
            public static string ExcelFileExtension = ".xlsx";
        }

        public static class Parsing
        {
            public static char LogDataDelimiter = ',';
            public static char NewStudyDelimiter = '#';
            public static string ChildNodeIndicator = "..";

            public static string RequiredPartialHeader = "STUDY,";

            public static string SummarySheetStartLine = @"*** SUMMARY ***";
            public static string SummarySheetEndLine = @"*** SUMMARY END ***";

            public static Regex EventHeader = new Regex(@"\*\s{3}\w");
            public static Regex EventDetails = new Regex(@"\*\s{5}\w");
        }

        public static class Validation
        {
            public static string IgnoreMessage = @"PL/SQL procedure successfully completed.";
        }

        public static class Labels
        {
            public static string Date = "Date";
            public static string Summary = "Summary";
            public static string Runs = "Runs";

            public static string SummaryWorksheetName = "Summary";
            public static string LogDetailsWorksheetName = "Details";
            public static string LogEventsWorksheetName = "Events";
        }
    }
}