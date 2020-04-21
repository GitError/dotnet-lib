using ClosedXML.Excel;
using LogConverterFramework.Common;
using LogConverterFramework.Models;
using LogConverterFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogConverterFramework.Services
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

                dat_ws.Rows(1, 1).Style.Font.Bold = true;
                dat_ws.SheetView.Freeze(1, 1);

                dat_ws.Columns().Width = 20;
                dat_ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                var vm = logData.Records.ToList().Select(x => new LogRecordVm
                {
                    INDX = x.Index,
                    STUDY = x.Study,
                    DATA_MODEL = x.DataModel,
                    JOB_ID = x.JobId,
                    LOAD_STEP = x.LoadStep,
                    STATUS = x.Status,
                    START_TS = x.StartTime,
                    END_TS = x.EndTime,
                    RUN_TIME = x.RunTime,
                    LOCK_TIME = x.LockTime,
                    TOTAL_TIME = x.TotalTime,
                    REFRESHED = x.Refreshed,
                    INSERTED = x.Inserted,
                    UPDATED = x.Updated,
                    DELETED = x.Deleted,
                    TOTAL_I_U_D = x.TotalInsertsUpdatedDeletes,
                    TOTAL_RECORDS = x.TotalRecords,
                    I_U_D_SEC = x.I_U_D_SEC,
                    TOTAL_SEC = x.TotalSeconds,
                    LOADED_SEC = x.LoadedSeconds,
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
                    dat_ws.Rows(group.ParentRow + 1, group.ChildRow).Group();
                    dat_ws.Rows(group.ParentRow + 1, group.ChildRow).Collapse();
                }

                dat_ws.Cell(1, 1).AsRange().AddToNamed("Titles");
                dat_ws.Cell(1, 1).InsertTable(vm.ToList()).Theme = XLTableTheme.TableStyleLight1;


                dat_ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;
                dat_ws.Columns("I:K").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                dat_ws.Columns("L:T").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                dat_ws.Columns("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                dat_ws.Columns().AdjustToContents();

                dat_ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
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
                            var data = record.Split(':');
                            var recToAdd = new Study
                            {
                                Id = int.TryParse(data[0].Substring(3, data[0].Length - 9), out asInt) ? asInt : 0,                                
                                Name = data[1].ToString().Substring(1, data[1].Substring(1).IndexOf(" ") - 1),
                                DataModelName = data[2].ToString().Substring(1).ToString()
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