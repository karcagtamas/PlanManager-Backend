using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.CSM;

namespace CsomorGenerator.Services.Interfaces
{
    public interface IGeneratorService
    {
        GeneratorSettings GenerateSimple(GeneratorSettings settings);
    }
}