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
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //
        // REFACTOR FILE READS ONCE REQUIREMENTS FINALIZED
        //
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public ExcelService()
        {

        }

        public bool SaveLogExcel(Log logData)
        {
            try
            {
                var wb = new XLWorkbook();

                // Summary  worksheet
                var sum_ws = wb.Worksheets.Add(AppSettings.SummaryWorksheetName);

                sum_ws.Columns("A-B").Width = 18;
                sum_ws.Columns("A-B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                sum_ws.Cell(1, 1).Value = "Date";
                sum_ws.Cell(1, 2).Value = logData.Summary.Date;

                sum_ws.Cell(2, 1).Value = "Summary";
                sum_ws.Cell(2, 2).Value = logData.Summary.Description;

                sum_ws.Cell(4, 1).Value = "Studies";

                sum_ws.SheetView.Freeze(5, 1);

                int i = 1;
                foreach (var a in new Study().GetType().GetProperties())
                {
                    sum_ws.Cell(5, i).Value = a.Name;
                    i++;
                }

                sum_ws.Cell(6, 1).InsertData(logData.Summary.Studies.ToList());

                // Details worksheet
                var dat_ws = wb.Worksheets.Add(AppSettings.LogDataWorksheetName);

                int j = 1;
                foreach (var a in new LogRecord().GetType().GetProperties())
                {
                    dat_ws.Cell(1, j).Value = a.Name;
                    j++;
                }

                dat_ws.Rows(1, 1).Style.Font.Bold = true;
                dat_ws.SheetView.Freeze(1, 1);

                dat_ws.Columns().Width = 18;
                dat_ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                dat_ws.Cell(2, 1).InsertData(logData.Records.ToList());

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
            //REFACTOR THE BELOW TO BE BASE DON ONE READ 
            //using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            //{
            //}

            try
            {
                Log log = new Log(filePath);
                int asInt = 0;

                var summaryEndLine = File.ReadLines(filePath)
                    .Select((text, index) => new { text, lineNumber = index + 1 })
                    .First(x => x.text.Contains(AppSettings.SummaryEnd));

                var summaryData = File.ReadLines(filePath)
                    .Skip(1)
                    .Take(summaryEndLine.lineNumber - 3)
                    .ToList();

                log.Summary.Date = summaryData[1].ToString().Substring(2);
                log.Summary.Description = summaryData[2].ToString().Substring(2);

                foreach (var record in summaryData.Where(x => x.Length > 1).Skip(2))
                {
                    if (record.Contains("#"))
                    {
                        log.Summary.Studies.Add(new Study()
                        {
                            // substring with index of # and STUDY:
                            Id = record.Substring(3, 4).ToString(),


                            Description = record.ToString().Substring(1)
                        }); ;
                    } 
                }

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
                })
                .ToList();

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