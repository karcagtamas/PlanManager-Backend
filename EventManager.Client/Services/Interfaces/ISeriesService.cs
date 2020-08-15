using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeriesService : IHttpCall<SeriesListDto, SeriesDto, SeriesModel>
    {
        Task<List<MySeriesListDto>> GetMyList();
        Task<MySeriesDto> GetMy(int id);
        Task<bool> UpdateMySeries(MySeriesModel model);
        Task<bool> UpdateSeenStatus(SeriesSeenStatusModel model);
        Task<bool> AddSeriesToMySeries(int id);
        Task<bool> RemoveSeriesFromMySeries(int id);
        Task<List<MySeriesSelectorListDto>> GetMySelectorList(bool onlyMine);
    }
}