using ManagerAPI.Domain.Entities.CSM;
using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Common.PDF
{
    public interface IPDFService
    {
        ExportResult GeneratePersonCsomor(List<CsomorPerson> persons);
        ExportResult GenerateWorkCsomor(List<CsomorWork> works);
    }
}
