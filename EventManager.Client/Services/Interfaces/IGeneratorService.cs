using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IGeneratorService
    {
        Task<GeneratorSettings> GenerateSimple(GeneratorSettings settings);
        Task<int> Create(GeneratorSettingsModel model);
        Task<bool> Update(int id, GeneratorSettingsModel model);
        Task<bool> Delete(int id);
        Task<GeneratorSettings> Get(int id);
        Task<List<CsomorListDTO>> GetPublicList();
        Task<List<CsomorListDTO>> GetOwnedList();
        Task<List<CsomorListDTO>> GetSharedList();
        Task<bool> Share(int id, List<CsomorAccessModel> models);
        Task<bool> ChangePublicStatus(int id, bool status);
    }
}
