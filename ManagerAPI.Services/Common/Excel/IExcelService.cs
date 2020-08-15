using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common.Excel
{
    public interface IExcelService
    {
        FileStreamResult GenerateTableExport<T>(IEnumerable<T> objectList, IEnumerable<Header> columnList,
            string fileName, bool appendCurrentDate);
    }
}