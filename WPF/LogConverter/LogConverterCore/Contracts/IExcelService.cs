using LogConverterCore.Models;

namespace LogConverterCore.Services
{
    public interface IExcelService
    {
        bool SaveLogExcel(Log logData);

        Log ReadLogData(string filePath);
    }
}