using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;

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
    }
}