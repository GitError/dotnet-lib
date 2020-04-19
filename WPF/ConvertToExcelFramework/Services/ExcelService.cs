using ClosedXML.Excel;
using ConvertToExcelFramework.Common;
using ConvertToExcelFramework.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace ConvertToExcelFramework.Services
{
    public class ExcelService : IExcelService
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //
        // REFACTOR FILE READS ONCE REQUIREMENTS FINALIZED
        // TO DO: remove hard coding as much as possible, read file once if possible 
        //
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public bool SaveLogExcel(Log logData)
        {
            try
            {
                var wb = new XLWorkbook();

                // Summary worksheet
                var sum_ws = wb.Worksheets.Add(AppConfig.Labels.SummaryWorksheetName);

                sum_ws.Columns("A-D").Width = 20;
                sum_ws.Columns("A-D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                sum_ws.Cell(1, 1).Value = AppConfig.Labels.Date;
                sum_ws.Cell(1, 2).Value = logData.Summary.Date;

                sum_ws.Cell(2, 1).Value = AppConfig.Labels.Summary;
                sum_ws.Cell(2, 2).Value = logData.Summary.Description;

                sum_ws.Cell(4, 1).Value = AppConfig.Labels.Studies;

                sum_ws.SheetView.Freeze(5, 1);

                int i = 1;
                foreach (var prop in new Study().GetType().GetProperties())
                {
                    var att = prop.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();

                    if (att.Count > 0)
                        sum_ws.Cell(5, i).Value = ((DescriptionAttribute)att[0]).Description;
                    else
                        sum_ws.Cell(5, i).Value = prop.Name;

                    i++;
                }

                var summaryDateset = logData.Summary.Studies
                    .Where(x => x.Id != 0)
                    .Select(x => new { x.Id, x.Name, x.DataModelName })
                    .ToList();

                sum_ws.Cell(6, 1).InsertData(summaryDateset);

                // Details worksheet
                var dat_ws = wb.Worksheets.Add(AppConfig.Labels.LogDataWorksheetName);

                int j = 1;
                foreach (var prop in new LogRecord().GetType().GetProperties())
                {
                    var att = prop.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();

                    if (att.Count > 0)
                        dat_ws.Cell(1, j).Value = ((DescriptionAttribute)att[0]).Description;
                    else
                        dat_ws.Cell(1, j).Value = prop.Name;

                    j++;
                }

                dat_ws.Rows(1, 1).Style.Font.Bold = true;
                dat_ws.SheetView.Freeze(1, 1);

                dat_ws.Columns().Width = 20;
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
            //
            //REFACTOR THE BELOW TO BE BASE DON ONE READ 
            //

            try
            {
                Log log = new Log(filePath);
                int asInt = 0;

                var summaryEndLine = File.ReadLines(filePath)
                    .Select((text, index) => new { text, lineNumber = index + 1 })
                    .First(x => x.text.Contains(AppConfig.Parsing.SummarySheetEndLine));

                var summaryData = File.ReadLines(filePath)
                    .Skip(1)
                    .Take(summaryEndLine.lineNumber - 3)
                    .ToList();

                log.Summary.Date = summaryData[1].ToString().Substring(2);
                log.Summary.Description = summaryData[2].ToString().Substring(2);

                foreach (var record in summaryData.Where(x => x.Length > 1).Skip(2))
                {
                    var recToAdd = new Study();

                    // new record
                    if (record.Contains(AppConfig.Parsing.NewStudyDelimiter))
                    {
                        recToAdd.Id = int.TryParse(record.Substring(record.IndexOf(AppConfig.Parsing.NewStudyDelimiter) + 1, record.IndexOf(" S") - 3), out asInt) ? asInt : 0;
                        recToAdd.Name = record.Substring(record.IndexOf("Y:") + 3, record.IndexOf(" D") - 12);
                        recToAdd.DataModelName = record.Substring(record.IndexOf("L:") + 2);
                    }

                    // event message
                    log.Summary.Studies.Add(recToAdd);
                }

                var logData = File.ReadAllLines(filePath)
                    .Skip(summaryEndLine.lineNumber + 1)
                    .Where(l => l != string.Empty && !l.Contains(AppConfig.Validation.IgnoreMessage));

                log.Records = logData
                    .Select(x => x.Split(AppConfig.Parsing.LogDataDelimiter))
                    .Select(x => new LogRecord
                    {
                        Index = int.TryParse(x[0], out asInt) ? asInt : 0,
                        Study = x[1],
                        DataModel = x[2],
                        JobId = int.TryParse(x[3], out asInt) ? asInt : 0,
                        LoadStep = x[4],
                        Status = x[5],
                        StartTime = x[6],
                        EndTime = x[7],
                        RunTime = x[8],
                        LockTime = x[9],
                        TotalTime = x[10],
                        Refreshed = int.TryParse(x[11], out asInt) ? asInt : 0,
                        Inserted = int.TryParse(x[12], out asInt) ? asInt : 0,
                        Updated = int.TryParse(x[13], out asInt) ? asInt : 0,
                        Deleted = int.TryParse(x[14], out asInt) ? asInt : 0,
                        TotalInsertsUpdatedDeletes = int.TryParse(x[15], out asInt) ? asInt : 0,
                        TotalRecords = int.TryParse(x[16], out asInt) ? asInt : 0,
                        I_U_D_SEC = int.TryParse(x[17], out asInt) ? asInt : 0,
                        TotalSeconds = int.TryParse(x[18], out asInt) ? asInt : 0,
                        LoadedSeconds = int.TryParse(x[19], out asInt) ? asInt : 0
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