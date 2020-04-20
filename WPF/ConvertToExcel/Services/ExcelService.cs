using ClosedXML.Excel;
using ConvertToExcel.Common;
using ConvertToExcel.Models;
using ConvertToExcelCore.Models;
using ConvertToExcelCore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace ConvertToExcel.Services
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

                sum_ws.Columns("A-D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                sum_ws.Cell(1, 1).Value = AppConfig.Labels.Date;
                sum_ws.Cell(1, 1).Style.Font.Bold = true;
                sum_ws.Cell(1, 2).Style.Font.Bold = true;

                sum_ws.Cell(1, 2).Value = logData.Summary.Date;
                sum_ws.Cell(2, 1).Style.Font.Bold = true;
                sum_ws.Cell(2, 2).Style.Font.Bold = true;

                sum_ws.Cell(2, 1).Value = AppConfig.Labels.Summary;
                sum_ws.Cell(2, 2).Value = logData.Summary.Description;

                if (logData.Summary.Studies.Count > 0)
                {
                    sum_ws.Cell(4, 1).Value = AppConfig.Labels.Runs;
                    sum_ws.Cell(4, 1).Style.Font.Bold = true;

                    sum_ws.Rows(5, 1).Style.Font.Bold = true;
                    sum_ws.SheetView.Freeze(5, 1);

                    var events = new List<EventVm>();

                    var studies = logData.Summary.Studies.ToList();
                    foreach (var study in studies)
                    {
                        foreach (var ev in study.Events)
                        {
                            events.Add(new EventVm
                            {
                                Id = study.Id,
                                Study = study.Name,
                                DataModel = study.DataModelName,
                                Level = ev.Level,
                                Message = ev.Message
                            });
                        }
                    }

                    sum_ws.Cell(5, 1).AsRange().AddToNamed("Titles");
                    sum_ws.Cell(5, 1).InsertTable(events.ToList());
                }

                sum_ws.Columns().AdjustToContents();
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

                InsertDetailsHeader(dat_ws);

                dat_ws.Rows(1, 1).Style.Font.Bold = true;
                dat_ws.SheetView.Freeze(1, 1);

                dat_ws.Columns().Width = 20;
                dat_ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                var vm = logData.Records.ToList().Select(x => new LogRecordVm
                {
                    Index = x.Index,
                    Study = x.Study,
                    DataModel = x.DataModel,
                    JobId = x.JobId,
                    LoadStep = x.LoadStep,
                    Status = x.Status,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    RunTime = x.RunTime,
                    LockTime = x.LockTime,
                    TotalTime = x.TotalTime,
                    Refreshed = x.Index,
                    Inserted = x.Inserted,
                    Updated = x.Updated,
                    Deleted = x.Deleted,
                    TotalInsertsUpdatedDeletes = x.TotalInsertsUpdatedDeletes,
                    TotalRecords = x.TotalRecords,
                    I_U_D_SEC = x.I_U_D_SEC,
                    TotalSeconds = x.TotalSeconds,
                    LoadedSeconds = x.LoadedSeconds,
                });

                var dataGroups = new List<DataGroupVm>();

                int i = 1;
                foreach (var record in logData.Records.ToList())
                {
                    if (record.LoadStep != string.Empty)
                    {
                        if (!record.LoadStep.Substring(0, 2).Contains(".."))
                        {
                            dataGroups.Add(new DataGroupVm
                            {
                                ParentRow = i + 1,
                                Orphen = true
                            });
                        }
                        else
                        {
                            var last = dataGroups.Last();
                            last.ChildRow = i + 1;
                            last.Orphen = false;
                        }
                    }
                    i++;
                }

                foreach (var group in dataGroups.Where(x => !x.Orphen).ToList())
                {
                    dat_ws.Rows(group.ParentRow, group.ChildRow - 1).Group();
                }

                dat_ws.Cell(2, 1).InsertData(vm.ToList());

                dat_ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;
                dat_ws.Columns("I:K").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                dat_ws.Columns("L:T").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                dat_ws.Columns("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                dat_ws.Columns().AdjustToContents();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw exception;
            }
        }

        private static void InsertDetailsHeader(IXLWorksheet dat_ws)
        {
            int i = 1;
            foreach (var prop in new LogRecordVm().GetType().GetProperties())
            {
                var att = prop.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();

                if (att.Count > 0)
                    dat_ws.Cell(1, i).Value = ((DescriptionAttribute)att[0]).Description;
                else
                    dat_ws.Cell(1, i).Value = prop.Name;

                i++;
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
                            Index = TryParseNullable(x[0]),
                            Study = x[1],
                            DataModel = x[2],
                            JobId = TryParseNullable(x[3]),
                            LoadStep = x[4],
                            Status = x[5],
                            StartTime = x[6],
                            EndTime = x[7],
                            RunTime = x[8],
                            LockTime = x[9],
                            TotalTime = x[10],
                            Refreshed = TryParseNullable(x[11]),
                            Inserted = TryParseNullable(x[12]),
                            Updated = TryParseNullable(x[13]),
                            Deleted = TryParseNullable(x[14]),
                            TotalInsertsUpdatedDeletes = TryParseNullable(x[15]),
                            TotalRecords = TryParseNullable(x[16]),
                            I_U_D_SEC = TryParseNullable(x[17]),
                            TotalSeconds = TryParseNullable(x[18]),
                            LoadedSeconds = TryParseNullable(x[19])
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

        public int? TryParseNullable(string val)
        {
            return int.TryParse(val, out int outValue) ? (int?)outValue : null;
        }
    }
}