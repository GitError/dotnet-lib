using LogConverterFramework.Models;

namespace LogConverterFramework.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logData);

        Log ReadLogData(string filePath);
    }
}