using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;

namespace CsomorGenerator.Services.Interfaces
{
    public interface IGeneratorService
    {
        GeneratorSettings GenerateSimple(GeneratorSettings settings);
        void Create(GeneratorSettingsModel model);
        void Update(int id, GeneratorSettingsModel model);
        void Delete(int id);
        void Get(int id);
        List<CsomorListDTO> GetPublicList();
        List<CsomorListDTO> GetOwnedList();
        List<CsomorListDTO> GetSharedList();
        List<CsomorListDTO> GetList();
    }
}