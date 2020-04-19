using ClosedXML.Excel;
using ConvertToExcelFramework.Common;
using ConvertToExcelFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace ConvertToExcelFramework.Services
{
    public class ExcelService : IExcelService
    {
        public bool SaveLogExcel(Log logData)
        {
            try
            {
                var wb = new XLWorkbook();

                if (logData.HasSummary)
                    InsertSummarySheet(logData, wb);

                if (logData.HasDetails)
                    InsertDetailsSheet(logData, wb);

                if (logData.HasEvents)
                    InsertEventsSheet(logData, wb);

                logData.FilePath = Path.GetFullPath(logData.FilePath.Substring(0, logData.FilePath.Length - 4) + AppConfig.OutputFile.ExcelFileExtension);

                wb.SaveAs(logData.FilePath);

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
        }

        #region Excel Build Help Methods

        private static void InsertSummarySheet(Log logData, XLWorkbook wb)
        {
            try
            {
                var sum_ws = wb.Worksheets.Add(AppConfig.Labels.SummaryWorksheetName);

                sum_ws.Columns("A-D").Width = 20;
                sum_ws.Columns("A-D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                sum_ws.Cell(1, 1).Value = AppConfig.Labels.Date;
                sum_ws.Cell(1, 2).Value = logData.Summary.Date;

                sum_ws.Cell(2, 1).Value = AppConfig.Labels.Summary;
                sum_ws.Cell(2, 2).Value = logData.Summary.Description;

                if (logData.Summary.Studies.Count > 0)
                {
                    sum_ws.Cell(4, 1).Value = AppConfig.Labels.Studies;

                    sum_ws.Rows(5, 1).Style.Font.Bold = true;
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

                    var summaryDateset = logData.Summary.Studies.Where(x => x.Id != 0)
                        .Select(x => new { x.Id, x.Name, x.DataModelName, x.Events.Count })
                        .ToList();

                    sum_ws.Cell(6, 1).InsertData(summaryDateset);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw exception;
            }
        }

        private static void InsertEventsSheet(Log logData, XLWorkbook wb)
        {
            if (logData is null)
            {
                throw new ArgumentNullException(nameof(logData));
            }

            try
            {
                var log_ws = wb.Worksheets.Add(AppConfig.Labels.LogEventsWorksheetName);

                log_ws.Columns("A-D").Width = 20;
                log_ws.Columns("A-D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                log_ws.Cell(1, 1).Value = AppConfig.Labels.LogEventsWorksheetName;

                log_ws.Rows(3, 1).Style.Font.Bold = true;
                log_ws.SheetView.Freeze(3, 1);

                log_ws.Columns().Width = 20;
                log_ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                int i = 1;
                foreach (var prop in new EventVm().GetType().GetProperties())
                {
                    var att = prop.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();

                    if (att.Count > 0)
                        log_ws.Cell(3, i).Value = ((DescriptionAttribute)att[0]).Description;
                    else
                        log_ws.Cell(3, i).Value = prop.Name;

                    i++;
                }

                List<EventVm> eventsData = new List<EventVm>();
                foreach (var study in logData.Summary.Studies.ToList())
                {
                    var studyEvents = study.Events.Select(x => new { x.Level, x.Message });
                    foreach (var element in studyEvents)
                    {
                        eventsData.Add(new EventVm
                        {
                            StudyName = study.Name,
                            EventLevel = element.Level,
                            EventMessage = element.Message
                        });
                    }
                }

                log_ws.Cell(4, 1).InsertData(eventsData);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw exception;
            }
        }

        private static void InsertDetailsSheet(Log logData, XLWorkbook wb)
        {
            try
            {
                var dat_ws = wb.Worksheets.Add(AppConfig.Labels.LogDetailsWorksheetName);

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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw exception;
            }
        }

        #endregion

        public Log ReadLogData(string filePath)
        {
            try
            {
                Log log = new Log(filePath);
                int asInt = 0;

                var hasSummary = File.ReadLines(filePath).Any(x => x.Contains(AppConfig.Parsing.SummarySheetEndLine));
                if (hasSummary)
                {
                    log.HasSummary = true;

                    var summaryEndLine = File.ReadLines(filePath)
                        .Select((text, index) => new { text, lineNumber = index + 1 })
                        .First(x => x.text.Contains(AppConfig.Parsing.SummarySheetEndLine));

                    var summaryData = File.ReadLines(filePath)
                        .Skip(1)
                        .Take(summaryEndLine.lineNumber - 3)
                        .ToList();

                    log.Summary.Date = summaryData[1].ToString().Substring(2);
                    log.Summary.Description = summaryData[2].ToString().Substring(2);

                    var recordsToAdd = new List<Study>();
                    foreach (var record in summaryData.Skip(5).ToList())
                    {
                        var eventHeader = AppConfig.Parsing.EventHeader.Match(record);
                        var eventDetails = AppConfig.Parsing.EventDetails.Match(record);

                        if (record.Contains(AppConfig.Parsing.NewStudyDelimiter))
                        {
                            var recToAdd = new Study
                            {
                                Id = int.TryParse(record.Substring(record.IndexOf(AppConfig.Parsing.NewStudyDelimiter) + 1, record.IndexOf(" S") - 3), out asInt) ? asInt : 0,
                                Name = record.Substring(record.IndexOf("Y:") + 3, record.IndexOf(" D") - 12),
                                DataModelName = record.Substring(record.IndexOf("L:") + 2)
                            };
                            recordsToAdd.Add(recToAdd);
                        }
                        else if (eventHeader.Success)
                        {
                            var lastItem = recordsToAdd.Last();
                            lastItem.Events.Add(new Event
                            {
                                Level = record.Substring(3).Trim()
                            });
                        }
                        else if (eventDetails.Success)
                        {
                            var lastItem = recordsToAdd.Last();
                            var lastEvent = lastItem.Events.Last();

                            lastEvent.Message = record.Substring(4).Trim();
                        }
                    }

                    foreach (var study in recordsToAdd)
                    {
                        log.Summary.Studies.Add(study);

                        if (study.Events.Count > 0)
                            log.HasEvents = true;
                    }
                }

                var hasDetails = File.ReadLines(filePath).Any(x => x.Contains(AppConfig.Parsing.RequiredPartialHeader));
                if (hasDetails)
                {
                    log.HasDetails = true;
                    int skipLines = 1;

                    if (log.HasSummary)
                    {
                        var line = File.ReadLines(filePath)
                            .Select((text, index) => new { text, lineNumber = index + 1 })
                            .First(x => x.text.Contains(AppConfig.Parsing.SummarySheetEndLine));
                        skipLines = line.lineNumber + 1;
                    }

                    var logData = File.ReadAllLines(filePath).Skip(skipLines)
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
                        }).ToList();
                }

                return log;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return new Log();
            }
        }
    }
}