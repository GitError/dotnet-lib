using ConvertToExcelFramework.Models;

namespace ConvertToExcelFramework.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logDataSet);

        Log ReadLog(string filePath);
    }
}