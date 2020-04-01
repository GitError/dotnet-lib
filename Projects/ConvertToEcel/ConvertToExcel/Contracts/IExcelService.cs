using ConvertToExcel.Models;

namespace ConvertToExcel.Services
{
    public interface IExcelService
    {
        bool SaveLogDataToExcel(LogDataSet logDataSet);

        LogDataSet ReadLogData(string filePath);
    }
}