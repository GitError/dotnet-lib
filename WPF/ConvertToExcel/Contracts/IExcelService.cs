using ConvertToExcel.Models;

namespace ConvertToExcel.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logData);

        Log ReadLogData(string filePath);
    }
}