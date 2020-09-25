using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common.CSV
{
    public interface ICsvService
    {
        FileStreamResult GenerateTableExport<T>(IEnumerable<T> objectList, IEnumerable<Header> columnList,
            string fileName, bool appendCurrentDate);
    }
}