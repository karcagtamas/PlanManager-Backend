﻿using ManagerAPI.Domain.Entities.CSM;
using ManagerAPI.Shared.DTOs.CSM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ManagerAPI.Services.Common.Excel
{
    public interface IExcelService
    {
        FileStreamResult GenerateTableExport<T>(IEnumerable<T> objectList, IEnumerable<Header> columnList,
            string fileName, bool appendCurrentDate);

        ExportResult GeneratePersonCsomor(List<CsomorPerson> persons);
        ExportResult GenerateWorkCsomor(List<CsomorWork> works);
    }
}