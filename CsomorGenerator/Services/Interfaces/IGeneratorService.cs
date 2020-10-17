using System.Collections.Generic;
using System.IO;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Enums;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Mvc;

namespace CsomorGenerator.Services.Interfaces
{
    public interface IGeneratorService
    {
        GeneratorSettings Generate(GeneratorSettings settings);
        GeneratorSettings GenerateSimple(GeneratorSettings settings);
        int Create(GeneratorSettingsModel model);
        void Update(int id, GeneratorSettingsModel model);
        void Delete(int id);
        GeneratorSettings Get(int id);
        List<CsomorListDTO> GetPublicList();
        List<CsomorListDTO> GetOwnedList();
        List<CsomorListDTO> GetSharedList();
        void Share(int id, List<CsomorAccessModel> models);
        void ChangePublicStatus(int id, GeneratorPublishModel model);
        CsomorRole GetRoleForCsomor(int id);
        ExportResult ExportPdf(int id, CsomorType type, List<string> filterList);
        ExportResult ExportXls(int id, CsomorType type, List<string> filterList);
    }
}