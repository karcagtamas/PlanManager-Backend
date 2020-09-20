using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class GeneratorService : IGeneratorService
    {
        public Task<bool> ChangePublicStatus(int id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(GeneratorSettingsModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneratorSettings> GenerateSimple(GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }

        public GeneratorSettings Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CsomorListDTO>> GetOwnedList()
        {
            throw new NotImplementedException();
        }

        public Task<List<CsomorListDTO>> GetPublicList()
        {
            throw new NotImplementedException();
        }

        public Task<List<CsomorListDTO>> GetSharedList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Share(int id, List<CsomorAccessModel> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, GeneratorSettingsModel model)
        {
            throw new NotImplementedException();
        }
    }
}
