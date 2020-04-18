using ConvertToExcelFramework.Models;

namespace ConvertToExcelFramework.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logData);

        Log ReadLogData(string filePath);
    }
}