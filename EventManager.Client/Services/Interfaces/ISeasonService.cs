using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeasonService : IHttpCall<SeasonListDto, SeasonDto, SeasonModel>
    {
        Task<bool> UpdateSeenStatus(List<SeasonSeenStatusModel> models);
        Task<bool> AddIncremented(int seriesId);
        Task<bool> DeleteDecremented(int seasonId);
    }
}