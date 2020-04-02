using ConvertToExcel.Models;

namespace ConvertToExcel.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logDataSet);

        Log ReadLog(string filePath);
    }
}