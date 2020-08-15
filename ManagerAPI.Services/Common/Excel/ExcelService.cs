using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common.Excel
{
    public class ExcelService : IExcelService
    {
        public FileStreamResult GenerateTableExport<T>(IEnumerable<T> objectList, IEnumerable<Header> columnList,
            string fileName,
            bool appendCurrentDate)
        {
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var name = appendCurrentDate
                ? $"{DateHelper.DateToString(DateTime.Now)}{fileName}.xlsx"
                : $"{fileName}.xlsx";

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(fileName);

                // Header
                var columns = columnList.ToList();
                for (var i = 0; i < columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = columns[i].DisplayName;
                }

                var objects = objectList.ToList();
                for (var i = 0; i < objects.Count; i++)
                {
                    for (var j = 0; j < columns.Count; j++)
                    {
                        worksheet.Cell(2 + i, j + 1).Value = columns[j].GetValue(objects[i]);
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var result = new FileStreamResult(stream, contentType) {FileDownloadName = name};
                    
                    return result;
                }
            }
        }
    }
}