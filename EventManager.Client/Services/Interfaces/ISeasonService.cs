using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeasonService : IHttpCall<SeasonListDto, SeasonDto, SeasonModel>
    {
        Task<bool> UpdateSeenStatus(List<SeasonSeenStatusModel> models);
        Task<bool> AddIncremented(int seriesId);
        Task<bool> DeleteDecremented(int seasonId);
    }
}