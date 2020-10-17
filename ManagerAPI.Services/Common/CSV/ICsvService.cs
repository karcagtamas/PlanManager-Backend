using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ManagerAPI.Services.Common.CSV
{
    public interface ICsvService
    {
        FileStreamResult GenerateTableExport<T>(IEnumerable<T> objectList, IEnumerable<Header> columnList,
            string fileName, bool appendCurrentDate);
    }
}