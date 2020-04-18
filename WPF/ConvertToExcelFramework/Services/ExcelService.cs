using ClosedXML.Excel;
using ConvertToExcelFramework.Common;
using ConvertToExcelFramework.Models;
using System;
using System.IO;
using System.Linq;

namespace ConvertToExcelFramework.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {

        }

        public bool SaveLogExcel(Log logData)
        {
            try
            {
                var wb = new XLWorkbook();

                var sum_ws = wb.Worksheets.Add(AppSettings.SummaryWorkSheetName);

                sum_ws.Rows(1, 1).Style.Font.Bold = true;
                sum_ws.Cell(1, 1).InsertData(logData.Summary.Description);

                var dat_ws = wb.Worksheets.Add(AppSettings.LogDataWorksheetName);

                dat_ws.Cell(2, 1).InsertData(logData.Records.ToList());

                string[] header = AppSettings.LogFileHeader.Split(AppSettings.DataDelimiter).ToArray();                
                dat_ws.Rows(1, 1).Style.Font.Bold = true;

                for (int i = 1; i < 21; i++)
                    dat_ws.Cell(1, i).Value = header[i - 1].ToString();

                dat_ws.Columns().Width = 18;
                dat_ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                dat_ws.SheetView.Freeze(1, 1);

                dat_ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;

                logData.FilePath = Path.GetFullPath(logData.FilePath.Substring(0, logData.FilePath.Length - 4) + ".xlsx");

                wb.SaveAs(logData.FilePath);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Log ReadLogData(string filePath)
        {
            try
            {
                Log log = new Log(filePath);

                int asInt = 0;
                DateTime asDt = new DateTime(1900, 1, 1);

                var summaryEndLine = File.ReadLines(filePath)
                    .Select((text, index) => new { text, lineNumber = index + 1 })
                    .First(x => x.text.Contains(AppSettings.SummaryEnd));

                var summaryData = File.ReadLines(filePath)
                    .Skip(2)
                    .Take(summaryEndLine.lineNumber - 3)
                    .ToList();

                foreach (var item in summaryData)
                {

                }

                log.Summary.Description = summaryData[1].ToString().Substring(2);

                var logData = File.ReadAllLines(filePath)
                    .Skip(summaryEndLine.lineNumber + 1)
                    .Where(l => l != string.Empty && !l.Contains(AppSettings.IgnoreMessage));

                log.Records = logData.Select(x => x.Split(AppSettings.DataDelimiter))
                .Select(x => new LogRecord
                {
                    INDEX = int.TryParse(x[0], out asInt) ? asInt : 0,
                    STUDY = x[1],
                    DATA_MODEL = x[2],
                    JOB_ID = int.TryParse(x[3], out asInt) ? asInt : 0,
                    LOAD_STEP = x[4],
                    STATUS = x[5],
                    START_TS = x[6],
                    END_TS = x[7],
                    RUN_TIME = x[8],
                    LOCK_TIME = x[9],
                    TOTAL_TIME = x[10],
                    REFRESHED = int.TryParse(x[11], out asInt) ? asInt : 0,
                    INSERTED = int.TryParse(x[12], out asInt) ? asInt : 0,
                    UPDATED = int.TryParse(x[13], out asInt) ? asInt : 0,
                    DELETED = int.TryParse(x[14], out asInt) ? asInt : 0,
                    TOTAL_I_U_D = int.TryParse(x[15], out asInt) ? asInt : 0,
                    TOTAL_RECORDS = int.TryParse(x[16], out asInt) ? asInt : 0,
                    I_U_D_SEC = int.TryParse(x[17], out asInt) ? asInt : 0,
                    TOTAL_SEC = int.TryParse(x[18], out asInt) ? asInt : 0,
                    LOADED_SEC = int.TryParse(x[19], out asInt) ? asInt : 0
                }).ToList();

                return log;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                return new Log();
            }
        }
    }
}