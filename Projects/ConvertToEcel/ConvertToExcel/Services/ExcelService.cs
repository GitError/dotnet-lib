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

        public bool SaveLogDataToExcel(LogDataSet ds)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Log Data");

                ws.Cell(2, 1).InsertData(ds.Rows.ToList());

                var header = AppSettings.LogFileHeader.Split(",").ToArray();
                ws.Rows(1, 1).Style.Font.Bold = true;

                for (int i = 1; i < 17; i++)
                    ws.Cell(1, i).Value = header[i - 1].ToString();

                ws.Columns().Width = 18;
                ws.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.SheetView.Freeze(1, 17);

                ws.Columns("I:Q").Style.NumberFormat.NumberFormatId = 3;

                wb.SaveAs(Path.GetFullPath(ds.FilePath[0..^4] + ".xlsx"));

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public LogDataSet ReadLogData(string filePath)
        {
            try
            {
                int asInt = 0;
                DateTime asDt = DateTime.MinValue;

                return new LogDataSet
                {
                    FilePath = filePath,
                    Rows = File.ReadAllLines(filePath)
                         .Skip(1)
                        .Select(x => x.Split(','))
                        .Select(x => new LogDataRow
                        {
                            JOB_ID = int.TryParse(x[0], out asInt) ? asInt : 0,
                            LOAD_STEP = x[1],
                            STATUS = x[2],
                            START_TS = DateTime.TryParse(x[3], out asDt) ? asDt : DateTime.MinValue,
                            END_TS = DateTime.TryParse(x[4], out asDt) ? asDt : DateTime.MinValue,
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
                        .Where(x => x.JOB_ID != 0)
                        .ToList()
                };
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}