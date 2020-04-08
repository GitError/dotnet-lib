using ClosedXML.Excel;
using ConvertToExcel.Common;
using ConvertToExcel.Models;
using System;
using System.IO;
using System.Linq;

namespace ConvertToExcel.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {

        }

        public bool SaveLogExcel(Log ds)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Log Data");

                ws.Cell(2, 1).InsertData(ds.Records.ToList());

                string[] header = AppSettings.LogFileHeader.Split(',').ToArray();
                ws.Rows(1, 1).Style.Font.Bold = true;

                for (int i = 1; i < 17; i++)
                    ws.Cell(1, i).Value = header[i - 1].ToString();

                ws.Columns().Width = 18;
                ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.SheetView.Freeze(1, 1);

                ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;

                ds.FilePath = Path.GetFullPath(ds.FilePath.Substring(0, ds.FilePath.Length - 4) + ".xlsx");

                wb.SaveAs(ds.FilePath);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Log ReadLog(string filePath)
        {
            try
            {
                int asInt = 0;
                DateTime asDt = new DateTime(1900, 1, 1);

                var fileData = File.ReadAllLines(filePath)
                    .Skip(1)
                    .Where(l => l != "" && !l.Contains(@"PL/SQL procedure successfully completed."));

                return new Log
                {
                    FilePath = filePath,
                    Records = fileData
                    .Select(x => x.Split(','))
                    .Select(x => new LogRecord
                    {
                        JOB_ID = int.TryParse(x[0], out asInt) ? asInt : 0,
                        LOAD_STEP = x[1],
                        STATUS = x[2],
                        START_TS = x[3],
                        END_TS = x[4],
                        RUN_TIME = x[5],
                        LOCK_TIME = x[6],
                        TOTAL_TIME = x[7],
                        REFRESHED = int.TryParse(x[8], out asInt) ? asInt : 0,
                        INSERTED = int.TryParse(x[9], out asInt) ? asInt : 0,
                        UPDATED = int.TryParse(x[10], out asInt) ? asInt : 0,
                        DELETED = int.TryParse(x[11], out asInt) ? asInt : 0,
                        TOTAL_I_U_D = int.TryParse(x[12], out asInt) ? asInt : 0,
                        TOTAL_RECORDS = int.TryParse(x[13], out asInt) ? asInt : 0,
                        I_U_D_SEC = int.TryParse(x[14], out asInt) ? asInt : 0,
                        TOTAL_SEC = int.TryParse(x[15], out asInt) ? asInt : 0,
                        LOADED_SEC = int.TryParse(x[16], out asInt) ? asInt : 0
                    })
                    .ToList()
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                return new Log();
            }
        }
    }
}